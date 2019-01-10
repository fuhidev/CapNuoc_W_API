using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DataProvider
{
    public interface IAction<M,T>
    {
        IEnumerable<M> GetAll();
        M Get(T id);
        bool Delete(T id);
        bool Update(T id, M model);
        M Create(M model);
    }
}
