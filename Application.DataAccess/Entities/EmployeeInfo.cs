using Application.DataAccess.Entities.Concrete;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DataAccess.Entities
{
    public class EmployeeInfo : BaseEntity<Guid>
    {
        [StringLength(64)]
        public string ManagerId { get; set; }
        [StringLength(64)]
        public string ManagerUnitId { get; set; }
        [StringLength(64)]
        public string UnitId { get; set; }
        [StringLength(256)]
        public string UnitName { get; set; }
        
        [StringLength(256)]
        public string EmployeeId { get; set; }
    }
}
