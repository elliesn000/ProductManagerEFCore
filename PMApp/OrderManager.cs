using Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace DbClasses;

public class OrderManager
{
    //============================================================================================================================ BUY
    public static void CheckIdtoBuy(int inputProductId, string inputUserId)
    {

        using (var context = new AppContext())
        {
            var userToBuy = context.Users.Find(inputUserId); //already check (Creat new or login) in CheckUser()
            var productToBuy = context.Products.Find(inputProductId);


            if (productToBuy != null)
            {
                Console.WriteLine($"Want to buy {productToBuy.ProductName} - Remaining: {productToBuy.ProductQtt}");
                int inputQttToBuy = MProgram.InputParse.GetInt("Input quantity want to buy");

                int updateQtt = productToBuy.ProductQtt - inputQttToBuy;

                if (updateQtt >= 0)
                {
                    var orderToCheck = context.Orders
                        .FirstOrDefault(p => p.Products.ProductId == inputProductId && p.Users.UserId == inputUserId);

                    if (orderToCheck != null) // product already bought, need add Qtt
                    {
                        Console.WriteLine("#product already bought, need add Qtt");
                        productToBuy.ProductQtt = updateQtt;

                        Console.WriteLine($"Buying.... {orderToCheck.Products.ProductName}: {orderToCheck.OrderQtt} ");
                        Console.ReadLine();
                    }
                    else // Crear new
                    {
                        Console.WriteLine("#new product, need creat new order");
                        var order = new Order { Products = productToBuy, Users = userToBuy, OrderQtt = inputQttToBuy };
                        context.Add(order);

                        Console.WriteLine($"Buying... you will have {order.Products.ProductName}: {order.OrderQtt} ");
                        Console.ReadLine();
                    }

                    decimal updateWallet = userToBuy.UserWallet - (productToBuy.ProductPrice * inputQttToBuy);
                    if (updateWallet >= 0)
                    {
                        userToBuy.UserWallet = updateWallet;
                        context.SaveChanges();

                        Console.WriteLine($"Buy succed: {productToBuy.ProductName} - Quantity: {inputQttToBuy} ");
                        Console.WriteLine($"Your Wallet: {userToBuy.UserWallet} $");
                        Console.ReadLine();
                        ShowAllOrder(inputUserId);
                    }
                    else
                    {
                        Console.WriteLine("Your balance not enough");
                        return;
                    }
                }
                else  // fail with Qtt < 0 after Buy
                {
                    Console.WriteLine("Qtt invalid");
                }
                return;

            }
            else //IdtoBuy invalid
            {
                Console.WriteLine("Id invalid");
            }
            return; 
        }
    }


    //============================================================================================ READ
    public static void ShowAllOrder(string inputUser)
    {
        Console.Clear();
        using (var context = new AppContext())
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("------------All order-----------");
            var allOrder = context.Orders

                    .Where(p => p.Users.UserId == inputUser)

                    .Select(p => new
                    {
                        UserId = p.Users.UserId,
                        ProductId = p.Products.ProductId,
                        ProductName = p.Products.ProductName,
                        OrderQtt = p.OrderQtt
                    })
                    .ToList();
            if (allOrder.Count != 0)
            {
                foreach (var p in allOrder)
                {
                    Console.WriteLine($"- user: {p.UserId} - productid: {p.ProductId} - productname {p.ProductName}: {p.OrderQtt}");
                }                

            }
            else
            {
                Console.WriteLine("None Product");
                Console.ReadLine();
            }
            return;
        }
    }
}
