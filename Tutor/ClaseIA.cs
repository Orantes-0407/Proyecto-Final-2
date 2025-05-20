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
using Tutor;

public class IAService
{
    public async Task<string> ConsultarIA(string pregunta)
    {
        using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromMinutes(5) })


        {
            var request = new Api.ChatRequest
            {
                model = "mistral",
                stream = false,
                messages = new List<Api.Message>
                {
                    new Api.Message { role = "user", content = pregunta }
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

            // Parsear la respuesta JSON usando JObject
            JObject jsonResponse = JObject.Parse(result);
            string textoRespuesta = jsonResponse["message"]?["content"]?.ToString();

            if (string.IsNullOrEmpty(textoRespuesta))
            {
                throw new InvalidOperationException("La respuesta de la API está vacía");
            }

            return textoRespuesta;
        }
    }

    public async Task GenerarTemasMatematicasDocx(string rutaArchivo)
    {
        string prompt = @"Eres un maestro de primaria. 
Genera una lista de temas básicos de matemáticas para niños de primaria.
Para cada tema:
- Explica el tema de forma clara y detallada, como si estuvieras enseñando en clase, usando un lenguaje sencillo y ejemplos visuales.
- Incluye un ejemplo resuelto paso a paso.
- Agrega al menos dos ejercicios en blanco para que el alumno los resuelva.
El formato debe ser amigable, didáctico y fácil de entender para niños.";

        try
        {
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

            // Crear el directorio si no existe
            string directorio = Path.GetDirectoryName(rutaArchivo);
            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            // Crear el archivo Word
            using (WordprocessingDocument doc = WordprocessingDocument.Create(rutaArchivo, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
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

            MessageBox.Show("El archivo fue generado exitosamente.\nUbicación: " + rutaArchivo, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Abrir el archivo DOCX automáticamente
            Process.Start(new ProcessStartInfo
            {
                FileName = rutaArchivo,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al generar el documento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}

