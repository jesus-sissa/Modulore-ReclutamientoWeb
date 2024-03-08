using System.Data;
using System.Data.SqlClient;


namespace Modulo_Reclutamiento_Web.Models
{
    public class Conexion
    {
       
        private static string Monterrey = "Data Source=SISSASQL; Initial Catalog=SIAC; User ID=SIACNET;Password=SisTema.SIACLogin";
        private static string Saltillo = "Data Source=192.168.10.1; Initial Catalog=SAL_SIAC; User ID=SIACNET;Password=SisTema.SIACLogin";
        private static string Prb_Mty = "Data Source=sql-mty-t01 ; Initial Catalog=SIAC; User ID=MEDRANO;Password=MEDRANO5638";
        private static string Prb_Salt = "Data Source=sql-mty-t01 ; Initial Catalog=SAL_SIAC; User ID=MEDRANO;Password=MEDRANO5638";
        private static string QA_Mty = "Data Source=dvl-mty-v01 ; Initial Catalog=SIAC; User ID=SIACNET;Password=SisTema.SIACLogin";

        /// <summary>
        /// Recibe una cadena de conexion y crea una nuevo conexion sqlserver
        /// </summary>
        /// <param name="ClaveSucursal"></param>
        /// <returns>Retorno una nueva conexion sqlserver</returns>
        public static SqlConnection creaConexion(string ClaveSucursal)
        {

            return new SqlConnection(ClaveSucursal);
        }
        public static SqlConnection creaConexionCentral(string ClaveSucursal)
        {

            return new SqlConnection(ClaveSucursal);
        }

        /// <summary>
        ///  Recibe una cadena de conexion y crea una nueva conexion y transaccion sqlserver
        /// </summary>
        /// <param name="ClaveSucursal"></param>
        /// <returns>Retorna una nueva conexion con una transaccion abierta</returns>
        public static SqlTransaction creaTransaccion(string ClaveSucursal)
        {

            var cn = new SqlConnection(ClaveSucursal);
            cn.Open();
            return cn.BeginTransaction();
        }

        /// <summary>
        /// Recibe sqlcommand y parametro con nombre, tipo y valor del parametro, se agrega al sqlcommand enviado
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        /// <param name="valor"></param>
        /// <param name="Mayusculas"></param>
        public static void creaParametro(SqlCommand cmd, string nombre, SqlDbType tipo, object valor, bool Mayusculas = false)
        {
            var prm = new SqlParameter(nombre, tipo);
            switch (tipo)
            {

                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                    if (Mayusculas)
                    {
                        prm.Value = valor.ToString().ToUpper();
                    }
                    else
                    {
                        prm.Value = valor;
                    }
                    break;
                default:
                    prm.Value = valor;
                    break;
            }
            cmd.Parameters.Add(prm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Procedimiento"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public static SqlCommand creaComando(string Procedimiento, SqlConnection conexion)
        {
            var cmd = new SqlCommand(Procedimiento, conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public static SqlCommand creaComando(string Procedimiento, SqlTransaction Transaccion)
        {
            var cmd = new SqlCommand(Procedimiento, Transaccion.Connection, Transaccion);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public static DataTable ejecutaConsulta(SqlCommand cmd)
        {
            var Tbl = new DataTable();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                Tbl.Load(cmd.ExecuteReader());
            }
            else
            {
                cmd.Connection.Open();
                Tbl.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            return Tbl;
        }

        public static void ejecutaConsulta(SqlCommand cmd, DataTable Tbl)
        {
            if (cmd.Connection.State == ConnectionState.Open)
            {
                Tbl.Load(cmd.ExecuteReader());
            }
            else
            {
                cmd.Connection.Open();
                Tbl.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
        }


        public static object ejecutaScalar(SqlCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Open)
            {
                return cmd.ExecuteScalar();
            }
            else
            {
                cmd.Connection.Open();
                var res = cmd.ExecuteScalar();
                cmd.Connection.Close();

                return res;
            }
        }

        public static int ejecutarNonquery(SqlCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Open)
            {
                return cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.Connection.Open();
                var res = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return res;
            }
        }


        public static string getClaveConexion(int clave)
        {
            string resp = null;
            switch (clave)
            {

                case 1:
                    resp = Monterrey;
                    break;
                case 2:
                    resp = Saltillo;
                    break;
                case 3:
                    resp = Prb_Mty;
                    break;
                case 4:
                    resp = Prb_Salt;
                    break;
                case 5:
                    resp = QA_Mty;
                    break;
            }

            return resp;
        }


    }
}
