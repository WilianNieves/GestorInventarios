using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CData.ORACLE
{
    public class OracleService
    {
        public readonly string connectionOracleString;
        public OracleService(IConfiguration configuration) 
        {
            connectionOracleString = configuration.GetConnectionString("OracleDBConnection");
        }
        // obtencion de una lista mediante una sentencia SQL
        public List<T> get_list<T>(string query) 
        {
            var conn = new OracleConnection(connectionOracleString);
            try 
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return SqlMapper.Query<T>(conn, query, commandType: CommandType.Text).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("OracleService, Error en la consulta", ex);
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
            var conn = new OracleConnection(connectionOracleString);
            try 
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return SqlMapper.Query<T>(conn, query, param: parametros, commandType: CommandType.Text).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("OracleService, Error en la consulta", ex);
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
            var conn = new OracleConnection(connectionOracleString);
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
                throw new Exception("OracleService, Error en la consulta", ex);
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
            var conn = new OracleConnection(connectionOracleString);
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
                throw new Exception("OracleService, Error en la consulta", ex);
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
            var conn = new OracleConnection(connectionOracleString);
            try 
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return SqlMapper.Query<T>(conn, query, commandType: CommandType.Text).SingleOrDefault();
            }
                catch (Exception ex)
            {
                throw new Exception("OracleService, Error en la consulta", ex);
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
            var conn = new OracleConnection(connectionOracleString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return SqlMapper.Query<T>(conn, query, param: parametros, commandType: CommandType.Text).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("OracleService, Error en la consulta", ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}


//// ejecucion del CRUD+L correspondiente
//public object GetMethode<T>(string tabla, string metodo, Saving guardable)
//{
//    // metodo = CRUD+L C= Insert , R = Select by PK- Details , U = Update by PK, D = Delete by PK, L = List all by empresa

//    object result = null;
//    var conn = new OracleConnection(connstring);
//    try
//    {

//        if (conn.State == ConnectionState.Closed)
//        {
//            conn.Open();
//        }

//        if (conn.State == ConnectionState.Open)
//        {
//            string sql = GetSQL(tabla, metodo, guardable);
//            var query = sql;

//            result = SqlMapper.Query<T>(conn, query, commandType: CommandType.Text);
//        }
//        return result;
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//    finally
//    {
//        conn.Close();
//        conn.Dispose();
//    }
//}


//// Obtencion de la sentencia SQL para GetMethode

//public string GetSQL(string tabla, string metodo, Saving guardable)
//{

//    // modelo
//    var e = guardable.GetType();
//    Type tt = guardable.GetType();
//    TypeInfo cInfo = tt.GetTypeInfo();


//    IEnumerable<PropertyInfo> declaredProperties = cInfo.DeclaredProperties;

//    // variables para la construccion del SQL
//    var titulos = "";
//    var cols = "";
//    var modificaciones = "";
//    var condicionesPk = "";
//    String Sql = "";
//    int l = declaredProperties.Count() - 1;
//    int i = 0;
//    var att = false;


//    // SQL para el metodo de Insert

//    if (metodo.Trim().ToUpper().Equals("C"))
//    {

//        foreach (var dd in declaredProperties)
//        {
//            titulos += dd.Name;
//            titulos += i < l ? " , " : "";

//            string tipo = dd.ToString();
//            if (tipo.ToUpper().Contains("STRING"))
//            {
//                cols += " '" + dd.GetValue(guardable) + "' ";
//            }
//            else
//            {
//                cols += dd.GetValue(guardable);
//            }

//            cols += i < l ? " , " : "";
//            i++;
//        }

//        Sql = "INSERT INTO " + cInfo.Name + "( " + titulos + " ) VALUES( " + cols + " )";

//    }
//    else
//    {
//        // SQL para el metodo de LIST -- falta PKs
//        if (metodo.Trim().ToUpper().Equals("L"))
//        {
//            Sql = "SELECT * FROM " + cInfo.Name;
//        }
//        else
//        {
//            if (metodo.Trim().ToUpper().Equals("U"))
//            {


//                foreach (var dd in declaredProperties)
//                {
//                    titulos = "";
//                    cols = "";
//                    att = false;
//                    var attributes = dd.GetCustomAttributes(false);

//                    foreach (var attribute in attributes)
//                    {
//                        if (attribute.GetType() == typeof(PrimaryKeyAttribute))
//                        {
//                            if (condicionesPk != "")
//                            {
//                                condicionesPk += " and ";
//                            }
//                            string msg = "The Primary Key for the {0} class is the {1} property";
//                            Console.WriteLine(msg, cInfo.Name, dd.Name);


//                            string tipo = dd.ToString();
//                            if (tipo.ToUpper().Contains("STRING"))
//                            {
//                                condicionesPk += dd.Name + " = '" + dd.GetValue(guardable) + "'";

//                            }
//                            else
//                            {
//                                condicionesPk += dd.Name + " = " + dd.GetValue(guardable);

//                            }



//                            att = true;
//                        }
//                    }
//                    if (att is false)
//                    {
//                        if (modificaciones != "")
//                        {
//                            modificaciones += " , ";
//                        }

//                        titulos = dd.Name;
//                        //titulos += i < l ? " , " : "";

//                        string tipo = dd.ToString();
//                        if (tipo.ToUpper().Contains("STRING"))
//                        {
//                            cols = " '" + dd.GetValue(guardable) + "' ";
//                        }
//                        else
//                        {
//                            cols = dd.GetValue(guardable) + "";

//                        }




//                        modificaciones += titulos + " = " + cols + " ";

//                    }



//                }


//                Sql = "UPDATE " + cInfo.Name + " SET  " + modificaciones + "  where  " + condicionesPk + " ";

//                //  UPDATE table_name
//                //SET column1 = value1, column2 = value2, ...
//                //WHERE condition;

//            }
//            if (metodo.Trim().ToUpper().Equals("R"))
//            {
//                foreach (var dd in declaredProperties)
//                {


//                    var attributes = dd.GetCustomAttributes(false);

//                    foreach (var attribute in attributes)
//                    {
//                        if (attribute.GetType() == typeof(PrimaryKeyAttribute))
//                        {
//                            if (condicionesPk != "")
//                            {
//                                condicionesPk += " and ";
//                            }
//                            string msg = "The Primary Key for the {0} class is the {1} property";
//                            Console.WriteLine(msg, cInfo.Name, dd.Name);


//                            string tipo = dd.ToString();
//                            if (tipo.ToUpper().Contains("STRING"))
//                            {
//                                condicionesPk += dd.Name + " = '" + dd.GetValue(guardable) + "'";

//                            }
//                            else
//                            {
//                                condicionesPk += dd.Name + " = " + dd.GetValue(guardable);

//                            }




//                        }
//                    }




//                }


//                Sql = "SELECT  * FROM " + cInfo.Name + "  where  " + condicionesPk + " ";
//            }
//        }
//    }
//    return Sql;
//}


//public int contar(string query)
//{
//    var conn = new OracleConnection(new D_Conexion().getCadena());
//    try
//    {
//        if (conn.State == ConnectionState.Closed)
//        {
//            conn.Open();
//        }
//        var res = SqlMapper.Query<int>(conn, query, commandType: CommandType.Text).First();
//        return res;
//    }
//    catch (Exception ex)
//    {
//        throw new Exception("Eror en la consulta", ex);
//    }
//    finally
//    {
//        conn.Close();
//        conn.Dispose();
//    }
//}

//public async Task ejecutarQueryAsync(string query)
//{
//    string q = $"{query}";
//    var conn = new OracleConnection(new D_Conexion().getCadena());
//    try
//    {
//        if (conn.State == ConnectionState.Closed)
//        {
//            conn.Open();
//        }
//        await Task.Run(() =>
//        {
//            SqlMapper.Query(conn, q, commandType: CommandType.Text);
//        });
//    }
//    catch (Exception ex)
//    {
//        throw new Exception("Eror al ejecutar comando SQL", ex);
//    }
//    finally
//    {
//        conn.Close();
//        conn.Dispose();
//    }
//}
//public void ejecutarQuery(string query)
//{
//    string q = $"{query}";
//    var conn = new OracleConnection(new D_Conexion().getCadena());
//    try
//    {
//        if (conn.State == ConnectionState.Closed)
//        {
//            conn.Open();
//        }

//        SqlMapper.Query(conn, q, commandType: CommandType.Text);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception("Eror al ejecutar comando SQL", ex);
//    }
//    finally
//    {
//        conn.Close();
//        conn.Dispose();
//    }
//}

//public void ejecutarQuery(string query, Object parametros)
//{
//    string q = $"{query}";
//    var conn = new OracleConnection(new D_Conexion().getCadena());
//    try
//    {
//        if (conn.State == ConnectionState.Closed)
//        {
//            conn.Open();
//        }

//        SqlMapper.Query(conn, q, param: parametros, commandType: CommandType.Text);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception("Eror al ejecutar comando SQL", ex);
//    }
//    finally
//    {
//        conn.Close();
//        conn.Dispose();
//    }
//}
//public async Task ejecutarQueryAsync(string query, Object parametros)
//{
//    string q = $"{query}";
//    var conn = new OracleConnection(new D_Conexion().getCadena());
//    try
//    {
//        if (conn.State == ConnectionState.Closed)
//        {
//            conn.Open();
//        }
//        await Task.Run(() =>
//        {
//            SqlMapper.Query(conn, q, param: parametros, commandType: CommandType.Text);
//        });
//    }
//    catch (Exception ex)
//    {
//        throw new Exception("Eror al ejecutar comando SQL", ex);
//    }
//    finally
//    {
//        conn.Close();
//        conn.Dispose();
//    }
//}