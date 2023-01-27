using System;
using System.Linq;
using Tests.CompilerTestSuit;

namespace Compiler
{
    class Program 
    {
        static void Main(string[] args)
        {
            /*while (true)
            {
                var syntaxTree = ProcessUserInput();
                if(syntaxTree == null) 
                {
                    Console.WriteLine("\n ::: Good night, master!!! ::: "); 
                    return;
                }
                
                if(!ReportErrors(syntaxTree))
                {
                    var evaluator = new Evaluator(syntaxTree.Root);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"  result: {evaluator.Evaluate()}");
                }
            }*/
             var testRunner = new TestRunner();
                testRunner.BeginTesting();
        }

        static public SyntaxTree ProcessUserInput()
        {
            var showTree = false;
            var showTokens = false;
            Console.Write(" > ");
            var line = System.Console.ReadLine();
            
            if(string.IsNullOrWhiteSpace(line))
                     return null;
            if(line == "--show-tree" || line == "-st"){
                showTree = !showTree;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(" > ok!");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(" > ");
                line = System.Console.ReadLine();
            }
            else if(line == "--show-tokens" || line == "--tokenize" || line == "-stk"){
                showTokens = !showTokens;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" > ok!");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write(" > ");
                line = System.Console.ReadLine();
            }
            return ProcessSyntaxTree(showTree, showTokens, line);
        }

        static public SyntaxTree ProcessSyntaxTree(bool show, bool tokens, string text)
        {
            if(show && !tokens){
                show = !show; 
                Console.WriteLine(" --> SHOWING PARSE TREE THIS TIME...");
                var syntaxTree = ProcessSyntaxTree(show, tokens, text);
                return ShowParseTree(syntaxTree);
            } else if(tokens && !show){
                Console.WriteLine(" --> SHOWING TOKENS THIS TIME...");
                var syntaxTree = SyntaxTree.Parse(text);
                Tokenize(text);    
                return syntaxTree;
            } 
            else if(tokens && show) 
            {
                show = !show; 
                tokens = !tokens;
                Console.WriteLine(" --> SHOWING PARSE TREE AND TOKENS THIS TIME...");
                Tokenize(text);    
                var syntaxTree = ProcessSyntaxTree(show, tokens, text);
                return ShowParseTree(syntaxTree);
            }
            else
            {
                var syntaxTree = SyntaxTree.Parse(text);
                return syntaxTree;
            }

        }
        static public SyntaxTree ShowParseTree(SyntaxTree syntaxTree)
        {
            var printer = new Printer();
            printer.SetColor();
            printer.ShowTree(syntaxTree.Root);                
            Console.ForegroundColor = new Printer().Color;
           return syntaxTree;
        }
        static public bool ReportErrors(SyntaxTree syntaxTree)
        {
            if(syntaxTree.Diagnostics.Any()){
                Console.ForegroundColor = ConsoleColor.DarkRed;
                foreach(var error in syntaxTree.Diagnostics)
                       Console.WriteLine($"\n\t{error}");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                return true;          
            }
            return false;
        }

        static public void Tokenize(string line)
        {
            var lexer = new Lexer(line);
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" --------------------------------- ");
                    
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                var token = lexer.NextToken();
                if(token.Kind == SyntaxKind.EndOfFileToken)
                    break;
                    
                Console.Write($"   ::: {token.Kind}: {token.Text}");
                Console.WriteLine();
            }            
        }
   }
} 