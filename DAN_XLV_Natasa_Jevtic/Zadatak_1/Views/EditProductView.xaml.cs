using System.Windows;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for EditProductView.xaml
    /// </summary>
    public partial class EditProductView : Window
    {
        /// <summary>
        /// Constructor with one parameter.
        /// </summary>
        /// <param name="productToEdit">The product to be edited.</param>
        public EditProductView(vwProduct productToEdit)
        {
            InitializeComponent();
            this.DataContext = new EditProductViewModel(this, productToEdit);
        }
    }
}