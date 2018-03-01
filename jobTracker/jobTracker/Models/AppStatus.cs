using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jobTracker.Models
{
    public class AppStatus
    {
        [Key]
        public int Id { get; set; }

        public string AppStatusTitle { get; set; }
    }
}
