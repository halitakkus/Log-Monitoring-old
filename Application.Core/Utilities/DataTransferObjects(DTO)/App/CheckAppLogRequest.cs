using System;

namespace Application.Core.Utilities.DataTransferObjects_DTO_.App;

public class CheckAppLogRequest
{
    public string Func { get; set; }
    public Guid AppId { get; set; }
}