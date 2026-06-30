using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Domain
{
    public class BaseAuditEntity
    {
        public bool IsActive { get; set; } = true;
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        
    }
}
