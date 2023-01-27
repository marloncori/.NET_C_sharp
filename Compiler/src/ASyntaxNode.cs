

using System.Collections.Generic;

namespace Compiler
{
    abstract class ASyntaxNode 
    {
        public abstract SyntaxKind Kind { get; }
        public abstract IEnumerable<ASyntaxNode> GetChildren();
    }
}