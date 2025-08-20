using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace author.service.Models
{
    public class UpdateUserDTO
    {
        public string ? FirstName { get; set; }
        public string ? LastName { get; set; }
        public string ? MobileNumber { get; set; }
        public IFormFile? ProfilePicture { get; set; } // File comes in multipart/form-data
    }
}
