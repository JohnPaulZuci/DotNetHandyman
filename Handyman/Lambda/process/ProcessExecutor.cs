
using Serilog;
using System.Reflection;


using AccessConfigNS;
using ActionInterfaceNS;
using ExceptionNS;


public class ProcessExecutor
{
    public static readonly Dictionary<string, Type> ACTION_CONTEXT_MAP = new Dictionary<string, Type>();
    public static readonly Dictionary<string, Type> ACTION_EXECUTION_MAP = new Dictionary<string, Type>();
    public static string CONTEXT = "Context";

    static ProcessExecutor()
    {
        try
        {
            ////Log.Info("IActionContext Execution classes loader initialized");
            HashSet<string> actionPackageNames = GetPackageAction();
            HashSet<Type> actionContexts = GetActionContextClass(actionPackageNames);
            HashSet<Type> actionExecutions = GetActionExecutionClass(actionPackageNames);

            if (actionExecutions.Count == 0 || actionContexts.Count == 0 ||
                actionExecutions.Count != actionContexts.Count)
            {
                throw new HandymanException("Size mismatched or Empty IActionContext initialization");
            }

            ACTION_CONTEXT_MAP = GetActionContext(actionContexts);
            ACTION_EXECUTION_MAP = GetActionExecution(actionExecutions);
            if (!ACTION_EXECUTION_MAP.Keys.All(key => ACTION_CONTEXT_MAP.ContainsKey(key)))
            {
                throw new HandymanException("Mismatched lambdaName");
            }
            Log.Information("IActionContext Execution classes successfully loaded");
        }
        catch (Exception e)
        {
            throw new HandymanException(e.Message, e);
        }
    }

    public static HashSet<Type> GetActionExecutionClass(HashSet<string> packageNames)
    {
        var actionExecutionClasses = new HashSet<Type>();
        try
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes()
                 .Where(t => t.GetCustomAttributes(typeof(ActionExecution), true).Length > 0);
            actionExecutionClasses.UnionWith(types);
        }
        catch (Exception e)
        {
            Log.Error($"Failed to load action Execution classes : {e.Message}");
        }

        return actionExecutionClasses;
    }

    private static Dictionary<string, Type> GetActionExecution(HashSet<Type> classes)
    {
        var actionExecutionMap = new Dictionary<string, Type>();
        foreach (var aClass in classes)
        {
            var annotation = aClass.GetCustomAttribute<ActionExecution>();
            if (typeof(IActionExecution).IsAssignableFrom(aClass) &&
                annotation != null && !string.IsNullOrEmpty(annotation.ActionName) &&
                 !string.IsNullOrWhiteSpace(annotation.ActionName))
            {
                actionExecutionMap[annotation.ActionName] = aClass;
            }
        }

        return actionExecutionMap;
    }

    private static Dictionary<string, Type> GetActionContext(IEnumerable<Type> classes)
    {
        Dictionary<string, Type> actionContextMap = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        foreach (var aClass in classes)
        {
            var annotation = aClass.GetCustomAttribute<ActionContext>();
            if (typeof(IActionContext).IsAssignableFrom(aClass) &&
                annotation != null &&
                !string.IsNullOrEmpty(annotation.ActionName) &&
                !string.IsNullOrWhiteSpace(annotation.ActionName))
            {
                actionContextMap[annotation.ActionName] = aClass;
            }
        }

        return actionContextMap;
    }


    public static HashSet<Type> GetActionContextClass(HashSet<string> packageNames)
    {
        //TODO check with particular folder/Namespace to load
        var actionContextClasses = new HashSet<Type>();
        try
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes()
                 .Where(t => t.GetCustomAttributes(typeof(ActionContext), true).Length > 0);
            actionContextClasses.UnionWith(types);
        }
        catch (Exception e)
        {
            Log.Error($"Failed to load action context classes from package : {e.Message}");
        }

        return actionContextClasses;
    }
    private static HashSet<String> GetPackageAction()
    {
        return ConfigAccess.GetPackageAction();
    }

    ///


    // public static HashSet<Type> LoadClassesInFolder(string folderPath)
    // {
    //     var loadedClasses = new HashSet<Type>();
    //     var provider = new CSharpCodeProvider();
    //     var compilerParams = new CompilerParameters();

    //     // Set compiler parameters
    //     compilerParams.GenerateInMemory = true;
    //     compilerParams.GenerateExecutable = false;
    //     compilerParams.IncludeDebugInformation = false;

    //     // Find all .cs files in the folder and its subfolders
    //     var files = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);

    //     foreach (var file in files)
    //     {
    //         try
    //         {
    //             // Read the contents of the .cs file
    //             var sourceCode = File.ReadAllText(file);

    //             // Compile the source code and get the resulting assembly
    //             var compileResult = provider.CompileAssemblyFromSource(compilerParams, sourceCode);
    //             if (compileResult.Errors.HasErrors)
    //             {
    //                 // Handle compilation errors
    //                 foreach (CompilerError error in compileResult.Errors)
    //                 {
    //                     Console.WriteLine($"Error in file: {file} Line: {error.Line} - {error.ErrorText}");
    //                 }
    //                 continue;
    //             }

    //             var assembly = compileResult.CompiledAssembly;
    //             var assemblyTypes = assembly.GetTypes();
    //             loadedClasses.UnionWith(assemblyTypes);
    //         }
    //         catch (Exception ex)
    //         {
    //             Console.WriteLine($"Failed to load types from file: {file}");
    //             Console.WriteLine(ex);
    //         }
    //     }

    //     return loadedClasses;
    // }
}