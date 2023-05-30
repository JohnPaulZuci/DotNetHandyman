using ClassGeneratorNS;
using System.IO;
using Serilog;
using PipelineExecutorNS;
using UtilityMethodsNS;

namespace RavenExample
{
    class Program
    {
        static void Main(string[] args)
        {
            SerilogConfig.Configure();
            Log.Information("TEst..");

            // List<Type> classes = typeof(RavenParser).GetNestedTypes().ToList();
            // var sourceCodeGenerator = new ActionGenerator();
            // sourceCodeGenerator.generate(classes, "DataModelNS", "ActionClassesNS");

            LContext request = new LContext();
            request.PipelineName = "root.test_pipeline";
            request.ProcessLoadType = HRequestResolver.LoadType.FILE.ToString();
            LambdaEngine.Start(request);


            // var sourceCodeGenerator = new ActionGenerator();
            // sourceCodeGenerator.AddUsingDirective("System");

            // sourceCodeGenerator.AddClass("MyClass");
            // sourceCodeGenerator.AddProperty("MyProperty", "string");

            // var parameterNames = new[] { "arg1", "arg2" };
            // var parameterTypes = new[] { "int", "string" };
            // var methodBody = "Console.WriteLine(arg2);";
            // sourceCodeGenerator.AddMethod("execute", "void", parameterNames, parameterTypes, methodBody);

            // sourceCodeGenerator.AddMethod("executeIf", "void", parameterNames, parameterTypes, methodBody);

            // var sourceCode = sourceCodeGenerator.GetSourceCode();
            // Console.WriteLine(sourceCode);

            ///Console.ReadLine();

            // Model
            // ModelClassGenerator classGenerator = new ModelClassGenerator("Person");
            // classGenerator.AddProperty("Name", "string");
            // classGenerator.AddProperty("Age", "int");

            // sourceCode = classGenerator.GetSourceCode();
            //  Console.WriteLine(sourceCode);
            //  string filePath ="Myjtestclass.cs";
            //   c.WriteAllText(filePath, sourceCode);

            // Console.ReadLine();

            // // //Action

            // var classGenerator1 = new ActionClassGenerator("Calculator");
            // classGenerator1.AddMethod("Add", "int", new[] { "a", "b" }, new[] { "int", "int" }, "return a + b;");
            // classGenerator1.AddMethod("Subtract", "int", new[] { "a", "b" }, new[] { "int", "int" }, "return a - b;");

            // var sourceCode1 = classGenerator1.GetSourceCode();
            // Console.WriteLine(sourceCode1);

            // Console.ReadLine();

        }
    }

}
