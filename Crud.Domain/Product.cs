using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Domain
{
    public class Product : BaseAuditEntity
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
    }
}
