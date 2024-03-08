using Modulo_Reclutamiento_Web.Models;
using Modulo_Reclutamiento_Web.Models.GeneralData;
using Modulo_Reclutamiento_Web.Models.HomeVisitData;
using System.Data;
using System.Data.SqlClient;

namespace Modulo_Reclutamiento_Web.Service
{
    public class HomeVisitServices
    {
        private static HomeVisitServices? instancia;

        public static HomeVisitServices Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new HomeVisitServices();
                }

                return instancia;
            }
        }

     
        /// <summary>
        /// Funcion que recibe Id de Departamento y Id de Posicion
        /// </summary>
        /// <param name="department"></param>
        /// <param name="position"></param>
        /// <returns>Retorna lista de empleados</returns>
        public List<Prospectus> get_Employees(int department, int position)
        {
            List<Prospectus> _employees = new List<Prospectus>();
            SqlCommand cmd;

            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    cmd = Conexion.creaComando("Cat_Empleados_Get", oConexion);
                    Conexion.creaParametro(cmd, "@Id_Sucursal", SqlDbType.Int, 1);
                    Conexion.creaParametro(cmd, "@Pista", SqlDbType.VarChar, "");
                    Conexion.creaParametro(cmd, "@Id_Departamento", SqlDbType.Int, department);
                    Conexion.creaParametro(cmd, "@Id_Puesto", SqlDbType.Int, position);
                    Conexion.creaParametro(cmd, "@Status", SqlDbType.VarChar, "A");

                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                _employees.Add(new Prospectus()
                                {
                                    Id = Convert.ToInt32(dr["Id_Empleado"]),
                                    Key = dr["Clave"].ToString(),
                                    Name = dr["Nombre"].ToString(),
                                    Department = dr["Departamento"].ToString(),
                                    Position = dr["Puesto"].ToString()
                                });
                            }

                        }

                    }

                }
                catch (Exception ex)
                {

                }
            }

            return _employees;
        }
        /// <summary>
        /// Funcion que recibe Id de Empleado
        /// </summary>
        /// <param name="emp"></param>
        /// <returns>Retorna informacion basica de empleado :
        /// No. de Nomina, Nombres, ApellidoPaterno, ApellidoMaterno, Edad, Puesto, Departamento, Domicilio, 
        /// Colonia, Ciudad, Estado, Telefono, TelefonoMovil, Antiguedad, Lugar de Nacimiento, Fecha Nacimiento,
        /// EstadoCivil,Conyugue,TelefonoEmergencia
        /// </returns>
        public HomeVisit getBasicInformationEmployee(int emp)
        {
            HomeVisit _homeVisit = new HomeVisit();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("getVisitaDom_InformacionEmpleado", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_Empleado", System.Data.SqlDbType.Int, emp);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            _homeVisit.PayrollNumber = dr["Clave_Empleado"].ToString();
                            _homeVisit.PaternalName = dr["APaterno"].ToString();
                            _homeVisit.MaternalName = dr["AMaterno"].ToString();
                            _homeVisit.FirstName = dr["Nombres"].ToString();
                            _homeVisit.Age = Convert.ToInt32(dr["Edad"]);
                            _homeVisit.Position = dr["Puesto"].ToString();
                            _homeVisit.Department = dr["Departamento"].ToString();
                            _homeVisit.Domicile = dr["Domicilio"].ToString();
                            _homeVisit.Colony = dr["Colonia"].ToString();
                            _homeVisit.City = dr["Ciudad"].ToString();
                            _homeVisit.State = dr["Estado"].ToString();
                            _homeVisit.Phone = dr["Telefono"].ToString();
                            _homeVisit.CellPhone = dr["Telefono_Movil"].ToString();
                            _homeVisit.Seniority = dr["Antiguedad"].ToString();
                            _homeVisit.BornLocation = dr["Lugar_Nacimiento"].ToString();
                            _homeVisit.BornDay = dr["Fecha_Nacimiento"].ToString();
                            _homeVisit.MaritalStatus = dr["EstadoCivil"].ToString();

                            _homeVisit.Spouse = dr["Conyugue"].ToString();
                            _homeVisit.EmergencyPhone = dr["TelefonoEmergencia"].ToString();
                        }


                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _homeVisit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pto"></param>
        /// <param name="stat"></param>
        /// <param name="idvisit"></param>
        /// <returns></returns>
        public HomeVisit getBasicInformationProspectus(int pto, int idvisit)
        {
            HomeVisit _homeVisit = new HomeVisit();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("getVisitaDom_InformacionProspecto", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.VarChar, pto);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            _homeVisit.PayrollNumber = dr["Clave_EmpleadoP"].ToString();
                            _homeVisit.PaternalName = dr["APaterno"].ToString();
                            _homeVisit.MaternalName = dr["AMaterno"].ToString();
                            _homeVisit.FirstName = dr["Nombres"].ToString();
                            _homeVisit.Age = Convert.ToInt32(dr["Edad"]);
                            _homeVisit.Position = dr["Puesto"].ToString();
                            _homeVisit.Department = dr["Departamento"].ToString();
                            _homeVisit.Domicile = dr["Domicilio"].ToString();
                            _homeVisit.Colony = dr["Colonia"].ToString();
                            _homeVisit.City = dr["Ciudad"].ToString();
                            _homeVisit.State = dr["Estado"].ToString();
                            _homeVisit.Phone = dr["Telefono"].ToString();
                            _homeVisit.CellPhone = dr["Telefono_Movil"].ToString();
                            _homeVisit.Seniority = dr["Antiguedad"].ToString();
                            _homeVisit.BornLocation = dr["Lugar_Nacimiento"].ToString();
                            _homeVisit.BornDay = dr["Fecha_Nacimiento"].ToString();
                            _homeVisit.MaritalStatus = dr["EstadoCivil"].ToString();

                            _homeVisit.Spouse = dr["Conyugue"].ToString();
                            _homeVisit.EmergencyPhone = dr["TelefonoEmergencia"].ToString();
                        }


                    }

                    _cmd.Parameters.Clear();
                    _cmd = Conexion.creaComando("getVisitaDomxProspecto", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, pto);
                    Conexion.creaParametro(_cmd, "@Reclutador", System.Data.SqlDbType.Int, Convert.ToString(User_Persistent_Data.Id));
                    Conexion.creaParametro(_cmd, "@idVisita", System.Data.SqlDbType.Int, idvisit);
                    //_cmd.Connection.Open();
                    using (SqlDataReader drinfad = _cmd.ExecuteReader())
                    {
                        if (drinfad.HasRows) 
                        {
                            while (drinfad.Read())
                            {
                                _homeVisit.IdHomeVisit = idvisit;
                                _homeVisit.DayOfVisit = drinfad["FechaVisita"].ToString();
                                _homeVisit.VisitingTime = drinfad["HoraVisita"].ToString();
                                _homeVisit.RecruiterName = drinfad["UsuarioVisita"].ToString();
                                _homeVisit.RecruiterPosition = drinfad["Puesto"].ToString();
                            }
                          
                        }
                        else
                        {
                            _homeVisit.DayOfVisit = DateTime.Now.ToString("yyyy-MM-dd");
                            _homeVisit.VisitingTime = DateTime.Now.TimeOfDay.ToString("HH:mm:ss");
                        }
                    }


                }
                catch (Exception ex)
                {
                    

                }
            }

            return _homeVisit;
        }
        #region VisitasDom
        /// <summary>
        /// Funcion que recibe id de empleado ó prospecto y un filtro para identificar cual es 
        /// (filter: 1-Prospecto, 2-Empleado)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        /// <returns>Retorna lista de visitas domiciliarias</returns>
        public List<HomeVisit> GetVisits(int id, int filter)
        {
            List<HomeVisit> _visits = new List<HomeVisit>();
            SqlCommand cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    //prospecto
                    if (filter == 1)
                    {
                        cmd = Conexion.creaComando("Cat_EmpleadosVisitas_GetxProspecto", oConexion);
                        Conexion.creaParametro(cmd, "@Id_EmpleadoP", SqlDbType.Int, id);
                    }
                    //cualquier otro empleado
                    else
                    {
                        cmd = Conexion.creaComando("Cat_EmpleadosVisitas_GetxEmpleado", oConexion);
                        Conexion.creaParametro(cmd, "@Id_Empleado", SqlDbType.Int, id);
                    }


                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                _visits.Add(new HomeVisit()
                                {
                                    IdHomeVisit = Convert.ToInt32(dr["Id_Visita"]),
                                    DayOfVisit = dr["FechaVisita"].ToString(),
                                    VisitingTime = dr["HoraVisita"].ToString(),
                                    RecruiterName = dr["UsuarioVisita"].ToString()

                                });
                            }

                        }

                    }

                }
                catch (Exception ex)
                {

                }
            }

            return _visits;
        }
        /// <summary>
        /// Funcion que recibe objeto HomeVisit y registra una nueva vista domiciliaria
        /// </summary>
        /// <param name="home"></param>
        /// <returns>Retorna True en caso de registrar exitosamente la visita domiciliaria y False en caso contrario</returns>
        public bool AddNewVisit(HomeVisit home)
        {
            SqlCommand _cmd;
            bool _Add = false;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosVisitas_Create", oConexion);
                    //Conexion.creaParametro(_cmd, "@clave_prospecto", System.Data.SqlDbType.VarChar, prosp);
                    Conexion.creaParametro(_cmd, "@Id_Empleado", SqlDbType.Int, (home.ProspOrEmpl == "E") ? home.IdProspOrEmpl : 0);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", SqlDbType.Int, (home.ProspOrEmpl == "P") ? home.IdProspOrEmpl : 0);
                    Conexion.creaParametro(_cmd, "@Fecha_Visita", SqlDbType.Date, home.DayOfVisit);
                    Conexion.creaParametro(_cmd, "@Hora_Visita", SqlDbType.Time, home.VisitingTime);
                    Conexion.creaParametro(_cmd, "@Usuario_Visita", SqlDbType.Int, User_Persistent_Data.Id);
                    Conexion.creaParametro(_cmd, "@Motivo_Visita", SqlDbType.Char, home.ReasonToVisit);
                    Conexion.creaParametro(_cmd, "@Observaciones", SqlDbType.Text, "");
                    Conexion.creaParametro(_cmd, "@UsuarioProx_Visita", SqlDbType.Int, 0);
                    Conexion.creaParametro(_cmd, "@FechaProx_Visita", SqlDbType.Date, DateTime.Now);
                    Conexion.creaParametro(_cmd, "@HoraProx_Visita", SqlDbType.Time, DateTime.Now.ToString("HH:mm"));
                    Conexion.creaParametro(_cmd, "@MotivoProx_Visita", SqlDbType.Char, "");
                    Conexion.creaParametro(_cmd, "@Usuario_Registro", SqlDbType.Int, User_Persistent_Data.Id);
                    Conexion.creaParametro(_cmd, "@Estacion_Registro", SqlDbType.Char, "RECLUTAMIENTO WEBS");
                    Conexion.creaParametro(_cmd, "@Status   ", SqlDbType.Char, "A");
                    _cmd.Connection.Open();

                    int reset = _cmd.ExecuteNonQuery();
                    if (reset > 0)
                    {
                        _Add = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }
            return _Add;
        }
        /// <summary>
        ///Funcion que recibe 3 parametros Id(claveprospecto),filter( 1-(Prospecto) ó 2-(Empleado) )
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        /// <param name="stat"></param>
        /// <returns>Retorna Objeto HomeVisit lleno o vacio dependiendo de si existe informacion ya capturada</returns>
        public HomeVisit NewHomeVisit(int id, int idvisit, string filter)
        {
            HomeVisit homeVisit = null;

            if (filter == "P"){ homeVisit = getBasicInformationProspectus(id, idvisit); }
            else if (filter == "E"){ homeVisit = getBasicInformationEmployee(id); }

            homeVisit.IdHomeVisit = idvisit;
            homeVisit.IncomeNExpenses = getIncomeNExpensesProspectus(homeVisit.IdHomeVisit);
            homeVisit.IncomeNExpenses.ResearchParam = new ResearchParam() { prosp = id, idvisit = idvisit, filter = ((filter == "P") ? 1 : 2), stat = ((homeVisit.IncomeNExpenses.Id == 0) ? "SV" : "UP") };
            homeVisit.IncomeNExpenses.FamilyMemberLiveAtHome = getFamilymembersLiveInHome(id, filter);
            homeVisit.IncomeNExpenses.MonthlyIncomeDistributions = GetMonthlyIncomeDistributions(homeVisit.IncomeNExpenses.Id);
            homeVisit.NeighborhoodReference.Id = homeVisit.IdHomeVisit;
            homeVisit.NeighborhoodReference.NeighborhoodReferenceList = getNeighbornhoodsRerefences(homeVisit.IdHomeVisit);
            homeVisit.DescriptionOfTheHouseRoom = getDescriptionOfTheHouseRoom(homeVisit.IdHomeVisit);
            homeVisit.DescriptionOfTheHouseRoom.ResearchParam = new ResearchParam() { prosp = id, idvisit = idvisit, filter = ((filter == "P") ? 1 : 2), stat = ((homeVisit.DescriptionOfTheHouseRoom.Id == 0) ? "SV" : "UP") };

            return homeVisit;

        }

        #endregion
        #region Ingresos y Egresos
        /// <summary>
        /// Funcion que recibe Id de visita domiciliaria
        /// </summary>
        /// <param name="idvisit"></param>
        /// <returns>Retorna objeto con la informacion de Ingresos y Egresos relacionado a la visita domiciliaria de un Prospecto, en caso de no encontrar informacion retorna objeto vacio</returns>
        public IncomeNExpenses getIncomeNExpensesProspectus(int idvisit)
        {
            IncomeNExpenses incomeNExpenses = new IncomeNExpenses();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosP_IngresosEgresos_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_VisitaDom", System.Data.SqlDbType.VarChar, idvisit);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            incomeNExpenses.Id = Convert.ToInt32(dr["Id_IngresosEgresos"]);
                            incomeNExpenses.LongTermBorrowingPlan = dr["PlanEndeudamiento"].ToString();
                            incomeNExpenses.LongTermBorrowingPlanExplication = dr["PlanEndeuDescripcion"].ToString();
                            incomeNExpenses.HaveDebts = dr["TieneDeuda"].ToString();
                            incomeNExpenses.DebtsAmount = Convert.ToInt32(dr["CantidadDeuda"]);
                            incomeNExpenses.IncomeAmount = Convert.ToInt32(dr["Ingresos"]);
                            incomeNExpenses.IncomeDedicatedToSavings = dr["IngresoDedicadoAhorro"].ToString();
                            incomeNExpenses.NumberOfPeopleContributeToFamilyEconomy = Convert.ToInt32(dr["NoPersAportaEconomicamente"]);
                            incomeNExpenses.NumberEconomicDependents = Convert.ToInt32(dr["NoPerDependienteEconomico"]);
                            incomeNExpenses.FamilyMemberWorksInPoliceOrSecurityCorp = dr["FamTrabCorpPoliOPriv"].ToString();
                            incomeNExpenses.CreditCardHandle = dr["ManejaTDC"].ToString();
                            incomeNExpenses.HaveOwnCar = dr["AutoPropio"].ToString();
                            incomeNExpenses.HaveOwnCarBrand = dr["Marca"].ToString();
                            incomeNExpenses.HaveOwnCarModel = dr["Modelo"].ToString();
                            incomeNExpenses.HaveProperties = dr["TienePropiedad"].ToString();
                            incomeNExpenses.PropertiesLocation = dr["UbicacionPropiedad"].ToString();
                        }


                    }


                }
                catch (Exception ex)
                {

                }
            }
            return incomeNExpenses;
        }
        /// <summary>
        /// Funcion que recibe Id de visita domiciliaria
        /// </summary>
        /// <param name="pto"></param>
        /// <returns>Retorna objeto con la informacion de Ingresos y Egresos relacionado a la visita domiciliaria de un Empleado, en caso de no encontrar informacion retorna objeto vacio</returns>
        public IncomeNExpenses getIncomeNExpensesEmployee(int pto)
        {
            IncomeNExpenses incomeNExpenses = new IncomeNExpenses();

            return incomeNExpenses;
        }
        /// <summary>
        /// Funcion que recibe id de IngresosEgresos
        /// </summary>
        /// <param name="Id_Incomes"></param>
        /// <returns>Retorna lista de la distribucion de los ingresos mensuales</returns>
        public List<MonthlyIncomeDistribution> GetMonthlyIncomeDistributions(int Id_Incomes)
        {
            List<MonthlyIncomeDistribution> monthlyIncomeDistribution = new List<MonthlyIncomeDistribution>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("Cat_EmpleadosPDistribucion_IngresoMensuales_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_IngresosEgresos", System.Data.SqlDbType.Int, Id_Incomes);
                    _cmd.Connection.Open();
                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            monthlyIncomeDistribution.Add(new MonthlyIncomeDistribution
                            {
                                Id = Convert.ToInt32(dr["Id_IngresosEgresos"]),
                                Income = dr["Ingreso"].ToString(),
                                Quantity = Convert.ToDecimal(dr["Cantidad"])

                            });
                        }

                        if (!dr.HasRows)
                        {
                            addMonthlyIncomeItems(monthlyIncomeDistribution);
                        }
                    }
                }
                catch (Exception)
                {
                    addMonthlyIncomeItems(monthlyIncomeDistribution);
                    //throw;
                }
            }

            return monthlyIncomeDistribution;
        }

        /// <summary>
        /// Funcion que recibe objeto List monthlyIncomeDistribution vacio para llenarlo con ingresos mensuales en 0
        /// </summary>
        /// <param name="monthlyIncomeDistribution"></param>
        private void addMonthlyIncomeItems(List<MonthlyIncomeDistribution> monthlyIncomeDistribution)
        {
            monthlyIncomeDistribution.Add(new MonthlyIncomeDistribution
            {
                Id = 0,
                Income = "Renta",
                Quantity = 0

            });
            monthlyIncomeDistribution.Add(new MonthlyIncomeDistribution
            {
                Id = 0,
                Income = "Agua",
                Quantity = 0

            });
            monthlyIncomeDistribution.Add(new MonthlyIncomeDistribution
            {
                Id = 0,
                Income = "Luz",
                Quantity = 0

            });
            monthlyIncomeDistribution.Add(new MonthlyIncomeDistribution
            {
                Id = 0,
                Income = "Alimentacion",
                Quantity = 0

            });
            monthlyIncomeDistribution.Add(new MonthlyIncomeDistribution
            {
                Id = 0,
                Income = "Transporte",
                Quantity = 0

            });
            monthlyIncomeDistribution.Add(new MonthlyIncomeDistribution
            {
                Id = 0,
                Income = "Escuela",
                Quantity = 0

            });
            monthlyIncomeDistribution.Add(new MonthlyIncomeDistribution
            {
                Id = 0,
                Income = "Telefono",
                Quantity = 0

            });
            monthlyIncomeDistribution.Add(new MonthlyIncomeDistribution
            {
                Id = 0,
                Income = "Distracciones",
                Quantity = 0

            });
            monthlyIncomeDistribution.Add(new MonthlyIncomeDistribution
            {
                Id = 0,
                Income = "Otros",
                Quantity = 0

            });


        }

        /// <summary>
        /// Funcion que recibe clave de empleado ó prospecto y tipo para identidicarlo (tipo: P-Prospecto, E-Empleado)
        /// </summary>
        /// <param name="prosp"></param>
        /// <param name="type"></param>
        /// <returns>Retorna lista de familiares que viven en el mismo domicilio</returns>
        public List<FamilyList> getFamilymembersLiveInHome(int pto, string type)
        {
            List<FamilyList> _familylist = new List<FamilyList>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("Cat_EmpleadosPFamiliares_VisDom", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, pto);
                    Conexion.creaParametro(_cmd, "@Tipo", System.Data.SqlDbType.VarChar, type);
                    _cmd.Connection.Open();
                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            _familylist.Add(new FamilyList
                            {
                                FullName = dr["Nombre"].ToString(),
                                Kinship = dr["Parentesco"].ToString(),
                                EconomicDependent = dr["Dependiente"].ToString()

                            }); ;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return _familylist;
        }

        /// <summary>
        /// Funcion que recibe <u>objeto</u> <b>IncomeNExpenses</b>, registra los datos de "Ingresos y Egresos" de la visita domiciliaria
        /// </summary>
        /// <param name="income"></param>
        /// <returns>Retorna <b>True</b> en caso de <u>guardar</u> <u>correctamente</u> en caso contrario <b>False</b></returns>
        public bool SaveIncomeNExpenses(IncomeNExpenses income)
        {
            SqlCommand _cmd;
            SqlTransaction _tr;
            bool _save = false;

            using (_tr = Conexion.creaTransaccion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("Cat_EmpleadosP_IngresosEgresos_Add", _tr);
                    Conexion.creaParametro(_cmd, "@Id_VisitaDom", SqlDbType.Int, income.ResearchParam.idvisit);
                    Conexion.creaParametro(_cmd, "@PlanEndeudamiento", SqlDbType.VarChar, income.LongTermBorrowingPlan);
                    Conexion.creaParametro(_cmd, "@PlanEndeuDescripcion", SqlDbType.VarChar,Tools.IsNULL(income.LongTermBorrowingPlanExplication));
                    Conexion.creaParametro(_cmd, "@TieneDeuda", SqlDbType.VarChar, income.HaveDebts);
                    Conexion.creaParametro(_cmd, "@CantidadDeuda", SqlDbType.Money, income.DebtsAmount);
                    Conexion.creaParametro(_cmd, "@Ingresos", SqlDbType.Decimal, income.IncomeAmount);
                    Conexion.creaParametro(_cmd, "@IngresoDedicadoAhorro", SqlDbType.Decimal, income.IncomeDedicatedToSavings);
                    Conexion.creaParametro(_cmd, "@NoPersAportaEconomicamente", SqlDbType.Int, income.NumberOfPeopleContributeToFamilyEconomy);
                    Conexion.creaParametro(_cmd, "@NoPerDependienteEconomico", SqlDbType.Int, income.NumberEconomicDependents);
                    Conexion.creaParametro(_cmd, "@FamTrabCorpPoliOPriv", SqlDbType.VarChar, income.FamilyMemberWorksInPoliceOrSecurityCorp);
                    Conexion.creaParametro(_cmd, "@ManejaTDC", SqlDbType.VarChar,Tools.IsNULL(income.CreditCardHandle));
                    Conexion.creaParametro(_cmd, "@AutoPropio", SqlDbType.VarChar, income.HaveOwnCar);
                    Conexion.creaParametro(_cmd, "@Marca", SqlDbType.VarChar,Tools.IsNULL(income.HaveOwnCarBrand));
                    Conexion.creaParametro(_cmd, "@Modelo", SqlDbType.VarChar,Tools.IsNULL(income.HaveOwnCarModel));
                    Conexion.creaParametro(_cmd, "@TienePropiedad", SqlDbType.VarChar, income.HaveProperties);
                    Conexion.creaParametro(_cmd, "@UbicacionPropiedad", SqlDbType.VarChar,Tools.IsNULL(income.PropertiesLocation));
                    Conexion.creaParametro(_cmd, "@Fecha_Registra", SqlDbType.DateTime, DateTime.Now.ToLongDateString());
                    Conexion.creaParametro(_cmd, "@Usuario_Registra", SqlDbType.Decimal, User_Persistent_Data.Id);
                    Conexion.creaParametro(_cmd, "@Estacion_Registra", SqlDbType.VarChar, "REclutamiento Web");
                    var IdIngresosEgresos = Conexion.ejecutaScalar(_cmd);
                    if (Convert.ToInt32(IdIngresosEgresos) != 0)
                    {
                        var orden = 1;
                        var mothlyIncome = income.MonthlyIncomeDistributions;
                        for (int i = 0; i < mothlyIncome.Count; i++)
                        {
                            _cmd.Parameters.Clear();
                            _cmd = Conexion.creaComando("Cat_EmpleadosDistribucion_IngresoMensuales_AddUpd", _tr);
                            Conexion.creaParametro(_cmd, "@Id_IngresosEgresos", SqlDbType.Int, IdIngresosEgresos);
                            Conexion.creaParametro(_cmd, "@Ingreso", SqlDbType.VarChar, mothlyIncome[i].Income);
                            Conexion.creaParametro(_cmd, "@Cantidad", SqlDbType.Decimal, mothlyIncome[i].Quantity);
                            Conexion.creaParametro(_cmd, "@Orden", SqlDbType.Int, orden);
                            var insert = Conexion.ejecutarNonquery(_cmd);
                            if (insert > 0)
                            {
                                orden++;
                            }
                        }
                      
                        if (orden == 10)
                        {
                            _tr.Commit();
                            _save = true;
                        }
                        else
                        {
                            _tr.Rollback();
                        }
                    }
                }
                catch (Exception)
                {
                    _tr.Rollback();
                    //throw;
                }
            }

            return _save;
        }
        /// <summary>
        /// Funcion que recibe <u>objeto</u> <b>IncomeNExpenses</b>, actualiza los datos de "Ingresos y Egresos" de la visita domiciliaria
        /// </summary>
        /// <param name="income"></param>
        /// <returns>Retorna <b>True</b> en caso de <u>actualizar</u> <u>correctamente</u> en caso contrario <b>False</b></returns>
        public bool UpdateIncomeNExpenses(IncomeNExpenses income)
        {
            SqlCommand _cmd;
            SqlTransaction _tr;
            bool _save = false;

            using (_tr = Conexion.creaTransaccion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("Cat_EmpleadosP_IngresosEgresos_Update", _tr);
                    Conexion.creaParametro(_cmd, "@Id_IngresosEgresos", SqlDbType.Int, income.Id);
                    Conexion.creaParametro(_cmd, "@Id_VisitaDom", SqlDbType.Int, income.ResearchParam.idvisit);
                    Conexion.creaParametro(_cmd, "@PlanEndeudamiento", SqlDbType.VarChar, income.LongTermBorrowingPlan);
                    Conexion.creaParametro(_cmd, "@PlanEndeuDescripcion", SqlDbType.VarChar,Tools.IsNULL(income.LongTermBorrowingPlanExplication));
                    Conexion.creaParametro(_cmd, "@TieneDeuda", SqlDbType.VarChar, income.HaveDebts);
                    Conexion.creaParametro(_cmd, "@CantidadDeuda", SqlDbType.Money, income.DebtsAmount);
                    Conexion.creaParametro(_cmd, "@Ingresos", SqlDbType.Decimal, income.IncomeAmount);
                    Conexion.creaParametro(_cmd, "@IngresoDedicadoAhorro", SqlDbType.Decimal, income.IncomeDedicatedToSavings);
                    Conexion.creaParametro(_cmd, "@NoPersAportaEconomicamente", SqlDbType.Int, income.NumberOfPeopleContributeToFamilyEconomy);
                    Conexion.creaParametro(_cmd, "@NoPerDependienteEconomico", SqlDbType.Int, income.NumberEconomicDependents);
                    Conexion.creaParametro(_cmd, "@FamTrabCorpPoliOPriv", SqlDbType.VarChar, income.FamilyMemberWorksInPoliceOrSecurityCorp);
                    Conexion.creaParametro(_cmd, "@ManejaTDC", SqlDbType.VarChar,Tools.IsNULL(income.CreditCardHandle));
                    Conexion.creaParametro(_cmd, "@AutoPropio", SqlDbType.VarChar, income.HaveOwnCar);
                    Conexion.creaParametro(_cmd, "@Marca", SqlDbType.VarChar, Tools.IsNULL(income.HaveOwnCarBrand));
                    Conexion.creaParametro(_cmd, "@Modelo", SqlDbType.VarChar,Tools.IsNULL(income.HaveOwnCarModel));
                    Conexion.creaParametro(_cmd, "@TienePropiedad", SqlDbType.VarChar, income.HaveProperties);
                    Conexion.creaParametro(_cmd, "@UbicacionPropiedad", SqlDbType.VarChar, Tools.IsNULL(income.PropertiesLocation));
                    Conexion.creaParametro(_cmd, "@Usuario_Actualiza", SqlDbType.Decimal, User_Persistent_Data.Id);
                    Conexion.creaParametro(_cmd, "@Estacion_Actualiza", SqlDbType.VarChar, "REclutamiento Web");

                    var _update = Conexion.ejecutarNonquery(_cmd);
                    if ((int)_update != 0)
                    {
                        var orden = 1;
                        var mothlyIncome = income.MonthlyIncomeDistributions;
                        for (int i = 0; i < mothlyIncome.Count; i++)
                        {
                            _cmd.Parameters.Clear();
                            _cmd = Conexion.creaComando("Cat_EmpleadosDistribucion_IngresoMensuales_AddUpd", _tr);
                            Conexion.creaParametro(_cmd, "@Id_IngresosEgresos", SqlDbType.Int, income.Id);
                            Conexion.creaParametro(_cmd, "@Ingreso", SqlDbType.VarChar, mothlyIncome[i].Income);
                            Conexion.creaParametro(_cmd, "@Cantidad", SqlDbType.Decimal, mothlyIncome[i].Quantity);
                            Conexion.creaParametro(_cmd, "@Orden", SqlDbType.Int, orden);
                            var insert = Conexion.ejecutarNonquery(_cmd);
                            if (insert > 0)
                            {
                                orden++;
                            }
                        }
                     
                        if (orden == 10)
                        {
                            _tr.Commit();
                            _save = true;
                        }
                        else
                        {
                            _tr.Rollback();
                        }
                    }
                }
                catch (Exception)
                {
                    _tr.Rollback();
                    //throw;
                }
            }

            return _save;
        }

        #endregion


        #region Referencias Vecinales
        /// <summary>
        /// Funcion que recibe <u>objeto</u> <b>NeighborhoodReference</b> ,  registra los datos de "Referencia Vecinal" de la visita domiciliaria
        /// </summary>
        /// <param name="neighborhood"></param>
        /// <returns>Retorna <b>True</b> en caso de <u>guardar correctamente</u> en caso contrario <b>False</b></returns>
        public bool AddNeighbornhoodRerefences(NeighborhoodReference neighborhood)
        {
            SqlCommand _cmd;
            bool resp = false;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosVisitasRef_Create", oConexion);
                    //Conexion.creaParametro(_cmd, "@clave_prospecto", System.Data.SqlDbType.VarChar, prosp);
                    Conexion.creaParametro(_cmd, "@Id_Visita", SqlDbType.Int, neighborhood.Id);
                    Conexion.creaParametro(_cmd, "@Nombre", SqlDbType.VarChar, neighborhood.NeiborhoodName);
                    Conexion.creaParametro(_cmd, "@Tipo_Persona", SqlDbType.TinyInt,Tools.IsNULL(Convert.ToInt32(neighborhood.HowYouWouldDefineThePerson)));
                    Conexion.creaParametro(_cmd, "@Definicion_Persona", SqlDbType.VarChar, Tools.IsNULL(neighborhood.KnowWhatTheCandidateDoes));
                    Conexion.creaParametro(_cmd, "@Definicion_Familia", SqlDbType.VarChar, Tools.IsNULL(neighborhood.DefinitionOfTheFamily));
                    Conexion.creaParametro(_cmd, "@Tiempo_Conocerlo", SqlDbType.Decimal, neighborhood.YearsOfGettingToKnowFamily);
                    _cmd.Connection.Open();

                    int reset = _cmd.ExecuteNonQuery();
                    if (reset > 0)
                    {
                        resp = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }
            return resp;
        }

        public bool UpdateNeighbornhoodRerefences(NeighborhoodReference neighborhood)
        {
            SqlCommand _cmd;
            bool resp = false;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosVisitasRef_Update", oConexion);
                    //Conexion.creaParametro(_cmd, "@clave_prospecto", System.Data.SqlDbType.VarChar, prosp);
                    Conexion.creaParametro(_cmd, "@Id_VisitaRef", SqlDbType.Int, neighborhood.Id);
                    Conexion.creaParametro(_cmd, "@Nombre", SqlDbType.VarChar, neighborhood.NeiborhoodName);
                    Conexion.creaParametro(_cmd, "@Tipo_Persona", SqlDbType.TinyInt, Tools.IsNULL(Convert.ToInt32(neighborhood.HowYouWouldDefineThePerson)));
                    Conexion.creaParametro(_cmd, "@Definicion_Persona", SqlDbType.VarChar, Tools.IsNULL(neighborhood.KnowWhatTheCandidateDoes));
                    Conexion.creaParametro(_cmd, "@Definicion_Familia", SqlDbType.VarChar, Tools.IsNULL(neighborhood.DefinitionOfTheFamily));
                    Conexion.creaParametro(_cmd, "@Tiempo_Conocerlo", SqlDbType.Decimal, neighborhood.YearsOfGettingToKnowFamily);
                    _cmd.Connection.Open();

                    int reset = _cmd.ExecuteNonQuery();
                    if (reset > 0)
                    {
                        resp = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }
            return resp;
        }

        /// <summary>
        /// Funcion que recibe <b>Id</b> de referencia vecinal
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna <b>True</b> en caso de <u>eliminar correctamente</u> en caso contrario <b>False</b></returns>
        public bool DeleteNeighbornhoodRerefences(int id)
        {
            SqlCommand _cmd;
            bool resp = false;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosVisitasRef_Delete", oConexion);
                    //Conexion.creaParametro(_cmd, "@clave_prospecto", System.Data.SqlDbType.VarChar, prosp);
                    Conexion.creaParametro(_cmd, "@Id_VisitaRef", SqlDbType.Int, id);

                    _cmd.Connection.Open();

                    int reset = _cmd.ExecuteNonQuery();
                    if (reset > 0)
                    {
                        resp = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }
            return resp;
        }
        /// <summary>
        ///Recibe Id de visita domiciliaria
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Retorna lista con la referencias vecinales agregadas</returns>
        public List<NeighborhoodReference> getNeighbornhoodsRerefences(int id)
        {
            List<NeighborhoodReference> neighbors = new List<NeighborhoodReference>();
            SqlCommand cmd;

            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    cmd = Conexion.creaComando("Cat_EmpleadosVisitasRef_Get", oConexion);
                    Conexion.creaParametro(cmd, "@Id_Visita", SqlDbType.Int, id);

                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                neighbors.Add(new NeighborhoodReference
                                {
                                    Id = Convert.ToInt32(dr["Id_VisitaRef"]),
                                    NeiborhoodName = dr["Nombre"].ToString(),
                                    HowYouWouldDefineThePerson = dr["TipoPersona"].ToString(),
                                    DefinitionOfTheFamily = dr["DefinicionFamilia"].ToString()
                                });
                            }

                        }

                    }

                }
                catch (Exception ex)
                {

                }
            }

            return neighbors;
        }
        public NeighborhoodReference getNeighbornhoodRerefences(int id)
        {
            NeighborhoodReference neighbors = new NeighborhoodReference();
            SqlCommand cmd;

            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    cmd = Conexion.creaComando("Cat_EmpleadosVisitaRef_Get", oConexion);
                    Conexion.creaParametro(cmd, "@Id_VisitaRef", SqlDbType.Int, id);

                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                neighbors.Id = Convert.ToInt32(dr["Id_VisitaRef"]);
                                neighbors.NeiborhoodName = dr["Nombre"].ToString();
                                neighbors.HowYouWouldDefineThePerson = dr["Tipo_Persona"].ToString();
                                neighbors.KnowWhatTheCandidateDoes = dr["DefinicionPersona"].ToString(); 
                                neighbors.DefinitionOfTheFamily = dr["DefinicionFamilia"].ToString();
                                neighbors.YearsOfGettingToKnowFamily = Convert.ToDouble(dr["TiempoDeConocer"]);
                               
                            }

                        }

                    }

                }
                catch (Exception ex)
                {

                }
            }

            return neighbors;
        }
        #endregion

        #region Descripcion de Casa Habitacion

        /// <summary>
        /// Funcion que recibe el <b>Id</b> de visita Domiciliaria
        /// </summary>
        /// <param name="idvisit">Id tipo int</param>
        /// <returns>Retorna <u>objeto</u> <b>DescriptionOfTheHouseRoom</b> con <u>informacion</u> de "Descripcion de la casa Habitacion" relacionada al <b>Id_visita</b> mandado, en caso de no 
        /// encontrar <u>informacion</u> retorna <b>objeto</b> vacio</returns>
        public DescriptionOfTheHouseRoom getDescriptionOfTheHouseRoom(int idvisit)
        {
            DescriptionOfTheHouseRoom description = new DescriptionOfTheHouseRoom();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosVisitasInmueble_Read", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_Visita", System.Data.SqlDbType.VarChar, idvisit);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            //description.IdVisita = Convert.ToInt32(dr["Id_Visita"]);
                            description.Id = Convert.ToInt32(dr["Id_VisitaInmueble"]);
                            description.RoomType = dr["Tipo_Casa"].ToString();
                            description.NameOwnerOfTheProperty = dr["Propietario"].ToString();
                            description.RoomTypeOther = dr["Tipo_CasaOtro"].ToString();
                            description.TypeOfContruction = dr["Tipo_Material"].ToString();
                            description.TypeOfConstructionOther = dr["Tipo_MaterialOtro"].ToString();
                            description.NumberOfInhabitants = Convert.ToInt32(dr["Cantidad_Habitantes"]);
                            description.hasALivingRoom = (dr["Tiene_Sala"].ToString() == "S") ? true : false;
                            description.hasAKitchen = (dr["Tiene_Cocina"].ToString() == "S") ? true : false;
                            description.hasDiningRoom = (dr["Tiene_Comedor"].ToString() == "S") ? true : false;
                            description.hasAGarage = (dr["Tiene_Cochera"].ToString() == "S") ? true : false;
                            description.hasAPatio = (dr["Tiene_PatioDelantero"].ToString() == "S") ? true : false;
                            description.hasMaidsRoom = (dr["Tiene_PatioTrasero"].ToString() == "S") ? true : false;
                            description.Furniture = dr["Cantidad_Mobiliario"].ToString();
                            //description.FornitureOther = dr["Cantidad_MobiliarioOtro"].ToString();
                            description.FloorsInTheProperty = Convert.ToInt32(dr["Cantidad_Plantas"]);
                            description.NumberOfRooms = Convert.ToInt32(dr["Cantidad_Recamaras"]);
                            description.NumberOfBathrooms = Convert.ToInt32(dr["Cantidad_Baños"]);
                            description.ObservedCleanAndTidy = dr["Limpio_Ordenado"].ToString();
                            description.Comments = dr["Observaciones_Interior"].ToString();
                        }


                    }


                }
                catch (Exception ex)
                {

                }
            }
            return description;
        }
        /// <summary>
        /// Funcion que recibe <u>objeto</u> <b>DescriptionOfTheHouseRoom</b> ,  registra los datos de "Descripcion de la casa Habitacion" de la visita domiciliaria
        /// </summary>
        /// <param name="houseRoom">Reprecentacion del Objeto tipo <b>DescriptionOfTheHouseRoom</b></param>
        /// <returns>Retorna <b>True</b> en caso de <u>guardar correctamente</u> en caso contrario <b>False</b></returns>
        public bool SaveDescriptionOfTheHouseRoom(DescriptionOfTheHouseRoom houseRoom)
        {
            SqlCommand _cmd;
            bool resp = false;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("Cat_EmpleadosVisitasInmueble_Create", oConexion);
                    //Conexion.creaParametro(_cmd, "@clave_prospecto", System.Data.SqlDbType.VarChar, prosp);
                    Conexion.creaParametro(_cmd, "@Id_Visita", SqlDbType.Int, houseRoom.ResearchParam.idvisit);
                    Conexion.creaParametro(_cmd, "@Tipo_Casa", SqlDbType.TinyInt, int.Parse(houseRoom.RoomType));
                    Conexion.creaParametro(_cmd, "@Propietario", SqlDbType.VarChar, houseRoom.NameOwnerOfTheProperty);
                    Conexion.creaParametro(_cmd, "@Tipo_CasaOtro", SqlDbType.VarChar, Tools.IsNULL(houseRoom.RoomTypeOther));
                    Conexion.creaParametro(_cmd, "@Tipo_Material", SqlDbType.TinyInt, houseRoom.TypeOfContruction);
                    Conexion.creaParametro(_cmd, "@Tipo_MaterialOtro", SqlDbType.VarChar,Tools.IsNULL(houseRoom.TypeOfConstructionOther));
                    Conexion.creaParametro(_cmd, "@Cantidad_Habitantes", SqlDbType.TinyInt, houseRoom.NumberOfInhabitants);
                    Conexion.creaParametro(_cmd, "@Tiene_Sala", SqlDbType.Char, (houseRoom.hasALivingRoom) ? "S" : "N");
                    Conexion.creaParametro(_cmd, "@Tiene_Cocina", SqlDbType.Char, (houseRoom.hasAKitchen) ? "S" : "N");
                    Conexion.creaParametro(_cmd, "@Tiene_Comedor", SqlDbType.Char, (houseRoom.hasDiningRoom) ? "S" : "N");
                    Conexion.creaParametro(_cmd, "@Tiene_Cochera", SqlDbType.Char, (houseRoom.hasAGarage) ? "S" : "N");
                    Conexion.creaParametro(_cmd, "@Tiene_PatioDelantero", SqlDbType.Char, (houseRoom.hasAPatio) ? "S" : "N");
                    Conexion.creaParametro(_cmd, "@Tiene_PatioTrasero", SqlDbType.Char, (houseRoom.hasMaidsRoom) ? "S" : "N");
                    Conexion.creaParametro(_cmd, "@Cantidad_Mobiliario", SqlDbType.TinyInt, int.Parse(houseRoom.Furniture));
                    Conexion.creaParametro(_cmd, "@Cantidad_MobiliarioOtro", SqlDbType.VarChar, "");
                    Conexion.creaParametro(_cmd, "@Tipo_Mobiliario", SqlDbType.TinyInt, houseRoom.TypeOfFurniture);
                    Conexion.creaParametro(_cmd, "@Tipo_MobiliarioOtro", SqlDbType.VarChar,Tools.IsNULL(houseRoom.TypeOfFurnitureDescription));
                    Conexion.creaParametro(_cmd, "@Calidad_Mobiliario", SqlDbType.TinyInt, houseRoom.QualityOfFurnature);
                    Conexion.creaParametro(_cmd, "@Calidad_MobiliarioOtro", SqlDbType.VarChar,Tools.IsNULL(houseRoom.QualityOfFurnatureDescription));
                    Conexion.creaParametro(_cmd, "@Cantidad_Plantas", SqlDbType.TinyInt, houseRoom.FloorsInTheProperty);
                    Conexion.creaParametro(_cmd, "@Cantidad_Recamaras", SqlDbType.TinyInt, houseRoom.NumberOfRooms);
                    Conexion.creaParametro(_cmd, "@Cantidad_Banos", SqlDbType.Decimal, houseRoom.NumberOfBathrooms);
                    Conexion.creaParametro(_cmd, "@Planea_Endeudamiento", SqlDbType.Char, "");
                    Conexion.creaParametro(_cmd, "@Descripcion_Endeudamiento", SqlDbType.VarChar, "");
                    Conexion.creaParametro(_cmd, "@Endeudamiento_Actual", SqlDbType.Char, "");
                    Conexion.creaParametro(_cmd, "@Descripcion_EndeudamientoA", SqlDbType.VarChar, "");
                    Conexion.creaParametro(_cmd, "@Limpio_Ordenado", SqlDbType.Char, houseRoom.ObservedCleanAndTidy);
                    Conexion.creaParametro(_cmd, "@Observaciones_Interior", SqlDbType.Text,Tools.IsNULL(houseRoom.Comments));
                    Conexion.creaParametro(_cmd, "@Observaciones_Exterior", SqlDbType.Text,Tools.IsNULL( houseRoom.DescriptionExteriorFacade));
                    _cmd.Connection.Open();

                    int _save = _cmd.ExecuteNonQuery();
                    if (_save > 0)
                    {
                        resp = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }
            return resp;
        }
        /// <summary>
        /// Funcion que recibe <u>objeto</u> <b>DescriptionOfTheHouseRoom</b>, actualiza los datos de "Descripcion de la casa Habitacion" de la visita domiciliaria
        /// </summary>
        /// <param name="houseRoom"></param>
        /// <returns>Retorna <b>True</b> en caso de <u>actualizar</u> <u>correctamente</u> en caso contrario <b>False</b></returns>
        public bool UpdateDescriptionOfTheHouseRoom(DescriptionOfTheHouseRoom houseRoom)
        {
            SqlCommand _cmd;
            bool resp = false;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("Cat_EmpleadosVisitasInmueble_Update", oConexion);
                    //Conexion.creaParametro(_cmd, "@clave_prospecto", System.Data.SqlDbType.VarChar, prosp);
                    Conexion.creaParametro(_cmd, "@Id_VisitaInmueble", SqlDbType.Int, houseRoom.Id);
                    Conexion.creaParametro(_cmd, "@Tipo_Casa", SqlDbType.TinyInt, int.Parse(houseRoom.RoomType));
                    Conexion.creaParametro(_cmd, "@Propietario", SqlDbType.VarChar, houseRoom.NameOwnerOfTheProperty);
                    Conexion.creaParametro(_cmd, "@Tipo_CasaOtro", SqlDbType.VarChar, (houseRoom.RoomTypeOther != null) ? houseRoom.RoomTypeOther : "");
                    Conexion.creaParametro(_cmd, "@Tipo_Material", SqlDbType.TinyInt, houseRoom.TypeOfContruction);
                    Conexion.creaParametro(_cmd, "@Tipo_MaterialOtro", SqlDbType.VarChar, (houseRoom.TypeOfConstructionOther!=null)?houseRoom.TypeOfConstructionOther:"");
                    Conexion.creaParametro(_cmd, "@Cantidad_Habitantes", SqlDbType.TinyInt, houseRoom.NumberOfInhabitants);
                    Conexion.creaParametro(_cmd, "@Tiene_Sala", SqlDbType.Char, (houseRoom.hasALivingRoom) ? "S" : "N");
                    Conexion.creaParametro(_cmd, "@Tiene_Cocina", SqlDbType.Char, (houseRoom.hasAKitchen) ? "S" : "N");
                    Conexion.creaParametro(_cmd, "@Tiene_Comedor", SqlDbType.Char, (houseRoom.hasDiningRoom) ? "S" : "N");
                    Conexion.creaParametro(_cmd, "@Tiene_Cochera", SqlDbType.Char, (houseRoom.hasAGarage) ? "S" : "N");
                    Conexion.creaParametro(_cmd, "@Tiene_PatioDelantero", SqlDbType.Char, (houseRoom.hasAPatio) ? "S" : "N");
                    Conexion.creaParametro(_cmd, "@Tiene_PatioTrasero", SqlDbType.Char, (houseRoom.hasMaidsRoom) ? "S" : "N");
                    Conexion.creaParametro(_cmd, "@Cantidad_Mobiliario", SqlDbType.TinyInt, int.Parse(houseRoom.Furniture));
                    Conexion.creaParametro(_cmd, "@Cantidad_MobiliarioOtro", SqlDbType.VarChar,"");
                    Conexion.creaParametro(_cmd, "@Tipo_Mobiliario", SqlDbType.TinyInt, houseRoom.TypeOfFurniture);
                    Conexion.creaParametro(_cmd, "@Tipo_MobiliarioOtro", SqlDbType.VarChar, (houseRoom.TypeOfFurnitureDescription != null) ? houseRoom.TypeOfFurnitureDescription : "");
                    Conexion.creaParametro(_cmd, "@Calidad_Mobiliario", SqlDbType.TinyInt, houseRoom.QualityOfFurnature);
                    Conexion.creaParametro(_cmd, "@Calidad_MobiliarioOtro", SqlDbType.VarChar, (houseRoom.QualityOfFurnatureDescription != null) ? houseRoom.QualityOfFurnatureDescription : "");
                    Conexion.creaParametro(_cmd, "@Cantidad_Plantas", SqlDbType.TinyInt, houseRoom.FloorsInTheProperty);
                    Conexion.creaParametro(_cmd, "@Cantidad_Recamaras", SqlDbType.TinyInt, houseRoom.NumberOfRooms);
                    Conexion.creaParametro(_cmd, "@Cantidad_Banos", SqlDbType.Decimal, houseRoom.NumberOfBathrooms);
                    Conexion.creaParametro(_cmd, "@Planea_Endeudamiento", SqlDbType.Char, "");
                    Conexion.creaParametro(_cmd, "@Descripcion_Endeudamiento", SqlDbType.VarChar, "");
                    Conexion.creaParametro(_cmd, "@Endeudamiento_Actual", SqlDbType.Char, "");
                    Conexion.creaParametro(_cmd, "@Descripcion_EndeudamientoA", SqlDbType.VarChar, "");
                    Conexion.creaParametro(_cmd, "@Limpio_Ordenado", SqlDbType.Char, houseRoom.ObservedCleanAndTidy);
                    Conexion.creaParametro(_cmd, "@Observaciones_Interior", SqlDbType.Text, (houseRoom.Comments!=null)?houseRoom.Comments:"");
                    Conexion.creaParametro(_cmd, "@Observaciones_Exterior", SqlDbType.Text, (houseRoom.DescriptionExteriorFacade!=null)?houseRoom.DescriptionExteriorFacade:"");
                    _cmd.Connection.Open();

                    int _save = _cmd.ExecuteNonQuery();
                    if (_save > 0)
                    {
                        resp = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }
            return resp;
        }

        #endregion



    }
}
