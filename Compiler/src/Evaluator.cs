using System;

namespace Compiler
{
    class Evaluator
    {
        private readonly AExpressionSyntax _root;
        public Evaluator(AExpressionSyntax root)
        {
            this._root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(this._root);
        }

        private int EvaluateExpression(AExpressionSyntax node)
        {
            if(node is NumberExpressionSyntax expr)
              return (int) expr.NumberToken.Value;

            if(node is BinaryExpressionSyntax bin){
               var left =  EvaluateExpression(bin.Left);
               var right = EvaluateExpression(bin.Right);
              return EvalBinaryExpression(bin, left, right);
            }

            if(node is ParenthesisExpressionSyntax par)
               return EvaluateExpression(par.Expression); 

            throw new Exception($" Unexpected expression kind: <{node.Kind}>");                
        }

        private int EvalBinaryExpression(BinaryExpressionSyntax expression, int left, int right)
        {
            switch(expression.OperatorToken.Kind){
                case SyntaxKind.PlusToken:
                    return (left + right);
                case SyntaxKind.MinusToken:
                    return (left - right);
                case SyntaxKind.TimesToken:
                    return (left * right);
                case SyntaxKind.DivideToken:
                    return (left / right);
                case SyntaxKind.PowerToken:
                    return (int)Math.Pow(left, right);
                case SyntaxKind.ModuloToken:
                    return (left % right);    
                default:
                    throw new Exception($" Unexpected binary operator kind: <{expression.OperatorToken.Kind}>");                
            }
        }
    }
}