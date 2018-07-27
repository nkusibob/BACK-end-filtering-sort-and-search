using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public static class GetValue
    {
       
            public static object GetPropValue(object src, string propName)
            {
                return src.GetType().GetProperty(propName).GetValue(src, null);
            }
        
    }
}
