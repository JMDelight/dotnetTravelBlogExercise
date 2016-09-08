using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelBlog.Models
{
    public class ExperiencePerson
    {
        public ExperiencePerson()
        {

        }
        public ExperiencePerson(int experienceId, int personId)
        {
            this.ExperienceId = experienceId;
            this.PersonId = personId;
        }

        [Key]
        public int ExperiencePersonId { get; set; }

        public int ExperienceId { get; set; }
        public Experience Experience { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

    }
}
