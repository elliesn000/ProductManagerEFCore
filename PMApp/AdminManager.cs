using System;
using System.Collections.Generic;
using System.Text;
using DbClasses;
namespace Classes;

public class AdminManager
{
    public static void s_AdminManager()
    {
        
        while (true)
        {
            
            //--- MENU ADMIN

            int choose = MProgram.InputParse.GetInt(@"
         Hello Admin !
================================
1. Add New Product
2. Update Quantity Product
3. Delete Product
4. Add Demo Product
5. See All Product

0. Return Login Menu

Press Number to choose
================================
");


            switch (choose)
            {
                case 0:
                    Console.Clear();
                    return;


                case 1: //--- CREAT
                    {
                        
                        DbClasses.ProductManager.CreateProduct();
                        break;
                    }

                case 2: //--- UPDATE QTT
                    {
                        
                        DbClasses.ProductManager.UpdateQttProduct();
                        continue;
                    }

                case 3:  //--- DELETE                
                    {
                        
                        DbClasses.ProductManager.DeleteProduct();
                        continue;
                    }


                case 4: //--- CREATE DEMO PRODUCT
                    {
                        Console.WriteLine("\n------Creating New Product------");
                        using (var context = new AppContext())
                        {
                            var product1 = new Product { ProductName = "Book1", ProductPrice = 20.50m, ProductQtt = 11 };
                            var product2 = new Product { ProductName = "NoteBook2", ProductPrice = 55.00m, ProductQtt = 23 };
                            var product3 = new Product { ProductName = "PencilBook3", ProductPrice = 37.56m, ProductQtt = 8 };

                            context.Products.Add(product1);
                            context.Products.AddRange(product2, product3);

                            context.SaveChanges();

                            Console.WriteLine($"Added Product 1: {product1.ProductName} with Id {product1.ProductId}");
                            Console.WriteLine($"Added Product 2: {product2.ProductName} with Id {product2.ProductId}");
                            Console.WriteLine($"Added Product 3: {product3.ProductName} with Id {product3.ProductId}");
                        }
                        continue;
                    }
                case 5:
                    {
                        ProductManager.ShowAllProduct();
                        continue;
                    }


            }
            
            Console.WriteLine("Input Invalid, Input again or Press 0 to return Login Menu");
            continue;
        }
    }
}
