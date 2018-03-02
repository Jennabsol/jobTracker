using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jobTracker.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        public string Notes { get; set; }

        [Display(Name = "Company")]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

    }
}
