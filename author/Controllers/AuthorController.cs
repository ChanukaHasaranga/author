using author.Models;
using author.service.Authors;
using author.service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace author.Controllers
{

    [Route("api/authordetail")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private readonly IAUthorRepo _service;
        private readonly IMapper _mapper;


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
        public ActionResult<AuthorDTO> CreateAuthor(CreateAuthorDTO author)
        {
            //check if email exits
            var exisiting = _service.GetAuthorByEmail(author.EmailAddress);
            if (exisiting!=null)
            {
                return Conflict("Email already registered.");
            }


            var authorEntity = _mapper.Map<Author>(author);

            var hasher = new PasswordHasher<Author>();
            authorEntity.PasswordHash = hasher.HashPassword(authorEntity, author.Password);// Hash the password
            var newauthor = _service.AddAuthor(authorEntity);

            var authorforreturn = _mapper.Map<AuthorDTO>(newauthor);

            return CreatedAtRoute("GetAuthor", new { id = authorforreturn.ID }, authorforreturn);

        }
    }
}
