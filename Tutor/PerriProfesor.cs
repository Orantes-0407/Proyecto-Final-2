using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Tutor
{
    internal class PerriProfesor
    {
        // Agrega este using

        public void GuardarHistorialEnBD(string pregunta, string respuesta)
        {
            // Cambia la cadena de conexión por la de tu base de datos
            string connectionString = "Server=MELANY\\SQLEXPRESS;Database=Tutor_DB;Trusted_Connection=True;";
            string query = @"INSERT INTO HistorialBot (Pregunta, Respuesta) VALUES (@Pregunta, @Respuesta)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Pregunta", pregunta);
                cmd.Parameters.AddWithValue("@Respuesta", respuesta);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public class ChatRequest
        {
            public string model { get; set; }
            public bool stream { get; set; }
            public List<Message> messages { get; set; }
        }

        public class Message
        {
            public string role { get; set; }
            public string content { get; set; }
        }

        public class ChatResponse
        {
            public Message message { get; set; }
        }

    }
}

