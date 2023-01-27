
using System.Collections.Generic;

namespace Compiler
{
    sealed class BinaryExpressionSyntax : AExpressionSyntax
    {
        public BinaryExpressionSyntax(AExpressionSyntax left, 
            SyntaxToken operatorToken, AExpressionSyntax right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
        public SyntaxToken OperatorToken { get; }
        public AExpressionSyntax Left { get; }
        public AExpressionSyntax Right { get; }

        public override IEnumerable<ASyntaxNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }
}