using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api3il.Models;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api3il.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;

        public AdminController(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Admin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAdministration>>> GetuserAdministration()
        {
            if (_context.userAdministration == null)
            {
                return NotFound();
            }
            return await _context.userAdministration.ToListAsync();
        }

        // GET: api/Admin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAdministration>> GetUserAdministration(int id)
        {
            if (_context.userAdministration == null)
            {
                return NotFound();
            }
            var userAdministration = await _context.userAdministration.FindAsync(id);

            if (userAdministration == null)
            {
                return NotFound();
            }

            return userAdministration;
        }

        // PUT: api/Admin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAdministration(int id, UserAdministration userAdministration)
        {
            if (id != userAdministration.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAdministration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAdministrationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDTO request)
        {
            try
            {
                // Vérifier si l'email est déjà utilisé
                var existingUser = await _context.userAdministration.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (existingUser != null)
                {
                    return BadRequest("Cet email est déjà associé à un utilisateur.");
                }

                // Créer le mot de passe hash et le salt
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                // Créer un nouvel utilisateur
                var newUser = new UserAdministration
                {
                    Username = request.UserName,
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                // Sauvegarder l'utilisateur en base de données
                _context.userAdministration.Add(newUser);
                await _context.SaveChangesAsync();

                // Générer le token
                string token = CreateToken(newUser);

                return Ok(token);
            }
            catch (Exception ex)
            {
                // Affichage des informations d'erreur dans les journaux
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            try
            {
                var user = await _context.userAdministration.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (user.Email != request.Email)
                {
                    return BadRequest("Utilisateur introuvable!!!");
                }

                // vérification du mot de passe
                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return BadRequest("Mot de passe incorrect");
                }

                string token = CreateToken(user);
                return Ok(token);
            }
            catch (Exception ex)
            {
                // affichage des informations d'erreur dans les journaux
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/Admin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAdministration(int id)
        {
            if (_context.userAdministration == null)
            {
                return NotFound();
            }
            var userAdministration = await _context.userAdministration.FindAsync(id);
            if (userAdministration == null)
            {
                return NotFound();
            }

            _context.userAdministration.Remove(userAdministration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAdministrationExists(int id)
        {
            return (_context.userAdministration?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //generation du token
        private string CreateToken(UserAdministration user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //methode de cryptage du mot de passe
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //cryptage du mot de passe envoyee
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        //methode pour verifier le mot de passe lors de l'authentification
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
