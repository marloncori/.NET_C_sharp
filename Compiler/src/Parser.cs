using System.Collections.Generic;

namespace Compiler 
{
    class Parser : IParser
    {
        private Lexer _lexer;
        private int _position;
        private SyntaxToken[] _tokenArray;
        private List<SyntaxToken> _tokenList;    

        private List<string> _diagnostics = new List<string>();
        public Parser(string input)
        {
            this._tokenList = new List<SyntaxToken>();
            this._lexer = new Lexer(input);
            this.GetAllTokens();
        }

        public bool IsValid(SyntaxToken token)
        {   
            return token.Kind != SyntaxKind.WhiteSpaceToken && 
                       token.Kind != SyntaxKind.UnknownToken;
        }

        public IEnumerable<string> Diagnostics => this._diagnostics;

        public void LookForToken(SyntaxToken token)
        {
           if(IsValid(token))
           {
             this._tokenList.Add(token);
           }
        }
       
        public void GetAllTokens()
        {
            SyntaxToken token;
            do
            {
               token = this._lexer.NextToken();
               LookForToken(token); 
            } 
            while (token.Kind != SyntaxKind.EndOfFileToken);

            this._tokenArray = this._tokenList.ToArray();
            this._diagnostics.AddRange(this._lexer.Diagnostics);
        }

        public bool OutOfBounds(int index)
        {
            return index >= this._tokenArray.Length;
        }

        public SyntaxToken Peek(int offset)
        {
            var index = this._position + offset;
            if(OutOfBounds(index))
            {
                return this._tokenArray[this._tokenArray.Length - 1];    
            }
            return this._tokenArray[index];
        }   

        private SyntaxToken Current => this.Peek(0);

        private SyntaxToken NextToken()
        {
            var current = Current;
            this._position++;
            return current;
        }

        private bool IsPlusOrMinusSignal()
        {
            return Current.Kind == SyntaxKind.PlusToken ||
                    Current.Kind == SyntaxKind.MinusToken;
        }

        private bool IsTimesOrSlashSignal()
        {
            return Current.Kind == SyntaxKind.TimesToken ||
                    Current.Kind == SyntaxKind.DivideToken;
        }
        private bool IsPowerSignal()
        {
            return Current.Kind == SyntaxKind.PowerToken;
        }
        private bool IsModuloSignal()
        {
            return Current.Kind == SyntaxKind.ModuloToken;
        }
        private bool IsValidOperatorSignal()
        {
            return IsPlusOrMinusSignal() || 
                      IsModuloSignal();
        }
        private bool IsValidFactorOperator()
        {
            return IsTimesOrSlashSignal() || 
                      IsPowerSignal();
        }
        
        public SyntaxTree Parse()
        {
            var expression = this.ParseExpression();
            var endOfFile = this.Match(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(this._diagnostics, expression, endOfFile);
        }

        private AExpressionSyntax ParseExpression()
        {
            return this.ParseTerm();
        }
        private AExpressionSyntax ParseFactor()
        {
            var left = this.ParsePrimaryExpr();
            while(IsValidFactorOperator())
            {
                var operatorToken = this.NextToken();
                var right = this.ParsePrimaryExpr();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }
            return left;
        }
        private AExpressionSyntax ParseTerm()
        {
            var left = this.ParseFactor();
            while(IsValidOperatorSignal())
            {
                var operatorToken = this.NextToken();
                var right = this.ParseFactor();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }
            return left;
        }

        public SyntaxToken Match(SyntaxKind kind)
        {
            if(Current.Kind == kind)
                return this.NextToken();

            this._diagnostics.Add($" ERROR: Unexpected token <{Current.Kind}>, expected: <{kind}>! ");
            return new SyntaxToken(kind, Current.Position, null, null);
        }


        private AExpressionSyntax ParenthesisExpressionFound()
        {
            var left = this.NextToken();
            var expression = this.ParseExpression();
            var right = this.Match(SyntaxKind.CloseParenthesisToken);
           return new ParenthesisExpressionSyntax(left, expression, right);
        }

        private AExpressionSyntax ParsePrimaryExpr()
        {
            if(Current.Kind == SyntaxKind.OpenParenthesisToken){
                return ParenthesisExpressionFound();
            }
            var numberToken = this.Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);    
        }
    }
}