using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using author.Models;
using author.service.Models;
using AutoMapper;


namespace author.service.Profiles
{
    public class AuthorProfile : Profile
    {
       public AuthorProfile()
        {

            CreateMap<Author, AuthorDTO>();
            CreateMap<CreateAuthorDTO, Author>()
            .ForMember(dest => dest.PasswordHash,
            opt=> opt.Ignore()

            );// hash manually before saving

            CreateMap<LoginDTO, Author>()
           .ForMember(dest => dest.PasswordHash,
           opt => opt.Ignore()

           );// hash manually before saving
        }
    }
}
