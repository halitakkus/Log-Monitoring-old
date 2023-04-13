using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Utilities.DataTransferObjects.RemoteWork;

    public class RemoteWorkUpdateRequest : RemoteWorkAddRequest
    {
        public Guid Id { get; set; }

        public Guid? TaskId { get; set; }
    }

