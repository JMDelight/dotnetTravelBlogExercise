using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelBlog.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Classification { get; set; }
        public string FunFact { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
    }
}
