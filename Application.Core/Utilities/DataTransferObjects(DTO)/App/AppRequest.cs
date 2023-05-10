using System;
using System.Collections.Generic;
using Application.Core.Utilities.DataTransferObjects_DTO_.Log;

namespace Application.Core.Utilities.DataTransferObjects_DTO_.App;

public class AppRequest
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<LogRequest> Logs { get; set; }
}