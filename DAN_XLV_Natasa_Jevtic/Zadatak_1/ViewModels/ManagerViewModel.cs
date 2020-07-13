using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class ManagerViewModel : BaseViewModel
    {
        ManagerView managerView;
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

        private ICommand addProduct;

        public ICommand AddProduct
        {
            get
            {
                if (addProduct == null)
                {
                    addProduct = new RelayCommand(param => AddProductExecute(), param => CanAddProductExecute());
                }
                return addProduct;
            }
        }

        private ICommand deleteProduct;

        public ICommand DeleteProduct
        {
            get
            {
                if (deleteProduct == null)
                {
                    deleteProduct = new RelayCommand(param => DeleteProductExecute(), param => CanDeleteProductExecute());
                }
                return deleteProduct;
            }
        }

        private ICommand editProduct;

        public ICommand EditProduct
        {
            get
            {
                if (editProduct == null)
                {
                    editProduct = new RelayCommand(param => EditProductExecute(), param => CanEditProductExecute());
                }
                return editProduct;
            }
        }

        public ManagerViewModel(ManagerView managerView)
        {
            this.managerView = managerView;            
            ProductList = products.ViewAllProduct();
        }
        /// <summary>
        /// This method ensures that stored products cannot be deleted.
        /// </summary>
        /// <returns></returns>
        public bool CanDeleteProductExecute()
        {
            if (Product != null)
            {
                if (Product.Stored == "yes")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This method invokes method for deleting product.
        /// </summary>
        public void DeleteProductExecute()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure that you want to delete the product?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    if (Product != null)
                    {
                        //invokes method to delete product
                        products.DeleteProduct(Product.ProductID);
                        //invokes method to update list of products
                        ProductList = products.ViewAllProduct();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public bool CanAddProductExecute()
        {
            return true;
        }
        /// <summary>
        /// This method invokes a method for showing a window for adding product.
        /// </summary>
        public void AddProductExecute()
        {
            try
            {
                AddProductView addProductView = new AddProductView();
                addProductView.ShowDialog();
                ProductList = products.ViewAllProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanEditProductExecute()
        {
            return true;
        }
        /// <summary>
        /// This method invokes a method for showing a window for editing product.
        /// </summary>
        public void EditProductExecute()
        {
            try
            {
                EditProductView editProductView = new EditProductView(Product);
                editProductView.ShowDialog();
                ProductList = products.ViewAllProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}