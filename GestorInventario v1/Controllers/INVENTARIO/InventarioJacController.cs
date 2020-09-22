using CBusiness.INV_JAC;
using GestorInventario_v1.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorInventario_v1.Controllers.INVENTARIO
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioJacController : ControllerBase
    {
        private B_Inventario_Jac invjac;
        private ExceptionsHandler exception;
        public InventarioJacController(B_Inventario_Jac _invjac) 
        {
            invjac = _invjac;
            exception = new ExceptionsHandler();
        }

        [HttpGet, Route("get_invsiapruebas")]
        public IActionResult getInventarioSiaPrueba() 
        {
            try 
            {
                return Ok(new { data = invjac.getInventarioSiaPrueba() });
            }
            catch (Exception ex) 
            {
                return StatusCode(exception.exceptionHandler(ex), ex);
            }
        }

        [HttpGet, Route("get_inventariosia")]
        public async Task<IActionResult> getInventarioSia() 
        {
            try 
            {
                return Ok(new { data =  invjac.getInventarioSia()});
            }
            catch (Exception ex) 
            {
                return StatusCode(exception.exceptionHandler(ex), ex);
            }
        }
    }
}