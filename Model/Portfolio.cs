using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string UserId { get; set; }
        public int stockId { get; set; }
        public AppUser User { get; set; }
        public Stock Stock { get; set; }
    }
}
