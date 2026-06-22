using System;
using System.Collections.Generic;
using System.Text;
namespace MProgram;


public class InputParse
{
    public static string GetString(string prompt)
    {
        while(true)
        {
            Console.WriteLine(prompt);
            string? input = Console.ReadLine();
            if(!string.IsNullOrEmpty(input))
            {
                return input;
            }
            Console.WriteLine("This value cannot be empty! Input again. Or press 0 return Main Menu.");
        }
    }

    public static int GetInt(string prompt)
    {
        while(true)
        {
            Console.WriteLine(prompt);
            string? input = Console.ReadLine();
            if(int.TryParse(input, out int intout))
            {
                return intout;
            }
            Console.WriteLine(@"This value invalid, need int type! Input again. Or press 0 return Main Menu.");

        }        
    }
}

