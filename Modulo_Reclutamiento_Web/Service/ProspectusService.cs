using Microsoft.AspNetCore.Mvc.Rendering;
using Modulo_Reclutamiento_Web.Models;
using Modulo_Reclutamiento_Web.Models.GeneralData;
using Modulo_Reclutamiento_Web.Models.MedicalQuestionData;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Modulo_Reclutamiento_Web.Service
{
    public class ProspectusService
    {
        private static ProspectusService? instancia = null;

        public static ProspectusService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ProspectusService();
                }

                return instancia;
            }
        }


        public  bool IsValidatePto(int pto) 
        {
            SqlCommand _cmd;
            bool _resp = false;
            using (SqlConnection _Conexion = new SqlConnection(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("get_ProspectoValidado", _Conexion);
                    Conexion.creaParametro(_cmd, "@Id", System.Data.SqlDbType.Int, pto);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr["ValidadoPorReclutador"].ToString() =="S")
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

        public bool ValidatePto(int pto,string stat) 
        {
            SqlCommand _cmd;
            bool _resp = false;
            using (SqlConnection _Conexion = new SqlConnection(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("ValidarProspecto_Upd", _Conexion);
                    Conexion.creaParametro(_cmd, "@Id", System.Data.SqlDbType.Int, pto);
                    Conexion.creaParametro(_cmd, "@Stat", System.Data.SqlDbType.VarChar, stat);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();
                  
                    var _upd = Conexion.ejecutarNonquery(_cmd);
                    if (_upd>0)
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


        //funciones relacionadas a la firma de documentos
        #region Documentos
        //informacion general del prospecto para llenar los documentos
        public Leaflet_Information getInformation(int prosp)
        {
            SqlCommand _cmd;
            Leaflet_Information _leaflet = null;
            List<Beneficiaries> _beneficiaries = new List<Beneficiaries>();
            using (SqlConnection _Conexion = new SqlConnection(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("getProspecto_Informacion", _Conexion);
                    Conexion.creaParametro(_cmd, "@Id", System.Data.SqlDbType.Int, prosp);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            _leaflet = new Leaflet_Information()
                            {
                                Key = Convert.ToInt16(dr["Id_EmpleadoP"]),
                                Key_Prospectus = dr["Clave_EmpleadoP"].ToString(),
                                Name = dr["Nombre"].ToString(),
                                Sex = dr["Sexo"].ToString(),
                                Age = dr["Edad"].ToString(),
                                Marital_Status = dr["EstadoCivil"].ToString(),
                                CURP = dr["CURP"].ToString(),
                                RFC = dr["RFC"].ToString(),
                                Address = dr["Domicilio"].ToString(),
                                Position = dr["Puesto"].ToString(),
                                Salary = dr["Sueldo"].ToString(),
                                Signature = dr["Firma_Candidato"].ToString(),
                                IsValidatedByRepresentantive = (Convert.ToInt32(dr["Representante"]) != 0) ? true : false
                                
                            };

                            string _arrbeneficiaries = dr["Beneficiarios"].ToString();
                            if (_arrbeneficiaries != "N")
                            {
                                dynamic obj = JsonConvert.DeserializeObject(_arrbeneficiaries);

                                for (int i = 0; i < obj.Count; i++)
                                {
                                    string arr = Convert.ToString(obj[i]);
                                    string[] arr1 = arr.Split(',');
                                    _beneficiaries.Add(new Beneficiaries()
                                    {
                                        Name = (arr1[0].Contains("[")) ? arr1[0].Replace("[", "").Replace("\"", "") : arr1[0].Replace("\"", ""),
                                        Kinship = arr1[1].Replace("\"", ""),
                                        Percentage = (arr1[2].Contains("]")) ? arr1[2].Replace("]", "").Replace("\"", "") : arr1[2].Replace("\"", "")
                                    });
                                }
                                _leaflet.Beneficiaries = _beneficiaries;
                            }



                        }
                    }

                }
                catch (Exception ex)
                {
                    _leaflet = new Leaflet_Information();
                }
            }
            return _leaflet;
        }
        //revisa si el aviso de privacidad fue firmado
        public bool Check_Sign_AvisoPrivDatos(int prosp)
        {
            SqlCommand _cmd;
            bool _resp = false;
            using (SqlConnection _Conexion = new SqlConnection(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("get_AvisoPrivFirmado", _Conexion);
                    Conexion.creaParametro(_cmd, "@Id", System.Data.SqlDbType.Int, prosp);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (Convert.ToInt32(dr["firma"]) > 0)
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
        //agruega la firma al documento de privacidad de datos
        public bool Add_ProspectusSignature(string signature, int prospectus)
        {
            SqlCommand _cmd;
            bool _resp = false;
            using (SqlConnection _Conexion = new SqlConnection(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Add_FirmaProspecto", _Conexion);
                    Conexion.creaParametro(_cmd, "@firma", System.Data.SqlDbType.VarChar, signature);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, prospectus);
                    //Conexion.creaParametro(cmd, "@status", System.Data.SqlDbType.VarChar, status);
                    _cmd.Connection.Open();

                    var add = _cmd.ExecuteNonQuery();
                    if (add > 0)
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
        // confirma el documento firmado
        public bool Confirm_Document(int prospectus, int doc, string beneficiario)
        {
            SqlCommand _cmd;
            bool _resp = false;
            using (SqlConnection _Conexion = new SqlConnection(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Firmar_Documento", _Conexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, prospectus);
                    Conexion.creaParametro(_cmd, "@Documento", System.Data.SqlDbType.Int, doc);
                    if (doc == 2)
                    {
                        Conexion.creaParametro(_cmd, "@Beneficiarios", System.Data.SqlDbType.VarChar, beneficiario);
                        Conexion.creaParametro(_cmd, "@Reclutador", System.Data.SqlDbType.Int, User_Persistent_Data.Id);
                    }

                    _cmd.Connection.Open();

                    var upd = _cmd.ExecuteNonQuery();
                    if (upd > 0)
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

        public bool Confirm_MedicalQuestionnaire(int medicalCuestionnaire)
        {
            SqlCommand _cmd;
            bool _resp = false;
            using (SqlConnection _Conexion = new SqlConnection(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Firmar_CuestionarioMedico", _Conexion);
                    Conexion.creaParametro(_cmd, "@Id_Cuestionario", System.Data.SqlDbType.Int, medicalCuestionnaire);
                 
                   
                    _cmd.Connection.Open();

                    var upd = _cmd.ExecuteNonQuery();
                    if (upd > 0)
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

        public bool CuestionarioFirmado(int medicalCuestionnaire) 
        {
            bool _resp = false;
            SqlCommand _cmd;
            using (SqlConnection _Conexion = new SqlConnection(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("CuestionarioFirmado", _Conexion);
                    Conexion.creaParametro(_cmd, "@Id_Cuestionario", System.Data.SqlDbType.Int, medicalCuestionnaire);

                    _cmd.Connection.Open();
                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                _resp = Convert.ToBoolean(dr["Firmado"]);
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

        public string getSignarutaProspEmp(int id,int filter) 
        {
            string _resp = null;
            SqlCommand _cmd;
            using (SqlConnection _Conexion = new SqlConnection(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("getFirmaEmpPros", _Conexion);
                    Conexion.creaParametro(_cmd, "@Id", System.Data.SqlDbType.Int, id);
                    Conexion.creaParametro(_cmd, "@filter", System.Data.SqlDbType.Int, filter);

                    _cmd.Connection.Open();
                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                _resp = dr["Firma_Candidato"].ToString();
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
        public int getHomeVisitId(int pto,string stat) 
        {
            int _id = 0;
            SqlCommand _cmd;
            using (SqlConnection _Conexion = new SqlConnection(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("getVisitaDom_IdVisita", _Conexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, pto);

                    _cmd.Connection.Open();
                    using (SqlDataReader dr = _cmd.ExecuteReader()) 
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                _id = Convert.ToInt32(dr["Id_Visita"]);
                            }
                        }
                    }



                }
                catch (Exception ex)
                {

                }
            }

            return _id;
        }

        #endregion



        #region Datos Academicos
        //retorna objeto para agregar datos academicos
        public AcademicData NewAcademic(int prosp)
        {
            var _academic = getAcademicData(prosp);

            if (_academic == null)
            {
                _academic = new AcademicData();
            }
            //academic data
            _academic.LastDegreeStudyOp = getDegreesOfStudies();
            _academic.DocumentsReceivedOp = getDocumentsReceivedOfSchool();
            _academic.StartYearsOp = getStartFinishYears();
            _academic.FinishYearsOp = getStartFinishYears();
            _academic.Courses = new CoursesReceived();
            _academic.Courses.DocucumentReceivedOp = getDocumentsReceivedOfSchool();
            _academic.CoursesList = getCoursesData(prosp);

            return _academic;
        }
        //guarda los datos academicos
        public bool SaveAcademicData(AcademicData academic)
        {
            bool _saving = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPEscolares_Create", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", SqlDbType.Int, academic.Id);
                    Conexion.creaParametro(_cmd, "@Id_GradoEscolar", SqlDbType.Int, academic.LastDegreeStudy);
                    Conexion.creaParametro(_cmd, "@NombreEscuela", SqlDbType.VarChar, academic.SchoolName);
                    Conexion.creaParametro(_cmd, "@Carrera", SqlDbType.VarChar, academic.Career);
                    Conexion.creaParametro(_cmd, "@Especialidad", SqlDbType.VarChar, academic.Speciality);
                    Conexion.creaParametro(_cmd, "@Id_TipoDoctoEscolar", SqlDbType.Int, academic.DocumentReceived);
                    Conexion.creaParametro(_cmd, "@FolioDocumento", SqlDbType.VarChar, academic.Folio);
                    Conexion.creaParametro(_cmd, "@FechaInicio", SqlDbType.Int, Convert.ToInt32(academic.StartYear));
                    Conexion.creaParametro(_cmd, "@FechaFin", SqlDbType.Int, Convert.ToInt32(academic.FinishYear));
                    Conexion.creaParametro(_cmd, "@Promedio", SqlDbType.Money, academic.SchoolAverage);
                    Conexion.creaParametro(_cmd, "@CedulaProfesional", SqlDbType.VarChar, academic.ProfessionalID);
                    Conexion.creaParametro(_cmd, "@Usuario_Registro", SqlDbType.Int, User_Persistent_Data.Id);
                    Conexion.creaParametro(_cmd, "@Estacion_Registro", SqlDbType.VarChar, "Reclutamiento Web");
                    _cmd.Connection.Open();
                    var insert = Conexion.ejecutarNonquery(_cmd);
                    if (insert > 0)
                    {
                        _saving = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _saving;
        }
        //actualiza los datos academicos
        public bool UpdateAcademicData(AcademicData academic)
        {
            bool _update = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPEscolares_Update", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", SqlDbType.Int, academic.Id);
                    Conexion.creaParametro(_cmd, "@Id_GradoEscolar", SqlDbType.Int, academic.LastDegreeStudy);
                    Conexion.creaParametro(_cmd, "@NombreEscuela", SqlDbType.VarChar, academic.SchoolName);
                    Conexion.creaParametro(_cmd, "@Carrera", SqlDbType.VarChar, academic.Career);
                    Conexion.creaParametro(_cmd, "@Especialidad", SqlDbType.VarChar, academic.Speciality);
                    Conexion.creaParametro(_cmd, "@Id_TipoDoctoEscolar", SqlDbType.Int, academic.DocumentReceived);
                    Conexion.creaParametro(_cmd, "@FolioDocumento", SqlDbType.VarChar, academic.Folio);
                    Conexion.creaParametro(_cmd, "@FechaInicio", SqlDbType.Int, Convert.ToInt32(academic.StartYear));
                    Conexion.creaParametro(_cmd, "@FechaFin", SqlDbType.Int, Convert.ToInt32(academic.FinishYear));
                    Conexion.creaParametro(_cmd, "@Promedio", SqlDbType.Money, academic.SchoolAverage);
                    Conexion.creaParametro(_cmd, "@CedulaProfesional", SqlDbType.VarChar, academic.ProfessionalID);
                    Conexion.creaParametro(_cmd, "@Usuario_Actualiza", SqlDbType.Int, User_Persistent_Data.Id);
                    Conexion.creaParametro(_cmd, "@Estacion_Actualiza", SqlDbType.VarChar, "Reclutamiento Web");
                    _cmd.Connection.Open();
                    var update = Conexion.ejecutarNonquery(_cmd);
                    if (update > 0)
                    {
                        _update = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _update;
        }
        //elimina los datos academicos
        public bool DeleteAcademicData(int id)
        {
            bool _delete = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("Cat_EmpleadosPEscolares_Create", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", SqlDbType.Int, id);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return _delete;
        }
        //retorna lista de grados escolares(Doctorado,Maestria,Licenciatura , etc.)
        private List<SelectListItem> getDegreesOfStudies()
        {
            List<SelectListItem> _degrees = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_GradosEscolaresCombo_Get", oConexion);
                    //Conexion.creaParametro(_cmd, "@Id_ModoContacto", System.Data.SqlDbType.Int, contactmode);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _degrees.Add(new SelectListItem
                            {
                                Text = dr["Descripcion"].ToString(),
                                Value = dr["Id_GradoEscolar"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _degrees;
        }
        //retorna lista de documentos que se pueden recibidos al agresar una carrera
        private List<SelectListItem> getDocumentsReceivedOfSchool()
        {
            List<SelectListItem> _degrees = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_TipoDoctoEscolarCombo_Get", oConexion);
                    //Conexion.creaParametro(_cmd, "@Id_ModoContacto", System.Data.SqlDbType.Int, contactmode);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _degrees.Add(new SelectListItem
                            {
                                Text = dr["Descripcion"].ToString(),
                                Value = dr["Id_TipoDoctoEscolar"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _degrees;
        }
        //retorna lista de años de 1950 a año actual
        private List<SelectListItem> getStartFinishYears()
        {
            List<SelectListItem> _years = new List<SelectListItem>();

            for (int i = 1950; i < DateTime.Now.Year; i++)
            {
                _years.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }


            return _years;
        }
        //retorna los datos academicos guardados
        private AcademicData getAcademicData(int prosp)
        {
            AcademicData? _academics = null;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPEscolares_Read", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, prosp);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _academics = new AcademicData
                            {
                                Id = Convert.ToInt32(dr["Id_EmpleadoPEscolares"]),
                                LastDegreeStudy = Convert.ToInt32(dr["Id_GradoEscolar"]),
                                SchoolName = dr["NombreEscuela"].ToString(),
                                Career = dr["Carrera"].ToString(),
                                Speciality = dr["Especialidad"].ToString(),
                                DocumentReceived = Convert.ToInt32(dr["Id_TipoDoctoEscolar"]),
                                StartYear = dr["FechaInicio"].ToString(),
                                FinishYear = dr["FechaFin"].ToString(),
                                Folio = dr["Folio"].ToString(),
                                SchoolAverage = Convert.ToDouble(dr["Promedio"]),
                                ProfessionalID = dr["CedulaProfesional"].ToString()

                            };

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _academics;
        }
        //retorna los cursos guardados
        private List<CoursesReceived> getCoursesData(int prosp)
        {
            List<CoursesReceived> _courses = new List<CoursesReceived>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPCursos_Read", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, prosp);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _courses.Add(new CoursesReceived
                            {
                                Id = Convert.ToInt32(dr["Id_EmpleadoPCurso"]),
                                Name = dr["Curso"].ToString(),
                                StartDate = dr["FechaInicio"].ToString(),
                                FinishDate = dr["FechaFin"].ToString(),
                                Finished = dr["Finalizado"].ToString(),
                                Instructor = dr["Instructor"].ToString(),
                                TypeCourse = Convert.ToInt32(dr["ITD"]),
                                Comments = dr["Comentarios"].ToString()

                            });


                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _courses;
        }
        //guarda el curso agregado
        public bool CoursesDataSave(CoursesReceived courses)
        {
            bool _saving = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPCursos_Create", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", SqlDbType.Int, courses.Id);
                    Conexion.creaParametro(_cmd, "@NombreCurso", SqlDbType.VarChar, courses.Name);
                    Conexion.creaParametro(_cmd, "@FechaInicio", SqlDbType.DateTime, courses.StartDate);
                    Conexion.creaParametro(_cmd, "@FechaFin", SqlDbType.DateTime, courses.FinishDate);
                    Conexion.creaParametro(_cmd, "@Finalizado", SqlDbType.VarChar, courses.Finished);
                    Conexion.creaParametro(_cmd, "@Instructor", SqlDbType.VarChar, courses.Instructor);
                    Conexion.creaParametro(_cmd, "@Id_TipoDocto", SqlDbType.Int, courses.DocucumentReceived);
                    Conexion.creaParametro(_cmd, "@Comentarios", SqlDbType.VarChar, (courses.Comments == null) ? "" : courses.Comments);
                    Conexion.creaParametro(_cmd, "@Usuario_Registro", SqlDbType.VarChar, User_Persistent_Data.Id);
                    Conexion.creaParametro(_cmd, "@Estacion_Registro", SqlDbType.VarChar, "Reclutamiento Web");
                    Conexion.creaParametro(_cmd, "@Id_Programacion", SqlDbType.Int, 0);

                    _cmd.Connection.Open();
                    var insert = Conexion.ejecutarNonquery(_cmd);
                    if (insert > 0)
                    {
                        _saving = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _saving;

        }
        //elimina el curso seleccionado
        public bool CoursesDataDelete(int id)
        {
            bool _saving = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPCursos_Delete", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoPCurso", SqlDbType.Int, id);


                    _cmd.Connection.Open();
                    var insert = Conexion.ejecutarNonquery(_cmd);
                    if (insert > 0)
                    {
                        _saving = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _saving;

        }
        // retorna el idEmpleadoP del prospecto, pasandole la clave del prospecto
        

        #endregion

        #region Datos Familiares
        //retorna objeto para agregar datos familiares
        public FamilyData NewFamily(int prosp)
        {
            var _family = new FamilyData();

            //familly data
            _family.KinshipOp = getKinships();
            _family.FamilyList = getFamilymembers(prosp);
            _family.CitiesOp = getDomicileCities(1);
            return _family;
        }
        //retorna lista de parentescos
        private List<SelectListItem> getKinships()
        {
            List<SelectListItem> _kinships = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_TipoParentescoCombo_Get", oConexion);
                    //Conexion.creaParametro(_cmd, "@Id_ModoContacto", System.Data.SqlDbType.Int, contactmode);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _kinships.Add(new SelectListItem
                            {
                                Text = dr["Descripcion"].ToString(),
                                Value = dr["Id_TipoParentesco"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _kinships;
        }
        //retorna lista de familiares agregados
        public List<FamilyList> getFamilymembers(int prosp)
        {
            List<FamilyList> _familylist = new List<FamilyList>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("Cat_EmpleadosPFamiliares_Read", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, prosp);
                    _cmd.Connection.Open();
                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            _familylist.Add(new FamilyList
                            {
                                Id = Convert.ToInt32(dr["Id_EmpleadoPFamiliares"]),
                                FullName = dr["Nombre"].ToString(),
                                Kinship = dr["Parentesco"].ToString(),
                                Lives = dr["Vive"].ToString(),
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
        //guarda familiar agregado
        public bool SaveFamilyData(FamilyData family)
        {
            bool _saving = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    
                    if (family.Id == 0) return false;
                    _cmd = Conexion.creaComando("Cat_EmpleadosPFamiliares_Create", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", SqlDbType.Int, family.Id);
                    Conexion.creaParametro(_cmd, "@Nombre", SqlDbType.VarChar, family.FullName);
                    Conexion.creaParametro(_cmd, "@DepEconomico", SqlDbType.VarChar, family.EconomicDependent);
                    Conexion.creaParametro(_cmd, "@Id_TipoParentesco", SqlDbType.Int, family.Kinship);
                    Conexion.creaParametro(_cmd, "@FechaNac", SqlDbType.DateTime, family.BirthDate);
                    Conexion.creaParametro(_cmd, "@Direccion", SqlDbType.VarChar, family.Domicile);
                    Conexion.creaParametro(_cmd, "@Id_Ciudad", SqlDbType.Int, family.City);
                    Conexion.creaParametro(_cmd, "@Telefono", SqlDbType.VarChar, family.Phone);
                    Conexion.creaParametro(_cmd, "@MismoDomicilio", SqlDbType.VarChar, family.LiveIntheSameAddress);
                    Conexion.creaParametro(_cmd, "@Vive", SqlDbType.VarChar, family.Lives);
                    Conexion.creaParametro(_cmd, "@Usuario_Registro", SqlDbType.Int, User_Persistent_Data.Id);
                    Conexion.creaParametro(_cmd, "@Estacion_Registro", SqlDbType.VarChar, "Reclutamiento Web");

                    _cmd.Connection.Open();
                    var insert = Conexion.ejecutarNonquery(_cmd);
                    if (insert > 0)
                    {
                        _saving = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _saving;

        }
        //elimina familiar seleccionado
        public bool DeleteFamilyData(int id)
        {
            bool _saving = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPFamiliares_Delete", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoPFamiliares", SqlDbType.Int, id);


                    _cmd.Connection.Open();
                    var insert = Conexion.ejecutarNonquery(_cmd);
                    if (insert > 0)
                    {
                        _saving = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _saving;

        }
        #endregion

        #region Datos de Empleos anteriores
        //retorna objeto para agregar datos de un empleo anterior
        public EmploymentData NewEmployment(int prosp = 0, int? country = 1)
        {
            EmploymentData _employmentData = new EmploymentData();
            _employmentData.Country = (country != null && country != 0) ? (int)country : 1;
            _employmentData.CountryOp = RecruiterService.Instancia.getCountries();
            _employmentData.CityOp = getDomicileCities(_employmentData.Country);
            _employmentData.ReasonSeparationOp = getReasonsOfSeparation();
            if (prosp != null && prosp != 0)
            {
                _employmentData.EmploymentLists = getEmployments(prosp);
            }

            return _employmentData;
        }
        //retorna lista de rasones de separacion de los empleos anteriores
        public List<SelectListItem> getReasonsOfSeparation()
        {
            List<SelectListItem> _reasons = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_MotivosBajaCombo_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Tipo", System.Data.SqlDbType.Int, 2);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _reasons.Add(new SelectListItem
                            {
                                Value = dr["Id_MotivoB"].ToString(),
                                Text = dr["Descripcion"].ToString()

                            });


                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _reasons;
        }
        // retorna lista de empleos anteriores agregados
        public List<EmploymentList> getEmployments(int prosp)
        {
            List<EmploymentList> _employments = new List<EmploymentList>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPLaborales_Read", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, prosp);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _employments.Add(new EmploymentList
                            {
                                Id = Convert.ToInt32(dr["Id_EmpleadoPLaborales"]),
                                CompanyName = dr["NombreEmpresa"].ToString(),
                                Domicile = dr["calle"].ToString() + " #" + dr["NumeroExt"].ToString() + "," + dr["Colonia"].ToString() + "," + dr["Ciudad"].ToString() + ",C.P. " + dr["CodigoPostal"].ToString(),
                                StartDate = dr["FechaIngreso"].ToString(),
                                FinishDate = dr["FechaBaja"].ToString(),
                                Position = dr["Puesto"].ToString(),
                                SecurityCompany = dr["EmpresaSeg"].ToString(),
                                Guns = dr["PorteArmas"].ToString(),
                                BossName = dr["NombreJefe"].ToString(),
                                Phone = dr["Telefono"].ToString()
                            });


                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _employments;
        }
        //
        public int getEmploymentRef(int prosp)
        {
            int Id = 0;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPLaborales_CountReferencias", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, prosp);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            Id = Convert.ToInt32(dr["NoReferenciasLaborales"]);

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return Id;
        }
        public int getPersonalRef(int prosp)
        {
            int Id = 0;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPReferencias_CountReferencias", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, prosp);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            Id = Convert.ToInt32(dr["NoReferenciasReferencia"]);

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return Id;
        }

        //guarda el empleo anterior agregado
        public bool SaveEmploymentData(EmploymentData employment)
        {
            bool _saving = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPLaborales_Create", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", SqlDbType.Int, employment.Id);
                    Conexion.creaParametro(_cmd, "@NombreEmpresa", SqlDbType.VarChar, employment.CompanyName);
                    Conexion.creaParametro(_cmd, "@Calle", SqlDbType.VarChar, employment.Street);
                    Conexion.creaParametro(_cmd, "@NumeroExt", SqlDbType.Int, employment.ExternalNumber);
                    Conexion.creaParametro(_cmd, "@NumeroInt", SqlDbType.VarChar, employment.InternalNumber);
                    Conexion.creaParametro(_cmd, "@Id_Ciudad", SqlDbType.Int, employment.City);
                    Conexion.creaParametro(_cmd, "@CodigoPostal", SqlDbType.Int,Tools.IsNULL(employment.PostalCode));
                    Conexion.creaParametro(_cmd, "@FechaIngreso", SqlDbType.DateTime, employment.StartDate);
                    Conexion.creaParametro(_cmd, "@FechaBaja", SqlDbType.DateTime, employment.FinishDate);
                    Conexion.creaParametro(_cmd, "@Puesto", SqlDbType.VarChar, employment.Position);
                    Conexion.creaParametro(_cmd, "@NombreJefe", SqlDbType.VarChar, employment.BossName);
                    Conexion.creaParametro(_cmd, "@PuestoJefe", SqlDbType.VarChar, Tools.IsNULL(employment.BossPosition));
                    Conexion.creaParametro(_cmd, "@Telefono", SqlDbType.VarChar, Tools.IsNULL(employment.Phone));
                    Conexion.creaParametro(_cmd, "@SueldoInicial", SqlDbType.Money, employment.StartSalary);
                    Conexion.creaParametro(_cmd, "@SueldoFinal", SqlDbType.Money, employment.FinishSalary);
                    Conexion.creaParametro(_cmd, "@Id_MotivoBaja", SqlDbType.Int, employment.ReasonSeparation);
                    Conexion.creaParametro(_cmd, "@OtroMotivo", SqlDbType.VarChar, Tools.IsNULL(employment.OtherMotive));
                    Conexion.creaParametro(_cmd, "@EntreCalle1", SqlDbType.VarChar, employment.StreetBetween1);
                    Conexion.creaParametro(_cmd, "@EntreCalle2", SqlDbType.VarChar, employment.StreetBetween2);
                    Conexion.creaParametro(_cmd, "@EmpresaSegPriv", SqlDbType.VarChar, employment.SecurityCompany);
                    Conexion.creaParametro(_cmd, "@PorteArmas", SqlDbType.VarChar, employment.Guns);
                    Conexion.creaParametro(_cmd, "@Colonia", SqlDbType.VarChar, employment.Colony);
                    Conexion.creaParametro(_cmd, "@Latitud", SqlDbType.Decimal, 0);
                    Conexion.creaParametro(_cmd, "@Longitud", SqlDbType.Decimal, 0);
                    Conexion.creaParametro(_cmd, "@Usuario_Registro", SqlDbType.Int, User_Persistent_Data.Id);
                    Conexion.creaParametro(_cmd, "@Estacion_Registro", SqlDbType.VarChar, "Reclutamiento Web");

                    _cmd.Connection.Open();
                    var insert = Conexion.ejecutarNonquery(_cmd);
                    if (insert > 0)
                    {
                        _saving = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _saving;

        }
        // elimina el empleo anterior seleccionado
        public bool DeleteDataEmployment(int id)
        {
            bool _saving = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPLaborales_Delete", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoPLaborales", SqlDbType.Int, id);


                    _cmd.Connection.Open();
                    var _delete = Conexion.ejecutarNonquery(_cmd);
                    if (_delete > 0)
                    {
                        _saving = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _saving;

        }

        #endregion

        #region Datos de Referencias Personales
        //retorna objeto para poder agregar referencias personales o laborales
        public References NewPersonalOrEmploymentReferences(int prosp = 0, int? country = 1, int type = 1)
        {
            References _references = new References();
            _references.TypeReferencesOp = gettypeReferences();
            _references.Gender = "M";
            _references.OcupationOp = getOcupations();
            _references.Country = (country != null && country != 0) ? (int)country : 1;
            _references.CountryOp = RecruiterService.Instancia.getCountries();
            _references.CityOp = getDomicileCities(_references.Country);

            if (prosp != null && prosp != 0)
            {
                _references.ReferencesLists = getPersonalReferences(prosp);
            }

            return _references;
        }
        //guarda la referencia agregada
        public bool SaveReferencesData(References references)
        {
            bool _saving = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPReferencias_CreateSSP", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", SqlDbType.Int, references.Id);
                    Conexion.creaParametro(_cmd, "@Id_TipoReferencia", SqlDbType.Int, references.TypeReferences);
                    Conexion.creaParametro(_cmd, "@Nombre", SqlDbType.VarChar, references.Name);
                    Conexion.creaParametro(_cmd, "@Sexo", SqlDbType.VarChar, references.Gender);
                    Conexion.creaParametro(_cmd, "@Ocupacion", SqlDbType.VarChar, getOcupations().Where(x => x.Value == references.Ocupation).FirstOrDefault().Text);
                    Conexion.creaParametro(_cmd, "@Calle", SqlDbType.VarChar, Tools.IsNULL(references.Street));
                    Conexion.creaParametro(_cmd, "@NumeroExt", SqlDbType.Int, Tools.IsNULL(references.ExternalNumber));
                    Conexion.creaParametro(_cmd, "@NumeroInt", SqlDbType.VarChar, Tools.IsNULL(references.InteriorNumber));
                    Conexion.creaParametro(_cmd, "@Telefono", SqlDbType.VarChar, references.Phone);
                    Conexion.creaParametro(_cmd, "@Id_Ciudad", SqlDbType.Int, references.City);
                    Conexion.creaParametro(_cmd, "@CodigoPostal", SqlDbType.Int, Tools.IsNULL(references.PostalCode));
                    Conexion.creaParametro(_cmd, "@Status", SqlDbType.VarChar, "A");
                    Conexion.creaParametro(_cmd, "@EntreCalle1", SqlDbType.VarChar, Tools.IsNULL(references.StreetBetween1));
                    Conexion.creaParametro(_cmd, "@EntreCalle2", SqlDbType.VarChar, Tools.IsNULL(references.StreetBetween2));
                    Conexion.creaParametro(_cmd, "@Colonia", SqlDbType.VarChar, Tools.IsNULL(references.Colony));
                    Conexion.creaParametro(_cmd, "@Usuario_Registro", SqlDbType.VarChar, User_Persistent_Data.Id);
                    Conexion.creaParametro(_cmd, "@Estacion_Registro", SqlDbType.VarChar, "Reclutamiento Web");
                    Conexion.creaParametro(_cmd, "@OcupacionSSP", SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(references.Ocupation)));
                    Conexion.creaParametro(_cmd, "@ApellidoP", SqlDbType.VarChar, references.PaternalName);
                    Conexion.creaParametro(_cmd, "@ApellidoM", SqlDbType.VarChar, references.MaternalName);
                    _cmd.Connection.Open();
                    var insert = Conexion.ejecutarNonquery(_cmd);
                    if (insert > 0)
                    {
                        _saving = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _saving;

        }
        //eleimina la referencia seleccionada
        public bool DeleteDataReferences(int id)
        {
            bool _deleted = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPReferencias_Delete", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoPReferencia", SqlDbType.Int, id);


                    _cmd.Connection.Open();
                    var _delete = Conexion.ejecutarNonquery(_cmd);
                    if (_delete > 0)
                    {
                        _deleted = true;
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _deleted;

        }
        //retorna lista de referencias personales o laborales
        public List<ReferencesList> getPersonalReferences(int prosp)
        {
            List<ReferencesList> _references = new List<ReferencesList>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosPReferencias_Read", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, prosp);
                    //Conexion.creaParametro(_cmd, "@Ref_Laboral_O_Personal", System.Data.SqlDbType.VarChar, type);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {

                            _references.Add(new ReferencesList
                            {
                                Id = Convert.ToInt32(dr["Id_EmpleadoPReferencia"]),
                                Description = dr["Descripcion"].ToString(),
                                Domicile = dr["Domicilio"].ToString() + " #" + dr["NumeroExt"].ToString() + "," + dr["Colonia"].ToString() + "," + dr["Ciudad"].ToString() + ",C.P. " + dr["CodigoPostal"].ToString(),
                                Name = dr["Nombre"].ToString(),
                                Gender = dr["Sexo"].ToString(),
                                Ocupation = dr["Ocupacion"].ToString(),
                                Phone = dr["Telefono"].ToString()
                            });


                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _references;
        }
        // retorna lista de tipos de referencia
        public List<SelectListItem> gettypeReferences()
        {
            List<SelectListItem> _references = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_TipoReferenciaCombo_Get", oConexion);
                    //Conexion.creaParametro(_cmd, "@Id_Pais", System.Data.SqlDbType.Int, );
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        if (!dr.HasRows)
                        {
                            _references.Add(new SelectListItem
                            {
                                Text = "Seleccione...",
                                Value = "0"
                            });
                        }

                        while (dr.Read())
                        {

                            _references.Add(new SelectListItem
                            {
                                Text = dr["Descripcion"].ToString(),
                                Value = dr["Id_TipoReferencia"].ToString()
                            });

                        }
                    }


                }
                catch (Exception ex)
                {
                    _references.Add(new SelectListItem
                    {
                        Text = "Seleccione...",
                        Value = "0"
                    });
                }
            }


            return _references;
        }
        //retorna ocupaciones
        public List<SelectListItem> getOcupations()
        {
            List<SelectListItem> _ocupations = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_OcupacionRefSSPCombo_Get", oConexion);
                    //Conexion.creaParametro(_cmd, "@Id_Pais", System.Data.SqlDbType.Int, );
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        if (!dr.HasRows)
                        {
                            _ocupations.Add(new SelectListItem
                            {
                                Text = "Seleccione...",
                                Value = "0"
                            });
                        }

                        while (dr.Read())
                        {

                            _ocupations.Add(new SelectListItem
                            {
                                Text = dr["Nombre"].ToString(),
                                Value = dr["Id_OcupacionSSP"].ToString()
                            });

                        }

                    }

                }
                catch (Exception ex)
                {
                    _ocupations.Add(new SelectListItem
                    {
                        Text = "Seleccione...",
                        Value = "0"
                    });
                }
            }

            return _ocupations;
        }

        #endregion

        #region Cuestionario Medico

        public MedicalQuestion NewMedicalQuestion(int pto,int filter)
        {
            // informacion de cuestionario medico
            MedicalQuestion _medicalQuestion = GetMedicalQuestion(pto,filter);
            var fingers = RecruiterService.Instancia.getFirmasDocuments(pto);
            _medicalQuestion.Signature = getSignarutaProspEmp(pto,filter);
            if (_medicalQuestion.ProspectusId ==0)
            {
                _medicalQuestion.ProspectusId = pto;
            }
            //informacion del prospecto
            _medicalQuestion.MedicalQuestionPerson = GetMedicalQuestionInformation(pto);
            var pmhId = (_medicalQuestion.personalMedicalHistoryId != null) ? _medicalQuestion.personalMedicalHistoryId : 0;
            var gaId = (_medicalQuestion.gynecologicalAntecentoId != null) ? _medicalQuestion.gynecologicalAntecentoId : 0;
            //antecedentes medicos personales
            _medicalQuestion.personalMedicalHistory = GetPersonaMedicalHistory(pmhId);
            //datos ginecologicos(solo si es mujer)
            _medicalQuestion.gynecologicalAntecento = GetGynecologicalAntecento(gaId);

            return _medicalQuestion;
        }

        private MedicalQuestion GetMedicalQuestion(int pto,int filter)
        {
            MedicalQuestion _medicalQuestion = new MedicalQuestion();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_CuestionarioMedico_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id", System.Data.SqlDbType.Int, pto);
                    Conexion.creaParametro(_cmd, "@filter", System.Data.SqlDbType.Int, filter);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            _medicalQuestion.Id = Convert.ToInt32(dr["Id_Cuestionario"]);
                            _medicalQuestion.ProspectusId = Convert.ToInt32(dr["Id_Prospecto"]);
                            _medicalQuestion.personalMedicalHistoryId = Convert.ToInt32(dr["Id_AntecedenteMP"]);
                            _medicalQuestion.gynecologicalAntecentoId = Convert.ToInt32(dr["Id_AntecedenteGine"]);
                            _medicalQuestion.Antidoping = dr["Antidoping"].ToString();
                            _medicalQuestion.Weight = dr["Peso"].ToString();
                            _medicalQuestion.height = dr["Altura"].ToString();
                            _medicalQuestion.IsSigned = Convert.ToBoolean(dr["Firmado"]);
                            _medicalQuestion.MedicalServices = dr["ServicioMedico"].ToString();
                            _medicalQuestion.MedicalServiceApproval = dr["ServicioMedicoAprobacion"].ToString();
                            _medicalQuestion.Remarks = dr["Observaciones"].ToString();
                            _medicalQuestion.Date = dr["Fecha_Registro"].ToString();

                        }
                        if (!dr.HasRows)
                        {
                            _medicalQuestion.Date = DateTime.Now.ToString();
                        }
                    }




                }
                catch (Exception ex)
                {

                }
            }

            return _medicalQuestion;
        }

        private MedicalQuestionPerson GetMedicalQuestionInformation(int pto)
        {
            MedicalQuestionPerson _medicalQuestionPerson = new MedicalQuestionPerson();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_EmpleadosP_CM_InformProsp_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, pto);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _medicalQuestionPerson.Id = Convert.ToInt32(dr["Id"]);
                            _medicalQuestionPerson.Position = dr["Puesto"].ToString();
                            _medicalQuestionPerson.Name = dr["Nombre"].ToString();
                            _medicalQuestionPerson.Age = Convert.ToInt32(dr["Edad"]);
                            _medicalQuestionPerson.DateOfBirth = dr["Fecha_Nacimiento"].ToString();
                            _medicalQuestionPerson.Gender = dr["Sexo"].ToString();
                            _medicalQuestionPerson.MaritalStatus = dr["EstadoCivil"].ToString();
                            var place = dr["Lugar_Nacimiento"].ToString().Split(',');
                            _medicalQuestionPerson.PlaceOfBirth = place[1];
                            _medicalQuestionPerson.State = place[0];
                            _medicalQuestionPerson.StreetOfDomicilie = dr["Calle"].ToString();
                            _medicalQuestionPerson.StreetNumberOfDomicilie = dr["Numero"].ToString();
                            _medicalQuestionPerson.ColonyOfDomicilie = dr["Colonia"].ToString();
                            var statecity = GetStateCity(1, Convert.ToInt32(dr["Id_Ciudad"])).Split(",");
                            _medicalQuestionPerson.CityOfDomicilie = statecity[1];
                            _medicalQuestionPerson.StateOfDomicilie = statecity[0];
                            _medicalQuestionPerson.PostalCodeOfDomicilie = dr["CP"].ToString();
                            _medicalQuestionPerson.Phone = dr["Telefono"].ToString();
                            _medicalQuestionPerson.CellPhone = dr["Telefono_Movil"].ToString();
                            _medicalQuestionPerson.NumberOfChilds = dr["Cantidad_Hijos"].ToString();
                            _medicalQuestionPerson.Complexion = dr["Complexion"].ToString();
                           

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }
            return _medicalQuestionPerson;
        }

        private string GetStateCity(int country, int city)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("Cat_CiudadesPais_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_Pais", System.Data.SqlDbType.Int, country);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader()) 
                    {
                        while (dr.Read())
                        {
                            list.Add(new SelectListItem { Text = dr["Nombre"].ToString(), Value = dr["Id_Ciudad"].ToString() });
                        }
                    }


                }
                catch (Exception ex)
                {
                }
            }

            return list.Where(x=>x.Value == city.ToString()).First().Text;
        }

        private PersonalMedicalHistory GetPersonaMedicalHistory(int pmhId)
        {
            PersonalMedicalHistory _personalMedicalHistory = new PersonalMedicalHistory();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_AntecedentesMedicosPersonales_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_AntecedenteMP", System.Data.SqlDbType.Int, pmhId);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            _personalMedicalHistory.Id = Convert.ToInt16(dr["Id_AntecedenteMP"]);
                            _personalMedicalHistory.BloodPressure = Tools.IsNULL(dr["PresionArterial"].ToString());
                            _personalMedicalHistory.Glucose = Tools.IsNULL(dr["Glucosa"].ToString());
                            _personalMedicalHistory.IMC = Tools.IsNULL(dr["IMC"].ToString());
                            _personalMedicalHistory.Remarks = Tools.IsNULL(dr["Observaciones_IMC"].ToString());
                            _personalMedicalHistory.WearGlassesOrPupils = gBit(Convert.ToBoolean(dr["UsoLentesORPup"]));
                            _personalMedicalHistory.WearGlassesOrPupilsSince = dr["ULP_Desde"].ToString();
                            _personalMedicalHistory.WearGlassesOrPupilsAnswers = gList(dr["ULP_Respuesta"].ToString(), Answers.WearGlassesOrPupilsAnswers);
                            _personalMedicalHistory.FamilyHistoryAnswers = gList(dr["AntecedentesFamiliares"].ToString(), Answers.FamilyHistoryAnswers);
                            _personalMedicalHistory.CongenitalOrInheritedDisease = gBit(Convert.ToBoolean(dr["EnfermedadCongenORHered"]));
                            _personalMedicalHistory.CongenitalDeformity = gBit(Convert.ToBoolean(dr["DeformidadCongenita"]));
                            _personalMedicalHistory.ChildhoodDiseases = gBit(Convert.ToBoolean(dr["EnfermedadesInfantiles"]));
                            _personalMedicalHistory.ChildhoodDiseasesAnswers = gList(dr["EI_Respuesta"].ToString(), Answers.ChildhoodDiseasesAnswers);
                            _personalMedicalHistory.ChildhoodDiseasesOther = dr["EI_Otras"].ToString();
                            _personalMedicalHistory.Allergies = gBit(Convert.ToBoolean(dr["Alergias"]));
                            _personalMedicalHistory.AllergiesAnswers = gList(dr["All_Respuesta"].ToString(), Answers.AllergiesAnswers);
                            _personalMedicalHistory.WearGlassesOrContactLenses = gBit(Convert.ToBoolean(dr["UsaLentesORLentesContac"]));
                            _personalMedicalHistory.WearGlassesOrContactLensesAnswers = gList(dr["ULLC_Respuesta"].ToString(), Answers.WearGlassesOrContactLensesAnswers);
                            _personalMedicalHistory.SeeYourselfWell = gBit(Convert.ToBoolean(dr["VeUstedBien"]));
                            _personalMedicalHistory.LeftEye = gBit(Convert.ToBoolean(dr["OjoIzquierdo"]));
                            _personalMedicalHistory.RightEye = gBit(Convert.ToBoolean(dr["OjoDerecho"]));
                            _personalMedicalHistory.EyeSurgery = gBit(Convert.ToBoolean(dr["CirugiaOcular"]));
                            _personalMedicalHistory.EyeSurgeryBy = dr["CO_Por"].ToString();
                            _personalMedicalHistory.dateSurgery = dr["CO_Fecha"].ToString();
                            _personalMedicalHistory.EarDisease = gBit(Convert.ToBoolean(dr["EnfermedadEnOidos"]));
                            _personalMedicalHistory.EarDiseaseAnswer = dr["EO_Cual"].ToString();
                            _personalMedicalHistory.ListenWell = gBit(Convert.ToBoolean(dr["OyeBienUsted"]));
                            _personalMedicalHistory.UseHearingAid = gBit(Convert.ToBoolean(dr["UsaAparatoAuditivo"]));
                            _personalMedicalHistory.UseHearingAidsince = dr["UAA_Desde"].ToString();
                            _personalMedicalHistory.DentalDiseases = gBit(Convert.ToBoolean(dr["EnfermedadDental"]));
                            _personalMedicalHistory.DentalDiseasesAnswers = gList(dr["ED_Respuesta"].ToString(), Answers.DentalDiseasesAnswers);
                            _personalMedicalHistory.HormonalDiseasesAnswers = gList(dr["EnfermedadHormonal"].ToString(), Answers.HormonalDiseasesAnswers);
                            _personalMedicalHistory.HormonalDiseasesOther = dr["EH_Otras"].ToString();
                            _personalMedicalHistory.LungDisease = gBit(Convert.ToBoolean(dr["EnfermedadPulmones"]));
                            _personalMedicalHistory.LungDiseaseAnswers = gList(dr["EP_Respuesta"].ToString(), Answers.LungDiseaseAnswers);
                            _personalMedicalHistory.LungDiseaseOther = dr["EP_Otras"].ToString();
                            _personalMedicalHistory.HeartDisease = gBit(Convert.ToBoolean(dr["EnfermedadCorazon"]));
                            _personalMedicalHistory.HeartDiseaseAnswers = gList(dr["EC_Respuesta"].ToString(), Answers.HeartDiseaseAnswers);
                            _personalMedicalHistory.AlteredBloodPressure = gBit(Convert.ToBoolean(dr["AlteracionArterial"]));
                            _personalMedicalHistory.AlteredBloodPressureAnswers = gList(dr["AA_Respuesta"].ToString(), Answers.AlteredBloodPressureAnswers);
                            _personalMedicalHistory.AlteredBloodPressureMedicaments = dr["AA_Medicamentos"].ToString();
                            _personalMedicalHistory.AlteredBloodPressureMedicalControl = gBit(Convert.ToBoolean(dr["AA_EstoyControlMedico"]));
                            _personalMedicalHistory.AlteredBloodPressureMedicalControlAnswers = gList(dr["AA_CM_Respuesta"].ToString(), Answers.AlteredBloodPressureMedicalControlAnswers);
                            _personalMedicalHistory.DigestiveDisease = gBit(Convert.ToBoolean(dr["EnfermedadesDigestivas"]));
                            _personalMedicalHistory.DigestiveDiseaseAnswers = gList(dr["EDG_Respuesta"].ToString(), Answers.DigestiveDiseaseAnswers);
                            _personalMedicalHistory.LiverDisease = gBit(Convert.ToBoolean(dr["EnfermedadesHigado"]));
                            _personalMedicalHistory.LiverDiseaseAnswers = gList(dr["EH_Respuesta"].ToString(), Answers.LiverDiseaseAnswers);
                            _personalMedicalHistory.DiabetesMellitus = gBit(Convert.ToBoolean(dr["DiabetesMellitus"]));
                            _personalMedicalHistory.DiabetesMellitusAnswers = gList(dr["DM_Respuesta"].ToString(), Answers.DiabetesMellitusAnswers);
                            _personalMedicalHistory.DateOfLastExamination = dr["DM_FechaUltExamSangre"].ToString();
                            _personalMedicalHistory.DiabetesMellitusMedicControl = gBit(Convert.ToBoolean(dr["DM_ControlMedicoDiabettes"]));
                            _personalMedicalHistory.DiabetesMellitusMedicControlAnswers = gList(dr["DM_CM_Respuesta"].ToString(), Answers.DiabetesMellitusMedicControlAnswers);
                            _personalMedicalHistory.CholesterolDisease = gBit(Convert.ToBoolean(dr["EnfermedadColesterol"]));
                            _personalMedicalHistory.CholesterolDiseaseMedications = dr["ECO_Medicamentos"].ToString();
                            _personalMedicalHistory.UricAcidDisease = gBit(Convert.ToBoolean(dr["EnfermedadAcidoUrico"]));
                            _personalMedicalHistory.UricAcidDiseaseMedication = dr["EAU_Medicamentos"].ToString();
                            _personalMedicalHistory.KidneyDisease = gBit(Convert.ToBoolean(dr["EnfermedadRiñon"]));
                            _personalMedicalHistory.KidneyDiseaseAnswers = gList(dr["ER_Respuesta"].ToString(), Answers.KidneyDiseaseAnswers);
                            _personalMedicalHistory.NeurologicalDiseases = gBit(Convert.ToBoolean(dr["EnfermedadNeurologicas"]));
                            _personalMedicalHistory.NeurologicalDiseasesAnswers = gList(dr["EN_Respuesta"].ToString(), Answers.NeurologicalDiseasesAnswers);
                            _personalMedicalHistory.PsychiatricIllnesses = gBit(Convert.ToBoolean(dr["EnfermedadPsiquiatrica"]));
                            _personalMedicalHistory.PsychiatricIllnessesAnswers = gList(dr["EPS"].ToString(), Answers.PsychiatricIllnessesAnswers);
                            _personalMedicalHistory.MusculoskeletalDiseases = gBit(Convert.ToBoolean(dr["EnfermedadOsteoMuscular"]));
                            _personalMedicalHistory.MusculoskeletalDiseasesAnswers = gList(dr["EOM_Respuesta"].ToString(), Answers.MusculoskeletalDiseasesAnswers);
                            _personalMedicalHistory.SkinDisease = gBit(Convert.ToBoolean(dr["EnfermedadPiel"]));
                            _personalMedicalHistory.NailDisease = gBit(Convert.ToBoolean(dr["EnfermedadUñas"]));
                            _personalMedicalHistory.HairDisease = gBit(Convert.ToBoolean(dr["EnfermedadPelo"]));
                            _personalMedicalHistory.InfectiousDiseases = gBit(Convert.ToBoolean(dr["EnfermedadesInfecciosas"]));
                            _personalMedicalHistory.InfectiousDiseasesAnswers = gList(dr["EIF_Respuesta"].ToString(), Answers.InfectiousDiseasesAnswers);
                            _personalMedicalHistory.MajorAccidents = gBit(Convert.ToBoolean(dr["AccidentesDeImportancia"]));
                            _personalMedicalHistory.BloodTransfusion = gBit(Convert.ToBoolean(dr["TransfusionSangre"]));
                            _personalMedicalHistory.AdmissionToHospital = gBit(Convert.ToBoolean(dr["IngresoHospital"]));
                            _personalMedicalHistory.AdmissionToHospitalAnswers = gList(dr["IH_Respuesta"].ToString(), Answers.AdmissionToHospitalAnswers);
                            _personalMedicalHistory.SurgeriesPerformed = dr["CirugiasRealizada"].ToString();
                            _personalMedicalHistory.SomeSequel = gBit(Convert.ToBoolean(dr["TieneSecuela"]));
                            _personalMedicalHistory.PhysicalOrPsychologicalImpairment = gBit(Convert.ToBoolean(dr["TieneUstImpFisPsicoEmocinal"]));
                            _personalMedicalHistory.ChronicIllness = gBit(Convert.ToBoolean(dr["TieneEfermedadCronica"]));
                            _personalMedicalHistory.ChronicIllnessAnswers = gList(dr["ECR_Respuesta"].ToString(), Answers.ChronicIllnessAnswers);
                            _personalMedicalHistory.ChronicIllnessOther = dr["ECR_Otra"].ToString();
                            _personalMedicalHistory.SufferedFromCancerOrMalignantTumor = gBit(Convert.ToBoolean(dr["PadecidoCancerOrTumor"]));
                            _personalMedicalHistory.HaveVaricoseVeins = gBit(Convert.ToBoolean(dr["Varices"]));
                            _personalMedicalHistory.Tabaco = dr["Tabaco"].ToString();
                            _personalMedicalHistory.TabacoDate = dr["DejeDeFumarHace"].ToString();
                            if (Tools.IsNULL(Convert.ToInt32(_personalMedicalHistory.Tabaco)) == 4) { _personalMedicalHistory.TabacoQuantity = dr["DiariamenteFumo"].ToString(); }
                            else if (Tools.IsNULL(Convert.ToInt32(_personalMedicalHistory.Tabaco)) == 5) { _personalMedicalHistory.TabacoQuantity = dr["SemanalmenteFumo"].ToString(); }
                            else { _personalMedicalHistory.TabacoQuantity = "0"; }
                            _personalMedicalHistory.Alcohol = dr["Alcohol"].ToString();
                            _personalMedicalHistory.Drink = dr["TipoBebida"].ToString();
                            _personalMedicalHistory.Drugs = dr["Drogas"].ToString();
                            _personalMedicalHistory.DrugsDate = dr["DejeDeConsumir"].ToString();
                            _personalMedicalHistory.DrugType = dr["TipoDroga"].ToString();
                            _personalMedicalHistory.PhysicalActivity = dr["Deportes"].ToString();
                            _personalMedicalHistory.PhysicalActivityType = dr["TipoEjercicio"].ToString();
                            _personalMedicalHistory.PhysicalActivityTimeSpent = dr["TiempoDedicado"].ToString();
                            _personalMedicalHistory.UseOfMedications = gBit(Convert.ToBoolean(dr["UsoMedicamentos"]));
                            _personalMedicalHistory.UseOfMedicationsAnswers = gList(dr["UM_Respuesta"].ToString(), Answers.UseOfMedicationsAnswers);
                            _personalMedicalHistory.MedicationName = dr["UM_NombreMedicamento"].ToString();
                            _personalMedicalHistory.UsedFor = dr["UM_UsadoPara"].ToString();
                            _personalMedicalHistory.TimeOfsleeping = Convert.ToDecimal(dr["HorasSueño"]);
                            _personalMedicalHistory.OtherJobsToBePerformed = gBit(Convert.ToBoolean(dr["OtrosTrabajos"]));
                            _personalMedicalHistory.OtherJobsWhere = dr["OT_Donde"].ToString();
                            _personalMedicalHistory.OtherJobsWhatsHeDoing = dr["OT_QueHace"].ToString();
                            _personalMedicalHistory.OtherJobsSince = dr["OT_Desde"].ToString();
                            _personalMedicalHistory.PerformHouseholdChores = gBit(Convert.ToBoolean(dr["RealizaTareasDomesticas"]));

                        }
                      
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _personalMedicalHistory;



        }
        private GynecologicalAntecento GetGynecologicalAntecento(int gaId)
        {
            GynecologicalAntecento _gynecologicalAntecento = new GynecologicalAntecento();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_AntecedentesMedicosGinecilogicos_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_AntecedenteGine", System.Data.SqlDbType.Int, gaId);
                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            _gynecologicalAntecento.Id = Convert.ToInt32(dr["Id_AntecedenteGine"]);
                            _gynecologicalAntecento.Pregnancies = Tools.IsNULL(Convert.ToInt32(dr["Embarazos"]));
                            _gynecologicalAntecento.Births = Tools.IsNULL(Convert.ToInt32(dr["Partos"]));
                            _gynecologicalAntecento.Cesarias = Tools.IsNULL(Convert.ToInt32(dr["Cesarias"]));
                            _gynecologicalAntecento.Abortion = Tools.IsNULL(Convert.ToInt32(dr["Abortos"]));
                            _gynecologicalAntecento.ChildrensBirthDates = Tools.IsNULL((dr["FechasNacimientosHijos"].ToString()));
                            _gynecologicalAntecento.MenstrualIrregularities = gBit(Convert.ToBoolean(dr["IrregularidadesMestruales"]));
                            _gynecologicalAntecento.Infections = gBit(Convert.ToBoolean(dr["Infecciones"]));
                            _gynecologicalAntecento.Cysts = gBit(Convert.ToBoolean(dr["QuistesOREnfOvarios"]));
                            _gynecologicalAntecento.Sterility = gBit(Convert.ToBoolean(dr["Esterilidad"]));
                            _gynecologicalAntecento.OtherProblems = gBit(Convert.ToBoolean(dr["OtrosProblemas"]));
                            _gynecologicalAntecento.OtherProblemsDesc = Tools.IsNULL(dr["OtrosProblemas_Desc"].ToString());
                            _gynecologicalAntecento.BreastBall = gBit(Convert.ToBoolean(dr["BultoORNoduloORBolita"]));
                            _gynecologicalAntecento.BreastCysts = gBit(Convert.ToBoolean(dr["QuistesSenos"]));
                            _gynecologicalAntecento.Secretion = gBit(Convert.ToBoolean(dr["Secrecion"]));
                            _gynecologicalAntecento.OtherProblemsBreast = Tools.IsNULL(dr["OtrosProblemasEnSenos_Desc"].ToString());
                            _gynecologicalAntecento.GynecologicalReviews = gBit(Convert.ToBoolean(dr["RevisionesGinecologicas"]));
                            _gynecologicalAntecento.LastDateMedicalReview = dr["FechaUltimaRevision"].ToString();
                            _gynecologicalAntecento.placeAnswers = gList(dr["Lugar"].ToString(),Answers.MedicalControlAnswers);
                            _gynecologicalAntecento.LastDateCancerScreeningTest = dr["FechaUltExamDetCancer"].ToString();
                            _gynecologicalAntecento.LastDateCancerScreeningTestAnswers = gList(dr["FUEDC_Respuesta"].ToString(), new List<SelectListItem> { new SelectListItem { Text = "No lo Recuerdo", Value = "0", Selected = false } });
                            _gynecologicalAntecento.ContraceptiveMethod = gBit(Convert.ToBoolean(dr["UsaMetodoAnticonceptivo"]));
                            _gynecologicalAntecento.ContraceptiveMethodDesc = Tools.IsNULL(dr["cual"].ToString());
                            _gynecologicalAntecento.LastMenstruation = dr["UltFechaMenstruacion"].ToString();
                            _gynecologicalAntecento.LastMenstruationAnswers = gList(dr["UFM_Respuesta"].ToString(), new List<SelectListItem> { new SelectListItem { Text = "No lo Recuerdo", Value = "0", Selected = false } });
                            _gynecologicalAntecento.ArePregnated = gBit(Convert.ToBoolean(dr["EstaEmbarazada"]));
                            _gynecologicalAntecento.DueDate = dr["FechaParto"].ToString();
                            _gynecologicalAntecento.SuspectedPregnancy = gBit(Convert.ToBoolean(dr["SospechaEmbarazo"]));

                        }
                        if (!dr.HasRows)
                        {

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _gynecologicalAntecento;
        }

        public bool SaveMedicalQuestion(MedicalQuestion medical)
        {
            bool _save = false;
            SqlCommand _cmd;
            SqlTransaction _tr;

            using (_tr = Conexion.creaTransaccion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("Cat_AntecedentesMedicosPersonales_Add", _tr);
                    Conexion.creaParametro(_cmd, "@PresionArterial", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.BloodPressure));
                    Conexion.creaParametro(_cmd, "@Glucosa", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.Glucose));
                    Conexion.creaParametro(_cmd, "@IMC", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.IMC));
                    Conexion.creaParametro(_cmd, "@Observaciones_IMC", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.Remarks));
                    Conexion.creaParametro(_cmd, "@UsoLentesORPup", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.WearGlassesOrPupils));
                    Conexion.creaParametro(_cmd, "@ULP_Desde", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.WearGlassesOrPupilsSince);
                    Conexion.creaParametro(_cmd, "@ULP_Respuesta", System.Data.SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.WearGlassesOrPupilsAnswer)));
                    Conexion.creaParametro(_cmd, "@AntecedentesFamiliares", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.FamilyHistoryAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadCongenORHered", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.CongenitalOrInheritedDisease));
                    Conexion.creaParametro(_cmd, "@DeformidadCongenita", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.CongenitalDeformity));
                    Conexion.creaParametro(_cmd, "@EnfermedadesInfantiles", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.ChildhoodDiseases));
                    Conexion.creaParametro(_cmd, "@EI_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.ChildhoodDiseasesAnswers));
                    Conexion.creaParametro(_cmd, "@EI_Otras", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.ChildhoodDiseasesOther));
                    Conexion.creaParametro(_cmd, "@Alergias", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.Allergies));
                    Conexion.creaParametro(_cmd, "@All_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.AllergiesAnswers));
                    Conexion.creaParametro(_cmd, "@UsaLentesORLentesContac", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.WearGlassesOrContactLenses));
                    Conexion.creaParametro(_cmd, "@ULLC_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.WearGlassesOrContactLensesAnswers));
                    Conexion.creaParametro(_cmd, "@VeUstedBien", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.SeeYourselfWell));
                    Conexion.creaParametro(_cmd, "@OjoDerecho", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.RightEye));
                    Conexion.creaParametro(_cmd, "@OjoIzquierdo", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.LeftEye));
                    Conexion.creaParametro(_cmd, "@CirugiaOcular", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.EyeSurgery));
                    Conexion.creaParametro(_cmd, "@CO_Por", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.EyeSurgeryBy));
                    Conexion.creaParametro(_cmd, "@CO_Fecha", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.dateSurgery);
                    Conexion.creaParametro(_cmd, "@EnfermedadEnOidos", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.EarDisease));
                    Conexion.creaParametro(_cmd, "@EO_Cual", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.EarDiseaseAnswer));
                    Conexion.creaParametro(_cmd, "@OyeBienUsted", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.ListenWell));
                    Conexion.creaParametro(_cmd, "@UsaAparatoAuditivo", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.UseHearingAid));
                    Conexion.creaParametro(_cmd, "@UAA_Desde", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.UseHearingAidsince);
                    Conexion.creaParametro(_cmd, "@EnfermedadDental", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.DentalDiseases));
                    Conexion.creaParametro(_cmd, "@ED_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.DentalDiseasesAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadHormonal", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.HormonalDiseasesAnswers));
                    Conexion.creaParametro(_cmd, "@EH_Otras", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.HormonalDiseasesOther));
                    Conexion.creaParametro(_cmd, "@EnfermedadPulmones", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.LungDisease));
                    Conexion.creaParametro(_cmd, "@EP_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.LungDiseaseAnswers));
                    Conexion.creaParametro(_cmd, "@EP_Otras", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.LungDiseaseOther));
                    Conexion.creaParametro(_cmd, "@EnfermedadCorazon", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.HeartDisease));
                    Conexion.creaParametro(_cmd, "@EC_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.HeartDiseaseAnswers));
                    Conexion.creaParametro(_cmd, "@AlteracionArterial", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.AlteredBloodPressure));
                    Conexion.creaParametro(_cmd, "@AA_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.AlteredBloodPressureAnswers));
                    Conexion.creaParametro(_cmd, "@AA_Medicamentos", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.AlteredBloodPressureMedicaments));
                    Conexion.creaParametro(_cmd, "@AA_EstoyControlMedico", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.AlteredBloodPressureMedicalControl));
                    Conexion.creaParametro(_cmd, "@AA_CM_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.AlteredBloodPressureMedicalControlAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadesDigestivas", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.DigestiveDisease));
                    Conexion.creaParametro(_cmd, "@EDG_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.DigestiveDiseaseAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadesHigado", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.LiverDisease));
                    Conexion.creaParametro(_cmd, "@EH_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.LiverDiseaseAnswers));
                    Conexion.creaParametro(_cmd, "@DiabetesMellitus", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.DiabetesMellitus));
                    Conexion.creaParametro(_cmd, "@DM_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.DiabetesMellitusAnswers));
                    Conexion.creaParametro(_cmd, "@DM_FechaUltExamSangre", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.DateOfLastExamination);
                    Conexion.creaParametro(_cmd, "@DM_ControlMedicoDiabettes", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.DiabetesMellitusMedicControl));
                    Conexion.creaParametro(_cmd, "@DM_CM_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.DiabetesMellitusMedicControlAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadColesterol", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.CholesterolDisease));
                    Conexion.creaParametro(_cmd, "@ECO_Medicamentos", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.CholesterolDiseaseMedications));
                    Conexion.creaParametro(_cmd, "@EnfermedadAcidoUrico", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.UricAcidDisease));
                    Conexion.creaParametro(_cmd, "@EAU_Medicamentos", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.UricAcidDiseaseMedication));
                    Conexion.creaParametro(_cmd, "@EnfermedadRiñon", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.KidneyDisease));
                    Conexion.creaParametro(_cmd, "@ER_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.KidneyDiseaseAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadNeurologicas", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.NeurologicalDiseases));
                    Conexion.creaParametro(_cmd, "@EN_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.NeurologicalDiseasesAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadPsiquiatrica", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.PsychiatricIllnesses));
                    Conexion.creaParametro(_cmd, "@EPS_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.PsychiatricIllnessesAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadOsteoMuscular", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.MusculoskeletalDiseases));
                    Conexion.creaParametro(_cmd, "@EOM_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.MusculoskeletalDiseasesAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadPiel", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.SkinDisease));
                    Conexion.creaParametro(_cmd, "@EnfermedadUñas", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.NailDisease));
                    Conexion.creaParametro(_cmd, "@EnfermedadPelo", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.HairDisease));
                    Conexion.creaParametro(_cmd, "@EnfermedadesInfecciosas", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.InfectiousDiseases));
                    Conexion.creaParametro(_cmd, "@EIF_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.InfectiousDiseasesAnswers));
                    Conexion.creaParametro(_cmd, "@AccidentesDeImportancia", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.MajorAccidents));
                    Conexion.creaParametro(_cmd, "@TransfusionSangre", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.BloodTransfusion));
                    Conexion.creaParametro(_cmd, "@IngresoHospital", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.AdmissionToHospital));
                    Conexion.creaParametro(_cmd, "@IH_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.AdmissionToHospitalAnswers));
                    Conexion.creaParametro(_cmd, "@CirugiasRealizada", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.SurgeriesPerformed));
                    Conexion.creaParametro(_cmd, "@TieneSecuela", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.SomeSequel));
                    Conexion.creaParametro(_cmd, "@TieneUstImpFisPsicoEmocinal", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.PhysicalOrPsychologicalImpairment));
                    Conexion.creaParametro(_cmd, "@TieneEfermedadCronica", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.ChronicIllness));
                    Conexion.creaParametro(_cmd, "@ECR_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.ChronicIllnessAnswers));
                    Conexion.creaParametro(_cmd, "@ECR_Otra", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.ChronicIllnessOther));
                    Conexion.creaParametro(_cmd, "@PadecidoCancerOrTumor", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.SufferedFromCancerOrMalignantTumor));
                    Conexion.creaParametro(_cmd, "@Varices", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.HaveVaricoseVeins));
                    Conexion.creaParametro(_cmd, "@Tabaco", System.Data.SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.Tabaco)));
                    Conexion.creaParametro(_cmd, "@DejeDeFumarHace", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.TabacoDate);
                    Conexion.creaParametro(_cmd, "@DiariamenteFumo", System.Data.SqlDbType.Int, (Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.Tabaco)) == 4) ? medical.personalMedicalHistory.TabacoQuantity : 0);
                    Conexion.creaParametro(_cmd, "@SemanalmenteFumo", System.Data.SqlDbType.Int, (Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.Tabaco)) == 5) ? medical.personalMedicalHistory.TabacoQuantity : 0);
                    Conexion.creaParametro(_cmd, "@Alcohol", System.Data.SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.Alcohol)));
                    Conexion.creaParametro(_cmd, "@TipoBebida", System.Data.SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.Drink)));
                    Conexion.creaParametro(_cmd, "@Drogas", System.Data.SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.Drugs)));
                    Conexion.creaParametro(_cmd, "@DejeDeConsumir", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.DrugsDate);
                    Conexion.creaParametro(_cmd, "@TipoDroga", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.DrugType));
                    Conexion.creaParametro(_cmd, "@Deportes", System.Data.SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.PhysicalActivity)));
                    Conexion.creaParametro(_cmd, "@TipoEjercicio", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.PhysicalActivityType));
                    Conexion.creaParametro(_cmd, "@TiempoDedicado", System.Data.SqlDbType.Decimal, Tools.IsNULL(Convert.ToDecimal(medical.personalMedicalHistory.PhysicalActivityTimeSpent)));
                    Conexion.creaParametro(_cmd, "@UsoMedicamentos", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.UseOfMedications));
                    Conexion.creaParametro(_cmd, "@UM_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.UseOfMedicationsAnswers));
                    Conexion.creaParametro(_cmd, "@UM_NombreMedicamento", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.MedicationName));
                    Conexion.creaParametro(_cmd, "@UM_UsadoPara", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.UsedFor));
                    Conexion.creaParametro(_cmd, "@HorasSueño", System.Data.SqlDbType.Decimal, Tools.IsNULL(Convert.ToDecimal(medical.personalMedicalHistory.TimeOfsleeping)));
                    Conexion.creaParametro(_cmd, "@OtrosTrabajos", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.OtherJobsToBePerformed));
                    Conexion.creaParametro(_cmd, "@OT_Donde", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.OtherJobsWhere));
                    Conexion.creaParametro(_cmd, "@OT_QueHace", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.OtherJobsWhatsHeDoing));
                    Conexion.creaParametro(_cmd, "@OT_Desde", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.OtherJobsSince);
                    Conexion.creaParametro(_cmd, "@RealizaTareasDomesticas", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.PerformHouseholdChores));
                    var _Insert_PMH = Conexion.ejecutaScalar(_cmd);

                    //limpiar parametros y crear la seccion de antecedentes ginecologicos
                    _cmd.Parameters.Clear();
                    _cmd = Conexion.creaComando("Cat_AntecedentesMedicosGinecilogicos_Add", _tr);
                    Conexion.creaParametro(_cmd, "@Embarazos", System.Data.SqlDbType.Int, Tools.IsNULL(medical.gynecologicalAntecento.Pregnancies));
                    Conexion.creaParametro(_cmd, "@Partos", System.Data.SqlDbType.Int, Tools.IsNULL(medical.gynecologicalAntecento.Births));
                    Conexion.creaParametro(_cmd, "@Cesarias", System.Data.SqlDbType.Int, Tools.IsNULL(medical.gynecologicalAntecento.Cesarias));
                    Conexion.creaParametro(_cmd, "@Abortos", System.Data.SqlDbType.Int, Tools.IsNULL(medical.gynecologicalAntecento.Abortion));
                    Conexion.creaParametro(_cmd, "@FechasNacimientosHijos", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.gynecologicalAntecento.ChildrensBirthDates));
                    Conexion.creaParametro(_cmd, "@IrregularidadesMestruales", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.MenstrualIrregularities));
                    Conexion.creaParametro(_cmd, "@Infecciones", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.Infections));
                    Conexion.creaParametro(_cmd, "@QuistesOREnfOvarios", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.Cysts));
                    Conexion.creaParametro(_cmd, "@Esterilidad", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.Sterility));
                    Conexion.creaParametro(_cmd, "@OtrosProblemas", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.OtherProblems));
                    Conexion.creaParametro(_cmd, "@OtrosProblemas_Desc", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.gynecologicalAntecento.OtherProblemsDesc));
                    Conexion.creaParametro(_cmd, "@BultoORNoduloORBolita", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.BreastBall));
                    Conexion.creaParametro(_cmd, "@QuistesSenos", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.BreastCysts));
                    Conexion.creaParametro(_cmd, "@Secrecion", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.Secretion));
                    Conexion.creaParametro(_cmd, "@OtrosProblemasEnSenos_Desc", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.gynecologicalAntecento.OtherProblemsBreastDesc));
                    Conexion.creaParametro(_cmd, "@RevisionesGinecologicas", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.GynecologicalReviews));
                    Conexion.creaParametro(_cmd, "@FechaUltimaRevision", System.Data.SqlDbType.DateTime, Tools.IsNULL(medical.gynecologicalAntecento.LastDateMedicalReview));
                    Conexion.creaParametro(_cmd, "@Lugar", System.Data.SqlDbType.VarChar, gJson(medical.gynecologicalAntecento.placeAnswers));
                    Conexion.creaParametro(_cmd, "@FechaUltExamDetCancer", System.Data.SqlDbType.DateTime, Tools.IsNULL(medical.gynecologicalAntecento.LastDateCancerScreeningTest));
                    Conexion.creaParametro(_cmd, "@FUEDC_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.gynecologicalAntecento.LastDateCancerScreeningTestAnswers));
                    Conexion.creaParametro(_cmd, "@UsaMetodoAnticonceptivo", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.ContraceptiveMethod));
                    Conexion.creaParametro(_cmd, "@Cual", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.gynecologicalAntecento.ContraceptiveMethodDesc));
                    Conexion.creaParametro(_cmd, "@UltFechaMenstruacion", System.Data.SqlDbType.DateTime, medical.gynecologicalAntecento.LastMenstruation);
                    Conexion.creaParametro(_cmd, "@UFM_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.gynecologicalAntecento.LastMenstruationAnswers));
                    Conexion.creaParametro(_cmd, "@EstaEmbarazada", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.ArePregnated));
                    Conexion.creaParametro(_cmd, "@FechaParto", System.Data.SqlDbType.DateTime, Tools.IsNULL(medical.gynecologicalAntecento.DueDate));
                    Conexion.creaParametro(_cmd, "@SospechaEmbarazo", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.SuspectedPregnancy));
                    var _Insert_GA = Conexion.ejecutaScalar(_cmd);

                    //limpiar parametros y crear cuestionario medico
                    _cmd.Parameters.Clear();
                    _cmd = Conexion.creaComando("Cat_Empleados_CuestionarioMedico_Add", _tr);
                    Conexion.creaParametro(_cmd, "@Id_Prospecto", System.Data.SqlDbType.Int, Tools.IsNULL(medical.ProspectusId));
                    Conexion.creaParametro(_cmd, "@Id_Empleado", System.Data.SqlDbType.Int, Tools.IsNULL(medical.EmployeerId));
                    Conexion.creaParametro(_cmd, "@Id_AntecedenteMP", System.Data.SqlDbType.Int, _Insert_PMH);
                    Conexion.creaParametro(_cmd, "@Id_AntecedenteGine", System.Data.SqlDbType.Int, _Insert_GA);
                    Conexion.creaParametro(_cmd, "@Usuario_Registro", System.Data.SqlDbType.Int, User_Persistent_Data.Id);
                    Conexion.creaParametro(_cmd, "@Estatus", System.Data.SqlDbType.VarChar, "A");
                    var saveCM = Conexion.ejecutarNonquery(_cmd);
                    if (saveCM > 0)
                    {
                        _tr.Commit();
                        _save = true;
                    }
                    else
                    {
                        _tr.Rollback();
                    }



                }
                catch (Exception ex)
                {
                    _tr.Rollback();
                }
            }

            return _save;
        }
        public bool UpdateMedicalQuestion(MedicalQuestion medical)
        {
            bool _update = false;
            SqlCommand _cmd;
            SqlTransaction _tr;

            using (_tr = Conexion.creaTransaccion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("Cat_AntecedentesMedicosPersonales_Upd", _tr);
                    Conexion.creaParametro(_cmd, "@Id_AntecedenteMP", System.Data.SqlDbType.Int, medical.personalMedicalHistory.Id);
                    Conexion.creaParametro(_cmd, "@PresionArterial", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.BloodPressure));
                    Conexion.creaParametro(_cmd, "@Glucosa", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.Glucose));
                    Conexion.creaParametro(_cmd, "@IMC", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.IMC));
                    Conexion.creaParametro(_cmd, "@Observaciones_IMC", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.Remarks));
                    Conexion.creaParametro(_cmd, "@UsoLentesORPup", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.WearGlassesOrPupils));
                    Conexion.creaParametro(_cmd, "@ULP_Desde", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.WearGlassesOrPupilsSince);
                    Conexion.creaParametro(_cmd, "@ULP_Respuesta", System.Data.SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.WearGlassesOrPupilsAnswer)));
                    Conexion.creaParametro(_cmd, "@AntecedentesFamiliares", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.FamilyHistoryAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadCongenORHered", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.CongenitalOrInheritedDisease));
                    Conexion.creaParametro(_cmd, "@DeformidadCongenita", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.CongenitalDeformity));
                    Conexion.creaParametro(_cmd, "@EnfermedadesInfantiles", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.ChildhoodDiseases));
                    Conexion.creaParametro(_cmd, "@EI_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.ChildhoodDiseasesAnswers));
                    Conexion.creaParametro(_cmd, "@EI_Otras", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.ChildhoodDiseasesOther));
                    Conexion.creaParametro(_cmd, "@Alergias", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.Allergies));
                    Conexion.creaParametro(_cmd, "@All_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.AllergiesAnswers));
                    Conexion.creaParametro(_cmd, "@UsaLentesORLentesContac", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.WearGlassesOrContactLenses));
                    Conexion.creaParametro(_cmd, "@ULLC_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.WearGlassesOrContactLensesAnswers));
                    Conexion.creaParametro(_cmd, "@VeUstedBien", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.SeeYourselfWell));
                    Conexion.creaParametro(_cmd, "@OjoDerecho", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.RightEye));
                    Conexion.creaParametro(_cmd, "@OjoIzquierdo", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.LeftEye));
                    Conexion.creaParametro(_cmd, "@CirugiaOcular", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.EyeSurgery));
                    Conexion.creaParametro(_cmd, "@CO_Por", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.EyeSurgeryBy));
                    Conexion.creaParametro(_cmd, "@CO_Fecha", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.dateSurgery);
                    Conexion.creaParametro(_cmd, "@EnfermedadEnOidos", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.EarDisease));
                    Conexion.creaParametro(_cmd, "@EO_Cual", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.EarDiseaseAnswer));
                    Conexion.creaParametro(_cmd, "@OyeBienUsted", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.ListenWell));
                    Conexion.creaParametro(_cmd, "@UsaAparatoAuditivo", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.UseHearingAid));
                    Conexion.creaParametro(_cmd, "@UAA_Desde", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.UseHearingAidsince);
                    Conexion.creaParametro(_cmd, "@EnfermedadDental", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.DentalDiseases));
                    Conexion.creaParametro(_cmd, "@ED_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.DentalDiseasesAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadHormonal", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.HormonalDiseasesAnswers));
                    Conexion.creaParametro(_cmd, "@EH_Otras", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.HormonalDiseasesOther));
                    Conexion.creaParametro(_cmd, "@EnfermedadPulmones", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.LungDisease));
                    Conexion.creaParametro(_cmd, "@EP_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.LungDiseaseAnswers));
                    Conexion.creaParametro(_cmd, "@EP_Otras", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.LungDiseaseOther));
                    Conexion.creaParametro(_cmd, "@EnfermedadCorazon", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.HeartDisease));
                    Conexion.creaParametro(_cmd, "@EC_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.HeartDiseaseAnswers));
                    Conexion.creaParametro(_cmd, "@AlteracionArterial", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.AlteredBloodPressure));
                    Conexion.creaParametro(_cmd, "@AA_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.AlteredBloodPressureAnswers));
                    Conexion.creaParametro(_cmd, "@AA_Medicamentos", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.AlteredBloodPressureMedicaments));
                    Conexion.creaParametro(_cmd, "@AA_EstoyControlMedico", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.AlteredBloodPressureMedicalControl));
                    Conexion.creaParametro(_cmd, "@AA_CM_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.AlteredBloodPressureMedicalControlAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadesDigestivas", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.DigestiveDisease));
                    Conexion.creaParametro(_cmd, "@EDG_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.DigestiveDiseaseAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadesHigado", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.LiverDisease));
                    Conexion.creaParametro(_cmd, "@EH_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.LiverDiseaseAnswers));
                    Conexion.creaParametro(_cmd, "@DiabetesMellitus", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.DiabetesMellitus));
                    Conexion.creaParametro(_cmd, "@DM_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.DiabetesMellitusAnswers));
                    Conexion.creaParametro(_cmd, "@DM_FechaUltExamSangre", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.DateOfLastExamination);
                    Conexion.creaParametro(_cmd, "@DM_ControlMedicoDiabettes", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.DiabetesMellitusMedicControl));
                    Conexion.creaParametro(_cmd, "@DM_CM_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.DiabetesMellitusMedicControlAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadColesterol", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.CholesterolDisease));
                    Conexion.creaParametro(_cmd, "@ECO_Medicamentos", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.CholesterolDiseaseMedications));
                    Conexion.creaParametro(_cmd, "@EnfermedadAcidoUrico", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.UricAcidDisease));
                    Conexion.creaParametro(_cmd, "@EAU_Medicamentos", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.UricAcidDiseaseMedication));
                    Conexion.creaParametro(_cmd, "@EnfermedadRiñon", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.KidneyDisease));
                    Conexion.creaParametro(_cmd, "@ER_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.KidneyDiseaseAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadNeurologicas", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.NeurologicalDiseases));
                    Conexion.creaParametro(_cmd, "@EN_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.NeurologicalDiseasesAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadPsiquiatrica", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.PsychiatricIllnesses));
                    Conexion.creaParametro(_cmd, "@EPS_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.PsychiatricIllnessesAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadOsteoMuscular", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.MusculoskeletalDiseases));
                    Conexion.creaParametro(_cmd, "@EOM_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.MusculoskeletalDiseasesAnswers));
                    Conexion.creaParametro(_cmd, "@EnfermedadPiel", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.SkinDisease));
                    Conexion.creaParametro(_cmd, "@EnfermedadUñas", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.NailDisease));
                    Conexion.creaParametro(_cmd, "@EnfermedadPelo", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.HairDisease));
                    Conexion.creaParametro(_cmd, "@EnfermedadesInfecciosas", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.InfectiousDiseases));
                    Conexion.creaParametro(_cmd, "@EIF_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.InfectiousDiseasesAnswers));
                    Conexion.creaParametro(_cmd, "@AccidentesDeImportancia", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.MajorAccidents));
                    Conexion.creaParametro(_cmd, "@TransfusionSangre", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.BloodTransfusion));
                    Conexion.creaParametro(_cmd, "@IngresoHospital", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.AdmissionToHospital));
                    Conexion.creaParametro(_cmd, "@IH_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.AdmissionToHospitalAnswers));
                    Conexion.creaParametro(_cmd, "@CirugiasRealizada", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.SurgeriesPerformed));
                    Conexion.creaParametro(_cmd, "@TieneSecuela", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.SomeSequel));
                    Conexion.creaParametro(_cmd, "@TieneUstImpFisPsicoEmocinal", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.PhysicalOrPsychologicalImpairment));
                    Conexion.creaParametro(_cmd, "@TieneEfermedadCronica", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.ChronicIllness));
                    Conexion.creaParametro(_cmd, "@ECR_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.ChronicIllnessAnswers));
                    Conexion.creaParametro(_cmd, "@ECR_Otra", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.ChronicIllnessOther));
                    Conexion.creaParametro(_cmd, "@PadecidoCancerOrTumor", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.SufferedFromCancerOrMalignantTumor));
                    Conexion.creaParametro(_cmd, "@Varices", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.HaveVaricoseVeins));
                    Conexion.creaParametro(_cmd, "@Tabaco", System.Data.SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.Tabaco)));
                    Conexion.creaParametro(_cmd, "@DejeDeFumarHace", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.TabacoDate);
                    Conexion.creaParametro(_cmd, "@DiariamenteFumo", System.Data.SqlDbType.Int, (Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.Tabaco)) == 4) ? medical.personalMedicalHistory.TabacoQuantity : 0);
                    Conexion.creaParametro(_cmd, "@SemanalmenteFumo", System.Data.SqlDbType.Int, (Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.Tabaco)) == 5) ? medical.personalMedicalHistory.TabacoQuantity : 0);
                    Conexion.creaParametro(_cmd, "@Alcohol", System.Data.SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.Alcohol)));
                    Conexion.creaParametro(_cmd, "@TipoBebida", System.Data.SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.Drink)));
                    Conexion.creaParametro(_cmd, "@Drogas", System.Data.SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.Drugs)));
                    Conexion.creaParametro(_cmd, "@DejeDeConsumir", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.DrugsDate);
                    Conexion.creaParametro(_cmd, "@TipoDroga", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.DrugType));
                    Conexion.creaParametro(_cmd, "@Deportes", System.Data.SqlDbType.Int, Tools.IsNULL(Convert.ToInt32(medical.personalMedicalHistory.PhysicalActivity)));
                    Conexion.creaParametro(_cmd, "@TipoEjercicio", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.PhysicalActivityType));
                    Conexion.creaParametro(_cmd, "@TiempoDedicado", System.Data.SqlDbType.Decimal, Tools.IsNULL(Convert.ToDecimal(medical.personalMedicalHistory.PhysicalActivityTimeSpent)));
                    Conexion.creaParametro(_cmd, "@UsoMedicamentos", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.UseOfMedications));
                    Conexion.creaParametro(_cmd, "@UM_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.personalMedicalHistory.UseOfMedicationsAnswers));
                    Conexion.creaParametro(_cmd, "@UM_NombreMedicamento", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.MedicationName));
                    Conexion.creaParametro(_cmd, "@UM_UsadoPara", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.UsedFor));
                    Conexion.creaParametro(_cmd, "@HorasSueño", System.Data.SqlDbType.Decimal, Tools.IsNULL(Convert.ToDecimal(medical.personalMedicalHistory.TimeOfsleeping)));
                    Conexion.creaParametro(_cmd, "@OtrosTrabajos", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.OtherJobsToBePerformed));
                    Conexion.creaParametro(_cmd, "@OT_Donde", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.OtherJobsWhere));
                    Conexion.creaParametro(_cmd, "@OT_QueHace", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.personalMedicalHistory.OtherJobsWhatsHeDoing));
                    Conexion.creaParametro(_cmd, "@OT_Desde", System.Data.SqlDbType.DateTime, medical.personalMedicalHistory.OtherJobsSince);
                    Conexion.creaParametro(_cmd, "@RealizaTareasDomesticas", System.Data.SqlDbType.Bit, gBit(medical.personalMedicalHistory.PerformHouseholdChores));
                    var _Update_PMH = Conexion.ejecutarNonquery(_cmd);

                    //limpiar parametros y crear la seccion de antecedentes ginecologicos
                    _cmd.Parameters.Clear();
                    _cmd = Conexion.creaComando("Cat_AntecedentesMedicosGinecilogicos_Upd", _tr);
                    Conexion.creaParametro(_cmd, "@Id_AntecedenteGine", System.Data.SqlDbType.Int, medical.gynecologicalAntecento.Id);
                    Conexion.creaParametro(_cmd, "@Embarazos", System.Data.SqlDbType.Int, Tools.IsNULL(medical.gynecologicalAntecento.Pregnancies));
                    Conexion.creaParametro(_cmd, "@Partos", System.Data.SqlDbType.Int, Tools.IsNULL(medical.gynecologicalAntecento.Births));
                    Conexion.creaParametro(_cmd, "@Cesarias", System.Data.SqlDbType.Int, Tools.IsNULL(medical.gynecologicalAntecento.Cesarias));
                    Conexion.creaParametro(_cmd, "@Abortos", System.Data.SqlDbType.Int, Tools.IsNULL(medical.gynecologicalAntecento.Abortion));
                    Conexion.creaParametro(_cmd, "@FechasNacimientosHijos", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.gynecologicalAntecento.ChildrensBirthDates));
                    Conexion.creaParametro(_cmd, "@IrregularidadesMestruales", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.MenstrualIrregularities));
                    Conexion.creaParametro(_cmd, "@Infecciones", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.Infections));
                    Conexion.creaParametro(_cmd, "@QuistesOREnfOvarios", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.Cysts));
                    Conexion.creaParametro(_cmd, "@Esterilidad", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.Sterility));
                    Conexion.creaParametro(_cmd, "@OtrosProblemas", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.OtherProblems));
                    Conexion.creaParametro(_cmd, "@OtrosProblemas_Desc", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.gynecologicalAntecento.OtherProblemsDesc));
                    Conexion.creaParametro(_cmd, "@BultoORNoduloORBolita", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.BreastBall));
                    Conexion.creaParametro(_cmd, "@QuistesSenos", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.BreastCysts));
                    Conexion.creaParametro(_cmd, "@Secrecion", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.Secretion));
                    Conexion.creaParametro(_cmd, "@OtrosProblemasEnSenos_Desc", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.gynecologicalAntecento.OtherProblemsBreastDesc));
                    Conexion.creaParametro(_cmd, "@RevisionesGinecologicas", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.GynecologicalReviews));
                    Conexion.creaParametro(_cmd, "@FechaUltimaRevision", System.Data.SqlDbType.DateTime, medical.gynecologicalAntecento.LastDateMedicalReview);
                    Conexion.creaParametro(_cmd, "@Lugar", System.Data.SqlDbType.VarChar, gJson(medical.gynecologicalAntecento.placeAnswers));
                    Conexion.creaParametro(_cmd, "@FechaUltExamDetCancer", System.Data.SqlDbType.DateTime, medical.gynecologicalAntecento.LastDateCancerScreeningTest);
                    Conexion.creaParametro(_cmd, "@FUEDC_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.gynecologicalAntecento.LastDateCancerScreeningTestAnswers));
                    Conexion.creaParametro(_cmd, "@UsaMetodoAnticonceptivo", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.ContraceptiveMethod));
                    Conexion.creaParametro(_cmd, "@Cual", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.gynecologicalAntecento.ContraceptiveMethodDesc));
                    Conexion.creaParametro(_cmd, "@UltFechaMenstruacion", System.Data.SqlDbType.DateTime, medical.gynecologicalAntecento.LastMenstruation);
                    Conexion.creaParametro(_cmd, "@UFM_Respuesta", System.Data.SqlDbType.VarChar, gJson(medical.gynecologicalAntecento.LastMenstruationAnswers));
                    Conexion.creaParametro(_cmd, "@EstaEmbarazada", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.ArePregnated));
                    Conexion.creaParametro(_cmd, "@FechaParto", System.Data.SqlDbType.DateTime, medical.gynecologicalAntecento.DueDate);
                    Conexion.creaParametro(_cmd, "@SospechaEmbarazo", System.Data.SqlDbType.Bit, gBit(medical.gynecologicalAntecento.SuspectedPregnancy));
                    var _Update_GA = Conexion.ejecutarNonquery(_cmd);

                    _cmd.Parameters.Clear();
                    _cmd = Conexion.creaComando("Cat_Empleados_CuestionarioMedico_Upd", _tr);
                    Conexion.creaParametro(_cmd, "@Id_Cuestionario", System.Data.SqlDbType.Int, medical.Id);
                    Conexion.creaParametro(_cmd, "@Antidoping", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.Antidoping));
                    Conexion.creaParametro(_cmd, "@Peso", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.Weight));
                    Conexion.creaParametro(_cmd, "@Altura", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.height));
                    Conexion.creaParametro(_cmd, "@ServicioMedico", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.MedicalServices));
                    Conexion.creaParametro(_cmd, "@ServicioMedicoAprobacion", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.MedicalServiceApproval));
                    Conexion.creaParametro(_cmd, "@Observaciones", System.Data.SqlDbType.VarChar, Tools.IsNULL(medical.Remarks));
                    Conexion.creaParametro(_cmd, "@Usuario_Actualiza", System.Data.SqlDbType.VarChar, User_Persistent_Data.Id);
                    var _Update_MQ = Conexion.ejecutarNonquery(_cmd);


                    if (_Update_PMH > 0 && _Update_GA > 0 && _Update_MQ>0)
                    {
                        _tr.Commit();
                        _update = true;
                    }
                    else
                    {
                        _tr.Rollback();
                    }

                }
                catch (Exception ex)
                {
                    _tr.Rollback();
                }
            }


            return _update;
        }


        #endregion

      
        // retorna lista de ciudades
        public List<SelectListItem> getDomicileCities(int country)
        {
            List<SelectListItem> _domiciles = new List<SelectListItem>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Cat_CiudadesPais_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_Pais", System.Data.SqlDbType.Int, country);
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


        private bool gBit(string data)
        {
            bool _resp = false;
            if (data == "S")
            {
                _resp = true;
            }

            return _resp;
        }
        private string gBit(bool data)
        {
            string _resp = "N";
            if (data)
            {
                _resp = "S";
            }

            return _resp;
        }

        private string gJson(List<SelectListItem> data)
        {
            try
            {
                return JsonConvert.SerializeObject(data);
            }
            catch (Exception)
            {
                return "";
            }


        }

        private List<SelectListItem> gList(string data, List<SelectListItem> list)
        {
            List<SelectListItem> _list = null;
            try
            {
                _list = JsonConvert.DeserializeObject<List<SelectListItem>>(data);
                if (_list == null)
                {
                    _list = list;
                }
            }
            catch (Exception)
            {
                _list = list;

            }
           
            return _list;

        }

    }
}
