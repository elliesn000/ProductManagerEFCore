using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Classes;
using DbClasses;
namespace MProgram;

public class Program
{
    public static void Main()
    {

        using (var context = new AppContext())
        {
            context.Database.Migrate();
        }        
        while (true)
        {            
            DbClasses.UserDb.ShowAllUser();
            string input = InputParse.GetString("Input userId or Press 0 to close");

            if (input == "0")
                break;
            else if (input == "admin")
            {
                AdminManager.s_AdminManager();
            }
            else
            {
                UserManager.s_UserManager(input); 
            }
        }
    }
}
