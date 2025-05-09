using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CapaDatos;
using CapaNegocio;

namespace Semana07
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window    
    {
        private BusinessCustomer customerService = new BusinessCustomer();
        public MainWindow()
        {
            InitializeComponent();
            CargarClientes();
        }
        private void CargarClientes()
        {
            var lista = customerService.ObtenerTodos();
            dgClientes.ItemsSource = lista; // dgClientes es tu DataGrid en XAML
        }
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();
            var resultado = customerService.BuscarClientesPorNombre(nombre);
            dgClientes.ItemsSource = resultado;
        }
        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            Customer c = new Customer
            {
                Name = txtName.Text,
                Address = txtAddress.Text,
                Phone = txtPhone.Text,
            };

            try
            {
                customerService.RegistrarCustomer(c);
                MessageBox.Show("Cliente registrado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message);
            }
        }
    }
}