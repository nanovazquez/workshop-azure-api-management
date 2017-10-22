using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NETConfAPI.Models
{
    public class Conference
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Year { get; set; }

        public virtual ICollection<Talk> Talks { get; set; }
    }
}
