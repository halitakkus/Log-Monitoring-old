using Application.DataAccess.Entities.Concrete;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DataAccess.Entities
{
    public class Notice : BaseEntity<Guid>
    {
        [StringLength(1024)]
        public string Content { get; set; }
    }
}
