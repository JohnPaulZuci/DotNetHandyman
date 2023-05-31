using ClassGeneratorNS;
using CsharpDotNet1.antlr.compiler;
using PipelineExecutorNS;
using UtilityMethodsNS;
using Xunit;

namespace TestCasesNS
{

    public class WorkFlowTests
    {

        [Fact]
        public void TestModelAndActionClassGeneration()
        {
            List<Type> classes = typeof(RavenParser).GetNestedTypes().ToList();
            var sourceCodeGenerator = new ActionGenerator();
            sourceCodeGenerator.generate(classes, "DataModelNS", "ActionClassesNS");
        }

        [Fact]
        public void TestModelClassGeneration()
        {
        }

        [Fact]
        public void TestActionClassGeneration()
        {
        }

        [Fact]
        public void TestLambdaFlow()
        {

            LContext request = new LContext();
            request.PipelineName = "root.test_pipeline";
            request.ProcessLoadType = HRequestResolver.LoadType.FILE.ToString();

            AuditDAONS.PipelineExecutionAudit pipelineExecutionAudit = LambdaEngine.Start(request);
            Assert.NotNull(pipelineExecutionAudit);
            Assert.NotNull(pipelineExecutionAudit.PipelineId);
        }
    }

    /* [Fact]
        public void TestActionClassGeneration()
        {
            // Arrange
            var sourceCodeGenerator = new ActionGenerator();
            sourceCodeGenerator.AddUsingDirective("System");

            sourceCodeGenerator.AddClass("MyClass");
            sourceCodeGenerator.AddProperty("MyProperty", "string");

            var parameterNames = new[] { "arg1", "arg2" };
            var parameterTypes = new[] { "int", "string" };
            var methodBody = "Console.WriteLine(arg2);";
            sourceCodeGenerator.AddMethod("MyMethod", "void", parameterNames, parameterTypes, methodBody);

            // Act
            var sourceCode = sourceCodeGenerator.GetSourceCode();
            Console.WriteLine(sourceCode);

            // Assert
            Assert.NotNull(sourceCode);
            // Assert.Equal(inClassname + "Action", result.ClassName);
            // Assert.Equal(executionTargetPackage, result.Filepath);
            // Assert.NotEmpty(result.SourceCode);
        } */

}