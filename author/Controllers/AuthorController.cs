using author.Models;
using author.service.Authors;
using author.service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace author.Controllers
{

    [Route("api/authordetail")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private readonly IAUthorRepo _service;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<Author> _passwordHasher = new PasswordHasher<Author>();
        private readonly string _jwtSecret = "076chanuka1003067_ThisIsLongSecretKey!!"; // replace with strong key

        public AuthorController(IAUthorRepo service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}",Name ="GetAuthor")]
        public ActionResult GetAuthor(int id)
        {
            var author = _service.GetAuthor(id);

            if (author is null)
            {
                return NotFound();
                
            }
            var mappedauthor = _mapper.Map<AuthorDTO>(author);
            return Ok(mappedauthor);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<AuthorDTO>> CreateAuthor(CreateAuthorDTO author)
        {
            //check if email exits
            var exisiting = _service.GetAuthorByEmail(author.EmailAddress);
            if (exisiting!=null)
            {
                return Conflict("Email already registered.");
            }


            var authorEntity = _mapper.Map<Author>(author);



            //profile picture adding
            if (author.ProfilePicture != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(author.ProfilePicture.FileName);
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/profile-pics");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await author.ProfilePicture.CopyToAsync(stream);
                }

                authorEntity.ProfilePictureURL = "/profile-pics/" + fileName;
            }



            var hasher = new PasswordHasher<Author>();
            authorEntity.PasswordHash = hasher.HashPassword(authorEntity, author.Password);// Hash the password
            var newauthor = _service.AddAuthor(authorEntity);

            var authorforreturn = _mapper.Map<AuthorDTO>(newauthor);

            return CreatedAtRoute("GetAuthor", new { id = authorforreturn.ID }, authorforreturn);

        }
        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto) 
        {
            var user = _service.GetAuthorByEmail(dto.EmailAddress);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid credentials");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Uri,user.ProfilePictureURL ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var profilepicurl = User.FindFirstValue(ClaimTypes.Uri);
            var fullName = User.Identity?.Name;

            return Ok(new
            {
                UserId = userId,
                Email = email,
                Name = fullName,
                ProfilePicURL=profilepicurl
            });
        }

        [HttpPut("UpdateUSer{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, UpdateUserDTO UDTO)
        {
            var updateEntity = _mapper.Map<Author>(UDTO);



            //profile picture update
            if (UDTO.ProfilePicture != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(UDTO.ProfilePicture.FileName);
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/profile-pics");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UDTO.ProfilePicture.CopyToAsync(stream);
                }

                updateEntity.ProfilePictureURL = "/profile-pics/" + fileName;
            }


            var result = _service.UpdateAuthor(id, updateEntity);
            if (result==null)
            {
                return NotFound(new { message = "User not found" });
            }

            var authorforreturn = _mapper.Map<AuthorDTO>(result);
            return Ok(authorforreturn);


        }

    }
}
