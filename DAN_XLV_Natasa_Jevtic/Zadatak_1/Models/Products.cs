using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class Products : Logger
    {
        public delegate void Notification(bool canStore, int capacity);
        public event Notification OnNotification;
        /// <summary>
        /// This method raises OnNotification event.
        /// </summary>
        /// <param name="canStore">Indicator can product be stored.</param>
        /// <param name="capacity">Capacity of warehouse.</param>
        public void Notify(bool canStore, int capacity)
        {
            OnNotification?.Invoke(canStore, capacity);
        }
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
                    LogAction("Product with ID " + productToDelete.ProductID + " is deleted. Key: " + productToDelete.ProductKey +
                        " Name: " + productToDelete.ProductName + " Price: " + productToDelete.Price + " Quantity: " + productToDelete.Quantity);
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
                    LogAction("Product with ID " + productToAdd.ProductID + " is added. Key: " + productToAdd.ProductKey +
                        " Name: " + productToAdd.ProductName + " Price: " + productToAdd.Price + " Quantity: " + productToAdd.Quantity);
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
                    LogAction("Product with ID " + productToEdit.ProductID + " is updated. Key: " + productToEdit.ProductKey +
                        " Name: " + productToEdit.ProductName + " Price: " + productToEdit.Price + " Quantity: " + productToEdit.Quantity);
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
                        Notify(true, remainingCapacity);
                    }
                    //if capacity higher than 100, then do not store product
                    else
                    {
                        remainingCapacity = 100 - capacityOfWarehouse;
                        Notify(false, remainingCapacity);
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