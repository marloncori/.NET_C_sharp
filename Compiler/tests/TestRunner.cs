using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Tests.CompilerTestSuit
 {
    class TestRunner
    {
        public void BeginTesting()
        {
            var testMethods = from type in Assembly.GetExecutingAssembly().GetTypes()
                            from method in type.GetMethods()
                            let attributes = method.GetCustomAttributes(typeof(TestMethodAttribute), true)
                            where attributes != null && attributes.Length == 1
                            select new { Method = method, Attribute = (TestMethodAttribute)attributes[0] };

            TestRunner.Start(testMethods);
            TestRunner.Finish();
        }

        static private void Start(IEnumerable<dynamic> testMethods)
        {

            foreach (var testMethod in testMethods)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Running test: " + testMethod.Attribute.Name);
                testMethod.Method.Invoke(null, null);
                Console.ResetColor();
                Console.WriteLine();

            }
        }
        static private void Finish()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n ===================================================");            
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("       ALL CREATED TESTS SUCCESSFULLY RUN !");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  ===================================================\n");                        
        }
    }
}
