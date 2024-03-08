using Modulo_Reclutamiento_Web.Models;
using System.Data;
using System.Data.SqlClient;

namespace Modulo_Reclutamiento_Web.Service
{
    public class UserService
    {
        private static UserService? instancia;

        public static UserService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new UserService();
                }

                return instancia;
            }
        }
        //
        /// <summary>
        ///Autentica al usuario que quiere entrar al sistema   <br></br>
        /// Funcion que recibe Objeto tipo <b>User</b>
        /// </summary>
        /// <param name="_user"></param>
        /// <returns>Retorna Objeto <b>Validation</b> el cual contiene si el usuario es valido o no</returns>
        public Validation AuthenticateUser(User _user)
        {
            Validation validation = new Validation();
            try
            {
                if (getUser(_user.user_Key))
                {
                    validation.IsExist = true;
                    validation.Error = "NA";
                    if (User_Persistent_Data.Status == "B")
                    {
                        validation.Error = "ERRBLOCK";
                        return validation;
                    }

                    if (_user.user_Password == null || _user.user_Password == "")
                    {
                        validation.Error = "ERRPASS";
                        return validation;
                    }
                    var pass = Tools.EncriptacionSHA1(_user.user_Password).ToUpper();
                    if (User_Persistent_Data.Password != pass)
                    {
                        validation.Error = "ERRPASS";
                        return validation;
                    }


                }
                else
                {
                    validation.IsExist = false;
                    validation.Error = "ERRINEX";
                }
            }
            catch (Exception)
            {
                validation.IsExist = false;
                validation.Error = "ERRINEX";
            }

            return validation;

        }

        //retorna si el usuario existe, despues se guardan globalmente para su uso posterior
        /// <summary>
        /// Extrae y Asigna la informacion del usuario como : Id, Clave, Nombre, Contraseña, Estatus,Sucursal, Menus, FirmaDigital
        /// </summary>
        /// <param name="user_key"></param>
        /// <returns>Retorno <b>True</b> si se asigno la informacion correctamente y <b>False</b> si no</returns>
        private bool getUser(int user_key)
        {
            // Usr_UsuariosLogin_Read
            SqlCommand cmd;
            bool resp = false;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    cmd = Conexion.creaComando("Usr_UsuariosLogin_Read", oConexion);
                    Conexion.creaParametro(cmd, "@Id_Empleado", SqlDbType.BigInt, user_key);
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                User_Persistent_Data.Id = Convert.ToInt32(dr["Id_Empleado"]);
                                User_Persistent_Data.Key = dr["Clave"].ToString();
                                User_Persistent_Data.Name = dr["Nombre"].ToString();
                                User_Persistent_Data.Password = dr["Password"].ToString();
                                User_Persistent_Data.Status = dr["Status"].ToString();
                                User_Persistent_Data.Branche = dr["Clave_Sucursal"].ToString();
                                User_Persistent_Data.menus = getMenus();
                                User_Persistent_Data.Signatures = RecruiterService.Instancia.getFirmas(Convert.ToInt32(dr["Id_Empleado"]));

                            }
                            resp = true;
                        }


                    }

                }
                catch (Exception ex)
                {

                }
            }
            return resp;
        }
        //retorna el menu  a los que se tiene acceso
        /// <summary>
        /// menus relacionados al modulo
        /// </summary>
        /// <returns>Retorna lista de menus asociado al modulo</returns>
        private List<Menu> getMenus()
        {
            SqlCommand cmd;
            List<Menu> Menus = new List<Menu>();
            List<subMenu> subMenus = new List<subMenu>();
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    cmd = Conexion.creaComando("Usr_MenusModulo_Get", oConexion);
                    Conexion.creaParametro(cmd, "@Clave_Modulo", SqlDbType.VarChar, "47");
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Menus.Add(new Menu()
                                {
                                    Id = Convert.ToInt32(dr["Id_Menu"]),
                                    Name = dr["Descripcion"].ToString(),
                                    Status = dr["Status"].ToString(),
                                    Order = dr["Orden"].ToString()
                                });
                            }

                            subMenus = getSubMenus();

                            for (int i = 0; i <= Menus.Count - 1; i++)
                            {
                                List<subMenu> subm = new List<subMenu>();
                                for (int sub = 0; sub <= subMenus.Count - 1; sub++)
                                {
                                    if (Menus[i].Id == subMenus[sub].MenuId)
                                    {
                                        subm.Add(subMenus[sub]);
                                        Menus[i].subMenus = subm;
                                    }


                                }
                            }

                        }


                    }

                }
                catch (Exception ex)
                {

                }
            }

            return Menus;
        }
        // submenus a los que se tiene acceso
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Lista de submenus</returns>
        private List<subMenu> getSubMenus()
        {
            SqlCommand cmd;
            List<subMenu> subMenus = new List<subMenu>();
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    cmd = Conexion.creaComando("Usr_Permisos_Get", oConexion);
                    Conexion.creaParametro(cmd, "@Id_Empleado", SqlDbType.BigInt, User_Persistent_Data.Id);
                    Conexion.creaParametro(cmd, "@Clave_Modulo", SqlDbType.BigInt, "47");
                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (Convert.ToInt32(dr["Id_Menu"]) == 226)
                                {
                                    var _link = dr["Enlace"].ToString();
                                    subMenus.Add(new subMenu()
                                    {
                                        MenuId = Convert.ToInt32(dr["Id_Menu"]),
                                        Name = dr["Descripcion"].ToString(),
                                        Link = _link.Replace("Id", User_Persistent_Data.Id.ToString())
                                    }); 
                                }
                                else 
                                {
                                    subMenus.Add(new subMenu()
                                    {
                                        MenuId = Convert.ToInt32(dr["Id_Menu"]),
                                        Name = dr["Descripcion"].ToString(),
                                        Link = dr["Enlace"].ToString()
                                    });
                                }
                               
                            }

                        }


                    }

                }
                catch (Exception ex)
                {

                }
            }

            return subMenus;
        }

      


    }
}
