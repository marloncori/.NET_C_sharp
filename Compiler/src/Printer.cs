using System;
using System.Linq;

namespace Compiler 
{

    class Printer
    {
        private string _marker;
        private ConsoleColor _color;
        public Printer() {}

        public void SetColor()
        {
           this._color = Console.ForegroundColor;
           Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        public ConsoleColor Color => this._color;
        public void ShowTree(ASyntaxNode node, string indent = "", bool isLast = true)
        {
            if(node == null)
                return;

            this._color = Console.ForegroundColor;
            this._marker = isLast ? " └──" : " ├──";
            
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(indent);
            Console.Write(this._marker);

            Console.ForegroundColor = this._color;
            Console.Write(node.Kind);
            if(node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }
            Console.WriteLine();
    
            indent += isLast ? "    " : " │   "; 
            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                ShowTree(child, indent, child == lastChild);
        }
 
    }
}
