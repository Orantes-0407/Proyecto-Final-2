using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tutor
{
    public partial class MenuInicio : Form
    {
        private int progreso = 0;
        public MenuInicio()
        {
            InitializeComponent();
        }

        private void MenuInicio_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
        }

        private void Iniciar_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progreso = 0;
            progressBar1.Value = 0;
            timer2.Start();
            Iniciar.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (progreso < 100)
            {
                progreso += 5;
                progressBar1.Value = progreso;
            }
            else
            {
                timer2.Stop();
                MessageBox.Show("¡Carga completada! Ahora se conectará a la base de datos.", "Progreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConectarYMostrarPrincipal();
            }
        }

        private void ConectarYMostrarPrincipal()
        {
            string connectionString = "Server=MELANY\\SQLEXPRESS;Database=Tutor_DB;Trusted_Connection=True;";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    MessageBox.Show("Conexión exitosa.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mostrar el formulario principal
                    MenuPrincipal form = new MenuPrincipal();
                    form.Show();

                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Iniciar.Enabled = true; // Asegúrate de que el botón se llama así
                    progressBar1.Value = 0;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}