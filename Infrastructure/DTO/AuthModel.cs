using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class AuthModel
    {
        public string? Message { get; set; }
        public bool ISAuth { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public IList<string>? Roles { get; set; }
        public DateTime? Expiresion { get; set; }
    }
}
