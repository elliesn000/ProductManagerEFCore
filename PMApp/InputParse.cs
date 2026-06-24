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
            Console.WriteLine("\nThis value cannot be empty! Input again. Or Press 0 return Main Menu.");
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
            Console.WriteLine("\nThis value invalid, need int type! Input again. Or Press 0 return Main Menu");

        }        
    }
}

