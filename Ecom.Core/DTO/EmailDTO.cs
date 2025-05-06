using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.DTO
{
    public class EmailDTO
    {
        public EmailDTO(string to, string form, string subject, string content)
        {
            To = to;
            Form = form;
            Subject = subject;
            Content = content;
        }

        public string To { get; set; }
        public string Form { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
