using WebApiNoteManagement.Helper;
using WebApiNoteManagement.Models;
using WebApiNoteManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace WebApiNoteManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginViewModel> _logger;
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private readonly JWTConfig _jWTConfig;

        public AccountController(IOptions<JWTConfig> jwtConfig, UserManager<ApplicationUser> userManager, ILogger<LoginViewModel> logger, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment iWebHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _jWTConfig = jwtConfig.Value;
            _iWebHostEnvironment = iWebHostEnvironment;
        }
        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<object> RegisterUser([FromBody] ApplicationUserViewModel objModel)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Name = objModel.Name,
                    UserName = objModel.Email,
                    Email = objModel.Email,
                    DateOfBirth=objModel.DateOfBirth,
                };
                var result = await _userManager.CreateAsync(user, objModel.Password);
                if (result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "User has been Registered", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "", result.Errors.Select(x => x.Description).ToArray()));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }
        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<object> SignIn([FromBody] LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.FindByNameAsync(model.Email);
                        var user = new ApplicationUserViewModel
                        {
                            UserId=appUser.Id,
                            Name = appUser.Name,
                            Email = appUser.Email,
                            UserName=appUser.UserName,
                            DateOfBirth = appUser.DateOfBirth,
                        };
                        user.Token = GenerateToken(appUser);

                        return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", user));

                    }
                }

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "invalid Email or password", null));

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }
        private string GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>(){
     new Claim(JwtRegisteredClaimNames.NameId,user.Id),
               new Claim(JwtRegisteredClaimNames.Email,user.Email),
               new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
           };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.Audience,
                Issuer = _jWTConfig.Issuer
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
