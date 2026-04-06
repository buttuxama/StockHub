using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Comment
{
    public class CreateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters long.")]
        [MaxLength(240, ErrorMessage = "Title must be at most 240 characters long.")]
        public string Title { get; set; } = String.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters long.")]
        [MaxLength(240, ErrorMessage = "Content must be at most 240 characters long.")]
        public string Content { get; set; } = String.Empty;
    }
}
