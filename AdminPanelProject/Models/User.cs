using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanelProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter username")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage ="Please enter password")]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [MaxLength(60)]

        public string EmailAddress { get; set; }
    }
}
