using Microsoft.AspNetCore.Mvc.Rendering;
using Modulo_Reclutamiento_Web.Models;
using Modulo_Reclutamiento_Web.Models.GeneralData;
using Modulo_Reclutamiento_Web.Models.HomeVisitData;
using Modulo_Reclutamiento_Web.Models.MedicalQuestionData;
using System.Data;
using System.Data.SqlClient;

namespace Modulo_Reclutamiento_Web.Service
{
    public class RecruiterService
    {
        private static RecruiterService? instancia;

        public static RecruiterService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new RecruiterService();
                }

                return instancia;
            }
        }


        /// <summary>
        /// Funcion que recibe 2 fechas para delimitar el tiempo en que se buscaran los prospectos
        /// </summary>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <returns>Lista de prospectos en base a las fechas recibidas</returns>
        public List<Prospectus> get_Prospectus(DateTime desde, DateTime hasta)
        {
            List<Prospectus> _prospectus = new List<Prospectus>();
            SqlCommand cmd;

            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    cmd = Conexion.creaComando("Cat_EmpleadosP_GetProspecto", oConexion);
                    Conexion.creaParametro(cmd, "@Id_Sucursal", SqlDbType.Int, 1);
                    Conexion.creaParametro(cmd, "@Status", SqlDbType.VarChar, "A");
                    Conexion.creaParametro(cmd, "@FDesde", SqlDbType.Date, desde);
                    Conexion.creaParametro(cmd, "@FHasta", SqlDbType.Date, hasta);
                    //Conexion.creaParametro(cmd, "@Apto", SqlDbType.VarChar, "S");
                    //Conexion.creaParametro(cmd, "@Reclutador", SqlDbType.Int, User_Persistent_Data.Id);

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
                                    Key = dr["Clave"].ToString(),
                                    Name = dr["Nombre"].ToString(),
                                    Department = dr["Departamento"].ToString(),
                                    Position = dr["Puesto"].ToString(),
                                    Date = dr["FechaRegistro"].ToString(),
                                    IsValidatedRecruiter = (dr["ValidadoPorReclutador"].ToString() == "S") ? true : false
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

        /// <summary>
        /// 
        /// Funcion que recibe Id de prospecto
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Retorna lista de documentos que hay que firmar</returns>
        public List<Documents> getDocumentProspectus(int prosp)
        {
            SqlCommand cmd;
            List<Documents> _documentos = new List<Documents>();
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    cmd = Conexion.creaComando("Get_DocumentosContratos", oConexion);
                    Conexion.creaParametro(cmd, "@Id", SqlDbType.Int, prosp);

                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                AddDocument(_documentos, (dr["AvisoCandidato"].ToString() == "S") ? true : false, "Aviso Candidato", dr["Fecha"].ToString());
                                AddDocument(_documentos, (dr["ContratoLaboral"].ToString() == "S") ? true : false, "Contrato Laboral", dr["Fecha"].ToString());
                                AddDocument(_documentos, (dr["AvisoPrivacidad"].ToString() == "S") ? true : false, "Aviso Privacidad", dr["Fecha"].ToString());
                                AddDocument(_documentos, (dr["CartaConfidencialidad"].ToString() == "S") ? true : false, "Carta Confidencialidad", dr["Fecha"].ToString());
                                AddDocument(_documentos, (dr["ConvenioConfidencialidad"].ToString() == "S") ? true : false, "Convenio de Confidencialidad", dr["Fecha"].ToString());
                                AddDocument(_documentos, (dr["Concentimientos"].ToString() == "S") ? true : false, "Consentimientos", dr["Fecha"].ToString());
                            //    if (dr["Concentimientos"].ToString() == "S")
                            //    {
                            //        _documentos.Add(new Documents
                            //        {
                            //            Name = "Consentimientos",
                            //            Sign = "SI",
                            //            Date = dr["Fecha"].ToString(),
                            //            Index = 1
                            //        });
                            //    }
                            //    else
                            //    {
                            //        _documentos.Add(new Documents
                            //        {
                            //            Name = "Consentimientos",
                            //            Sign = "NO",
                            //            Date = dr["Fecha"].ToString(),
                            //            Index = 0
                            //        });
                            //    }


                            }

                        }
                        else
                        {
                            AddDocument(_documentos,false, "Aviso Candidato", DateTime.Today.ToString("yyyy-MM-dd"));
                            AddDocument(_documentos,false, "Contrato Laboral", DateTime.Today.ToString("yyyy-MM-dd"));
                            AddDocument(_documentos,false, "Aviso Privacidad", DateTime.Today.ToString("yyyy-MM-dd"));
                            AddDocument(_documentos,false, "Carta Confidencialidad", DateTime.Today.ToString("yyyy-MM-dd"));
                            AddDocument(_documentos,false, "Convenio de Confidencialidad", DateTime.Today.ToString("yyyy-MM-dd"));
                            AddDocument(_documentos,false, "Consentimientos", DateTime.Today.ToString("yyyy-MM-dd"));
                            
                            //_documentos.Add(new Documents
                            //{
                            //    Name = "Consentimientos",
                            //    Sign = "NO",
                            //    Date = DateTime.Today.ToString("yyyy-MM-dd"),
                            //    Index = 0
                            //});

                        }

                    }

                }
                catch (Exception ex)
                {
                    AddDocument(_documentos, false, "Aviso Candidato", DateTime.Today.ToString("yyyy-MM-dd"));
                    AddDocument(_documentos, false, "Contrato Laboral", DateTime.Today.ToString("yyyy-MM-dd"));
                    AddDocument(_documentos, false, "Aviso Privacidad", DateTime.Today.ToString("yyyy-MM-dd"));
                    AddDocument(_documentos, false, "Carta Confidencialidad", DateTime.Today.ToString("yyyy-MM-dd"));
                    AddDocument(_documentos, false, "Convenio de Confidencialidad", DateTime.Today.ToString("yyyy-MM-dd"));
                    AddDocument(_documentos, false, "Consentimientos", DateTime.Today.ToString("yyyy-MM-dd"));
                }
            }

            return _documentos;
        }

        private void AddDocument(List<Documents> _documentos,bool _add,string nombre,string fecha) 
        {
                _documentos.Add(new Documents
                {
                    Name = nombre,
                    Sign = (_add)?"SI":"NO",
                    Date = fecha,
                    Index =(_add)?1:0
                }); 
        }
        
        /// <summary>
        /// huellas dactilares del prospecto<br></br>
        /// Funcion que recibe Id de prospecto
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Retorna Objeto <b>FingerPrints</b> con las imagenes de las huellas dactirales del prospecto en formato base64</returns>
        public FingerPrints getFingerPrintsProspectus(int key)
        {
            FingerPrints _fingerPrints = new FingerPrints();
            SqlCommand _cmd;

            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("getHuellasProspecto", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, key);
                    Conexion.creaParametro(_cmd, "@filter", System.Data.SqlDbType.Int, 1);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            _fingerPrints.FingerPrintRight = dr["PulgarD"].ToString();
                            _fingerPrints.FingerPrintLeft = dr["PulgarI"].ToString();

                        }
                        if (!dr.HasRows)
                        {
                            _fingerPrints.FingerPrintRight = null;
                            _fingerPrints.FingerPrintRight = null;
                        }
                    }


                }
                catch (Exception ex)
                {
                    _fingerPrints.FingerPrintRight = null;
                    _fingerPrints.FingerPrintRight = null;
                }
            }

            return _fingerPrints;

        }

        /// <summary>
        /// funcion que recibe id de reclutador
        /// </summary>
        /// <param name="recruiter"></param>
        /// <returns>Retorna lista con la firmas de reprecentante legal y usuario</returns>
        public List<Signatures> getFirmas(int recruiter)
        {
            SqlCommand _cmd;
            List<Signatures> _firmas = new List<Signatures>();
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("getFirmasDocumentosContratos", oConexion);
                    Conexion.creaParametro(_cmd, "@reclutador", System.Data.SqlDbType.Int, recruiter);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        _firmas.Add(new Signatures()
                        {
                            Name = "Nombre y Firma",
                            Signature = User_Persistent_Data.imgRepPatr,
                            Position = "1-ReprecentanteLegal"
                        });
                        while (dr.Read())
                        {

                            _firmas.Add(new Signatures()
                            {
                                Name = dr["Nombre"].ToString(),
                                Signature = Tools.ConvertToBase64((byte[])dr["Firma"]),
                                Position = dr["Rol"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {
                    _firmas.Add(new Signatures()
                    {
                        Name = "Reprecentante",
                        Signature = User_Persistent_Data.imgRepPatr,
                        Position = "1-ReprecentanteLegal"
                    });
                }
            }
            return _firmas;
        }

        //devuelve la firma del reprecentante legal,reclutador y suplente
        /// <summary>
        /// Funcion que recibe clave de prospecto
        /// </summary>
        /// <param name="emp"></param>
        /// <returns>Retorna lista con las firmas del documento :<br></br>
        /// Reprentante Legal,Reclutador y usuario
        /// </returns>
        public List<Signatures> getFirmasDocuments(int pto)
        {
            SqlCommand _cmd;
            List<Signatures> _firmas = new List<Signatures>();
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("getFirmasTestigoReprecentante", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_Empleado", System.Data.SqlDbType.Int, pto);
                    Conexion.creaParametro(_cmd, "@filter", System.Data.SqlDbType.Int, 1);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            _firmas.Add(new Signatures()
                            {
                                Name = dr["Nombre"].ToString(),
                                Signature = Tools.ConvertToBase64((byte[])dr["Firma"]),
                                Position = dr["Rol"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }
            return _firmas;
        }

        /// <summary>
        /// Reinicio de Proceso de firma de contratos
        /// </summary>
        /// <param name="pto">Id de Prospecto</param>
        /// <returns>Retorna True si el reinicio fue exitoso</returns>
        public bool Reset_Prospectus(int pto)
        {
            SqlCommand _cmd;
            bool resp = false;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Reiniciar_Prospecto", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpledoP", System.Data.SqlDbType.Int, pto);
                    Conexion.creaParametro(_cmd, "@reclutador", System.Data.SqlDbType.Int, User_Persistent_Data.Id);
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

        //validar prospecto
        public bool Validate_Prospectus(int prosp)
        {
            SqlCommand _cmd;
            bool resp = false;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Validar_Prospecto", oConexion);
                    Conexion.creaParametro(_cmd, "@clave_prospecto", System.Data.SqlDbType.VarChar, prosp);
                    Conexion.creaParametro(_cmd, "@reclutador", System.Data.SqlDbType.Int, User_Persistent_Data.Id);
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


        #region GeneralData Functions

        /// <summary>
        /// Funcion que reprecenta una nueva solicitud de nuevo prospeto
        /// </summary>
        /// <param name="country">id de pais</param>
        /// <param name="state">id de estado</param>
        /// <param name="cityOfDom">id de ciudad</param>
        /// <param name="department">id de departamento</param>
        /// <param name="contact">tipo contanto</param>
        /// <param name="addfamily">objeto FamilyData</param>
        /// <returns>Retorna un nuevo Objeto tipo <b>ProspectusData</b></returns>
        public ProspectusData NewProspectusData(int? country = 1, int? state = 0, int? cityOfDom = 0, int? department = 0, int? contact = 0, FamilyData? addfamily = null)
        {
            ProspectusData _general = new ProspectusData();

            _general.PositionData.Departments = getDepartments();
            _general.PositionData.Department = (department != null && department != 0) ? (int)department : Convert.ToInt32(_general.PositionData.Departments[0].Value);
            _general.PositionData.Positions = getPositions(_general.PositionData.Department);
            _general.PersonalData.MaritalStatusOp = getMaritimalStatus();
            _general.PersonalData.CountriesOp = getCountries();
            _general.PersonalData.Country = (int)country;

            _general.PersonalData.StatesOp = getStates(_general.PersonalData.Country);
            _general.PersonalData.State = (state != null && state != 0) ? (int)state : Convert.ToInt32(_general.PersonalData.StatesOp[0].Value);
            _general.PersonalData.CityOfStateOp = getCities(_general.PersonalData.State);
            _general.DomicileData.Cities = getDomicileCities();
            _general.DomicileData.Colony = (cityOfDom != null && cityOfDom != 0) ? (int)cityOfDom : Convert.ToInt32(_general.DomicileData.Cities[0].Value);
            _general.DomicileData.Colonies = getColonies(_general.DomicileData.Colony);
            _general.DomicileData.Zone = (cityOfDom != null && cityOfDom != 0) ? (int)cityOfDom : Convert.ToInt32(_general.DomicileData.Cities[0].Value);
            _general.DomicileData.Zones = getZones(_general.DomicileData.Zone);
            _general.PositionData.ContactModeOp = getContactMode();
            _general.PositionData.ContactMode = (contact != null && contact != 0) ? (int)contact : Convert.ToInt32(_general.PositionData.ContactModeOp[0].Value);
            _general.PositionData.SpecifyOp = getSpecify(_general.PositionData.ContactMode);
            _general.OfIdentificationData.UMFOp = getUFMS().OrderBy(x => x.Value).ToList();
            _general.OfIdentificationData.LicenseTypeOp = getLicenseType();
            _general.EmployeeReferenceOp = getEmployeeReference();

            return _general;
        }

        public ProspectusData ValidationData(ProspectusData _data)
        {
            ProspectusData _general = _data;
            _general.PositionData.Departments = getDepartments();
            _general.PositionData.Positions = getPositions(_general.PositionData.Department);
            _general.PersonalData.MaritalStatusOp = getMaritimalStatus();
            _general.PersonalData.CountriesOp = getCountries();

            _general.PersonalData.StatesOp = getStates(_general.PersonalData.Country);

            _general.PersonalData.CityOfStateOp = getCities(_general.PersonalData.State);
            _general.DomicileData.Cities = getDomicileCities();
            _general.DomicileData.Colonies = getColonies(_general.DomicileData.Colony);
            _general.DomicileData.Zones = getZones(_general.DomicileData.Zone);
            _general.PositionData.ContactModeOp = getContactMode();

            _general.PositionData.SpecifyOp = getSpecify(_general.PositionData.ContactMode);
            _general.OfIdentificationData.UMFOp = getUFMS().OrderBy(x => x.Value).ToList();
            _general.OfIdentificationData.LicenseTypeOp = getLicenseType();
            _general.EmployeeReferenceOp = getEmployeeReference();

            return _general;
        }
        /// <summary>
        /// Recibe un Objeto ProspectusData y lo guarda en la bd
        /// </summary>
        /// <param name="_general">Objeto ProspectusData</param>
        /// <returns>Retorna True en caso de haber guardado con exito</returns>
        public bool SaveData(ProspectusData _general)
        {
            bool _saving = false;
            SqlCommand _cmd;
            SqlTransaction _tr;

            using (_tr = Conexion.creaTransaccion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("Cat_EmpleadosP_Create", _tr);
                    Conexion.creaParametro(_cmd, "@Id_Sucursal", SqlDbType.Int, 1);
                    //Conexion.creaParametro(_cmd, "@Clave_EmpleadoP", SqlDbType.VarChar, _general.PersonalData.Key);
                    var key = getNextKeyProspectus();
                    var _edad = _general.PersonalData.Age;
                    if (_edad == 0) { _edad =(DateTime.Now.Year -DateTime.Parse(_general.PersonalData.DateOfBirth).Year);    }
                    Conexion.creaParametro(_cmd, "@Clave_EmpleadoP", SqlDbType.VarChar, key);
                    Conexion.creaParametro(_cmd, "@Nombre", SqlDbType.VarChar, _general.PersonalData.FirstName + " " + _general.PersonalData.PaternalName + " " + _general.PersonalData.MaternalName);
                    Conexion.creaParametro(_cmd, "@Id_Departamento", SqlDbType.Int, _general.PositionData.Department);
                    Conexion.creaParametro(_cmd, "@Id_Puesto", SqlDbType.Int, _general.PositionData.Position);
                    Conexion.creaParametro(_cmd, "@Sexo", SqlDbType.VarChar, _general.PersonalData.Gender);
                    Conexion.creaParametro(_cmd, "@Id_EstadoCivil", SqlDbType.Int, _general.PersonalData.MaritalStatus);
                    Conexion.creaParametro(_cmd, "@Mail", SqlDbType.VarChar, Tools.IsNULL(_general.ContactData.PersonalEmail));
                    Conexion.creaParametro(_cmd, "@Usuario_Registro", SqlDbType.Int, User_Persistent_Data.Id);
                    Conexion.creaParametro(_cmd, "@Status", SqlDbType.VarChar, "A");
                    Conexion.creaParametro(_cmd, "@Catalogo", SqlDbType.VarChar, "N");
                    Conexion.creaParametro(_cmd, "@APaterno", SqlDbType.VarChar, _general.PersonalData.PaternalName);
                    Conexion.creaParametro(_cmd, "@AMaterno", SqlDbType.VarChar, _general.PersonalData.MaternalName);
                    Conexion.creaParametro(_cmd, "@Nombres", SqlDbType.VarChar, _general.PersonalData.FirstName);
                    Conexion.creaParametro(_cmd, "@Sueldo_Base", SqlDbType.Money, 0);
                    Conexion.creaParametro(_cmd, "@Calle", SqlDbType.VarChar, Tools.IsNULL(_general.DomicileData.Street));
                    Conexion.creaParametro(_cmd, "@NumeroExterior", SqlDbType.Int, _general.DomicileData.ExteriorNumber);
                    Conexion.creaParametro(_cmd, "@NumeroInterior", SqlDbType.VarChar, Tools.IsNULL(_general.DomicileData.InteriorNumber.ToString()));
                    var colonia = _general.DomicileData.Colonies = getColonies(_general.DomicileData.City);
                    var col = colonia.Where(x => x.Value == Convert.ToString(_general.DomicileData.Colony)).First();
                    Conexion.creaParametro(_cmd, "@Colonia", SqlDbType.VarChar, Tools.IsNULL(getColonies(_general.DomicileData.City).Where(x => x.Value == Convert.ToString(_general.DomicileData.Colony)).First().Text));
                    Conexion.creaParametro(_cmd, "@Zona", SqlDbType.Int, _general.DomicileData.Zone);
                    Conexion.creaParametro(_cmd, "@CodigoPostal", SqlDbType.Int, _general.DomicileData.ZipCode);
                    Conexion.creaParametro(_cmd, "@Telefono", SqlDbType.VarChar, Tools.IsNULL(_general.ContactData.Phone));
                    Conexion.creaParametro(_cmd, "@TelefonoMovil", SqlDbType.VarChar, Tools.IsNULL(_general.ContactData.MobilePhone));
                    Conexion.creaParametro(_cmd, "@FechaNacimiento", SqlDbType.DateTime, _general.PersonalData.DateOfBirth);
                    var state = getStates(_general.PersonalData.Country).Where(x => x.Value == Convert.ToString(_general.PersonalData.State)).First().Text;
                    var colony = getCities(_general.PersonalData.State).Where(x => x.Value == Convert.ToString(_general.PersonalData.CityOfState)).First().Text;
                    Conexion.creaParametro(_cmd, "@LugarNacimiento", SqlDbType.VarChar, state + "," + colony);
                    Conexion.creaParametro(_cmd, "@RFC", SqlDbType.VarChar, Tools.IsNULL(_general.OfIdentificationData.RFC));
                    Conexion.creaParametro(_cmd, "@CURP", SqlDbType.VarChar, Tools.IsNULL(_general.OfIdentificationData.CURP));
                    Conexion.creaParametro(_cmd, "@IMSS", SqlDbType.VarChar, Tools.IsNULL(_general.OfIdentificationData.IMSS));
                    Conexion.creaParametro(_cmd, "@IFE", SqlDbType.VarChar, Tools.IsNULL(_general.OfIdentificationData.VoterKey));
                    Conexion.creaParametro(_cmd, "@TipoLicencia", SqlDbType.Int, _general.OfIdentificationData.LicenseType);
                    Conexion.creaParametro(_cmd, "@FechaExpira", SqlDbType.DateTime, _general.OfIdentificationData.LincenseExpirationDate);
                    Conexion.creaParametro(_cmd, "@NumCartilla", SqlDbType.VarChar, Tools.IsNULL(_general.OfIdentificationData.BookletNumber));
                    Conexion.creaParametro(_cmd, "@Certificacion", SqlDbType.VarChar, "N");
                    Conexion.creaParametro(_cmd, "@ConFamilia", SqlDbType.VarChar, _general.PersonalData.LiveWithFamily);
                    Conexion.creaParametro(_cmd, "@CantidadHijos", SqlDbType.Int, _general.PersonalData.NumberOfChildren);
                    Conexion.creaParametro(_cmd, "@Edad", SqlDbType.Int, _edad);
                    //'Cn_Datos.Crea_Parametro(_cmd, "@FechaIngreso", SqlDbType.DateTime, FechaIngreso);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoRef", SqlDbType.Int, Tools.IsNULL(_general.EmployeeReference));
                    Conexion.creaParametro(_cmd, "@EntreCalle1", SqlDbType.VarChar, Tools.IsNULL(_general.DomicileData.BetweenStreet1));
                    Conexion.creaParametro(_cmd, "@EntreCalle2", SqlDbType.VarChar, Tools.IsNULL(_general.DomicileData.BetweenStreet2));
                    Conexion.creaParametro(_cmd, "@ModoNacionalidad", SqlDbType.Int, Tools.IsNULL(_general.PersonalData.NationalityMode));
                    Conexion.creaParametro(_cmd, "@PaisNacimiento", SqlDbType.VarChar, _general.PersonalData.Country);
                    Conexion.creaParametro(_cmd, "@FechaNaturalizacion", SqlDbType.Date, _general.PersonalData.Naturalizationday);
                    Conexion.creaParametro(_cmd, "@Pasaporte", SqlDbType.VarChar, Tools.IsNULL(_general.OfIdentificationData.Passport));
                    Conexion.creaParametro(_cmd, "@Jefe_Area", SqlDbType.VarChar, "N");
                    Conexion.creaParametro(_cmd, "@Latitud", SqlDbType.Decimal, 0); ;
                    Conexion.creaParametro(_cmd, "@UMF", SqlDbType.VarChar, _general.OfIdentificationData.UMF);
                    Conexion.creaParametro(_cmd, "@Estacion_Registro", SqlDbType.VarChar, "Reclutamiento Web");
                    Conexion.creaParametro(_cmd, "@Id_ModoContactoD", SqlDbType.VarChar, Tools.IsNULL(_general.PositionData.ContactMode));
                    Conexion.creaParametro(_cmd, "@Sale_Ruta", SqlDbType.VarChar, "N");
                    Conexion.creaParametro(_cmd, "@Credito_Infonavit", SqlDbType.VarChar, _general.InfonaCredit.CreditInfonavit);
                    Conexion.creaParametro(_cmd, "@Verifica_Servicios", SqlDbType.VarChar, "N");
                    Conexion.creaParametro(_cmd, "@Ciudad_Nacimiento", SqlDbType.Int, Tools.IsNULL(_general.PersonalData.CityOfState));
                    Conexion.creaParametro(_cmd, "@Numero_Credito", SqlDbType.VarChar, Tools.IsNULL(_general.InfonaCredit.CreditNumber));
                    Conexion.creaParametro(_cmd, "@Tipo_Descuento", SqlDbType.VarChar, Tools.IsNULL(_general.InfonaCredit.DiscountType));
                    Conexion.creaParametro(_cmd, "@Monto_Descuento", SqlDbType.Decimal, Tools.IsNULL(_general.InfonaCredit.DiscountedAmount));
                    Conexion.creaParametro(_cmd, "@NumeroLicencia", SqlDbType.VarChar, Tools.IsNULL(_general.OfIdentificationData.LicenseNumber));
                    Conexion.creaParametro(_cmd, "@NumeroLicenciaFed", SqlDbType.VarChar, Tools.IsNULL(_general.OfIdentificationData.FederalLicenseNumber));
                    Conexion.creaParametro(_cmd, "@IdTributario", SqlDbType.VarChar, Tools.IsNULL(_general.OfIdentificationData.TaxId));

                    var insert = Conexion.ejecutarNonquery(_cmd);

                    if (insert > 0)
                    {
                        _cmd.Parameters.Clear();
                        _cmd = Conexion.creaComando("Cat_Sucursales_IncrementaEmpleadoP", _tr);
                        Conexion.creaParametro(_cmd, "@Id_Sucursal", SqlDbType.Int, 1);
                        Conexion.creaParametro(_cmd, "@Ult_EmpleadoP", SqlDbType.Int, Convert.ToInt32(key));
                        var increment = Conexion.ejecutarNonquery(_cmd);
                        if (increment > 0)
                        {
                            _tr.Commit();
                            _saving = true;
                        }
                        else
                        {
                            _tr.Rollback();
                        }

                    }
                    else
                    {
                        _tr.Rollback();
                    }
                }
                catch (Exception)
                {
                    _tr.Rollback();
                    throw;
                }

            }

            return _saving;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Retorna una nueva Clave Prospecto</returns>
        private string getNextKeyProspectus()
        {
            string _key = null;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_ParametrosL_Read", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_Sucursal", System.Data.SqlDbType.Int, 1);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            var key = Convert.ToInt32(dr["Ult_EmpleadoP"].ToString()) + 1;
                            _key = key.ToString();

                        }
                        if (!dr.HasRows)
                        {
                            _key = "0";
                        }
                    }


                }
                catch (Exception ex)
                {
                    _key = "0";
                }
            }


            return _key;
        }

        #region DesiredWork
        public List<SelectListItem> getDepartments()
        {
            List<SelectListItem> _departments = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_DepartamentosCombo_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Status", System.Data.SqlDbType.VarChar, "A");
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _departments.Add(new SelectListItem
                            {
                                Text = dr["Descripcion"].ToString(),
                                Value = dr["Id_Departamento"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }


            return _departments;
        }
        public List<SelectListItem> getPositions(int _department)
        {
            List<SelectListItem> _departments = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_PuestosCombo_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_Departamento", System.Data.SqlDbType.Int, _department);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _departments.Add(new SelectListItem
                            {
                                Text = dr["Descripcion"].ToString(),
                                Value = dr["Id_Puesto"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }


            return _departments;
        }
        private List<SelectListItem> getEmployeeReference()
        {
            List<SelectListItem> _employees = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosCombo_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_Sucursal", System.Data.SqlDbType.Int, 1);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _employees.Add(new SelectListItem
                            {
                                Text = dr["Nombre"].ToString(),
                                Value = dr["Id_Empleado"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }


            return _employees;
        }
        #endregion

        #region DatosPersonales
        private List<SelectListItem> getMaritimalStatus()
        {
            List<SelectListItem> _maritalStatus = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EstadoCivilCombo_Get", oConexion);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _maritalStatus.Add(new SelectListItem
                            {
                                Text = dr["Descripcion"].ToString(),
                                Value = dr["Id_EstadoCivil"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }


            return _maritalStatus;
        }
        public List<SelectListItem> getCountries()
        {
            List<SelectListItem> _countries = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_Paises_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Pista", System.Data.SqlDbType.VarChar, "");
                    Conexion.creaParametro(_cmd, "@Status", System.Data.SqlDbType.VarChar, "A");
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _countries.Add(new SelectListItem
                            {
                                Text = dr["Nombre"].ToString(),
                                Value = dr["Id_Pais"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }


            return _countries;
        }
        private List<SelectListItem> getStates(int country)
        {
            List<SelectListItem> _states = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EstadosPais_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_Pais", System.Data.SqlDbType.Int, country);
                    Conexion.creaParametro(_cmd, "@Pista", System.Data.SqlDbType.VarChar, "");
                    Conexion.creaParametro(_cmd, "@Status", System.Data.SqlDbType.VarChar, "A");
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _states.Add(new SelectListItem
                            {
                                Text = dr["Nombre"].ToString(),
                                Value = dr["Id_Estado"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }


            return _states;
        }
        private List<SelectListItem> getCities(int state)
        {
            List<SelectListItem> _cities = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_CiudadesEstado_Get", oConexion);

                    Conexion.creaParametro(_cmd, "@Pista", System.Data.SqlDbType.VarChar, "");
                    Conexion.creaParametro(_cmd, "@Id_Estado", System.Data.SqlDbType.Int, state);
                    Conexion.creaParametro(_cmd, "@Status", System.Data.SqlDbType.VarChar, "A");
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _cities.Add(new SelectListItem
                            {
                                Text = dr["Nombre"].ToString(),
                                Value = dr["Id_Ciudad"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }


            return _cities;
        }
        #endregion

        #region Datos Domicilio
        private List<SelectListItem> getDomicileCities()
        {
            List<SelectListItem> _domiciles = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_CiudadesPais_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_Pais", System.Data.SqlDbType.Int, 1);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _domiciles.Add(new SelectListItem
                            {
                                Text = dr["Nombre"].ToString(),
                                Value = dr["Id_Ciudad"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }


            return _domiciles;
        }
        private List<SelectListItem> getColonies(int city)
        {
            List<SelectListItem> _colonies = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_ColoniasCiudades_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Pista", System.Data.SqlDbType.VarChar, "");
                    Conexion.creaParametro(_cmd, "@Id_Ciudad", System.Data.SqlDbType.Int, city);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _colonies.Add(new SelectListItem
                            {
                                Text = dr["nombre"].ToString(),
                                Value = dr["id_Colonia"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }


            return _colonies;
        }
        private List<SelectListItem> getZones(int city)
        {
            List<SelectListItem> _zones = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_ZonasCiudades_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Pista", System.Data.SqlDbType.VarChar, "");
                    Conexion.creaParametro(_cmd, "@Id_Ciudad", System.Data.SqlDbType.Int, city);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _zones.Add(new SelectListItem
                            {
                                Text = dr["Nombre"].ToString(),
                                Value = dr["Id_Zona"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }


            return _zones;
        }

        #endregion

        #region Datos Contacto
        private List<SelectListItem> getContactMode()
        {
            List<SelectListItem> _contacts = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_ModoContacto_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Tipo", System.Data.SqlDbType.Int, 2);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _contacts.Add(new SelectListItem
                            {
                                Text = dr["Descripcion"].ToString(),
                                Value = dr["Id_ModoContacto"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }


            return _contacts;
        }
        private List<SelectListItem> getSpecify(int contactmode)
        {
            List<SelectListItem> _contacts = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_ModoContactoD_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_ModoContacto", System.Data.SqlDbType.Int, contactmode);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _contacts.Add(new SelectListItem
                            {
                                Text = dr["Descripcion"].ToString(),
                                Value = dr["Id_ModoContactoD"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }


            return _contacts;
        }
        #endregion

        #region Datos de Identifiacion 
        private List<SelectListItem> getUFMS()
        {
            List<SelectListItem> _ufms = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_UMF_Get", oConexion);
                    //Conexion.creaParametro(_cmd, "@Id_ModoContacto", System.Data.SqlDbType.Int, contactmode);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _ufms.Add(new SelectListItem
                            {
                                Text = dr["UMF"].ToString() + "-" + dr["Municipio"].ToString() + "-" + dr["CP"].ToString(),
                                Value = dr["UMF"].ToString().PadLeft(3, '0')
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _ufms;
        }
        private List<SelectListItem> getLicenseType()
        {
            List<SelectListItem> _license = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_TipoLicenciaCombo_Get", oConexion);
                    //Conexion.creaParametro(_cmd, "@Id_ModoContacto", System.Data.SqlDbType.Int, contactmode);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _license.Add(new SelectListItem
                            {
                                Text = dr["Descripcion"].ToString(),
                                Value = dr["Id_TipoLicencia"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _license;
        }


        #endregion

        #region CuestionarioMedico

        public List<MedicalQuestion> GetCuestionaries(int id, int filter)
        {
            List<MedicalQuestion> _questions = new List<MedicalQuestion>();
            SqlCommand cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    
                    cmd = Conexion.creaComando("Cat_AntecedentesMedicos_Get", oConexion);
                    Conexion.creaParametro(cmd, "@Id", SqlDbType.Int, id);
                    Conexion.creaParametro(cmd, "@filter", SqlDbType.Int, filter);


                    cmd.Connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                _questions.Add(new MedicalQuestion()
                                {
                                    EmployeerId = Convert.ToInt32(dr["Id_Empleado"]),
                                    ProspectusId = Convert.ToInt32(dr["Id_Prospecto"]),
                                    Name = dr["Nombre"].ToString(),
                                    IsSigned = Convert.ToBoolean(dr["Firmado"]),
                                    personalMedicalHistoryId = Convert.ToInt32(dr["Id_AntecedenteMP"]),
                                    gynecologicalAntecentoId = Convert.ToInt32(dr["Id_AntecedenteGine"]),
                                    Date = dr["Fecha_Registro"].ToString()
                                    
                                });
                            }

                        }

                    }

                }
                catch (Exception ex)
                {

                }
            }

            return _questions;
        }

        public bool AddCuestionarie(int Id,int filter) 
        {
            MedicalQuestion medical = new MedicalQuestion();
            medical.personalMedicalHistory = new PersonalMedicalHistory();
            medical.gynecologicalAntecento = new GynecologicalAntecento();
            if (filter == 1)
            {
                medical.ProspectusId = Id;
            }
            else 
            {
                medical.EmployeerId = Id;
            }
            bool _add = ProspectusService.Instancia.SaveMedicalQuestion(medical);
          

            return _add;
        }

        #endregion

        #endregion





    }
}
