using System;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Validations;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class EditProductViewModel : BaseViewModel
    {
        EditProductView editProductView;
        Products products = new Products();
        Validation validation = new Validation();

        public vwProduct ProductBeforeEdit { get; set; }

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

        public EditProductViewModel(EditProductView editProductView, vwProduct product)
        {
            this.editProductView = editProductView;
            Product = product;
            ProductBeforeEdit = new vwProduct
            {
                ProductName = product.ProductName,
                ProductKey = product.ProductKey,
                Quantity = product.Quantity,
                Price = product.Price
            };
        }
        /// <summary>
        /// This method invokes a method for editing product.
        /// </summary>
        public void SaveExecute()
        {
            try
            {
                products.EditProduct(Product);
                editProductView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// This method checks if data of product changes and if valid.
        /// </summary>
        /// <returns>True if valid, false if not.</returns>
        public bool CanSaveExecute()
        {
            try
            {
                //checks if user input data valid and if data changed
                if ((Product.ProductName != ProductBeforeEdit.ProductName || Product.ProductKey != ProductBeforeEdit.ProductKey || Product.Quantity != ProductBeforeEdit.Quantity
                    || Product.Price != ProductBeforeEdit.Price) && !String.IsNullOrEmpty(Product.ProductName) && !String.IsNullOrEmpty(Product.ProductKey) &&
                    Int32.TryParse(Product.Quantity.ToString(), out int quantity) && quantity >= 0 && quantity <= 100 && Decimal.TryParse(Product.Price.ToString(), out decimal price)
                    && price > 0)
                {
                    //only if product key changed, check if unique
                    if (Product.ProductKey != ProductBeforeEdit.ProductKey)
                    {
                        if (validation.UniqueProductKey(Product.ProductKey) == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

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
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method invokes a method for closing a window for editing product.
        /// </summary>
        public void CancelExecute()
        {
            try
            {
                editProductView.Close();
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