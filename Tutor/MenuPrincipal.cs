using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Tutor
{
    public partial class MenuPrincipal : Form
    {


        public MenuPrincipal()
        {
            InitializeComponent();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            string idTexto = Textbox1.Text.Trim();

            if (string.IsNullOrEmpty(idTexto))
            {
                MessageBox.Show("Por favor, ingresa el ID del estudiante.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si el ID es numérico, valida antes de consultar
            int idIngresado;
            if (!int.TryParse(idTexto, out idIngresado))
            {
                MessageBox.Show("El ID debe ser un número.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Server=MELANY\\SQLEXPRESS;Database=Tutor_DB;Trusted_Connection=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    // Consulta para buscar el EstudianteId en la tabla Sesion
                    string query = "SELECT COUNT(*) FROM Sesion WHERE EstudianteId = @EstudianteId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EstudianteId", idIngresado);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // Actualiza la hora de conexión antes de abrir el siguiente formulario
                            ActualizarHoraConexionSesion(idIngresado);

                            MessageBox.Show("Sesión iniciada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Form2 form2 = new Form2();
                            form2.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("ID de estudiante no registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Método para actualizar la hora de conexión en la tabla Sesion
        private void ActualizarHoraConexionSesion(int estudianteId)
        {
            string connectionString = "Server=MELANY\\SQLEXPRESS;Database=Tutor_DB;Trusted_Connection=True;";
            string query = "UPDATE Sesion SET FechaHora = GETDATE() WHERE EstudianteId = @EstudianteId";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EstudianteId", estudianteId);
                    conn.Open();
                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0)
                    {
                        // Opcional: puedes mostrar un mensaje aquí si lo deseas
                        // MessageBox.Show("¡Hora de conexión actualizada!", "Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el usuario para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la hora de conexión:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }
    }
}
