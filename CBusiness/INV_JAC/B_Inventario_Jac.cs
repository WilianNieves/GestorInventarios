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
            int codigomarca = 300;
            var fechareg = DateTime.Now;
            List<E_Inventario_Sia> chasises = await invjacdata.getInventarioSia();
            chasises.ForEach(async chasis => 
            {
                chasis.fecha_reg_factura = Convert.ToDateTime(chasis.fecha_factura);
                chasis.cod_marca = codigomarca;
                int cantidadchasoracle = invjacdata.verifyExisteChasisOracle(chasis.nro_factura, chasis.chasis, chasis.id_cliente);
                var existechasoracle = cantidadchasoracle <= 0 ? false : true;
                if (!existechasoracle) 
                {
                    await invjacdata.SaveInventarioOracle(chasis, fechareg); 
                }
                else 
                {
                    var datchasisoracle = await invjacdata.getInfoChasisOracle(chasis.nro_factura, chasis.chasis, chasis.id_cliente);
                    chasis.sub_clase = datchasisoracle.j_sub_clase;
                    chasis.cod_marca = datchasisoracle.j_cod_marca;
                    //chasis.clasificacion_exonerado = datchasisoracle.j_clasificacion_exonerado;

                    int cantidadrepetido = invjacdata.verifyExisteChasisOracle(chasis.nro_factura, chasis.chasis, chasis.id_cliente);
                    if (cantidadrepetido > 1) 
                    {
                        await invjacdata.DeleteChasisRepetidosoracle(chasis.nro_factura, chasis.chasis, chasis.id_cliente);
                        await invjacdata.SaveInventarioOracle(chasis, fechareg);
                    }
                    await invjacdata.UpdateInventarioOracle(chasis, fechareg);
                }
            });
        }
    }
}