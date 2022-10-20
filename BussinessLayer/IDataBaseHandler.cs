using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public interface IDataBaseHandler
    {
        List<EmployeeDetail> Get();
        EmployeeDetail Get(String username);
        void delete(String username);
        void create(EmployeeDetail employee);
       

    }
}
