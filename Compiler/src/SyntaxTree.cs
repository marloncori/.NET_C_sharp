
using System.Collections.Generic;
using System.Linq;

namespace Compiler 
{
    class SyntaxTree
    {
        public SyntaxTree(IEnumerable<string> diagnostics, AExpressionSyntax root,
            SyntaxToken endOfFileToken )
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOfFileToken = endOfFileToken;
        }

        public IReadOnlyList<string> Diagnostics { get; }
        public AExpressionSyntax Root { get; }
        public SyntaxToken EndOfFileToken { get; }
        
        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }

}