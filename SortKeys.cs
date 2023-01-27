 using System;
 using System.Collections.Generic;

 namespace MilleniumRobotics 
 {
     class SortKeys 
     {

       static String[] names = {"Rugby", "Criket", "Soccer", "Volleyball"};
       static int[] values = {1, 2, 3, 4};

       static void Print(String& value){
            Console.WriteLine($"   --> Sport name = \'{value}\'.");
       }

       static void Main(String[] args)
       {
            Dictionary<String, int> sports = new();
           for(int i=0; i<4; i++)
           {
             sports.Add(names[i], values[i]);
           }	

           var keys = sports.Keys.ToList();
               keys.Sort();

           foreach(var key in keys)
           {
              Print(key); 
           }          
       }

     }
 }