﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required int Rating { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
