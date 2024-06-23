using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VekhaNNApp.Model;

namespace VekhaNNApp
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        private VekhaNNEntities _context;

        public OrdersWindow()
        {
            InitializeComponent();
            _context = new VekhaNNEntities();
            LoadClients();
            LoadOrders();
        }

        private void LoadClients()
        {
            ClientComboBox.ItemsSource = _context.Clients.ToList();
        }

        private void LoadOrders()
        {
            OrdersDataGrid.ItemsSource = _context.Orders.Include("Clients").ToList();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientComboBox.SelectedItem is Clients selectedClient && StatusComboBox.SelectedItem is ComboBoxItem selectedStatus)
            {
                var order = new Orders
                {
                    ClientID = selectedClient.ClientID,
                    OrderDate = OrderDatePicker.SelectedDate.Value,
                    Status = selectedStatus.Content.ToString()
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                LoadOrders();
            }
        }

        private void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem is Orders selectedOrder && ClientComboBox.SelectedItem is Clients selectedClient && StatusComboBox.SelectedItem is ComboBoxItem selectedStatus)
            {
                selectedOrder.ClientID = selectedClient.ClientID;
                selectedOrder.OrderDate = OrderDatePicker.SelectedDate.Value;
                selectedOrder.Status = selectedStatus.Content.ToString();
                _context.SaveChanges();
                LoadOrders();
            }
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem is Orders selectedOrder)
            {
                _context.Orders.Remove(selectedOrder);
                _context.SaveChanges();
                LoadOrders();
            }
        }

        private void OrdersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem is Orders selectedOrder)
            {
                Console.WriteLine($"Выбранный статус: {selectedOrder.Status}");  // Для отладки
                ClientComboBox.SelectedItem = _context.Clients.FirstOrDefault(c => c.ClientID == selectedOrder.ClientID);
                OrderDatePicker.SelectedDate = selectedOrder.OrderDate;

                foreach (var item in StatusComboBox.Items.Cast<ComboBoxItem>()) // Для отладки
                {
                    Console.WriteLine($"Статус в ComboBox: '{item.Content}'");
                }

                StatusComboBox.SelectedIndex = StatusComboBox.Items.Cast<ComboBoxItem>()
                    .ToList()
                    .FindIndex(item => item.Content.ToString() == selectedOrder.Status);
            }
        }

    }
}
