using System;
using System.Collections.Generic;
using System.Linq;
using Compiler;

namespace Tests.CompilerTestSuit
{
    public class LexerUnitTest
    {
        private static string ThenText(string text)
        {
            return $"The text of the token should be '{text}'.";
        } 
        private static string ThenValue(int value)
        {
            if(value == 1 )
                return "The value of the token should be NULL.";
            return $"The value of the token should be {value}.";
        } 
        private static string ThenKind(string kind)
        {
            return $"The kind of the token should be {kind}Token.";
        }

        private static string ThenSize(int size)
        {
            return $"The number of tokens should be {size}1.";
        }

       [TestMethodAttribute("Test #1: positive integers")]
        public static void TestPositiveIntegers()
        {
            var lexer = new Lexer("12345");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(1, token.Text.Length, ThenSize(1));
            CheckWhether.AreEqual(SyntaxKind.NumberToken, token.Kind, ThenKind("Number"));
        }

        [TestMethodAttribute("Test #2: a valid input of five digit number")]
        public static void TestFiveDigitNumber()
        {
            var lexer = new Lexer("12345");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.NumberToken, token.Kind, ThenKind("Number"));
            CheckWhether.AreEqual("12345", token.Text, ThenText("12345"));
            CheckWhether.AreEqual(12345, token.Value,  ThenValue(12345));
        }

        [TestMethodAttribute("Test #3: a valid input of a number followed by a whitespace")]
        public static void TestNumberFollowedByWhiteSpace()
        {
            var lexer = new Lexer("78912 ");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.NumberToken, token.Kind, ThenKind("Number"));
            CheckWhether.AreEqual("78912", token.Text, ThenText("78912"));
            CheckWhether.AreEqual(78912, token.Value, ThenValue(78912));

            token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.WhiteSpaceToken, token.Kind, ThenKind("WhiteSpace"));

            CheckWhether.AreEqual(" ", token.Text, ThenText(" "));
            CheckWhether.AreEqual(0, token.Value, ThenValue(0));
        }

        
        [TestMethodAttribute("Test #4: a valid input of a number followed by an operator")]
        public static void TestNumberFollowedByOperator()
        {
            var lexer = new Lexer("12345+");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.NumberToken, token.Kind, ThenKind("Number"));
            CheckWhether.AreEqual(6, token.Text.Length, ThenSize(6));
            CheckWhether.AreEqual("12345", token.Text, ThenText("12345"));
            CheckWhether.AreEqual(12345, token.Value, ThenValue(12345));

            token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.PlusToken, token.Kind, ThenKind("Plus"));
            CheckWhether.AreEqual(1, token.Text.Length, ThenSize(1));
            CheckWhether.AreEqual("+", token.Text, ThenText("+"));
            //CheckWhether.AreEqual(null, token.Value, ThenValue(-1));
        }

        [TestMethodAttribute("Test #5: an invalid input of a character that is not a digit, whitespace, operator or parenthesis")]
        public static void TestInvalidCharacterInput()
        {
            var lexer = new Lexer("12345@");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.NumberToken, token.Kind, ThenKind("Number"));
            CheckWhether.AreEqual(6, token.Text.Length, ThenSize(6));
            CheckWhether.AreEqual("12345", token.Text, ThenText("12345"));
            CheckWhether.AreEqual(12345, token.Value, ThenValue(12345));

            token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.UnknownToken, token.Kind, ThenKind("Unknown"));
            CheckWhether.AreEqual(1, token.Text.Length, ThenSize(1));
            CheckWhether.AreEqual("@", token.Text, ThenText("@"));
            //CheckWhether.AreEqual(null, token.Value, ThenValue(-1));

            CheckWhether.IsTrue(lexer.Diagnostics.Any(d => d.Contains("Bad character input")), 
                "Test whether any bad character input has been detected.");
        }

        [TestMethodAttribute("Test #6: number token found with a negative number in the beginning.")]
        public static void TestNumberTokenFoundWithNegativeNumber()
        {
            var lexer = new Lexer("-789");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.MinusToken, token.Kind, ThenKind("Minus"));
            CheckWhether.AreEqual("-", token.Text, ThenText("-"));

            token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.NumberToken, token.Kind, ThenKind("Number"));
            CheckWhether.AreEqual("789", token.Text, ThenText("789"));
            CheckWhether.AreEqual(789, token.Value, ThenValue(789));
        }

        [TestMethodAttribute("Test #7: number token found with multiple digits")]
        public static void TestNumberTokenFoundWithMultipleDigits()
        {
            var lexer = new Lexer("# + 4 - 8 * 12");
            var kinds = new SyntaxKind[9]{SyntaxKind.UnknownToken, SyntaxKind.WhiteSpaceToken, SyntaxKind.PlusToken,
                 SyntaxKind.WhiteSpaceToken, SyntaxKind.NumberToken, SyntaxKind.WhiteSpaceToken,
                 SyntaxKind.MinusToken, SyntaxKind.WhiteSpaceToken, SyntaxKind.NumberToken};
            var names = new string[9]{"Unknown", "WhiteSpace", "Plus", "WhiteSpace", "Number", "WhiteSpace", "Minus", "WhiteSpace", "Number"};
            var symbols = new string[9]{"#", " ", "+", " ", "4", " ", "-", " ", "8"};
            for(var i=0; i<9; i++)
            {
                var token = lexer.NextToken();
                CheckWhether.AreEqual(kinds[i], token.Kind, ThenKind(names[i]));
                CheckWhether.AreEqual(symbols[i], token.Text, ThenText(symbols[i]));
                if(token.Value != null && token.Value.Equals(4))
                    CheckWhether.AreEqual(4, token.Value, ThenValue(4));
                else if(token.Value != null && token.Value.Equals(8))
                    CheckWhether.AreEqual(8, token.Value, ThenValue(8));    
                else
                    continue;    
            }
        }
        
/*        [TestMethodAttribute("Test #8: numberToken found with invalid number (too long)")]
        public void TestNumberTokenFoundWithInvalidNumber()
        {
            string bigNum = "1234567898765432112345678987654321";
            var lexer = new Lexer(bigNum);
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.NumberToken, token.Kind, ThenKind("Number"));
            CheckWhether.AreEqual(bigNum, token.Text, ThenText(bigNum));
            //CheckWhether.AreEqual(null, token.Value, ThenValue);
            CheckWhether.AreEqual(1, lexer.Diagnostics.Count, ThenSize(1));
            string warning = " The number cannot be represented by a number of type Int32!";
            CheckWhether.AreEqual(warning, lexer.Diagnostics[0], ThenText(warning));
        }*/
        
        [TestMethodAttribute(" Test #9: has a whitespace token been found?")]
        public static void TestWhiteSpaceTokenFound()
        {
            var lexer = new Lexer("  \t\r\n");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.WhiteSpaceToken, token.Kind, ThenKind("WhiteSpace"));
            CheckWhether.AreEqual("  \t\r\n", token.Text, ThenText("  \t\r\n"));
           // CheckWhether.AreEqual(null, token.Value);
        }

        [TestMethod(" Test #10: has a plus operator token been found?")]
        public static void TestOperatorPlus()
        {
            var lexer = new Lexer("+");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.PlusToken, token.Kind, ThenKind("Plus"));
            CheckWhether.AreEqual("+", token.Text, ThenText("+"));
            //CheckWhether.AreEqual(null, token.Value);
        }

        [TestMethodAttribute(" Test #11: has a minus operator token been found?")]
        public static void TestOperatorMinus()
        {
            var lexer = new Lexer("-");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.MinusToken, token.Kind, ThenKind("Minus"));
            CheckWhether.AreEqual("-", token.Text, ThenText("-"));
            //CheckWhether.AreEqual(null, token.Value);
        }


        [TestMethodAttribute(" Test #12: times operator token found ")]
        public static void TestOperatorTimes()
        {
            var lexer = new Lexer("*");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.TimesToken, token.Kind, ThenKind("Times"));
            CheckWhether.AreEqual("*", token.Text, ThenText("*"));
            //CheckWhether.AreEqual(null, token.Value);
        }

        [TestMethodAttribute(" Test #13: power operator token found ")]
        public static void TestOperatorPower()
        {
            var lexer = new Lexer("^");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.PowerToken, token.Kind, ThenKind("Power"));
            CheckWhether.AreEqual("^", token.Text, ThenText("^"));
            //CheckWhether.AreEqual(null, token.Value, ThenValue(-1));
        }

        [TestMethodAttribute(" Test #14: modulo operator found ")]
        public static void TestOperatorModulo()
        {
            var lexer = new Lexer("%");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.ModuloToken, token.Kind, ThenKind("Modulo"));
            CheckWhether.AreEqual("%", token.Text, ThenText("%"));
            //CheckWhether.AreEqual(null, token.Value, ThenValue(-1));
        }

        [TestMethodAttribute(" Test #15: division operation token found ")]
        public static void TestOperatorDivide()
        {
            var lexer = new Lexer("/");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.DivideToken, token.Kind, ThenKind("Divide"));
            CheckWhether.AreEqual("/", token.Text, ThenText("/"));
            //CheckWhether.AreEqual(null, token.Value, ThenValue(-1));
        }

        [TestMethodAttribute(" Test #16: open parenthesis found ")]
        public static void TestOpenParenthesisFound()
        {
            var lexer = new Lexer("()");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.OpenParenthesisToken, token.Kind, ThenKind("OpenParenthesis"));
            CheckWhether.AreEqual("(", token.Text, ThenText("("));
            //CheckWhether.AreEqual(null, token.Value, ThenValue(-1));
        }

        [TestMethodAttribute(" Test #17: close parenthesis found ")]
        public static void TestCloseParenthesisFound()
        {
            var lexer = new Lexer(")");
            var token = lexer.NextToken();
            CheckWhether.AreEqual(SyntaxKind.CloseParenthesisToken, token.Kind, ThenKind("CloseParenthesis"));
            CheckWhether.AreEqual(")", token.Text, ThenText(")"));
           // CheckWhether.AreEqual(null, token.Value, ThenValue(-1));
        }
        
        /*[TestMethodAttribute(" Test #18: seven invalid characters found ")]
        public static void TestInvalidCharacters_GetAllTokens()
        {
            var lexer = new Lexer("@#$%^&*");
            var tokens = new List<SyntaxToken>();
            var i=0;
            while(i<7) tokens.Add(lexer.NextToken()); i++;
            CheckWhether.AreEqual(i++, tokens.Count, ThenSize(7));
        }
/*
        [TestMethod]
        public void TestInvalidCharacters_TokenKind()
        {
            var lexer = new Lexer("@#$%^&*");
            var tokens = lexer.GetAllTokens();
            for(int i=0; i<tokens.Count; i++)
            {
                CheckWhether.AreEqual(SyntaxKind.UnknownToken, tokens[i].Kind);
            }
        }
    
        [TestMethod]
        public void TestInvalidCharacters_TokenText()
        {
            var lexer = new Lexer("@#$%^&*");
            var tokens = lexer.GetAllTokens();
            var symbols = new List<string>(){"@","#","$","%","^","&","*"};

            for(int i = 0; i < symbols.Length; i++)
                CheckWhether.AreEqual(symbols[i], tokens[i].Text);
        }
    
        [TestMethod]
        public void TestInvalidCharacters_DiagnosticsCount()
        {
            var lexer = new Lexer("@#$%^&*");
            var tokens = lexer.GetAllTokens();
            CheckWhether.AreEqual(1, lexer.Diagnostics.Count);
        }
    
        [TestMethod]
        public void TestInvalidCharacters_DiagnosticsMessage()
        {
            var lexer = new Lexer("@#$%^&*");
            var tokens = lexer.GetAllTokens();
            CheckWhether.AreEqual("ERROR: Bad character input: '@!", lexer.Diagnostics[0]);
        }

        [TestMethod]
        public void TestMultipleInvalidCharacters_TokenKind()
        {
            var lexer = new Lexer("@#$%^&!@#$%^&");
            var tokens = lexer.GetAllTokens();

            for(int i=0; i<tokens.Count; i++)
                CheckWhether.AreEqual(SyntaxKind.UnknownToken, tokens[i].Kind);
        }

        [TestMethod]
        public void TestMultipleInvalidCharacters_TokenText()
        {
            var lexer = new Lexer("@#$%^&*!@#$%^&*");
            var tokens = lexer.GetAllTokens();
            var symbols = new List<string>(){"@","#","$","%","^","&",
                "*","!","@","#","$","%","^","&","*"};
            
            for(int i = 0; i < symbols.Length; i++)
                CheckWhether.AreEqual(symbols[i], tokens[i].Text);
            
        }

        [TestMethod]
        public void TestMultipleInvalidCharacters_DiagnosticsCount()
        {
            var lexer = new Lexer("@#$%^&*!@#$%^&*");
            var tokens = lexer.GetAllTokens();
            CheckWhether.AreEqual(2, lexer.Diagnostics.Count);
        }

        [TestMethod]
        public void TestMultipleInvalidCharacters_DiagnosticsMessage()
        {
            var lexer = new Lexer("@#$%^&*!@#$%^&*");
            var tokens = lexer.GetAllTokens();
            CheckWhether.AreEqual("ERROR: Bad character input: '@!", lexer.Diagnostics[0]);
            CheckWhether.AreEqual("ERROR: Bad character input: '!", lexer.Diagnostics[1]);
        }

        [TestMethod]
        public void TestMixedValidAndInvalidCharacters_TokenKind()
        {
            var lexer = new Lexer("1+2-3@#$%^&*");
            var tokens = lexer.GetAllTokens();
            for(int i=0; i<tokens.Count-6; i++)
            {
                CheckWhether.AreNotEqual(SyntaxKind.UnknownToken, tokens[i].Kind);
            }
            for(int i=tokens.Count-6; i<tokens.Count; i++)
            {
                CheckWhether.AreEqual(SyntaxKind.UnknownToken, tokens[i].Kind);
            }
        }

        [TestMethod]
        public void TestMultipleOperators()
        {
            var lexer = new Lexer("+-*^%/");
            var tokens = lexer.GetAllTokens();
            var kinds = new List<SyntaxToken>(){
                SyntaxKind.PlusToken,SyntaxKind.MinusToken,SyntaxKind.TimesToken,
                SyntaxKind.PowerToken,SyntaxKind.ModuloToken,SyntaxKind.DivideToken
            };
            var symbols = new List<string>(){"+","-","*","^","%","/"};
            CheckWhether.AreEqual(6, tokens.Count);
            for(int i=0; i<symbols.Length; i++)
            {
                CheckWhether.AreEqual(kinds[i], tokens[i].Kind);
                CheckWhether.AreEqual(symbols[i], tokens[i].Text);
            }
        }

        [TestMethod]
        public void TestMultipleParenthesis()
        {
            var lexer = new Lexer("()()");
            var tokens = lexer.GetAllTokens();
            CheckWhether.AreEqual(4, tokens.Count);
            CheckWhether.AreEqual(SyntaxKind.OpenParenthesisToken, tokens[0].Kind);
            CheckWhether.AreEqual(SyntaxKind.CloseParenthesisToken, tokens[1].Kind);
            CheckWhether.AreEqual(SyntaxKind.OpenParenthesisToken, tokens[2].Kind);
            CheckWhether.AreEqual(SyntaxKind.CloseParenthesisToken, tokens[3].Kind);
        }*/
    }
}