using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.CompilerTestSuit
 {
    public class CheckWhether
    {
        public static void AreEqual(object expected, object actual, string message)
        {
            if(expected == null && actual == null)
            {
                CheckWhether.TestPassed(message);
            }
            if (!expected.Equals(actual))
            {
                CheckWhether.TestFailed(message);
            }
            else
            {
                CheckWhether.TestPassed(message);
            }
        }

        public static void IsTrue(object possible, string message)
        {
            if(possible == null)
            {
                TestFailed(message);
            }
            else
            {
                TestPassed(message);
            }
        }

        public static void HaveEqualValues<T>(T expected, T actual)
        {
            var failures = new List<string>();
            var fields = typeof(T).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach(var field in fields)
            {
                var v1 = field.GetValue(expected);
                var v2 = field.GetValue(actual);
                if (v1 == null && v2 == null) continue;
                if(!v1.Equals(v2)) failures.Add(string.Format("{0}: Expected:<{1}> Actual:<{2}>", field.Name, v1, v2));
            }
            if (failures.Any())
                TestFailed(Environment.NewLine+ string.Join(Environment.NewLine, failures));
        }
        static private void DrawLine()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" ----------------------------------");
        }

        static private void TestFailed(string message)
        {
            CheckWhether.DrawLine();    
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($" >>> Assertion Failed: {message}");
            Console.ResetColor();
        }

        static private void TestPassed(string message)
        {
            CheckWhether.DrawLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($" ::: Assertion Passed: {message}");
            Console.ResetColor();
        }
    }
}
