using System.Linq;
using System.Windows;
using VekhaNNApp.Model;

namespace VekhaNNApp
{
    /// <summary>
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        private VekhaNNEntities _context;

        public ProductsWindow()
        {
            InitializeComponent();
            _context = new VekhaNNEntities();
            LoadProducts();
        }

        private void LoadProducts()
        {
            ProductsDataGrid.ItemsSource = _context.Products.ToList();
            ClearTextBox();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var product = new Products
            {
                ProductName = ProductNameTextBox.Text,
                Description = DescriptionTextBox.Text,
                Quantity = int.Parse(QuantityTextBox.Text)
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            LoadProducts();
        }

        private void ClearTextBox()
        {
            ProductNameTextBox.Text = string.Empty;
            DescriptionTextBox.Text = string.Empty;
            QuantityTextBox.Text = string.Empty;
        }

        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Products selectedProduct)
            {
                selectedProduct.ProductName = ProductNameTextBox.Text;
                selectedProduct.Description = DescriptionTextBox.Text;
                selectedProduct.Quantity = int.Parse(QuantityTextBox.Text);
                _context.SaveChanges();
                LoadProducts();
            }
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Products selectedProduct)
            {
                _context.Products.Remove(selectedProduct);
                _context.SaveChanges();
                LoadProducts();
            }
        }

        private void ProductsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var _selectedItem = (Products)ProductsDataGrid.SelectedItem;
            if(_selectedItem != null )
            {
                ProductNameTextBox.Text = _selectedItem.ProductName;
                DescriptionTextBox.Text= _selectedItem.Description;
                QuantityTextBox.Text = _selectedItem.Quantity.ToString();
            }
        }
    }
}
