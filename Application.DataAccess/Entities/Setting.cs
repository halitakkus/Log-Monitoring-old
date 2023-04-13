using Application.DataAccess.Entities.Concrete;
using System;

namespace Application.DataAccess.Entities
{
    public class Setting : BaseEntity<Guid>
    {
        public string Key { get; set; }
        public Guid? ParentId { get; set; }
        public string Value { get; set; }
    }
}
