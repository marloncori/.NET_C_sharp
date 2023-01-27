using System;

namespace MilleniumRobotics
{
    class ConsoleRobotApp
    {
        static String option;

        struct Motion
        {
            public String cmd;
            public Action run;
        }

        static void Main(string[] args)
        {
            Robot bot = new Robot();

            Motion moveForward;
            moveForward.cmd = "F"; moveForward.run = bot.Forward;
            
            Motion moveBackward;
            moveBackward.cmd = "B"; moveBackward.run = bot.Backward;
            
            Motion turnRight;
            turnRight.cmd = "R"; turnRight.run = bot.Right;

            Motion turnLeft;
            turnLeft.cmd = "L"; turnLeft.run = bot.Left;

            Motion[] control =
            {
                moveForward, moveBackward, turnRight, turnLeft
            };

            Console.WriteLine("\x1b[1;32m\n\t ----------------------------------\x1b[0m");
            Console.WriteLine("\x1b[1;36m\n\t   --  CONSOLE ROBOT APP IN C# --\x1b[0m");
            Console.WriteLine("\x1b[1;32m\n\t ----------------------------------\x1b[0m");

            Console.Write("\t\x1b[1;33m Let us control the robot from console!\x1b[0m");

            Console.WriteLine("\x1b[1;32m\n\t    :::::::::::::::::::::::::::::::::\x1b[0m");
            Console.WriteLine("\t\x1b[1;33m ---> Choose an action from the list:\x1b[0m \x1b[1;35m");

            Console.WriteLine("\t\t\x1b[1;36m [F] forward\n\t\t [B] backward\n\t\t [L] left\n\t\t [R] right\n\x1b[0m");
            Console.Write("\n--->Your option: \x1b[1;35m ");

            option = Console.ReadLine();
            Console.WriteLine("\x1b[0m");

            for (int i = 0; i < 4; i++)
            {
                if(option == control[i].cmd)
                {
                    control[i].run();
                }
            }

            Console.WriteLine("\t --------------------------------------");
        } 
      
    }

    public class Robot
    {
        public void Forward()
        {
            Console.WriteLine("    Robot is moving forward!");
        }
        public void Backward()
        {
            Console.WriteLine("    Robot is moving backward!");
        }
        public void Right()
        {
            Console.WriteLine("    Robot is turning right!");
        }
	public void Left()
        {
            Console.WriteLine("    Robot is turning left!");
        }
    }

}
