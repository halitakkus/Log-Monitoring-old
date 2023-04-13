using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DataAccess.Entities.Concrete;

public class Holiday  : BaseEntity<Guid>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [StringLength(256)]
    public string Name { get; set; }
    public int? TypeId { get; set; }
}