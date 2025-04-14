using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }

        [StringLength(50)]
        public string? Priority { get; set; } // Added nullability with ?

    }
}