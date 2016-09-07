using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelBlog.Models
{
    public class Experience
    {
        [Key]
        public int ExperienceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
