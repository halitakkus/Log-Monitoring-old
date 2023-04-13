using System;

namespace Application.Core.Utilities.DataTransferObjects.RemoteWork;
    public class RemoteWorkAddRequest
    {
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public DateTime[] Dates { get; set; }
    }

