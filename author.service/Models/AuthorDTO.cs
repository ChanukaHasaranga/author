using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace author.service.Models
{
    public class AuthorDTO
    {
        public int ID { get; set; }

       
        public string FirstName { get; set; }

        
        public string LastName { get; set; }


       
        public string EmailAddress { get; set; }


        
        public string MobileNumber { get; set; }


        public string? ProfilePictureURL { get; set; }


    }
}
