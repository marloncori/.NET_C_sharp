
using System.Collections.Generic;

namespace Compiler
{
    sealed class NumberExpressionSyntax : AExpressionSyntax
    {
        public NumberExpressionSyntax(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;
        public SyntaxToken NumberToken { get; }

        public override IEnumerable<ASyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }
    }
}