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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string estudianteIdTexto = textBox2.Text.Trim();
            string nombre = textBox1.Text.Trim();
            string grado = textBox3.Text.Trim();
            string fechaHoraTexto = textBox4.Text.Trim();

            if (string.IsNullOrEmpty(estudianteIdTexto) || string.IsNullOrEmpty(nombre) ||
                string.IsNullOrEmpty(grado) || string.IsNullOrEmpty(fechaHoraTexto))
            {
                MessageBox.Show("Por favor, completa todos los campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int estudianteId;
            DateTime fechaHora;
            if (!int.TryParse(estudianteIdTexto, out estudianteId))
            {
                MessageBox.Show("El Id del estudiante debe ser un número.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!DateTime.TryParse(fechaHoraTexto, out fechaHora))
            {
                MessageBox.Show("La fecha y hora no tiene un formato válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Server=MELANY\\SQLEXPRESS;Database=Tutor_DB;Trusted_Connection=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // 1. Verificar si el estudiante ya existe en la tabla Estudiantes
                    string checkQuery = "SELECT COUNT(*) FROM Estudiantes WHERE Id = @Id";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@Id", estudianteId);
                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists == 0)
                        {
                            // Insertar nuevo estudiante con Grado y UltimaSesion
                            string insertEstudiante = "INSERT INTO Estudiantes (Id, Nombre, Grado, UltimaSesion) VALUES (@Id, @Nombre, @Grado, @UltimaSesion)";
                            using (SqlCommand insertCmd = new SqlCommand(insertEstudiante, con))
                            {
                                insertCmd.Parameters.AddWithValue("@Id", estudianteId);
                                insertCmd.Parameters.AddWithValue("@Nombre", nombre);
                                insertCmd.Parameters.AddWithValue("@Grado", grado);
                                insertCmd.Parameters.AddWithValue("@UltimaSesion", fechaHora);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Actualizar Grado y UltimaSesion del estudiante existente
                            string updateEstudiante = "UPDATE Estudiantes SET Grado = @Grado, UltimaSesion = @UltimaSesion WHERE Id = @Id";
                            using (SqlCommand updateCmd = new SqlCommand(updateEstudiante, con))
                            {
                                updateCmd.Parameters.AddWithValue("@Id", estudianteId);
                                updateCmd.Parameters.AddWithValue("@Grado", grado);
                                updateCmd.Parameters.AddWithValue("@UltimaSesion", fechaHora);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // 2. Insertar en IniciosSesion solo los campos válidos
                    string query = "INSERT INTO IniciosSesion (EstudianteId, NombreEstudiante, FechaHora) VALUES (@EstudianteId, @NombreEstudiante, @FechaHora)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EstudianteId", estudianteId);
                        cmd.Parameters.AddWithValue("@NombreEstudiante", nombre);
                        cmd.Parameters.AddWithValue("@FechaHora", fechaHora);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Cuenta agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox2.Clear();
                            textBox1.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo agregar la cuenta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar en la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            MenuPrincipal menu = new MenuPrincipal();
            menu.Show();
            this.Close();
        }
    }

}
