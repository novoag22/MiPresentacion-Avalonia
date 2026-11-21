using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace MiPresentacion
{
    public partial class MainWindow : Window
    {
        // Variable para alternar entre los dos mensajes del reto adicional
        private bool usarSegundoSaludo = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnPresentarme_Click(object? sender, RoutedEventArgs e)
        {
            // 1. Obtener los datos de los TextBox
            string nombre = TxtNombre.Text ?? "";
            string apellido = TxtApellido.Text ?? "";
            string edadTexto = TxtEdad.Text ?? "";

            // 2. Comprobar que nombre y apellido no estén vacíos ni tengan solo espacios
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MostrarAdvertencia("Por favor, ingresa tu nombre.");
                TxtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(apellido))
            {
                MostrarAdvertencia("Por favor, ingresa tu apellido.");
                TxtApellido.Focus();
                return;
            }

            // 3. Verificar que la edad sea un número entero positivo usando int.TryParse()
            if (!int.TryParse(edadTexto.Trim(), out int edad))
            {
                MostrarAdvertencia("Por favor, ingresa una edad numérica válida.");
                TxtEdad.Focus();
                return;
            }

            if (edad <= 0)
            {
                MostrarAdvertencia("La edad debe ser un número entero mayor a cero.");
                TxtEdad.Focus();
                return;
            }

            // 4. Mostrar mensaje personalizado (alternando según el botón Cambiar saludo)
            TxtResultado.Foreground = Brushes.DarkGreen;

            if (!usarSegundoSaludo)
            {
                TxtResultado.Text = $"¡Hola! Mi nombre es {nombre.Trim()} {apellido.Trim()},\n" +
                                    $"tengo {edad} años y estoy aprendiendo\n" +
                                    $"a desarrollar interfaces gráficas con C#.";
            }
            else
            {
                TxtResultado.Text = $"¡Mucho gusto! Soy {nombre.Trim()} {apellido.Trim()} ({edad} años).\n" +
                                    $"Actualmente estudio programación en .NET\n" +
                                    $"y construyo aplicaciones de escritorio con Avalonia UI.";
            }
        }

        // Reto adicional: alternar entre dos mensajes sin cerrar la app
        private void BtnCambiarSaludo_Click(object? sender, RoutedEventArgs e)
        {
            usarSegundoSaludo = !usarSegundoSaludo;

            // Si ya hay datos válidos escritos, actualiza el saludo inmediatamente
            if (!string.IsNullOrWhiteSpace(TxtNombre.Text) &&
                !string.IsNullOrWhiteSpace(TxtApellido.Text) &&
                int.TryParse((TxtEdad.Text ?? "").Trim(), out int edad) && edad > 0)
            {
                BtnPresentarme_Click(sender, e);
            }
            else
            {
                TxtResultado.Foreground = Brushes.DarkBlue;
                TxtResultado.Text = usarSegundoSaludo
                    ? "Modo de saludo 2 activado. Llena tus datos y presiona Presentarme."
                    : "Modo de saludo 1 activado. Llena tus datos y presiona Presentarme.";
            }
        }

        // Botón Limpiar
        private void BtnLimpiar_Click(object? sender, RoutedEventArgs e)
        {
            // 1. Vaciar los tres campos
            TxtNombre.Text = string.Empty;
            TxtApellido.Text = string.Empty;
            TxtEdad.Text = string.Empty;

            // 2. Restaurar el mensaje inicial
            TxtResultado.Foreground = Brushes.Black;
            TxtResultado.Text = "Completa tus datos y presiona Presentarme.";

            // 3. Colocar el cursor en la caja del nombre mediante Focus()
            TxtNombre.Focus();
        }

        private void MostrarAdvertencia(string mensaje)
        {
            TxtResultado.Foreground = Brushes.Red;
            TxtResultado.Text = "Advertencia: " + mensaje;
        }
    }
}