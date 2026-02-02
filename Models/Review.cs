using System;
using System.ComponentModel.DataAnnotations;

namespace MaisonTelecom.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string ReviewerName { get; set; }

        [Required]
        public string Content { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;
    }
}