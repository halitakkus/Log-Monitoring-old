using System;
using Application.DataAccess.Entities.Concrete;

namespace Application.DataAccess.Entities;

public class Log : BaseEntity<Guid>
{
    public virtual App Apps { get; set; }
    public Guid AppId { get; set; }
    public Guid? UserId { get; set; }
    public  string Name { get; set; }
    public string Content { get; set; }
    public string Level { get; set; }
    public bool IsItFixed { get; set; }
    public string ServerName { get; set; }
    public string ServerIp { get; set; }
    public DateTime LogDate { get; set; }
}