using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Classes;
namespace DbClasses;

public class UserDb
{
    //============================================================================================================ CHECK TO LOGIN OR CREAT
    public static string CheckUser(string inputUser)
    {
        using (var context = new AppContext())
        {

            Console.WriteLine($"\nFinding user with name : {inputUser}:");
            var userById = context.Users.Find(inputUser);

            if (userById != null)
            {
                Console.WriteLine($"- Found: {inputUser}");
                Console.WriteLine($"\n------ Login user {inputUser} ------");
                return inputUser;
            }
            else
            {
                Console.WriteLine($" user with name {inputUser} not found.");
                ShowAllUser();
                Console.WriteLine("\n------ Creating a user ------");

                var user = new User { UserId = inputUser, UserWallet = 5000 };
                context.Users.Add(user);
                context.SaveChanges();
                Console.WriteLine($"Add user Done: {user.UserId} - {user.UserWallet}");
                Console.WriteLine("--------------------------------");
                return inputUser;
            }
        }
    }

    //============================================================================================================ BUY
    public static void UserBuyProduct(string inputUser)
    {        
        using (var context = new AppContext())
        {
            string choose = MProgram.InputParse.GetString("Input name product to Find or Press 0 to See all");
            if (choose =="0")
            {
                ProductManager.ShowAllProduct();
            }
            else
            {
                ProductManager.FindProductWithName(choose);
            }

            Console.WriteLine("--------------------------------");
            int inputIdtoBuy = MProgram.InputParse.GetInt("Input Id Product want to buy");
            OrderManager.CheckIdtoBuy(inputIdtoBuy, inputUser);
        }
    }


    

    //================================================================================================================= READ

    public static void ShowAllUser()
    {
        using (var context = new AppContext())
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("----------- All users ----------");
            var userAll = context.Users.ToList();
            foreach (var u in userAll)
            {
                Console.WriteLine($" - {u.UserId}: {u.UserWallet}");
            }
            Console.WriteLine("--------------------------------");
        }
    }
}
