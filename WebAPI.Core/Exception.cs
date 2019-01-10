using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
* @author phuonghieuho
*
* @date - 11/19/2018 5:30:47 PM 
*/

namespace WebAPI.Core
{
    public class Exception:System.Exception
    {
        public Exception(string message):base(message)
        {
            
        }
    }
}
