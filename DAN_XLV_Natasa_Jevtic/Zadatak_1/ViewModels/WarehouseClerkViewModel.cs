using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class WarehouseClerkViewModel : BaseViewModel
    {
        WarehouseClerkView warehouseClerkView;
        Products products = new Products();

        private vwProduct product;

        public vwProduct Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }

        private List<vwProduct> productList;

        public List<vwProduct> ProductList
        {
            get
            {
                return productList;
            }
            set
            {
                productList = value;
                OnPropertyChanged("ProductList");
            }
        }

        private ICommand storeProduct;

        public ICommand StoreProduct
        {
            get
            {
                if (storeProduct == null)
                {
                    storeProduct = new RelayCommand(param => StoreProductExecute(), param => CanStoreProductExecute());
                }
                return storeProduct;
            }
        }

        public WarehouseClerkViewModel(WarehouseClerkView warehouseClerkView)
        {
            this.warehouseClerkView = warehouseClerkView;
            ProductList = products.ViewAllRecordedProducts();
            //subscribe on event
            products.OnNotification += ShowMessage;
        }

        public bool CanStoreProductExecute()
        {
            return true;
        }
        /// <summary>
        /// This method invokes method for storing product.
        /// </summary>
        public void StoreProductExecute()
        {
            try
            {
                products.StoreProduct(Product);
                ProductList = products.ViewAllRecordedProducts();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// This is event handler for OnNotification event. Shows message to user.
        /// </summary>
        /// <param name="canStore">Indicator can product be stored.</param>
        /// <param name="capacity">Capacity of warehouse.</param>
        public void ShowMessage(bool canStore, int capacity)
        {
            if (canStore == true)
            {
                MessageBox.Show("Products have been successfully stored. The remaining capacity of the warehouse is " + capacity + " products.", "Notification");
            }
            else
            {
                MessageBox.Show("Products cannot be stored because there is not enough capacity in the warehouse. " + capacity + " products can be stored.", "Notification");
            }
        }
    }
}