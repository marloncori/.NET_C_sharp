
namespace Compiler{
    interface ILexer
    {
        SyntaxToken EndOfFileTokenFound();
        SyntaxToken NumberTokenFound();
        SyntaxToken WhiteSpaceTokenFound();
        SyntaxToken OperatorOrParenthesisFound();
        SyntaxToken NextToken();
    }
}