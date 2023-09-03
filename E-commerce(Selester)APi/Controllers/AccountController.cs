using Infrastructure.Helper;
using Infrastructure.IRepository;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domin.Entities.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeekOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IServicesAccount<AppUserModel> _accountRepository;

        private readonly UserManager<AppUserModel> _userManager;

        public AccountController( IServicesAccount<AppUserModel> accountRepository,  UserManager<AppUserModel> userManager)
        { 
    
            _accountRepository = accountRepository;
            _userManager= userManager;

        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
        
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterView model)
        {
            if (ModelState.IsValid)
            {
                if ( await _accountRepository.Register(model))
                {
                    return Ok(new { message = "success" });
                }
                else
                    return BadRequest();
            }
            else
                return BadRequest();

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginView model)
        {
            if (ModelState.IsValid)
            {
                if (await _accountRepository.Login(model))
                {
                    var data = await _accountRepository.GetUserTokenAsync(model);   
                    return Ok(new {  data});
                }
                else
                    return BadRequest();
            }
            else
                return BadRequest();

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]

        public void Put(int id, [FromBody] string value)
        {
            


        }


        // DELETE api/<ValuesController>/5
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
               if(user == null)
                return NotFound();
               
               return Ok(user);

        }
        [HttpPost("resetpassword")]
        public async Task<IActionResult> resetpassword([FromQuery] string Email, [FromQuery] string Token, ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
       
                var user = await _userManager.FindByEmailAsync(Email);
                if(user is not null)
                {
                    
                    var result = await _userManager.ResetPasswordAsync(user, Token, model.Password);
                    if (result.Succeeded)
                        return Ok("Valid Email");
                    else
                        return BadRequest();
                }
                return BadRequest();
            }
            return BadRequest();

        }

        [HttpPost("ForgetPassword")]
       
        public async Task<IActionResult> ForgetPassword(string email)
        {
          if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if ( user is null)
                    return NotFound();
                else
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    
                    var url = Url.Action("resetpassword", "Account", new { Email = email, Token = token }, Request.Scheme);
                    var Email = new FormSendMail()
                    {
                        title = "ResetPassword",
                        body = url,
                        email = "bishoy.bishoy2001@gmail.com"

                    };
                 await Mail.SendMailAsync(Email);
                }
                return Ok();

            }
            return BadRequest();

        }
    }
}
