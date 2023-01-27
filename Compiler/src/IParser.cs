
namespace Compiler
{
    interface IParser 
    { 
        bool IsValid(SyntaxToken token);
        
        void LookForToken(SyntaxToken token);
        
        void GetAllTokens();
        
        bool OutOfBounds(int index);
        
        SyntaxToken Peek(int offset);
    }
}