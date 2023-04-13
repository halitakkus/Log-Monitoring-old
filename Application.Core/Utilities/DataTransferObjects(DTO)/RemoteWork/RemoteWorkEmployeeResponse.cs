using System.Collections.Generic;

namespace Application.Core.Utilities.DataTransferObjects.RemoteWork;

    public class RemoteWorkEmployeeResponse
    {
        public string EmployeeId { get; set; }
        public List<string> Dates { get; set; }
        public string Description { get; set; }
    }

