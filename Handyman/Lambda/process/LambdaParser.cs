using Antlr4.Runtime;

namespace LambdaParserNS
{
    public class LambdaParser
    {
        public static RavenParser.ProcessContext DoParse(string processFile, Dictionary<string, string> context)
        {
            var input = CharStreams.fromstring(processFile);
            // create lexer
            var lexer = new RavenLexer(input);
            // create token stream
            var tokens = new CommonTokenStream(lexer);
            // create parser
            var parser = new RavenParser(tokens);
            parser.BuildParseTree = true;
            parser.ErrorHandler = new BailErrorStrategy();
            // build parse tree
            return parser.process();
        }
    }

    public class RavenParserContext
    {
        public string ProcessName { get; set; }
        public IList<RavenParser.ActionContext> TryContext { get; set; }
        public IList<RavenParser.ActionContext> CatchContext { get; set; }
        public IList<RavenParser.ActionContext> FinallyContext { get; set; }



    }
}