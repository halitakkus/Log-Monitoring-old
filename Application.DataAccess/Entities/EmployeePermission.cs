using Application.DataAccess.Entities.Concrete;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DataAccess.Entities
{
    public class EmployeePermission : BaseEntity<Guid>
    {
        [StringLength(64)]
        public string EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        public int? TypeId { get; set; }
    }
}
