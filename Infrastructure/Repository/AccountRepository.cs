using Domin.Entities;

using Infrastructure.Helper;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domin.Entities.Identity;

namespace Infrastructure.IRepository.ServicesRepository
{
    public class AccountRepository : IServicesAccount<AppUserModel>
    {
        private readonly SelesterDbContext _context;
        private readonly RoleManager<IdentityRole> _identityRoles;
        private readonly UserManager<AppUserModel> _identityUser;
        private readonly SignInManager<AppUserModel> _signInManager;
        private readonly JWT _jwt;
        public AccountRepository(RoleManager<IdentityRole> identityRoles, UserManager<AppUserModel> identityUser, SelesterDbContext context , SignInManager<AppUserModel> signInManager, IOptions<JWT> jwt) { 
        _context= context;
            _identityRoles= identityRoles;
            _identityUser= identityUser;
            _signInManager = signInManager;
            _jwt = jwt.Value;
        }

        public async Task<bool> Login(LoginView model)
        {
            try
            {
                var user = await _identityUser.FindByEmailAsync(model.Email);
                if (user == null) { return false; }
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                return false;

            }


        }
        public async Task<AuthModel> GetUserTokenAsync(LoginView model)
        {
            var user = await _identityUser.FindByEmailAsync(model.Email);
            var userrole = await _identityUser.GetRolesAsync(user);
            var JWTSec = await CreateJwtToken(model);
            var data = new AuthModel()
            {
                Email = model.Email,
                ISAuth = true,
                Message = "success",
                Name = user.Name,
                Roles = userrole,
                Expiresion = DateTime.Now.AddDays(7),
                Token = new JwtSecurityTokenHandler().WriteToken(JWTSec)
            };
            return data;

        }


        public async Task<bool> Register(RegisterView model)
        {
            try
            {
                //create 
                bool PassIsValid = IsPasswordValid(model.Password);
                if (PassIsValid)
                {

                    var user = new AppUserModel()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = model.Email,
                        ActiveUser = true,
                        Name = model.Name,
                        UserName = model.Email,

                    };
                    var result = await _identityUser.CreateAsync(user, model.Password);
                    if (result.Succeeded) { await _identityUser.AddToRoleAsync(user, "BASIC"); return true; }

                    // password is invalid

                }
                else
                {
                    return false;

                }

                return false;
            }
            catch (Exception ex)
            {

                return false;

            }
        }

        private async Task<JwtSecurityToken> CreateJwtToken(LoginView model)

        {
            var user = await _identityUser.FindByEmailAsync(model.Email);
            //  var userrole = await _identityUser.GetRolesAsync(user);
            var userClaims = await _identityUser.GetClaimsAsync(user);
            var roles = await _identityUser.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));
            //var dfce = new SecurityTokenDescriptor { Expires = DateTime.Now.AddDays(7) };

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            //.Union((IEnumerable<Claim>)dfce);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }


        private bool IsPasswordValid(string password)
        {
            bool hasNonAlphanumeric = Regex.IsMatch(password, "^(?=.*[\\W_]).+$@");
            bool hasUppercase = Regex.IsMatch(password, "[A-Z]");
            bool hasLowercase = Regex.IsMatch(password, "[a-z]");
            bool hasDigit = Regex.IsMatch(password, "[0-9]");


            if (hasNonAlphanumeric && hasUppercase && !hasLowercase && !hasDigit && password.Length >= 10)
            {
                // The password meets the configured requirements
                return false;
            }
            else
            {
                // The password does not meet the configured requirements
                return true;
            }
        }

      
    }

}
