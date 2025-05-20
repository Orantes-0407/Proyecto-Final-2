using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Tutor
{
    internal class Comprensión_Lectora
    {
        public async Task<string> ConsultarIA(string pregunta)
        {
            using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromMinutes(6) })
            {
                var request = new
                {
                    model = "mistral",
                    stream = false,
                    messages = new[]
                    {
                        new { role = "user", content = pregunta }
                    }
                };

                string json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://localhost:11434/api/chat", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error en la API: {response.StatusCode}");
                }

                string result = await response.Content.ReadAsStringAsync();

                JObject jsonResponse = JObject.Parse(result);
                string textoRespuesta = jsonResponse["message"]?["content"]?.ToString();

                if (string.IsNullOrEmpty(textoRespuesta))
                {
                    throw new InvalidOperationException("La respuesta de la API está vacía");
                }

                return textoRespuesta;
            }
        }

        public async Task GenerarCuentoConPreguntasDocx(string rutaArchivo)
        {
            string prompt = @"Eres un maestro de primaria.
Escribe un cuento corto apropiado en español para niños de primaria (entre 7 y 12 años).
El cuento debe:
- Tener un título atractivo
- Ser adecuado para la edad mencionada
- Incluir personajes interesantes y una trama clara
- Contener una enseñanza o valor
- Tener una extensión de aproximadamente 1 páginas

Al final del cuento, agrega 5 preguntas en español de comprensión lectora sobre:
1. Personajes principales
2. Escenario o lugar donde ocurre la historia
3. Problema principal de la historia
4. Resolución del problema
5. Enseñanza o moraleja

Las preguntas deben ayudar a evaluar si el niño comprendió correctamente la lectura.
Formato: Escribe primero todo el cuento y después las preguntas numeradas.";

            try
            {
                // Crear la carpeta específica "Comprensión Lectora"
                string carpetaBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Comprensión Lectora");
                if (!Directory.Exists(carpetaBase))
                {
                    Directory.CreateDirectory(carpetaBase);
                }

                // Usamos la nueva ubicación para el archivo
                string nombreArchivo = Path.GetFileName(rutaArchivo);
                string nuevaRuta = Path.Combine(carpetaBase, nombreArchivo);

                string respuestaIA;
                try
                {
                    respuestaIA = await ConsultarIA(prompt);
                    MessageBox.Show("Conexión exitosa con la API.", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo conectar con la API: " + ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Crear el archivo Word
                using (WordprocessingDocument doc = WordprocessingDocument.Create(nuevaRuta, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = doc.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = mainPart.Document.AppendChild(new Body());

                    foreach (string linea in respuestaIA.Split('\n'))
                    {
                        var p = body.AppendChild(new Paragraph());
                        p.AppendChild(new Run(new Text(linea)));
                    }
                }

                MessageBox.Show("El cuento con preguntas fue generado exitosamente.\nUbicación: " + nuevaRuta, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir el archivo DOCX automáticamente
                Process.Start(new ProcessStartInfo
                {
                    FileName = nuevaRuta,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el documento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
