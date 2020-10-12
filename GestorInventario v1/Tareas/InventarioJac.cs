using CBusiness.INV_JAC;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorInventario_v1.Tareas
{
    internal class InventarioJac : IInvocable
    {
        private readonly ILogger logger;
        private B_Inventario_Jac invjac;
        public InventarioJac(ILogger<InventarioJac> _logger, B_Inventario_Jac _invjac) 
        {
            logger = _logger;
            invjac = _invjac;
        }
        public Task Invoke() 
        {
            return Task.Run(() =>
            {
                //logger.LogInformation("Inventario Jac vista Sia: " + DateTime.Now.ToString() + Guid.NewGuid().ToString("n").Substring(16));
                invjac.getInventarioSia();
            });
        }
    }
}
