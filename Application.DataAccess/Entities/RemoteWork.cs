using Application.DataAccess.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.DataAccess.Entities
{
    public class RemoteWork : BaseEntity<Guid>
    {
        [StringLength(64)]
        public string EmployeeId { get; set; }
        [StringLength(1024)]
        public string Description { get; set; }
        public Guid? ProcessId { get; set; }
        public Guid? CurrentTaskId { get; set; }
        [StringLength(32)]
        public string Status { get; set; }
        public int TotalWorkDay { get; set; }
        public int RemoteWorkDay { get; set; }
        public DateTime PeriodDate { get; set; }    
        public Guid DemandId { get; set; }


        public ICollection<Day> Days { get; set; }
    }
}
