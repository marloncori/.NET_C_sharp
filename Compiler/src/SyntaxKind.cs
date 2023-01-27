
namespace Compiler
{
    enum SyntaxKind
    {
      NumberToken, WhiteSpaceToken,
      PlusToken, MinusToken, ModuloToken,
      TimesToken, DivideToken, PowerToken,
      OpenParenthesisToken, CloseParenthesisToken,
      UnknownToken, EndOfFileToken, NumberExpression,
      BinaryExpression, ParenthesisExpression,
    }
}