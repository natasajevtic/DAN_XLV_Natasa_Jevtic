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
        }

        public bool CanStoreProductExecute()
        {
            return true;
        }

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
    }
}