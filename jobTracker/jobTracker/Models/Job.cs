using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jobTracker.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Position { get; set; }

        public string Notes { get; set; }

        public bool Active { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [Display(Name = "Contact")]
        public int? ContactId { get; set; }
        public Contact Contact { get; set; }

        [Required]
        [Display(Name = "Application Status")]
        public int AppStatusId { get; set; }
        public AppStatus AppStatus { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public Job()
        {
            Active = true;  
        }
    }
}
