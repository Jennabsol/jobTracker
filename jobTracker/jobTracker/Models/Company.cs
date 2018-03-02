using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jobTracker.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name= "Company Name")]
        public string Name { get; set; }

        
        [Display(Name = "Company Website")]
        public string Website { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

    }
}
