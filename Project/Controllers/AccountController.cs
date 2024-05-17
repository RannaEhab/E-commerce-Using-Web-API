using Context.Context;
using DB.DTOs.DTOs.User;
using DTOs.DTOs.Customer;
using DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<Customer> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IConfiguration configuration;
        public AccountController(UserManager<Customer> _userManager, RoleManager<IdentityRole> _roleManager, IConfiguration _configuration)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            configuration = _configuration;
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleDTO role)
        {
            var existRole = await roleManager.FindByNameAsync(role.RoleName);
            if (existRole == null)
            {
                var newRole = new IdentityRole()
                {
                    Name = char.ToUpper(role.RoleName[0])+role.RoleName.Substring(1),
                };
                await roleManager.CreateAsync(newRole);
                return Ok("The user was created successfully");
            }
            else
            {
                return BadRequest("This user already exists");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerRegisterDTO customer, string role)
        {
            var existUser = await userManager.FindByEmailAsync(customer.Email);
            if (existUser == null)
            {
                var existRole = await roleManager.FindByNameAsync(role);
                if (existRole == null)
                {
                    return NotFound("Role doesn't exist");
                }
                else
                {
                    var newCustomer = new Customer()
                    {
                        UserName = customer.UserName,
                        Email = customer.Email,
                        PhoneNumber = customer.PhoneNumber,
                    };
                    await userManager.CreateAsync(newCustomer, customer.Password);
                    await userManager.AddToRoleAsync(newCustomer, role);
                    return Ok();
                }
            }
            else
            {
                return BadRequest("This user already exists");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(CustomerLoginDTO customer)
        {
            var existUser = await userManager.FindByNameAsync(customer.Name);
            if (existUser == null)
            {
                return BadRequest("This user doesn't exist");
            }
            else
            {
                if (!await userManager.CheckPasswordAsync(existUser, customer.Password))
                {
                    return BadRequest("This password isn't correct");
                }
                else
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]));
                    var userRoles = await userManager.GetRolesAsync(existUser);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.DenyOnlySid,existUser.Id),
                    };
                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var setToken = new JwtSecurityToken(
                        expires: DateTime.Now.AddHours(2),
                        claims: claims,
                        signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                        issuer: configuration["jwt:issuer"],
                        audience: configuration["jwt:audience"]);
                    var token = new JwtSecurityTokenHandler().WriteToken(setToken);
                    return Ok(token);
                }
            }

        }

    }
}
