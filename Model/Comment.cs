using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Model
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Stock? Stock { get; set; }
        public int? StockId { get; set; }
    }
}
