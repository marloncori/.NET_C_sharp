using System.Collections.Generic;

namespace Compiler
{
    class Lexer : ILexer 
    {
        private readonly string _text;
        private int _position; 
        private List<string> _diagnostics = new List<string>();

        public Lexer(string text) 
        {
            this._text = text;

        }

        private char _current
        {
            get
            {
                if(this._position >= this._text.Length)
                    return '\0';
                
                return this._text[this._position];
            }
        }

        private void Next()
        {
            this._position++;
        }

        public IEnumerable<string> Diagnostics => this._diagnostics;

        public SyntaxToken EndOfFileTokenFound()
        {
            return new SyntaxToken(SyntaxKind.EndOfFileToken, this._position, "\0", null);
        }
        public SyntaxToken NumberTokenFound()
        {
            var start = this._position;
            while(char.IsDigit(this._current))
                this.Next();

            var length = this._position - start;
            var text = this._text.Substring(start, length);
            if(!int.TryParse(text, out var value))
                this._diagnostics.Add(" The number cannot be represented by a number of type Int32!");
            return new SyntaxToken(SyntaxKind.NumberToken, length, text, value);
        }

        public SyntaxToken WhiteSpaceTokenFound()
        {
            var start = this._position;
            while(char.IsWhiteSpace(this._current))
                this.Next();

            var length = this._position - start;
            var text = this._text.Substring(start, length);
            int.TryParse(text, out var value);
           return new SyntaxToken(SyntaxKind.WhiteSpaceToken, length, text, value);

        }

        public SyntaxToken OperatorOrParenthesisFound()
        {
            switch(this._current)
            {
                case '+':
                   return new SyntaxToken(SyntaxKind.PlusToken, this._position++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxKind.MinusToken, this._position++, "-", null);
                case '*':
                   return new SyntaxToken(SyntaxKind.TimesToken, this._position++, "*", null);
                case '^':
                   return new SyntaxToken(SyntaxKind.PowerToken, this._position++, "^", null);
                case '%':
                   return new SyntaxToken(SyntaxKind.ModuloToken, this._position++, "%", null);
                case '/':
                   return new SyntaxToken(SyntaxKind.DivideToken, this._position++, "-", null);
                case '(':
                   return new SyntaxToken(SyntaxKind.OpenParenthesisToken, this._position++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxKind.CloseParenthesisToken, this._position++, ")", null);        
                default:
                    this._diagnostics.Add($"  ERROR: Bad character input: '{this._current}!");
                    return new SyntaxToken(SyntaxKind.UnknownToken, this._position++, this._text.Substring(this._position-1, 1), null);
                }
        }

        public SyntaxToken NextToken()
        {
            if(this._position >= this._text.Length)
                return this.EndOfFileTokenFound();

            if(char.IsDigit(this._current))
                return this.NumberTokenFound();
        
            else if(char.IsWhiteSpace(this._current))
                return this.WhiteSpaceTokenFound();

            else 
                return this.OperatorOrParenthesisFound();
        }

        public List<SyntaxToken> GetAllTokens()
        {
            var tokens = new List<SyntaxToken>();
            SyntaxToken token;
            while ((token = NextToken()) != null)
            {
                tokens.Add(token);
            }
            return tokens;
        }

    }
}
