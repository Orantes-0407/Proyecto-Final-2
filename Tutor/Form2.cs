using System;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Forms.Design;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace Tutor
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            progressBar1.Visible = false;
        }



        private async void buttonGenerar_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            try
            {
                // Iniciamos el proceso: 10%
                progressBar1.Value = 10;

                // Creamos la carpeta de TemasMatematicas
                string carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TemasMatematicas");
                Directory.CreateDirectory(carpeta);
                progressBar1.Value = 20;

                // Definimos la ruta del archivo
                string rutaDocx = Path.Combine(carpeta, $"TemasMatematicas_{DateTime.Now:yyyyMMdd_HHmmss}.docx");
                progressBar1.Value = 30;

                // Instanciamos la clase y actualizamos la ProgressBar antes de llamar a la generación
                IAService ia = new IAService();
                progressBar1.Value = 40;

                // Llamada asíncrona para generar el contenido. Aquí se simula el tiempo de procesamiento.
                await ia.GenerarTemasMatematicasDocx(rutaDocx);
                progressBar1.Value = 100;

                MessageBox.Show("Archivo generado correctamente en:\n" + rutaDocx, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Espera breve para visualizar el 100% y luego resetea la barra
                await Task.Delay(500);
                progressBar1.Value = 0;
                progressBar1.Visible = false;
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Visible = true;

            try
            {
                progressBar1.Value = 10;
                string carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ArchivosTutorIA");
                Directory.CreateDirectory(carpeta);
                string rutaWord = Path.Combine(carpeta, $"ComunicacionLenguaje_{DateTime.Now:yyyyMMdd_HHmmss}.docx");

                progressBar1.Value = 30;
                var ia = new Tutor.Comunicación_y_Lenguaje();
                await ia.GenerarTemasComunicacionLenguajeDocx(rutaWord);
                progressBar1.Value = 90;

                // Mensaje de éxito si lo necesitas
                MessageBox.Show("El archivo de Comunicación y Lenguaje fue generado exitosamente.\nUbicación: " + rutaWord,
                               "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                progressBar1.Value = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Pequeña pausa para ver el 100%
                await Task.Delay(500);
                progressBar1.Visible = false;
                progressBar1.Value = 0;
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {

            progressBar1.Value = 0;
            progressBar1.Visible = true;
            try
            {
                progressBar1.Value = 10;
                string fechaHora = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string rutaArchivo = $"Cuento_Comprension_Lectora_{fechaHora}.docx";

                var comprensionLectora = new Comprensión_Lectora();
                progressBar1.Value = 30;
                await comprensionLectora.GenerarCuentoConPreguntasDocx(rutaArchivo);
                progressBar1.Value = 100;

                MessageBox.Show("El cuento con preguntas fue generado exitosamente.\nUbicación: " + rutaArchivo,
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                await Task.Delay(500); // Pequeña pausa para visualizar el progreso al 100%
                progressBar1.Visible = false;
                progressBar1.Value = 0;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var perriProfe = new PerriProfe();
            perriProfe.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            string connectionString = "Server=MELANY\\SQLEXPRESS;Database=Tutor_DB;Trusted_Connection=True;";
            string query = "SELECT * FROM Sesion";

            try
            {
                StringBuilder resultado = new StringBuilder();

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            resultado.AppendLine(
                                $"ID: {reader["EstudianteId"]} | Nombre: {reader["NombreEstudiante"]} | FechaHora: {reader["FechaHora"]}"
                            );
                        }
                    }
                }

                if (resultado.Length == 0)
                    MessageBox.Show("No hay registros de inicio de sesión.", "Sesiones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(resultado.ToString(), "Sesiones", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar la base de datos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

