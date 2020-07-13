using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class Products 
    { 
        /// <summary>
        /// This method creates a list of products.
        /// </summary>
        /// <returns>List of products.</returns>
        public List<vwProduct> ViewAllProduct()
        {
            try
            {
                using (WarehouseEntities context = new WarehouseEntities())
                {
                    return context.vwProducts.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method deletes product from DbSet and saves changes to database.
        /// </summary>
        /// <param name="productID">The ID of the product to be deleted.</param>
        public void DeleteProduct(int productID)
        {
            try
            {
                using (WarehouseEntities context = new WarehouseEntities())
                {
                    tblProduct productToDelete = context.tblProducts.Where(x => x.ProductID == productID).FirstOrDefault();
                    context.tblProducts.Remove(productToDelete);
                    context.SaveChanges();                   
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// This method adds product to DbSet and saves changes to database.
        /// </summary>
        /// <param name="productToAdd">The product to be added.</param>
        public void AddProduct(vwProduct productToAdd)
        {
            try
            {
                using (WarehouseEntities context = new WarehouseEntities())
                {
                    tblProduct product = new tblProduct
                    {
                        ProductName = productToAdd.ProductName,
                        ProductKey = productToAdd.ProductKey,
                        Quantity = productToAdd.Quantity,
                        Price = productToAdd.Price,
                        Stored = "no"
                    };
                    context.tblProducts.Add(product);
                    context.SaveChanges();
                    productToAdd.ProductID = product.ProductID;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// This method edits data of product and saves changes to database.
        /// </summary>
        /// <param name="product">The product to be edited.</param>
        /// <returns>Edited product.</returns>
        public vwProduct EditProduct(vwProduct product)
        {
            try
            {
                using (WarehouseEntities context = new WarehouseEntities())
                {
                    tblProduct productToEdit = context.tblProducts.Where(x => x.ProductID == product.ProductID).FirstOrDefault();
                    productToEdit.ProductName = product.ProductName;
                    productToEdit.ProductKey = product.ProductKey;
                    productToEdit.Quantity = product.Quantity;
                    productToEdit.Price = product.Price;
                    context.SaveChanges();
                    return product;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method creates a list of recorded products.
        /// </summary>
        /// <returns>List of recorded products.</returns>
        public List<vwProduct> ViewAllRecordedProducts()
        {
            try
            {
                using (WarehouseEntities context = new WarehouseEntities())
                {
                    //returning list of products which are not stored yet
                    return context.vwProducts.Where(x => x.Stored == "no").ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method stores product to the warehouse if there is available capacity.
        /// </summary>
        /// <param name="productToStore">Product to be stored.</param>
        public void StoreProduct(vwProduct productToStore)
        {
            try
            {
                using (WarehouseEntities context = new WarehouseEntities())
                {
                    int capacityOfWarehouse = 0;
                    int remainingCapacity = 0;
                    //creating a list of stored products
                    List<tblProduct> storedProducts = context.tblProducts.Where(x => x.Stored == "yes").ToList();
                    //checking if the list of stored products is not empty
                    if (storedProducts.Count() > 0)
                    {
                        //calculating current capacity of warehouse
                        capacityOfWarehouse = storedProducts.Sum(x => x.Quantity);
                    }
                    //finding the recorded product to be stored based on the product ID
                    tblProduct product = context.tblProducts.Where(x => x.ProductID == productToStore.ProductID).FirstOrDefault();
                    //calculating current capacity of warehouse with the product to be stored
                    int capacityOfWarehouseWithNewProduct = capacityOfWarehouse + product.Quantity;
                    //if capacity less than 100, then store product
                    if (capacityOfWarehouseWithNewProduct <= 100)
                    {
                        product.Stored = "yes";
                        context.SaveChanges();
                        remainingCapacity = 100 - capacityOfWarehouseWithNewProduct;                        
                    }
                    //if capacity higher than 100, then do not store product
                    else
                    {
                        remainingCapacity = 100 - capacityOfWarehouse;                        
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}