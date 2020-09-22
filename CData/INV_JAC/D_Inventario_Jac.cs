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
                //$" AND t.FechaFactura between GETDATE() - 11 and GETDATE() " +
                $" ORDER BY(t.fecha_factura), (t.chasis) ASC ";
            return sqlServerService.get_list<E_Inventario_Sia>(query);
        }
        public int verifyExisteChasisOracle(string nroFactura, string chasis, string idCliente) 
        {
            string query = $"";
            return oracleService.contar(query);
        }
        public async Task SaveInventarioOracle(E_Inventario_Sia chasis) 
        {
            string query = $"";
            await oracleService.ejecutarQueryAsync(query);
        }
        public async Task<E_Inventario_Sia> getInfoChasisOracle(string nroFactura, string chasis, string idCliente) 
        {
            string query = $"";
            return oracleService.get_uno<E_Inventario_Sia>(query);
        }
        public int verifyCantidadChasisRepetido(string nroFactura, string chasis, string idCliente) 
        {
            string query = $"";
            return oracleService.contar(query);
        }
        public async Task DeleteChasisRepetidosoracle(string nroFactura, string chasis, string idCliente) 
        {
            string query = $"";
            await oracleService.ejecutarQueryAsync(query);
        }
        public async Task UpdateInventarioOracle(E_Inventario_Sia chasis) 
        {
            string query = $"";
            await oracleService.ejecutarQueryAsync(query);
        }
    }
}