using CExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorInventario_v1.Exceptions
{
    public class ExceptionsHandler
    {
        public int exceptionHandler(Exception ex) 
        {
            if (ex.GetType() == typeof(GetUsuarioException))
            {
                return 401;
            }
            else 
            {
                return 500;
            }
        }
    }
}
