using System.Windows;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for AddProductView.xaml
    /// </summary>
    public partial class AddProductView : Window
    {
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public AddProductView()
        {
            InitializeComponent();
            this.DataContext = new AddProductViewModel(this);
        }
    }
}