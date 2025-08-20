using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace author.Models
{
    public class Author
    {
    public int ID { get; set; }

        [Required]
        [MaxLength(20)]
    public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
    public string LastName { get; set; }


        [Required]
        [MaxLength(50)]
        public string EmailAddress { get; set; }


        [Required]
        [MaxLength(20)]
    public string MobileNumber { get; set; }

        [Required]
        [MaxLength(100)]
    public string PasswordHash { get; set; } //store hash password



        public string? ProfilePictureURL { get; set; }


    }
}
