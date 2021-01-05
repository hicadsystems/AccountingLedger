using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IContrRepo: IRepository<npf_contrdisc>
    {
        npf_contrdisc GetContrDiscByCode(Expression<Func<npf_contrdisc, bool>> predicate);
        IEnumerable<ContrDiscVM> Getbyfundcode(string fundcode);
    }
}
