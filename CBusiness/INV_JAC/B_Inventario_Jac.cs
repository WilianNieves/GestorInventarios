using CData.INV_JAC;
using CModels.INV_JAC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CBusiness.INV_JAC
{
    public class B_Inventario_Jac
    {
        private D_Inventario_Jac invjacdata;
        public B_Inventario_Jac(D_Inventario_Jac _invjacdata) 
        {
            invjacdata = _invjacdata;
        }
        public List<E_Inventario_Sia> getInventarioSiaPrueba()
        {
            var lista = invjacdata.getInventarioSiaPrueba();
            return lista;
        }
        public async Task getInventarioSia()
        {
            List<E_Inventario_Sia> chasises = await invjacdata.getInventarioSia();
            chasises.ForEach(async chasis => 
            {
                int cantidadchasoracle = invjacdata.verifyExisteChasisOracle(chasis.nro_factura, chasis.chasis, chasis.id_cliente);
                var existechasoracle = cantidadchasoracle <= 0 ? false : true;
                if (!existechasoracle) 
                {
                    await invjacdata.SaveInventarioOracle(chasis); 
                }
                else 
                {
                    var datchasisoracle = await invjacdata.getInfoChasisOracle(chasis.nro_factura, chasis.chasis, chasis.id_cliente);
                    int cantidadrepetido = invjacdata.verifyCantidadChasisRepetido(chasis.nro_factura, chasis.chasis, chasis.id_cliente);
                    var existerepetidos = cantidadrepetido <= 0 ? false : true;
                    if (existerepetidos) 
                    {
                        await invjacdata.DeleteChasisRepetidosoracle(chasis.nro_factura, chasis.chasis, chasis.id_cliente);
                        await invjacdata.SaveInventarioOracle(chasis);
                    }
                    await invjacdata.UpdateInventarioOracle(chasis);
                }
            });
        }
    }
}