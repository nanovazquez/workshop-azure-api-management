using System;
using System.ComponentModel.DataAnnotations;

namespace NETConfAPI.Models
{
    public class Speaker
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
