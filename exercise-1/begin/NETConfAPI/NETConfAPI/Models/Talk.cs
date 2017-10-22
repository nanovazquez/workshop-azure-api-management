using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETConfAPI.Models
{
    public class Talk
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [ForeignKey("Speaker")]
        public int SpeakerId { get; set; }
        public virtual Speaker Speaker { get; set; }
    }
}
