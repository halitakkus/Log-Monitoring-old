using Application.DataAccess.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Entities
{
    public class Day : BaseEntity<Guid>
    {
        public Guid RemoteWorkId { get; set; }
        public virtual RemoteWork RemoteWork { get; set; }
        public DateTime Date { get; set; }
    }
}
