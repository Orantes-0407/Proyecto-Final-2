using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutor
{
    internal class Api
    {
        public class Message
        {
            public string role { get; set; }
            public string content { get; set; }
        }

        public class ChatRequest
        {
            public string model { get; set; }
            public bool stream { get; set; } = false;
            public List<Message> messages { get; set; }
        }

        public class ChatResponse
        {
            public string model { get; set; }
            public Message message { get; set; }
        }
    }
}
