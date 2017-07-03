using System;
/// <summary>
/// Condition Breakpoint
/// </summary>
namespace ConditionBreakpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            for (int i = 0; i < 100; i++)
            {
                //while (i-1)%10==0 and i>10, print i and sum
                sum += i;
            }
            Console.WriteLine("Sum: "+sum);
            Console.ReadLine();
        }
    }
}