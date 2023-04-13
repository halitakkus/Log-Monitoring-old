using Application.DataAccess.Abstract.Repository;
using Application.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Abstract
{
    public interface ISettingDal : IRepository<Setting, Guid>
    {
    }
}
