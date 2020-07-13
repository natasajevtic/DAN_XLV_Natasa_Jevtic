using System.Windows;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for WarehouseClerkView.xaml
    /// </summary>
    public partial class WarehouseClerkView : Window
    {
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public WarehouseClerkView()
        {
            InitializeComponent();
            this.DataContext = new WarehouseClerkViewModel(this);
        }
    }
}
