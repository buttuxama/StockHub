using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Stock? Stock { get; set; } = new Stock();
        public int? StockId { get; set; }
    }
}
