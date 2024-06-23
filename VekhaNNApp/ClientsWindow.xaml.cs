using System.Linq;
using System.Windows;
using VekhaNNApp.Model;

namespace VekhaNNApp
{
    /// <summary>
    /// Логика взаимодействия для ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        private VekhaNNEntities _context;

        public ClientsWindow()
        {
            InitializeComponent();
            _context = new VekhaNNEntities();
            LoadClients();
        }

        private void LoadClients()
        {
            ClientsDataGrid.ItemsSource = _context.Clients.ToList();
            ClearTextBox();
        }

        private void ClearTextBox()
        {
            ClientNameTextBox.Text = string.Empty;
            ContactInfoTextBox.Text = string.Empty;
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new Clients
            {
                ClientName = ClientNameTextBox.Text,
                ContactInfo = ContactInfoTextBox.Text
            };
            _context.Clients.Add(client);
            _context.SaveChanges();
            LoadClients();
        }

        private void UpdateClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem is Clients selectedClient)
            {
                selectedClient.ClientName = ClientNameTextBox.Text;
                selectedClient.ContactInfo = ContactInfoTextBox.Text;
                _context.SaveChanges();
                LoadClients();
            }
        }

        private void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem is Clients selectedClient)
            {
                _context.Clients.Remove(selectedClient);
                _context.SaveChanges();
                LoadClients();
            }
        }

        private void ClientsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var _selectedItem = (Clients) ClientsDataGrid.SelectedItem;
            if(_selectedItem != null )
            {
                ClientNameTextBox.Text = _selectedItem.ClientName;
                ContactInfoTextBox.Text = _selectedItem.ContactInfo;
            }
        }
    }
}
