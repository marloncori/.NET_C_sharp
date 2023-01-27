using System;

namespace Compiler
{
    class SimpleParser
    {
       public SimpleParser()
       {
         Console.WriteLine("  Simple Parser has been instantiated!");
       } 
       void ParseLine(String line)
            {
                if(String.IsNullOrWhiteSpace(line))
                    return;

                char[] delimiter = new char[] { ' ' };
                string[] charArray = line.Split(delimiter, StringSplitOptions.None);
                
                if(charArray.Length < 2)
                    Console.WriteLine(charArray[0]);
                else if(charArray.Length == 3)
                     try
                     {
                        int num1 = Int32.Parse(charArray[0]);
                        int num2 = Int32.Parse(charArray[2]);

                        int result = Calculate(charArray[1], num1, num2);                         
                        Console.WriteLine(result);
                     }                    
                     catch (FormatException e)
                     {
                        Console.WriteLine(e.Message);
                     }
                else if(charArray.Length == 5)
                     try
                     {
                        int num1 = Int32.Parse(charArray[0]);
                        int num2 = Int32.Parse(charArray[2]);
                        int num3 = Int32.Parse(charArray[4]);
                        
                        int partialResult = Calculate(charArray[1], num1, num2);
                        int finalResult = Calculate(charArray[3], partialResult, num3);
                        Console.WriteLine(finalResult);
                     }                    
                     catch (FormatException e)
                     {
                        Console.WriteLine(e.Message);
                     }
                else 
                {
                    Console.WriteLine("  ERROR: Invalid expression!");
                }
            }
            
       int Calculate(string oper, int value1, int value2)
            {
                switch(oper){
                    case "+":
                        return (value1+value2);
                    case "-":
                        return (value1-value2);
                    case "*":
                        return (value1*value2); 
                    case "/":
                        return (value1-value2); 
                    default:
                        return -1;
                }
            }
    }
}
