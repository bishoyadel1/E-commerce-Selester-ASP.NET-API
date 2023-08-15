using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entities.Identity
{
    public class AppUserModel : IdentityUser
    {
        [Required]

        public string Name { get; set; }
        public bool ActiveUser { get; set; }
    }
}
