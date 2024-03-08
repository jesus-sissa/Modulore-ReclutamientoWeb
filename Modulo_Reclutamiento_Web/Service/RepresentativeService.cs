using Modulo_Reclutamiento_Web.Models;
using System.Data;
using System.Data.SqlClient;

namespace Modulo_Reclutamiento_Web.Service
{
    public class RepresentativeService
    {
        private static RepresentativeService? instancia;

        public static RepresentativeService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new RepresentativeService();
                }

                return instancia;
            }
        }

        public List<Prospectus> get_ProspectusWithContrats(DateTime desde, DateTime hasta)
        {
            List<Prospectus> _prospectus = new List<Prospectus>();
            SqlCommand cmd;

            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    cmd = Conexion.creaComando("Cat_EmpleadosP_GetProspectos", oConexion);
                    Conexion.creaParametro(cmd, "@Id_Sucursal", SqlDbType.Int, 1);
                    Conexion.creaParametro(cmd, "@FDesde", SqlDbType.Date, desde);
                    Conexion.creaParametro(cmd, "@FHasta", SqlDbType.Date, hasta);

                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                _prospectus.Add(new Prospectus()
                                {
                                    Id = Convert.ToInt32(dr["Id_EmpleadoP"]),
                                    Name = dr["Nombre"].ToString(),
                                    Department = dr["Departamento"].ToString(),
                                    Date = dr["Fecha_Contrato"].ToString(),
                                    IsValidatedRecruiter = (dr["ValidadoPorReclutador"].ToString() == "S") ? true : false,
                                    IsValidatedReprecentative = (dr["Validado"].ToString()=="S") ? true : false
                                });
                            }

                        }

                    }

                }
                catch (Exception ex)
                {

                }
            }

            return _prospectus;
        }


        public bool IsEmployee(int pto)
        {
            SqlCommand _cmd;
            bool _valid = false;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("IsEmpleado", oConexion);
                    Conexion.creaParametro(_cmd, "@Id", System.Data.SqlDbType.Int, pto);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (Convert.ToInt32(dr["Empleado"]) != 0)
                            {
                                _valid = true;
                            }

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }
            return _valid;
        }

        public bool ValidatePto(int pto, string stat)
        {
            SqlCommand _cmd;
            bool _resp = false;
            using (SqlConnection _Conexion = new SqlConnection(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("ValidarProspectoPorRepresentante", _Conexion);
                    Conexion.creaParametro(_cmd, "@Id", System.Data.SqlDbType.Int, pto);
                    Conexion.creaParametro(_cmd, "@stat", System.Data.SqlDbType.VarChar, stat);
                    Conexion.creaParametro(_cmd, "@Representante", System.Data.SqlDbType.Int, User_Persistent_Data.Id);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    var _upd = Conexion.ejecutarNonquery(_cmd);
                    if (_upd > 0)
                    {
                        _resp = true;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return _resp;
        }

        public bool IsValidateProspectus(int pto)
        {
            SqlCommand _cmd;
            bool _resp = false;
            using (SqlConnection _Conexion = new SqlConnection(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("getProspectoValidadoPorRepresentante", _Conexion);
                    Conexion.creaParametro(_cmd, "@Id", System.Data.SqlDbType.Int, pto);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr["Validado"].ToString() == "S")
                            {
                                _resp = true;
                            }

                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }

            return _resp;
        }



    }
}
