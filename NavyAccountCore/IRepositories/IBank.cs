using NavyAccountCore.Core.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IBank:IRepository<py_bank>
    {
        py_bank GetBankbyName(Expression<Func<py_bank, bool>> predicate);
    }
}
