using System.Linq;
using System.Windows;
using VekhaNNApp.Model;

namespace VekhaNNApp
{
    /// <summary>
    /// Логика взаимодействия для WarehouseWindow.xaml
    /// </summary>
    public partial class WarehouseWindow : Window
    {
        private VekhaNNEntities _context;

        public WarehouseWindow()
        {
            InitializeComponent();
            _context = new VekhaNNEntities();
            LoadMaterials();
        }

        private void LoadMaterials()
        {
            MaterialsDataGrid.ItemsSource = _context.RawMaterials.ToList();
            ClearTextBox();
        }

        private void AddMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            var material = new RawMaterials
            {
                MaterialName = MaterialNameTextBox.Text,
                Quantity = int.Parse(QuantityTextBox.Text)
            };
            _context.RawMaterials.Add(material);
            _context.SaveChanges();
            LoadMaterials();
        }

        private void UpdateMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            if (MaterialsDataGrid.SelectedItem is RawMaterials selectedMaterial)
            {
                selectedMaterial.MaterialName = MaterialNameTextBox.Text;
                selectedMaterial.Quantity = int.Parse(QuantityTextBox.Text);
                _context.SaveChanges();
                LoadMaterials();
            }
        }

        private void DeleteMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            if (MaterialsDataGrid.SelectedItem is RawMaterials selectedMaterial)
            {
                _context.RawMaterials.Remove(selectedMaterial);
                _context.SaveChanges();
                LoadMaterials();
            }
        }

        private void ClearTextBox()
        {
            MaterialNameTextBox.Text = string.Empty;
            QuantityTextBox.Text = string.Empty;
        }

        private void MaterialsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var _selectedItem = MaterialsDataGrid.SelectedItem as RawMaterials;
            if (_selectedItem != null)
            {
                MaterialNameTextBox.Text = _selectedItem.MaterialName;
                QuantityTextBox.Text += _selectedItem.Quantity.ToString();
            }
        }
    }
}
