using Application.DataAccess.Entities.Concrete;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DataAccess.Entities
{
    public class ApproveHistorie : BaseEntity<Guid>
    {
        public Guid RemoteWorkId { get; set; }
        public virtual RemoteWork RemoteWork { get; set; }
        public Guid? TaskId { get; set; }
        [StringLength(512)]
        public string Comment { get; set; }
        [StringLength(32)]
        public string Action { get; set; }
    }
}
