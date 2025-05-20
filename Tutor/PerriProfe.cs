using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;





namespace Tutor
{
    public partial class PerriProfe : Form
    {
        public async Task<string> ConsultarIA(string pregunta)
        {
            using (HttpClient client = new HttpClient())
            {
                var request = new PerriProfesor.ChatRequest
                {
                    model = "mistral",
                    stream = false,
                    messages = new List<PerriProfesor.Message>
            {
                new PerriProfesor.Message { role = "user", content = $"Imagina que eres un perro maestro llamado PerriProfesor. Explica como a un niño de primaria: {pregunta}" }
            }
                };

                string json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://localhost:11434/api/chat", content);
                response.EnsureSuccessStatusCode();

                // Mensaje al conectarse exitosamente a la API
                MessageBox.Show("¡Conexión exitosa con la API de PerriProfe!", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string result = await response.Content.ReadAsStringAsync();

                var respuesta = JsonConvert.DeserializeObject<PerriProfesor.ChatResponse>(result);

                // Condición: si la pregunta es muy larga, espera más tiempo
                if (pregunta.Length > 150)
                {
                    // Espera 5 minutos si la pregunta es larga
                    await Task.Delay(TimeSpan.FromMinutes(7));
                }
                else
                {
                    // Espera 3 minutos si la pregunta es corta o normal
                    await Task.Delay(TimeSpan.FromMinutes(5));
                }

                if (respuesta?.message?.content != null)
                    return respuesta.message.content;
                else
                    return "PerriProfe no pudo responder en este momento. Intenta de nuevo.";
            }
        }






        public PerriProfe()
        {
            InitializeComponent();
            this.Text = "PerriProfe, tu amigo para aprender";
        }

        private void PerriProfe_Load(object sender, EventArgs e)
        {

        }
        private List<string> DividirEnLineas(string texto, int maxLongitud)
        {
            var resultado = new List<string>();
            string[] palabras = texto.Split(' ');
            StringBuilder lineaActual = new StringBuilder();

            foreach (var palabra in palabras)
            {
                if (lineaActual.Length + palabra.Length + 1 > maxLongitud)
                {
                    resultado.Add(lineaActual.ToString());
                    lineaActual.Clear();
                }
                if (lineaActual.Length > 0)
                    lineaActual.Append(" ");
                lineaActual.Append(palabra);
            }
            if (lineaActual.Length > 0)
                resultado.Add(lineaActual.ToString());

            return resultado;
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            string pregunta = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(pregunta))
            {
                MessageBox.Show("Por favor escribe una duda para que PerriProfe la responda.");
                return;
            }

            // Mostrar la pregunta del usuario
            listBox1.Items.Add("Tú: " + pregunta);

            // Mostrar mensaje de "pensando"
            int pensandoIndex = listBox1.Items.Add("PerriProfe está pensando... 🐶");
            listBox1.TopIndex = listBox1.Items.Count - 1; // Desplaza al final

            try
            {
                string respuesta = await ConsultarIA(pregunta);

                // Elimina el mensaje de "pensando"
                listBox1.Items.RemoveAt(pensandoIndex);

                // Divide la respuesta en líneas y las agrega una por una, ajustando el ancho
                string[] lineas = respuesta.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var linea in lineas)
                {
                    var lineasAjustadas = DividirEnLineas(linea.Trim(), 60); // 60 caracteres por línea, ajusta según tu ListBox
                    foreach (var l in lineasAjustadas)
                    {
                        listBox1.Items.Add("PerriProfe: " + l);
                    }
                }
                listBox1.TopIndex = listBox1.Items.Count - 1;
            }
            catch (Exception ex)
            {
                listBox1.Items[pensandoIndex] = "PerriProfe: Ocurrió un error: " + ex.Message;
            }

            textBox1.Clear();
            textBox1.Focus();
        }

        public void GuardarHistorialEnBD(string pregunta, string respuesta)
        {
            // Cambia la cadena de conexión por la de tu base de datos real
            string connectionString = "Server=MELANY\\SQLEXPRESS;Database=Tutor_DB;Trusted_Connection=True;";
            string query = @"INSERT INTO Historial (Pregunta, Respuesta) VALUES (@Pregunta, @Respuesta)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Pregunta", pregunta);
                cmd.Parameters.AddWithValue("@Respuesta", respuesta);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StringBuilder preguntas = new StringBuilder();
            StringBuilder respuestas = new StringBuilder();

            foreach (var item in listBox1.Items)
            {
                string linea = item.ToString();
                if (linea.StartsWith("Tú:"))
                    preguntas.AppendLine(linea.Substring(3).Trim());
                else if (linea.StartsWith("PerriProfe:"))
                    respuestas.AppendLine(linea.Substring("PerriProfe:".Length).Trim());
            }

            // Guardar en la base de datos
            GuardarHistorialEnBD(preguntas.ToString(), respuestas.ToString());

            // Crear carpeta "Historial de Bot" en Mis Documentos
            string carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Historial de Bot");
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            // Nombre del archivo con fecha y hora
            string nombreArchivo = $"HistorialBot_{DateTime.Now:yyyyMMdd_HHmmss}.docx";
            string rutaArchivo = Path.Combine(carpeta, nombreArchivo);

            // Crear el archivo Word con el historial
            using (WordprocessingDocument doc = WordprocessingDocument.Create(rutaArchivo, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                foreach (var item in listBox1.Items)
                {
                    var p = body.AppendChild(new Paragraph());
                    p.AppendChild(new Run(new Text(item.ToString())));
                }
            }

            MessageBox.Show("¡Historial guardado en la base de datos y en el archivo Word!\nUbicación: " + rutaArchivo, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}

