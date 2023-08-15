using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class FormSendMail
    {
        public string title { get; set; }
        public string body { get; set; }
        public string email { get; set; }
    }
}
