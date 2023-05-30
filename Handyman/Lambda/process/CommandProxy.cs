using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Serilog;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

using ActionInterfaceNS;
using ExceptionNS;
using ConfigNS;

public class CommandProxy
{

    private static HandymanRepo HANDYMAN_REPO = new HandymanRepoImpl();
    private CommandProxy()
    {
    }

    private static MethodInfo GetMethod(IActionContext target, string fieldName, Type returnType)
    {
        string methodName = "set_" + fieldName;
        Type targetType = target!.GetType();

        MethodInfo methodInfo = targetType.GetMethod(methodName, new[] { returnType });
        return methodInfo;
    }
    private static void SetValue(IActionContext target, string fieldName, MethodInfo getter, object node)
    {
        MethodInfo method = GetMethod(target, fieldName, getter.ReturnType);
        Type parameterType = method.GetParameters().FirstOrDefault()?.ParameterType;

        if (parameterType != null)
        {
            try
            {
                method.Invoke(target, new[] { Convert.ChangeType(node, parameterType) });
            }
            catch (Exception e)
            {
                throw new HandymanException("RavenParserContext mapping failed for ExpressionContext", e);
            }
        }
    }

    private static bool Condition(RavenParser.ExpressionContext context, Dictionary<string, string> configMap)
    {
        if (context != null && context.@operator != null)
        {
            string @operator = GetString(context.@operator, configMap);
            string lhsText = GetString(context.lhs, configMap);
            string rhsText = GetString(context.rhs, configMap);
            if (lhsText != null && rhsText != null)
            {
                if (@operator == "<")
                {
                    double lhs = double.Parse(lhsText);
                    double rhs = double.Parse(rhsText);
                    return lhs < rhs;
                }
                else if (@operator == ">")
                {
                    double lhs = double.Parse(lhsText);
                    double rhs = double.Parse(rhsText);
                    return lhs > rhs;
                }
                else if (@operator == "==")
                {
                    return lhsText.Equals(rhsText);
                }
                else if (@operator == "contains")
                {
                    return lhsText.Contains(rhsText);
                }
                else
                {
                    throw new HandymanException("Unknown Operator");
                }
            }
        }
        return false;
    }
    public static string GetString(IToken o, Dictionary<string, string> context)
    {
        if (o != null)
        {
            string text;
            if (o.Type == RavenLexer.STRING)
            {
                text = o.Text.Substring(1, o.Text.Length - 2);
            }
            else
            {
                text = o.Text;
            }
            return GetString(context, text);
        }
        else
        {
            return string.Empty;
        }
    }

    // private static string GetString(Dictionary<string, string> context, string text)
    // {
    //     var paramEngine = new StringBuilder(text.Trim());
    //     foreach (var entry in context)
    //     {
    //         paramEngine.Replace(entry.Key, entry.Value);
    //     }
    //     var replace = paramEngine.ToString();
    //     Log.Debug($"content {replace} given context {string.Join(", ", context.Select(kvp => $"{kvp.Key}={kvp.Value}"))}");
    //     return replace ?? string.Empty;
    // }

    private static string GetString(Dictionary<string, string> context, string text)
    {
        var regex = new Regex(@"\${(.*?)}"); // Match ${param-1} pattern
        var replaced = regex.Replace(text.Trim(), match =>
        {
            var key = match.Groups[1].Value; // Extract the parameter name
            if (context.TryGetValue(key, out var value))
            {
                return value; // Replace with dictionary value if found
            }
            else
            {
                return match.Value; // Keep original string if not found
            }
        });
        Log.Debug($"content {replaced} given context {string.Join(", ", context.Select(kvp => $"{kvp.Key}={kvp.Value}"))}");
        return replaced ?? string.Empty;
    }

    public static void SetTarget(IActionContext target, IParseTree source, Dictionary<string, string> context)
    {
        Dictionary<string, MethodInfo> methodNames = target.GetType().GetMethods()
            .Where(method => method.Name.StartsWith("get"))
            .ToDictionary(method => Regex.Replace(method.Name, "^[^_]*_", ""), //method.Name.Substring(4),
                method => method, StringComparer.OrdinalIgnoreCase);

        foreach (FieldInfo field in source.GetType().GetFields())
        {
            string fieldName = field.Name;
            string methodName = fieldName; //char.ToUpper(fieldName[0]) + fieldName.Substring(1);
            try
            {
                if (methodNames.TryGetValue(methodName, out MethodInfo getter))
                {
                    object fieldValue = field.GetValue(source);
                    if (fieldValue != null)
                    {
                        if (field.FieldType == typeof(IToken))
                        {
                            IToken token = (IToken)fieldValue;
                            string text = GetString(token, context);
                            MethodInfo methodinfo = GetMethod(target, methodName, getter.ReturnType);
                            Type paramType = methodinfo.GetParameters().FirstOrDefault()?.ParameterType;
                            if (paramType != null)
                            {
                                object convertedValue = Convert.ChangeType(text, paramType);
                                methodinfo.Invoke(target, new object[] { convertedValue });
                            }
                        }
                        else if (field.FieldType == typeof(RavenParser.ExpressionContext))
                        {
                            RavenParser.ExpressionContext expression = (RavenParser.ExpressionContext)fieldValue;
                            bool condition = Condition(expression, context);
                            SetValue(target, methodName, getter, condition);
                        }
                        else if (field.FieldType == typeof(RavenParser.JsonContext))
                        {
                            RavenParser.JsonContext json = (RavenParser.JsonContext)fieldValue;
                            string text = json.GetText();
                            string jsonString = GetString(context, text);
                            JToken node = JToken.Parse(jsonString);
                            SetValue(target, methodName, getter, node);
                        }
                        else if (field.FieldType == typeof(RavenParser.ResourceContext))
                        {
                            RavenParser.ResourceContext resource = (RavenParser.ResourceContext)fieldValue;
                            string text = resource.GetText().Substring(1, resource.GetText().Length - 1);
                            SpwResourceConfig connection = HANDYMAN_REPO.GetResourceConfig(GetString(context, text));
                            SetValue(target, methodName, getter, connection);
                        }
                        else if (field.FieldType == typeof(IList<IToken>))
                        {
                            IList<IToken> tokens = (IList<IToken>)fieldValue;
                            Type actualTypeArgument = getter.ReturnType.GetGenericArguments().FirstOrDefault();
                            IList<Object> list = tokens.Select(o =>
                            {
                                if (o is IToken token)
                                {
                                    string s = GetString(token, context);
                                    Object convertValue = Convert.ChangeType(s, Type.GetType(actualTypeArgument.FullName));
                                    return convertValue;
                                }
                                else if (o is RavenParser.RestPartContext restPart)
                                {
                                    return new object();
                                    // return new RestPart.
                                    //     PartName = GetString(restPart.partName, context);
                                    //     PartData = GetString(restPart.partData, context);
                                    //     Type = GetString(restPart.type, context);
                                    //     );
                                }
                                else if (o is RavenParser.StartProcessContext)
                                {
                                    return new object();

                                }
                                else if (o is RavenParser.ActionContext || o is RavenParser.ProducerContext || o is RavenParser.ConsumerContext)
                                {
                                    return o;
                                }
                                throw new HandymanException("RavenParserContext mapping failed for List");
                            }).ToList();

                            Type returnType = getter.ReturnType;
                            MethodInfo method = GetMethod(target, fieldName, returnType);
                            Type[] parameterTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();
                            Type aClass = parameterTypes.FirstOrDefault();
                            if (aClass != null)
                            {
                                try
                                {
                                    //TODO list value's aof string, if needed other data type add LOGIC
                                    method.Invoke(target, new Object[] { list
                                        .Select(item => item.ToString())
                                        .ToList() });
                                }
                                catch (Exception e)
                                {
                                    throw new HandymanException("RavenParserContext mapping failed for List", e);
                                }
                            }
                        }
                        else
                        {
                            throw new HandymanException("Unknown RavenParserContext provided");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in settarget field: {fieldName}: ", e);
            }
        }
    }
}
