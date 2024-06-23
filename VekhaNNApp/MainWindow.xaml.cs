using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VekhaNNApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ManageClientsButton_Click(object sender, RoutedEventArgs e)
        {
            var clientsWindow = new ClientsWindow();
            clientsWindow.ShowDialog();
        }

        private void ManageProductsButton_Click(object sender, RoutedEventArgs e)
        {
            var productsWindow = new ProductsWindow();
            productsWindow.ShowDialog();
        }

        private void ManageWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            var warehouseWindow = new WarehouseWindow();
            warehouseWindow.ShowDialog();
        }

        private void ManageOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            var ordersWindow = new OrdersWindow();
            ordersWindow.ShowDialog();
        }

        private void ManageProductionTasksButton_Click(object sender, RoutedEventArgs e)
        {
            var productionTasksWindow = new ProductionTasksWindow();
            productionTasksWindow.ShowDialog();
        }
    }
}
