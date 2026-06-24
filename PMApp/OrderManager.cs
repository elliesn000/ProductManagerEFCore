using Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace DbClasses;

public class OrderManager
{
    //============================================================================================================================ BUY
    public static int CreatOrder( string inputUserId, List<Product> inputListProduct)
    {
        int inputProductId = MProgram.InputParse.GetInt("Input Id Product want to buy or Press 0 to retun User Menu");
        if (inputProductId == 0)
            return 0;
        //check Id in List
        foreach (var p in inputListProduct)
        {
            if (inputProductId == p.ProductId) //trueId
            {

                using (var context = new AppContext())
                {
                    var userToBuy = context.Users.Find(inputUserId)!; //already check User valid in CheckUser()
                    var productToBuy = context.Products.Find(inputProductId)!; //already check Id valid
                    
                    Console.WriteLine($"Want to buy {productToBuy.ProductName} - Remaining: {productToBuy.ProductQtt}");
                    int inputQttToBuy = MProgram.InputParse.GetInt("Input quantity want to buy");

                    int updateQtt = productToBuy.ProductQtt - inputQttToBuy;
                    decimal updateWallet = userToBuy.UserWallet - (productToBuy.ProductPrice * inputQttToBuy);

                    if (updateQtt >= 0 && updateWallet >= 0)
                    {
                        var orderToCheck = context.Orders
                            .FirstOrDefault(p => p.Products.ProductId == inputProductId && p.Users.UserId == inputUserId);

                        if (orderToCheck != null) // product already bought, need update Qtt
                        {
                            Console.WriteLine("#product already bought, need update Qtt");
                            orderToCheck.OrderQtt += inputQttToBuy;
                            userToBuy.UserWallet = updateWallet;
                            productToBuy.ProductQtt = updateQtt;
                            context.SaveChanges();

                            Console.WriteLine($"Buying.... {orderToCheck.Products.ProductName}: {orderToCheck.OrderQtt} ");

                        }
                        else // Crear new
                        {
                            Console.WriteLine("#new product, need creat new order");
                            var order = new Order { Products = productToBuy, Users = userToBuy, OrderQtt = inputQttToBuy };
                            userToBuy.UserWallet = updateWallet;
                            productToBuy.ProductQtt = updateQtt;
                            context.Add(order);
                            context.SaveChanges();

                            Console.WriteLine($"Buying... you will have {order.Products.ProductName}: {order.OrderQtt} ");

                        }
                        Console.WriteLine($"Buy succed: {productToBuy.ProductName} - Quantity: {inputQttToBuy} ");
                        Console.WriteLine($"Your Wallet: {userToBuy.UserWallet} $");
                        Console.ReadLine();
                        ShowAllOrder(inputUserId);
                    }

                    else if (updateWallet < 0) // fail with Wallet < 0 after Buy
                    {
                        Console.WriteLine("Your balance not enough. Input again");
                        return CreatOrder(inputUserId, inputListProduct); ;
                    }
                    
                    else  // fail with Qtt < 0 after Buy
                    {
                        Console.WriteLine("Qtt invalid. Input again");
                    }
                    return CreatOrder(inputUserId, inputListProduct); ;

                }
            }        
                continue;            
        }
        Console.WriteLine("\nError: Id invalid. Input again.");        
        return CreatOrder(inputUserId, inputListProduct);
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
            }
            return;
        }
    }
}
