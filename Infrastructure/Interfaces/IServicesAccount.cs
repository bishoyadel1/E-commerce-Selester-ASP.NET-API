using Domin.Entities;
using Infrastructure.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IServicesAccount<T> where T : class
    {
       Task<bool> Login(LoginView model);
       Task<bool> Register(RegisterView model);
        Task<AuthModel> GetUserTokenAsync(LoginView model);

    }
}
