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
                $" WHERE t.j_nro_factura = '{nroFactura}' " +
                $" AND UPPER(TRIM(t.j_chasis)) = UPPER(TRIM('{chasis}'))" +
                $" AND t.j_id_cliente = '{idCliente}'";
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
                $"(j_cod_marca, j_chasis, j_motor, j_nro_factura, j_tido_doc_factura, j_fecha_factura, " +
                $" j_concesionario_asignado, j_marca, j_descripcion_factura, j_veh_color, j_fsc, j_familia, j_costo, " +
                $" j_precio, j_categoria_cliente, j_clase, j_sub_clase, j_linea, j_estado, j_id_cliente, j_descripcion_color, " +
                $" j_autorizacion_eje, j_version_anio, j_clasificacion_exonerado, j_codigo_interno, j_fecha_sincroniza_sia) " +
                $" VALUES({chasis.cod_marca},'{chasis.chasis}', '{chasis.motor}', '{chasis.nro_factura}', '{chasis.tipo_doc_factura}', :fechafactura, " +
                $" '{chasis.concesionario_asignado}', '{chasis.marca}', '{chasis.descripcion_factura}', '{chasis.veh_color}', '{chasis.fsc}', '{chasis.familia}', " +
                $" :costo, :precio, '{chasis.categoria_cliente}', '{chasis.clase}', '{chasis.sub_clase}', '{chasis.linea}', '{chasis.estado}', '{chasis.id_cliente}', '{chasis.descripcion_color}', " +
                $" '{chasis.autorizacion_eje}', {chasis.version_anio}, {chasis.clasificacion_exonerado}, '{chasis.codigo_interno}', {fecharegistro})";
            await oracleService.ejecutarQueryAsync(query, param);
        }
        public async Task<E_Chasis> getInfoChasisOracle(string nroFactura, string chasis, string idCliente) 
        {
            string query = $"SELECT t.* " +
                $" FROM BD_INVENTARIO_JAC_RED t " +
                $" WHERE t.j_nro_factura = '{nroFactura}' " +
                $" AND UPPER(TRIM(t.j_chasis)) = UPPER(TRIM('{chasis}')) " +
                $" AND t.j_id_cliente = '{idCliente}' " +
                $" AND t.j_fecha_factura = (SELECT MAX(x.j_fecha_factura) FROM BD_INVENTARIO_JAC_RED x WHERE x.j_nro_factura = '{nroFactura}' AND UPPER(TRIM(x.j_chasis)) = UPPER(TRIM('{chasis}')) AND x.j_id_cliente = '{idCliente}')";
           return oracleService.get_uno<E_Chasis>(query);
        }
        public async Task DeleteChasisRepetidosoracle(string nroFactura, string chasis, string idCliente) 
        {
            string query = $"DELETE " +
                $" FROM BD_INVENTARIO_JAC_RED t " +
                $" WHERE t.j_nro_factura = '{nroFactura}' " +
                $" AND t.j_chasis = '{chasis}' " +
                $" AND t.j_id_cliente = '{idCliente}'";
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
                $" SET t.j_tido_doc_factura = '{chasis.tipo_doc_factura}', " +
                $" t.j_fecha_factura = :fechafactura, " +
                $" t.j_concesionario_asignado = '{chasis.concesionario_asignado}', " +
                $" t.j_marca = '{chasis.marca}', " +
                $" t.j_descripcion_factura = '{chasis.descripcion_factura}', " +
                $" t.j_veh_color = '{chasis.veh_color}', " +
                $" t.j_fsc = '{chasis.fsc}', " +
                $" t.j_familia = '{chasis.familia}', " +
                $" t.j_costo = :costo, " +
                $" t.j_precio = :precio, " +
                $" t.j_categoria_cliente = '{chasis.categoria_cliente}', " +
                $" t.j_clase = '{chasis.clase}', " +
                $" t.j_sub_clase = '{chasis.sub_clase}', " +
                $" t.j_linea = '{chasis.linea}', " +
                $" t.j_estado = '{chasis.estado}', " +
                $" t.j_descripcion_color = '{chasis.descripcion_color}', " +
                $" t.j_autorizacion_eje = '{chasis.autorizacion_eje}', " +
                $" t.j_version_anio = {chasis.version_anio}, " +
                $" t.j_clasificacion_exonerado = {chasis.clasificacion_exonerado}, " +
                $" t.j_codigo_interno = '{chasis.codigo_interno}', " +
                $" t.j_fecha_actualiza_sia = {fechaactualiza} " +
                $" WHERE t.j_chasis = '{chasis.chasis}' " +
                $" AND t.j_nro_factura = '{chasis.nro_factura}' " +
                $" AND t.j_id_cliente = '{chasis.id_cliente}' " +
                $" AND t.j_cod_marca = {chasis.cod_marca}";
            await oracleService.ejecutarQueryAsync(query, param);
        }
        public async Task CreateProductoVehiculo() 
        {
            string query = $"pkg_inventario.crea_producto_vehiculo_jac_ecu";
            await oracleService.ejecutarProcedureAsync(query);
        }
    }
}