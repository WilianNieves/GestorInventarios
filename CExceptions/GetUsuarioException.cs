using System;
using System.Collections.Generic;
using System.Text;

namespace CExceptions
{
    public class GetUsuarioException : Exception
    {
        public GetUsuarioException(string message) : base(message) { }
        public GetUsuarioException() : base("Error de Credenciales de Usuario") { }
    }
}
