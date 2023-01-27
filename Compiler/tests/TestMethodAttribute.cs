using System;

namespace Tests.CompilerTestSuit 
{
    public class TestMethodAttribute : Attribute
    {
        public string Name { get; set; }

        public TestMethodAttribute(string name)
        {
            this.Name = name;
        }
    }
}
