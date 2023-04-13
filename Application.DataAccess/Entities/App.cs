using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.DataAccess.Entities.Concrete;

namespace Application.DataAccess.Entities;

public class App: BaseEntity<Guid>
{
   [StringLength(150)]
   public string Name { get; set; }
   public virtual IEnumerable<Log> Logs { get; set; }
}