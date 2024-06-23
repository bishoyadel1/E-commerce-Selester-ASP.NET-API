using Core.Models;
using ECommerceGP.Bl.Dtos.UserDtos;
using ECommerceGP.Bl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Repositories;
using Core.IRepositories;
using Core.IServices;
using Core.DTOs;
using Infrastructure;
using ECommerceGP.Bl.Dtos.Errors;
using System.Web;
using System;
using Core.DTOs.UserDtos;
using Core.Helper ;
using System.Security.Cryptography;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        // Modification :
        // -> check the role exists

        #region Injection
        private readonly UserManager<User> userManager;
        private readonly IEmailSettings emailSettings;
        private readonly IAccountManagerServices accountManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly ECommerceDBContext context;

        public AccountsController(UserManager<User> _userManager, IAccountManagerServices _accountManager, IConfiguration _configuration, IEmailSettings emailSettings, RoleManager<IdentityRole<int>> _roleManager,ECommerceDBContext _context)
        {
            this.userManager = _userManager;
            this.emailSettings = emailSettings;
            accountManager = _accountManager;
            this.configuration = _configuration;
            roleManager = _roleManager;
            context = _context;
        }
        #endregion

        #region Register New Version
        //[HttpPost("register")]
        //public async Task<IActionResult> Register(UserRegisterDTO registerDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await accountManager.RegisterAsync(registerDto);

        //    if (!result.IsAuthenticated)
        //        return BadRequest(result.Message);

        //    return Ok(result);
        //}
        #endregion

        #region login New version
        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginCredentialsDTO loginDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await accountManager.LoginAsync(loginDto);

        //    if (!result.IsAuthenticated)
        //        return BadRequest(result.Message);

        //    return Ok(result);
        //}
        #endregion
        
        #region Register 
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> register(UserRegisterDTO input)
        {
            string encryptedAddress = CryptionHelper.EncryptString(input.Address);
            User NewUser = new User
            {
                FullName = input.FullName,
                UserName = input.UserName,
                Email = input.Email,
                Address = encryptedAddress,
                PhoneNumber = input.PhoneNumber,
                Active = false
            };

            var creationResult = await userManager.CreateAsync(NewUser, input.password);

            if (!creationResult.Succeeded)
            {
                return BadRequest(creationResult.Errors);

            }

            // check the role exists
            var check = await roleManager.RoleExistsAsync("Client");
            var check1 = await roleManager.RoleExistsAsync("Admin");
            if (!check  && !check1)
            {
                var role = new IdentityRole<int>("Client");
                var role1 = new IdentityRole<int>("Admin");
                var checkRoleCreation = await roleManager.CreateAsync(role);
                var checkRoleCreation1 = await roleManager.CreateAsync(role1);
                if (!checkRoleCreation.Succeeded)
                    return BadRequest("can not add role");
            }

           
            await userManager.AddToRoleAsync(NewUser, "Client");
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier,NewUser.Id.ToString()),
               new Claim(ClaimTypes.Role,"Client"),
          
            };

            var claimsResult = await userManager.AddClaimsAsync(NewUser, claims);
            if (!claimsResult.Succeeded)
            {
                return BadRequest(claimsResult.Errors);
            }

            #region Add the user Address to the Addresses Table

            var addressDetails = input?.Address?.Split(',');

            var lastUser = await userManager.FindByEmailAsync(input.Email);

            if (addressDetails.Length < 3)
            {
                // if the address not consist from 3 parts will delete it
                lastUser.Address = null;
                await userManager.UpdateAsync(lastUser);

                return BadRequest("The User was Addede but the address and saved not saved");
            }

            context.Address.Add(new Address
            {
                UserID = lastUser?.Id,
                Street = addressDetails[0],
                City = addressDetails[1],
                Country = addressDetails[2]
            });

            context.Phones.Add(new Phone
            {
                UserID = lastUser?.Id,
                PhoneNumber = input.PhoneNumber
            });

            try
            {
                context.SaveChanges();

                var user = await userManager.FindByEmailAsync(input.Email);
                if (user == null)
                    return BadRequest(new ApiResponse(400, "User not found"));
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedToken = HttpUtility.UrlEncode(token);
                var EmailConfirmationUrl = $"{configuration["FrontEndUrl"]}/confirmEmail?email={user.Email}&token={encodedToken}";
                var message = $@"<html>
<head>
    <style>
        /* Define styles for the body */
        body {{
            background-color: #f2f2f2;
            font-family: Arial, sans-serif;
            font-size: 16px;
            margin: 0;
            padding: 0;
        }}

        /* Define styles for the container */
        .container {{
            background-color: #ffffff;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            margin: 20px auto;
            max-width: 600px;
            padding: 20px;
        }}

        /* Define styles for the heading */
        h1 {{
            color: #333;
            font-size: 24px;
            margin-bottom: 16px;
            text-align: center;
        }}

        /* Define styles for the paragraph */
        p {{
            color: #666;
            margin-bottom: 16px;
            text-align: center;
        }}

        /* Define styles for the button container */
        .button-container {{
            text-align: center;
        }}

        /* Define styles for the button */
        .button {{
            background-color: #007bff;
            border-radius: 5px;
            color: #fff;
            display: inline-block;
            font-size: 16px;
            margin: 20px auto;
            padding: 12px 24px;
            text-align: center;
            text-decoration: none;
            transition: background-color 0.3s ease;
        }}

        .button:hover {{
            background-color: #0069d9;
            cursor: pointer;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <h1>Confirm your Email</h1>
        <p>Hello,</p>
        <p>We have sent you this email in response to your request to Register on company name.</p>
        <p>To Confirm your Email, please click the button below:</p>
        <div class=""button-container"">
            <a href=""{EmailConfirmationUrl}"" class=""button"" style=""color: #fff"">Confirm your Email</a>
        </div>
        <p>Please ignore this email if you did not request a register on site.</p>
    </div>
</body>
</html>";

                var email = new Email
                {
                    Subject = "Email Confirmation",
                    To = input.Email,
                    Body = message
                };
                emailSettings.SendEmail(email);

                return Ok(new ApiResponse(200, "The Reset Password link has been sent to your mail"));
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest($"There is an error on saving Address and Phones : {ex.Message}");
            }

            #endregion
        }


        #endregion




        #region Login
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginCredentialsDTO loginInput)
        {

            #region User name vertification
            var user = await userManager.FindByNameAsync(loginInput.username);

            if (user == null)
            {
                return BadRequest(new { message = "user not found" });
            }
            #endregion

            #region User lockout check
            var Islocked = await userManager.IsLockedOutAsync(user);
            if (Islocked)
            {
                return BadRequest(new { message = "You  are Locked Out" });
            }
            #endregion

            #region Check Email Validation 
            if (user.Active == false /*|| user.Active == null*/)
            {
                await userManager.AccessFailedAsync(user);
                return Unauthorized();
            }
            #endregion


            #region User Password Vertification
            if (!await userManager.CheckPasswordAsync(user, loginInput.password))
            {
                await userManager.AccessFailedAsync(user);
                return Unauthorized();
            }
            #endregion

            #region Get user Claims and Add more custom claims to the list
            var userclaims = await userManager.GetClaimsAsync(user);
            userclaims.Add(new Claim(ClaimTypes.Name, user.UserName));
            userclaims.Add(new Claim(ClaimTypes.GivenName, user.FullName));
            userclaims.Add(new Claim(ClaimTypes.Email, user.Email));
            userclaims.Add(new Claim(ClaimTypes.StreetAddress, CryptionHelper.DecryptString(  user.Address)));
            userclaims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            userclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            #endregion

            #region Get the role of the user and add it to the userclaims
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                // add to the userclaims list 
                userclaims.Add(new Claim(ClaimTypes.Role, role));
            }
            #endregion

            #region Getting the secret key from Appsettings file
            var KeyString = configuration.GetValue<string>("SecretKey");
            var KeyInBytes = Encoding.ASCII.GetBytes(KeyString);
            var Key = new SymmetricSecurityKey(KeyInBytes);
            #endregion

            #region Token Signing
            var signingCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);

            var jwt = new JwtSecurityToken(
                claims: userclaims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(30),
                notBefore: DateTime.Now
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(jwt);
            return Ok(new TokenDTO
            {
                Token = tokenString
            });
            #endregion

        }
        #endregion


        #region ForgotPassword
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
                return BadRequest(new ApiResponse(400, "User not found"));
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            
            var encodedToken = HttpUtility.UrlEncode(token);
            var resetPasswordUrl = $"{configuration["FrontEndUrl"]}/resetPassword?email={user.Email}&token={encodedToken}";
            var message = $@"<html>
<head>
    <style>
        /* Define styles for the body */
        body {{
            background-color: #f2f2f2;
            font-family: Arial, sans-serif;
            font-size: 16px;
            margin: 0;
            padding: 0;
        }}

        /* Define styles for the container */
        .container {{
            background-color: #ffffff;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            margin: 20px auto;
            max-width: 600px;
            padding: 20px;
        }}

        /* Define styles for the heading */
        h1 {{
            color: #333;
            font-size: 24px;
            margin-bottom: 16px;
            text-align: center;
        }}

        /* Define styles for the paragraph */
        p {{
            color: #666;
            margin-bottom: 16px;
            text-align: center;
        }}

        /* Define styles for the button container */
        .button-container {{
            text-align: center;
        }}

        /* Define styles for the button */
        .button {{
            background-color: #007bff;
            border-radius: 5px;
            color: #fff;
            display: inline-block;
            font-size: 16px;
            margin: 20px auto;
            padding: 12px 24px;
            text-align: center;
            text-decoration: none;
            transition: background-color 0.3s ease;
        }}

        .button:hover {{
            background-color: #0069d9;
            cursor: pointer;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <h1>Reset Password</h1>
        <p>Hello,</p>
        <p>We have sent you this email in response to your request to reset your password on company name.</p>
        <p>To reset your password, please click the button below:</p>
        <div class=""button-container"">
            <a href=""{resetPasswordUrl}"" class=""button"" style=""color: #fff"">Reset Password</a>
        </div>
        <p>Please ignore this email if you did not request a password change.</p>
    </div>
</body>
</html>";

            var email = new Email
            {
                Subject = "Reset Password",
                To = forgotPasswordDto.Email,
                Body = message
            };
            emailSettings.SendEmail(email);

            return Ok(new ApiResponse(200, "The Reset Password link has been sent to your mail"));
        }

        #endregion

        #region ResetPassword 
        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return BadRequest(new ApiResponse(400, "User not found"));
            }
            var result = await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetPasswordDto.Token);
            if (!result)
            {
                return BadRequest(new ApiResponse(400, "Invalid password reset token"));
            }
            var resetResult = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (!resetResult.Succeeded)
            {
                return BadRequest(new ApiResponse(400, "Failed to reset password"));
            }

            return Ok(new ApiResponse(200, "Password has been changed successfully"));
        }
        #endregion

        #region EmailConfrmation
        [HttpPost("EmailConfirmation")]
        public async Task<ActionResult> EmailConfirmation(EmailConfirmationDto emailConfirmationDto)
        {
            var user = await userManager.FindByEmailAsync(emailConfirmationDto.Email);
            if (user == null)
            {
                return BadRequest(new ApiResponse(400, "User not found"));
            }
            var result = await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.EmailConfirmationTokenProvider , "EmailConfirmation", emailConfirmationDto.Token);
            if (!result)
            {
                return BadRequest(new ApiResponse(400, "Invalid password reset token"));
            }
            user.Active = true;
            await userManager.UpdateAsync(user);
            context.SaveChanges();


            return Ok(new ApiResponse(200, "Password has been changed successfully"));
        }
        #endregion

        // hashed?
        #region Get Current User
        //[HttpGet]
        //[Authorize]
        //[Route("CurrentUser")]
        //public async Task<ActionResult> GetCurrentUser()
        //{
        //    var CurrentUser = await userManager.GetUserAsync(User);
        //    return Ok(
        //        new
        //        {
        //            Id = CurrentUser.Id,
        //            UserName = CurrentUser.UserName
        //        }
        //        );
        //}
        #endregion

    }

}