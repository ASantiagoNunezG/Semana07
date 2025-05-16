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
            string nombre = txtName.Text.Trim();

            // Validar nombre duplicado
            var existente = customerService.ObtenerTodos().FirstOrDefault(x => x.Name.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (existente != null)
            {
                MessageBox.Show("Ya existe un cliente con ese nombre.");
                return;
            }

            Customer c = new Customer
            {
                Name = nombre,
                Address = txtAddress.Text,
                Phone = txtPhone.Text,
            };

            try
            {
                customerService.RegistrarCustomer(c);
                MessageBox.Show("Cliente registrado correctamente");
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message);
            }
        }


        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var customer = ((Button)sender).Tag as Customer;
            if (customer != null)
            {
                txtId.Text = customer.CustomerId.ToString();
                txtName.Text = customer.Name;
                txtAddress.Text = customer.Address;
                txtPhone.Text = customer.Phone;

                // Oculta el botón Registrar y muestra Actualizar
                btnActualizar.Visibility = Visibility.Visible;
            }
        }



        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var customer = (sender as Button).Tag as Customer;

            var result = MessageBox.Show($"¿Deseas eliminar a {customer.Name}?", "Confirmar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                customerService.EliminarCliente(customer.CustomerId);
                CargarClientes(); 
            }
        }
        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string nombre = txtName.Text.Trim();

            // Verifica si otro cliente ya tiene ese nombre
            var existeOtro = customerService.ObtenerTodos().FirstOrDefault(x =>
                x.Name.Equals(nombre, StringComparison.OrdinalIgnoreCase) && x.CustomerId != id);

            if (existeOtro != null)
            {
                MessageBox.Show("Otro cliente ya tiene ese nombre.");
                return;
            }

            Customer c = new Customer()
            {
                CustomerId = id,
                Name = nombre,
                Address = txtAddress.Text,
                Phone = txtPhone.Text
            };

            customerService.ActualizarCliente(c);
            CargarClientes();

            // Limpia el formulario y oculta el botón Actualizar
            txtId.Text = "";
            btnActualizar.Visibility = Visibility.Collapsed;
            MessageBox.Show("Cliente actualizado correctamente");
        }





    }
}