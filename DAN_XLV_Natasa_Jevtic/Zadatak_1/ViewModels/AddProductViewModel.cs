using System;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Validations;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class AddProductViewModel
    {
        AddProductView addProductView;
        Products products = new Products();
        Validation validation = new Validation();

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
            }
        }

        private ICommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private ICommand cancel;

        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        public AddProductViewModel(AddProductView addProductView)
        {
            this.addProductView = addProductView;
            Product = new vwProduct();
        }
        /// <summary>
        /// This method invokes method for adding product.
        /// </summary>
        public void SaveExecute()
        {
            try
            {
                products.AddProduct(Product);
                addProductView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// This method checks if user input data valid and can saving be executed.
        /// </summary>
        /// <returns>True if data valid, false if not.</returns>
        public bool CanSaveExecute()
        {
            try
            {
                //checking if user input data valid
                if (!String.IsNullOrEmpty(Product.ProductName) && !String.IsNullOrEmpty(Product.ProductKey) &&
                    Int32.TryParse(Product.Quantity.ToString(), out int quantity) && quantity > 0 && quantity <= 100 && Decimal.TryParse(Product.Price.ToString(), out decimal price)
                    && price > 0 && validation.UniqueProductKey(Product.ProductKey) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method closing window for adding product.
        /// </summary>
        public void CancelExecute()
        {
            try
            {
                addProductView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanCancelExecute()
        {
            return true;
        }
    }
}