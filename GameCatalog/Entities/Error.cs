using System;

namespace GameCatalog.Entities 
{
    public class InvalidParamError : Exception
    {
        public InvalidParamError(string ErrMsg)
           :     base(ErrMsg)
        {
        }

        public InvalidParamError(string ErrMsg, Exception Inner)
            : base(ErrMsg, Inner)
        {
        }            
   }
}