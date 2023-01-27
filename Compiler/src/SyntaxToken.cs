using System.Collections.Generic;
using System.Linq;

namespace Compiler
{
    class SyntaxToken : ASyntaxNode
    {
        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;  
            Value = value;   
        }

        public override SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }

        public object Value { get; }

        public override IEnumerable<ASyntaxNode> GetChildren() => Enumerable.Empty<ASyntaxNode>();
    }
}
