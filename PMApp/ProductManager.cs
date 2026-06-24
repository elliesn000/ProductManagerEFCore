using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Classes;
namespace DbClasses;

public class ProductManager
{

    //============================================================================================================ CREATE
    public static void CreateProduct()
    {

        ShowAllProduct();
        Console.WriteLine("\n------ Creating a product ------");
        string inputName = MProgram.InputParse.GetString("Input Name or Press 0 retun Main Menu");

        if (inputName == "0") 
            return;


        int inputPrice = MProgram.InputParse.GetInt("Input Price or Press 0 retun Main Menu ");
        
        if (inputPrice == 0)
            return;


        int inputQtt = MProgram.InputParse.GetInt("Input Quantity or Press 0 retun Main Menu");

        if (inputQtt == 0)
            return;
        
        using (var context = new AppContext())
        {
            var product = new Product { ProductName = inputName, ProductPrice = inputPrice, ProductQtt = inputQtt };
            context.Products.Add(product);
            context.SaveChanges();
            Console.WriteLine($"Add Product Done: {product.ProductId} - {product.ProductName} - {product.ProductPrice} - {product.ProductQtt}");
            ShowAllProduct();
        }
    }


    //============================================================================================================== UPDATE QTT
    public static void UpdateQttProduct()
    {

        ShowAllProduct();
        Console.WriteLine("\n------ Updating a product ------");
        using (var context = new AppContext())
        {
            int inputId = MProgram.InputParse.GetInt("Input Id to update");
            var productToUpdate = context.Products.Find(inputId);

            if (productToUpdate != null)
            {
                Console.WriteLine($"Original Quantity of '{productToUpdate.ProductName}': {productToUpdate.ProductQtt}");
                int inputQtt = MProgram.InputParse.GetInt("Input Qtt to change");
                productToUpdate.ProductQtt = inputQtt;

                context.SaveChanges();

                Console.WriteLine($"Updated Quantity to: {productToUpdate.ProductQtt}");
            }
            else
            {
                Console.WriteLine($"Product with Id {inputId} not found for update.");
            }
            ShowAllProduct();
        }
    }


    //============================================================================================================== DELETE
    public static void DeleteProduct()
    {

        ShowAllProduct();
        Console.WriteLine("\n------ Deleting a product ------");
        using (var context = new AppContext())
        {
            int inputId = MProgram.InputParse.GetInt("Input Id to delete");

            var productToDelete = context.Products.Find(inputId);

            if (productToDelete != null)
            {
                Console.WriteLine($"Deleting product: '{productToDelete.ProductName}'");
                context.Products.Remove(productToDelete);

                context.SaveChanges();

                Console.WriteLine("Product deleted.");
                // --- Verify DELETE
                Console.WriteLine("\n------ Verifying deletion ------");

                var remainingProducts = context.Products.ToList();
                Console.WriteLine("Remaining products:");
                if (remainingProducts.Any())
                {
                    foreach (var p in remainingProducts)
                    {
                        Console.WriteLine($"- {p.ProductId}: {p.ProductName} - {p.ProductPrice:C} - {p.ProductQtt}");
                    }
                }
                else
                {
                    Console.WriteLine("No products left in the database.");
                }

                Console.WriteLine("\nDone.");
            }
            else
            {
                Console.WriteLine($"Product with Id {inputId} not found for deletion.");
            }
        }
    }

    //================================================================================================================= CREATE LIST PRODUCT
    public static List<Product> CreatListProduct(string inputNameProduct)
    {
        using (var context = new AppContext())
        {
            var allProduct = context.Products.ToList();
            if (inputNameProduct != "0")
            {
                var samenameProduct = context.Products
                    .Where(p => p.ProductName.Contains(inputNameProduct))
                    .OrderBy(p => p.ProductId)
                    .ToList();
                if (samenameProduct.Count != 0)
                {
                    return samenameProduct;
                }
                else
                {
                    Console.WriteLine("Don't have this product, you can continue with all our product");
                    return allProduct;
                }
            }
            return allProduct;
        }
    }



    //================================================================================================================= READ
    public static void ShowAllProduct()
    {

        using (var context = new AppContext())
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("----------All products----------");
            var allProducts = context.Products.ToList();
            foreach (var p in allProducts)
            {
                Console.WriteLine($"- {p.ProductId}: {p.ProductName} - {p.ProductPrice:C} - {p.ProductQtt}");
            }
        }
    }



    //=============================================================================================================== FIND
    public static void FindProductWithId(int inputId)
    {
        using (var context = new AppContext())
        {
            int productIdToFind = inputId;
            Console.WriteLine($"\nFinding product with Id = {productIdToFind}:");
            var productById = context.Products.Find(productIdToFind);
            if (productById != null)
            {
                Console.WriteLine($"- Found: {productById.ProductName}");
            }
            else
            {
                Console.WriteLine($"- Product with Id {productIdToFind} not found.");
            }
        }
    }
    public static void FindProductWithName(string inputName)
    {
        while(true)
        {
            using (var context = new AppContext())
            {
                var samenameProduct = context.Products
                                             .Where(p => p.ProductName.Contains(inputName))
                                             .OrderBy(p => p.ProductId)
                                             .ToList();
                if (samenameProduct.Count != 0)
                {
                    foreach (var p in samenameProduct)
                    {
                        Console.WriteLine($"- {p.ProductId} - {p.ProductName} - {p.ProductPrice} - {p.ProductQtt}");
                    }
                }
                else //notfound
                {
                    Console.WriteLine($"We don't have product with name {inputName}");
                    ProductManager.ShowAllProduct();
                    break;
                }
            }
        }
    }
}


