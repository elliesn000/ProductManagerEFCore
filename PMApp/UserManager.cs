using System;
using System.Collections.Generic;
using System.Text;
using DbClasses;
namespace Classes;

public class UserManager
{
    public static void s_UserManager(string inputUser)
    {
        
        DbClasses.UserDb.CheckUser(inputUser);

        while (true)        {
            
            //--- MENU USER

            int choose = MProgram.InputParse.GetInt(@$"
================================
    USER MENU: Hello {inputUser}

1. Buy Product

2. See All Product You Bought

0. Return Login Menu

Press Number to choose
================================
");


            switch (choose)
            {
                case 0:
                    return;


                case 1: //--- BUY
                    {                        
                        DbClasses.UserDb.UserBuyProduct(inputUser );
                        break;
                    }

                case 2:
                    {
                        DbClasses.OrderManager.ShowAllOrder(inputUser);
                        Console.ReadLine();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Input again");
                        break;
                    }
            }
            
            continue;
        }
    }
}
