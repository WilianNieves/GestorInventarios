﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CData.SQLSERVER
{
    public class SqlServerService
    {
        public readonly string connectionSqlServerString;
        public SqlServerService(IConfiguration configuration) 
        {
            connectionSqlServerString = configuration.GetConnectionString("SqlServerDBConnection");
        }
        // obtencion de una lista mediante una sentencia SQL
        public List<T> get_list<T>(string query)
        {
            var conn = new SqlConnection(connectionSqlServerString);
            try 
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return SqlMapper.Query<T>(conn, query, commandType: CommandType.Text).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("SqlServerService, Error en la consulta: ", ex);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }
        // obtencion de una lista mediante una sentencia SQL con asignacion de parametros
        public List<T> get_list<T>(string query, Object parametros) 
        {
            var conn = new SqlConnection(connectionSqlServerString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return SqlMapper.Query<T>(conn, query, param: parametros, commandType: CommandType.Text).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("SqlServerService, Error en la consulta: ", ex);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }
        // obtencion de un objeto mediante una sentencia SQL
        public T getObject<T>(string query) 
        {
            var conn = new SqlConnection(connectionSqlServerString);
            try 
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var l = SqlMapper.Query<T>(conn, query, commandType: CommandType.Text).ToList();
                if (l.Count > 0)
                    return l[0];
                else
                    return default(T);
            }
            catch (Exception ex) 
            {
                throw new Exception("SqlServerService, Error en la consulta: ", ex);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }
        // obtencion de un objeto mediante una sentencia SQL con asignacion de parametros
        public T getObject<T>(string query, Object parametros) 
        {
            var conn = new SqlConnection(connectionSqlServerString);
            try 
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var l = SqlMapper.Query<T>(conn, query, param: parametros, commandType: CommandType.Text).ToList();
                if (l.Count > 0)
                    return l[0];
                else
                    return default(T);
            }
            catch (Exception ex) 
            {
                throw new Exception("SqlServerService, Error en la consulta: ", ex);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }
        // obtencion de una clase mediante una sentencia SQL
        public T get_uno<T>(string query) 
        {
            var conn = new SqlConnection(connectionSqlServerString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return SqlMapper.Query<T>(conn, query, commandType: CommandType.Text).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("SqlServerService, Error en la consulta: ", ex);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }
        // obtencion de una clase mediante una sentencia SQL con asignacion de parametros
        public T get_uno<T>(string query, Object parametros) 
        {
            var conn = new SqlConnection(connectionSqlServerString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return SqlMapper.Query<T>(conn, query, param: parametros, commandType: CommandType.Text).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("SqlServerService, Error en la consulta: ", ex);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }
        // obtencion de un entero para verificacion mediante una sentencia SQL
        public int contar(string query) 
        {
            var conn = new SqlConnection(connectionSqlServerString);
            try 
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var res = SqlMapper.Query<int>(conn, query, commandType: CommandType.Text).First();
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("SqlServerService, Error en la consulta: ", ex);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }
        // obtencion de un entero para verificacion mediante una sentencia SQL con asignacion de parametros
        public int contar(string query, Object parametros) 
        {
            var conn = new SqlConnection(connectionSqlServerString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var res = SqlMapper.Query<int>(conn, query, param: parametros, commandType: CommandType.Text).First();
                return res;
            }
            catch (Exception ex) 
            {
                throw new Exception("SqlServerService, Error en la consulta: ", ex);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }
        // ejecucion de setencia SQL
        public void ejecutarquery(string query)
        {
            var conn = new SqlConnection(connectionSqlServerString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlMapper.Query(conn, query, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception("SqlServerService, Eror al ejecutar comando SQL: ", ex);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }
        // ejecucion de setencia SQL con envio de parametros
        public void ejecutarquery(string query, Object parametros) 
        {
            var conn = new SqlConnection(connectionSqlServerString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlMapper.Query(conn, query, param: parametros, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception("SqlServerService, Eror al ejecutar comando SQL: ", ex);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }
        // ejecucion de setencia SQL en un Task asyncrono
        public async Task ejecutarQueryAsync(string query) 
        {
            var conn = new SqlConnection(connectionSqlServerString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                await Task.Run(() =>
                {
                    SqlMapper.Query(conn, query, commandType: CommandType.Text);
                });
            }
            catch (Exception ex)
            {
                throw new Exception("SqlServerService, Eror al ejecutar comando SQL: ", ex);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }
        // ejecucion de setencia SQL en un Task asyncrono con envio de parametros
        public async Task ejecutarQueryAsync(string query, Object parametros) 
        {
            var conn = new SqlConnection(connectionSqlServerString);
            try 
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                await Task.Run(() => {
                    SqlMapper.Query(conn, query, param: parametros, commandType: CommandType.Text);
                });
            } 
            catch (Exception ex)
            {
                throw new Exception("SqlServerService, Eror al ejecutar comando SQL: ", ex);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}