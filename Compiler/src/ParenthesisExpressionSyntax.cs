
using System.Collections.Generic;

namespace Compiler
{
    class ParenthesisExpressionSyntax : AExpressionSyntax
    {
        public ParenthesisExpressionSyntax(SyntaxToken openParenthesisToken,
                                            AExpressionSyntax expression,
                                           SyntaxToken closeParenthesisToken)
        {
            OpenParenthesisToken = openParenthesisToken;
            Expression = expression;
            CloseParenthesisToken = closeParenthesisToken;
        }
        public override SyntaxKind Kind => SyntaxKind.ParenthesisExpression;
        public SyntaxToken OpenParenthesisToken { get; }
        public AExpressionSyntax Expression { get; }
        public SyntaxToken CloseParenthesisToken { get; }

        public override IEnumerable<ASyntaxNode> GetChildren()
        {
            yield return OpenParenthesisToken;
            yield return Expression;
            yield return CloseParenthesisToken;
        }
    }
}