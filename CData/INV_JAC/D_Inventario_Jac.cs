using CData.COMUN;
using CData.ORACLE;
using CData.SQLSERVER;
using CModels.INV_JAC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CData.INV_JAC
{
    public class D_Inventario_Jac
    {
        private OracleService oracleService;
        private SqlServerService sqlServerService;
        public D_Inventario_Jac(OracleService _oracleService, SqlServerService _sqlServerService) 
        {
            oracleService = _oracleService;
            sqlServerService = _sqlServerService;
        }
        public List<E_Inventario_Sia> getInventarioSiaPrueba() 
        {
            string query = $"SELECT t.* " +
                $" FROM VST_VEHICULOS_ADA t " +
                $" WHERE t.chasis is not null  " +
                //$" AND t.FechaFactura between GETDATE() - 11 and GETDATE() " +
                $" ORDER BY(t.fecha_factura), (t.chasis) ASC ";
            return sqlServerService.get_list<E_Inventario_Sia>(query);
        }
        public async Task<List<E_Inventario_Sia>> getInventarioSia() 
        {
            string query = $"SELECT t.* " +
                $" FROM VST_VEHICULOS_ADA t " +
                $" WHERE t.chasis is not null  " +
                $" AND t.chasis = '8LG3EKR20JE000013' " +
                //$" AND t.FechaFactura between GETDATE() - 11 and GETDATE() " +
                $" ORDER BY(t.fecha_factura), (t.chasis) ASC ";
            return sqlServerService.get_list<E_Inventario_Sia>(query);
        }
        public int verifyExisteChasisOracle(string nroFactura, string chasis, string idCliente) 
        {
            string query = $"SELECT COUNT(*) AS cantidad " +
                $" FROM BD_INVENTARIO_JAC_RED t " +
                $" WHERE t.d_nro_factura = '{nroFactura}' " +
                $" AND UPPER(TRIM(t.d_chasis)) = UPPER(TRIM('{chasis}'))" +
                $" AND t.d_id_cliente = '{idCliente}'";
            return oracleService.contar(query);
        }
        public async Task SaveInventarioOracle(E_Inventario_Sia chasis, DateTime fecha) 
        {
            var param = new
            {
                fechafactura = chasis.fecha_reg_factura,
                costo = chasis.costo,
                precio = chasis.precio
            };
            var fecharegistro = D_Comun.getfecha(fecha);
            string query = $"INSERT INTO BD_INVENTARIO_JAC_RED " +
                $"(d_cod_marca, d_chasis, d_motor, d_nro_factura, d_tido_doc_factura, d_fecha_factura, " +
                $" d_concesionario_asignado, d_marca, d_descripcion_factura, d_veh_color, d_fsc, d_familia, d_costo, " +
                $" d_precio, d_categoria_cliente, d_clase, d_sub_clase, d_linea, d_estado, d_id_cliente, d_descripcion_color, " +
                $" d_autorizacion_eje, d_version_anio, " +
                //$" d_clasificacion_exonerado, " +
                $" d_codigo_interno, d_fecha_sincroniza_sia) " +
                $" VALUES({chasis.cod_marca},'{chasis.chasis}', '{chasis.motor}', '{chasis.nro_factura}', '{chasis.tipo_doc_factura}', :fechafactura, " +
                $" '{chasis.concesionario_asignado}', '{chasis.marca}', '{chasis.descripcion_factura}', '{chasis.veh_color}', '{chasis.fsc}', '{chasis.familia}', " +
                $" :costo, :precio, '{chasis.categoria_cliente}', '{chasis.clase}', '{chasis.sub_clase}', '{chasis.linea}', '{chasis.estado}', '{chasis.id_cliente}', '{chasis.descripcion_color}', " +
                $" '{chasis.autorizacion_eje}', {chasis.version_anio}, " +
                //$" {chasis.clasificacion_exonerado}, " +
                $" '{chasis.codigo_interno}', {fecharegistro})";
            await oracleService.ejecutarQueryAsync(query, param);
        }
        public async Task<E_Chasis> getInfoChasisOracle(string nroFactura, string chasis, string idCliente) 
        {
            string query = $"SELECT t.* " +
                $" FROM BD_INVENTARIO_JAC_RED t " +
                $" WHERE t.d_nro_factura = '{nroFactura}' " +
                $" AND UPPER(TRIM(t.d_chasis)) = UPPER(TRIM('{chasis}')) " +
                $" AND t.d_id_cliente = '{idCliente}' " +
                $" AND t.d_fecha_factura = (SELECT MAX(x.d_fecha_factura) FROM BD_INVENTARIO_JAC_RED x WHERE x.d_nro_factura = '{nroFactura}' AND UPPER(TRIM(x.d_chasis)) = UPPER(TRIM('{chasis}')) AND x.d_id_cliente = '{idCliente}')";
           return oracleService.get_uno<E_Chasis>(query);
        }
        public async Task DeleteChasisRepetidosoracle(string nroFactura, string chasis, string idCliente) 
        {
            string query = $"DELETE " +
                $" FROM BD_INVENTARIO_JAC_RED t " +
                $" WHERE t.d_nro_factura = '{nroFactura}' " +
                $" AND t.d_chasis = '{chasis}' " +
                $" AND t.d_id_cliente = '{idCliente}'";
            await oracleService.ejecutarQueryAsync(query);
        }
        public async Task UpdateInventarioOracle(E_Inventario_Sia chasis, DateTime fecha) 
        {
            var param = new
            {
                fechafactura = chasis.fecha_reg_factura,
                costo = chasis.costo,
                precio = chasis.precio
            };
            var fechaactualiza = D_Comun.getfecha(fecha);
            string query = $" UPDATE BD_INVENTARIO_JAC_RED t " +
                $" SET t.d_tido_doc_factura = '{chasis.tipo_doc_factura}', " +
                $" t.d_fecha_factura = :fechafactura, " +
                $" t.d_concesionario_asignado = '{chasis.concesionario_asignado}', " +
                $" t.d_marca = '{chasis.marca}', " +
                $" t.d_descripcion_factura = '{chasis.descripcion_factura}', " +
                $" t.d_veh_color = '{chasis.veh_color}', " +
                $" t.d_fsc = '{chasis.fsc}', " +
                $" t.d_familia = '{chasis.familia}', " +
                $" t.d_costo = :costo, " +
                $" t.d_precio = :precio, " +
                $" t.d_categoria_cliente = '{chasis.categoria_cliente}', " +
                $" t.d_clase = '{chasis.clase}', " +
                $" t.d_sub_clase = '{chasis.sub_clase}', " +
                $" t.d_linea = '{chasis.linea}', " +
                $" t.d_estado = '{chasis.estado}', " +
                $" t.d_descripcion_color = '{chasis.descripcion_color}', " +
                $" t.d_autorizacion_eje = '{chasis.autorizacion_eje}', " +
                $" t.d_version_anio = {chasis.version_anio}, " +
                //$" t.d_clasificacion_exonerado = {chasis.clasificacion_exonerado}, " +
                $" t.d_codigo_interno = '{chasis.codigo_interno}', " +
                $" t.d_fecha_actualiza_sia = {fechaactualiza} " +
                $" WHERE t.d_chasis = '{chasis.chasis}' " +
                $" AND t.d_nro_factura = '{chasis.nro_factura}' " +
                $" AND t.d_id_cliente = '{chasis.id_cliente}' " +
                $" AND t.d_cod_marca = {chasis.cod_marca}";
            await oracleService.ejecutarQueryAsync(query, param);
        }
        public async Task CreateProductoVehiculo() 
        {
            string query = $"pkg_inventario.crea_producto_vehiculo_jac_ecu";
            await oracleService.ejecutarProcedureAsync(query);
        }
    }
}