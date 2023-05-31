using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using Antlr4.Runtime;
using Newtonsoft.Json.Linq;


using ConfigNS;
using CsharpDotNet1.antlr.compiler;

namespace ClassGeneratorNS
{
    public class ActionGenerator
    {
        private List<string> _sourceCodeLines;

        public ActionGenerator()
        {
            _sourceCodeLines = new List<string>();
        }

        // public void generate( List classes, 
        // string modelTargetPackage,  string executionTargetPackage) {
        // }

        public void AddUsingDirective(string namespaceName)
        {
            _sourceCodeLines.Add($"using {namespaceName};");
        }

        public void AddClass(string className, string baseClassName = null)
        {
            var classDeclaration = new StringBuilder();
            classDeclaration.Append($"public class {className}");

            if (!string.IsNullOrEmpty(baseClassName))
            {
                classDeclaration.Append($" : {baseClassName}");
            }
            classDeclaration.Append(" {");

            _sourceCodeLines.Add(classDeclaration.ToString());
        }

        public void AddProperty(string propertyName, string propertyType)
        {
            _sourceCodeLines.Add($"public {propertyType} {propertyName} {{ get; set; }}");
        }

        public void AddFields(string[] parameterNames, string[] parameterTypes)
        {
            var methodDeclaration = new StringBuilder();
            for (int i = 0; i < parameterNames.Length; i++)
            {
                methodDeclaration.Append($" private {parameterTypes[i]} {parameterNames[i]}; \n");

            }
            _sourceCodeLines.Add(methodDeclaration.ToString());
        }

        public void AddConstructor(string className, string modelName, string[] parameterNames, string[] parameterTypes)
        {
            var methodDeclaration = new StringBuilder();
            methodDeclaration.Append($"public {className} (");
            for (int i = 0; i < parameterNames.Length; i++)
            {
                methodDeclaration.Append($"{parameterTypes[i]} {parameterNames[i]}");

                if (i != parameterNames.Length - 1)
                {
                    methodDeclaration.Append(", ");
                }
            }

            methodDeclaration.Append("){");

            int paramlen = parameterNames.Length;
            methodDeclaration.Append($"\n\tthis.{parameterNames[paramlen - 1]} = ({modelName}) {parameterNames[paramlen - 1]} ;");
            for (int i = 0; i < paramlen - 1; i++)
            {
                methodDeclaration.Append($"\n\tthis.{parameterNames[i]} = {parameterNames[i]}; ");
            }

            _sourceCodeLines.Add(methodDeclaration.ToString());
            _sourceCodeLines.Add("}");


        }

        public void AddMethod(string methodName, string returnType, string[] parameterNames, string[] parameterTypes, string methodBody = null)
        {
            var methodDeclaration = new StringBuilder();
            methodDeclaration.Append($"public {returnType} {methodName}(");

            for (int i = 0; i < parameterNames.Length; i++)
            {
                methodDeclaration.Append($"{parameterTypes[i]} {parameterNames[i]}");

                if (i != parameterNames.Length - 1)
                {
                    methodDeclaration.Append(", ");
                }
            }

            methodDeclaration.Append(")");

            if (!string.IsNullOrEmpty(methodBody))
            {
                methodDeclaration.Append(" {");
                methodDeclaration.Append(methodBody);
                methodDeclaration.Append("}");
            }
            else
            {
                methodDeclaration.Append(" { }");
            }

            _sourceCodeLines.Add(methodDeclaration.ToString());
        }

        public string GetSourceCode()
        {
            var sourceCodeBuilder = new StringBuilder();

            foreach (var sourceCodeLine in _sourceCodeLines)
            {
                sourceCodeBuilder.AppendLine(sourceCodeLine);
            }

            return sourceCodeBuilder.ToString();
        }

        public ProtoClass modelGenerate(Type context, string classname, string modelTargetPackage)
        {
            var sourceCodeGenerator = new ActionGenerator();
            sourceCodeGenerator.AddUsingDirective("System");
            sourceCodeGenerator.AddUsingDirective("ActionInterfaceNS");
            sourceCodeGenerator._sourceCodeLines.Add($"[ActionContext(ActionName = \"{classname}\")] ");

            sourceCodeGenerator.AddClass(classname, "IActionContext");

            FieldInfo[] fields = context.GetFields();

            foreach (FieldInfo field in fields)
            {

                string datatype = AddFieldMember(field.FieldType);
                sourceCodeGenerator.AddProperty(field.Name, datatype);

            }
            sourceCodeGenerator._sourceCodeLines.Add("}");
            var sourceCode = sourceCodeGenerator.GetSourceCode();

            return new ProtoClass(classname, modelTargetPackage, sourceCode);
        }

        public ProtoClass actionclassGenerate(Type context, string inClassname, string executionTargetPackage)
        {
            string classname = inClassname + "Action";
            var sourceCodeGenerator = new ActionGenerator();
            sourceCodeGenerator.AddUsingDirective("System");
            sourceCodeGenerator.AddUsingDirective("Serilog");
            sourceCodeGenerator.AddUsingDirective("ActionInterfaceNS");
            sourceCodeGenerator.AddUsingDirective("AuditDAONS");

            sourceCodeGenerator._sourceCodeLines.Add($"\n[ActionExecution(ActionName = {classname}.{inClassname.ToUpper()})]");
            sourceCodeGenerator.AddClass(classname, "IActionExecution");

            string[] propertyTypes = new string[] { "ActionExecutionAudit", "ILogger", inClassname };
            string[] propertyNames = new string[] { "actionExecutionAudit", "log", inClassname.ToLower() };

            sourceCodeGenerator._sourceCodeLines.Add($"public const string {inClassname.ToUpper()}= \"{inClassname}\";");
            sourceCodeGenerator.AddFields(propertyNames, propertyTypes);

            string[] constructorPropertyTypes = new string[] { "ActionExecutionAudit", "ILogger", "object" };
            sourceCodeGenerator.AddConstructor(classname, inClassname, propertyNames, constructorPropertyTypes);

            sourceCodeGenerator.AddMethod("execute", "void", new string[] { }, new string[] { }, "");
            sourceCodeGenerator.AddMethod("executeIf", "bool", new string[] { }, new string[] { }, "return true;");

            sourceCodeGenerator._sourceCodeLines.Add("}");

            var sourceCode = sourceCodeGenerator.GetSourceCode();
            return new ProtoClass(classname, executionTargetPackage, sourceCode);
        }

        public void generate(List<Type> classes, string modelTargetPackage, string executionTargetPackage)
        {
            HashSet<string> actions = typeof(RavenParser.ActionContext).GetMethods()
                .Select(method => method.ReturnType.Name)
                .Where(name => name.Contains("Context"))
                .ToHashSet();

            List<ProtoClass> csFiles = new List<ProtoClass>();

            foreach (Type context in classes)
            {
                string contextFullName = context.Name;
                string name = contextFullName.Replace("Context", "");

                if (actions.Contains(contextFullName))
                {
                    csFiles.Add(modelGenerate(context, name, modelTargetPackage));
                    csFiles.Add(actionclassGenerate(context, name, executionTargetPackage));
                }
            }

            string projectPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            var basePath = projectPath + "/Lambda/";
            foreach (ProtoClass csFile in csFiles)
            {
                string directoryPath = basePath + csFile.filepath + "/";
                string filePath = Path.Combine(directoryPath, csFile.name + ".cs");
                //FileInfo fileInfo = new FileInfo("./" + path + "/" + csFile.name + ".cs");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                if (!File.Exists(filePath))
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.Write(csFile.sourceCode);
                    }
                }

            }
        }

        public class ProtoClass
        {

            public ProtoClass(string classname, string modelTargetPackage, string sourceCode)
            {
                this.name = classname;
                this.filepath = modelTargetPackage;
                this.sourceCode = sourceCode;
            }

            public string name { get; set; }
            public string filepath { get; set; }
            public string sourceCode { get; set; }

        }


        private string AddFieldMember(Type type)
        {
            if (type == typeof(RavenParser.ExpressionContext))
            {
                return "bool";
            }
            else if (type == typeof(IToken))
            {
                return "string";
            }
            else if (type == typeof(RavenParser.ResourceContext))
            {
                return "ConfigNS.SpwResourceConfig";
            }
            else if (type == typeof(IList<IToken>))
            {
                Type actualTypeArgument = type.GenericTypeArguments[0];

                if (actualTypeArgument == typeof(IToken))
                {
                    return "IList<string>";

                }
                else if (actualTypeArgument == typeof(RavenParser.ActionContext))
                {
                    return "IList<RavenParser.ActionContext>";
                }
                else if (actualTypeArgument == typeof(RavenParser.RestPartContext))
                {
                    return "IList<T>";
                    //TODO   typeof(List<>).MakeGenericType(typeof(RestPart));
                }
                else if (actualTypeArgument == typeof(RavenParser.StartProcessContext))
                {
                    return "IList<T>";
                    //TODO  //.MakeGenericType(typeof(StartProcess));
                }
                else
                {
                    return "string";
                }
            }
            else if (type == typeof(RavenParser.JsonContext))
            {
                return "Newtonsoft.Json.Linq.JToken";
            }
            else
            {
                return "string";
            }

        }
    }

}
