using Modulo_Reclutamiento_Web.Models;
using Modulo_Reclutamiento_Web.Models.Exams;
using System.Data.SqlClient;

namespace Modulo_Reclutamiento_Web.Service
{
    public class ExamsService
    {
        private static ExamsService? instancia = null;

        public static ExamsService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ExamsService();
                }

                return instancia;
            }
        }
        
        //retorna si tienes un examen hecho en el sistema y asigna sus valores, en caso de que no te genera uno y asigna sus valores
        public bool ExamBarsit(int pto)
        {
            bool result = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("CREAR_EXAM_BARSIT", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, pto);

                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            User_Persistent_Data.IdExam = Convert.ToInt32(dr["Id_EXAMEN"]);
                            User_Persistent_Data.StatusExam = dr["Status"].ToString();
                            User_Persistent_Data.StageExam = Convert.ToInt32(dr["Stage"]);
                            result = true;
                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return result;

        }
        //reseteal el examen del prospecto eliminando todas las preguntas ya realizadas
        public bool ResetExam(int id_exam)
        {
            bool result = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {
                    _cmd = Conexion.creaComando("REINICIAR_EXAMEN_BARSIT", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_Examen", System.Data.SqlDbType.Int, id_exam);

                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            User_Persistent_Data.IdExam = Convert.ToInt32(dr["Id_EXAMEN"]);
                            User_Persistent_Data.StatusExam = dr["Status"].ToString();
                            User_Persistent_Data.StageExam = Convert.ToInt32(dr["Stage"]);
                            result = true;
                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }
        //retorna lista de las preguntas de ejemplo con sus respuestas
        public List<Exam> getExampleQuestionsExamBarsit()
        {
            List<Exam> _references = new List<Exam>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Get_PreguntasEjemplo_Examen_Barsit", oConexion);
                    //Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, getProspectusId(prosp));

                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _references.Add(new Exam
                            {
                                Id = Convert.ToInt32(dr["Id_Pregunta"]),
                                Question = dr["Pregunta"].ToString(),
                                Options = new List<QuestionOption> {
                                    new QuestionOption { Index =1, Answer = dr["R1"].ToString() },
                                    new QuestionOption { Index =2, Answer = dr["R2"].ToString() },
                                    new QuestionOption { Index =3, Answer = dr["R3"].ToString() },
                                    new QuestionOption { Index =4, Answer = dr["R4"].ToString() },
                                    new QuestionOption { Index =5, Answer = dr["R5"].ToString() }
                                },
                                CorrectAnswer = dr["RC"].ToString()

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
        //guarda la respuesta de la pregunta contestado en el examen
        public bool SaveQuestions(List<QuestionResponse> questions)
        {
            bool resp = false;
            foreach (var question in questions)
            {
                if (question == null)
                {
                    break;
                }

                var findquestion = User_Persistent_Data.QuestionsExam.Where(x => x.Id == question.Id);
                if (question.ChosenAnswer == findquestion.First().CorrectAnswer)
                {
                    question.Correct = true;
                }

                SqlCommand _cmd;
                SqlTransaction _tr;

                using (_tr = Conexion.creaTransaccion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
                {
                    try
                    {
                        _cmd = Conexion.creaComando("GUARDAR_PREGUNTAS_EXAMEN", _tr);
                        Conexion.creaParametro(_cmd, "@Id_Examen", System.Data.SqlDbType.Int, User_Persistent_Data.IdExam);
                        Conexion.creaParametro(_cmd, "@Id_pregunta", System.Data.SqlDbType.Int, question.Id);
                        Conexion.creaParametro(_cmd, "@Respuesta", System.Data.SqlDbType.VarChar, question.ChosenAnswer);
                        Conexion.creaParametro(_cmd, "@RespuestaCorrecta", System.Data.SqlDbType.Bit, question.Correct);



                        var insert = _cmd.ExecuteNonQuery();

                        if (insert > 0)
                        {
                            _tr.Commit();
                            resp = true;

                        }

                    }
                    catch (Exception ex)
                    {
                        _tr.Rollback();

                    }
                }


            }

            return resp;

        }
        //retorna estatus para saber si el examen ya fue contestado por el prospecto
        public bool ExamBarsitResolve(int prosp)
        {
            bool result = false;
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("EXAM_BARSIT_RESUELTO", oConexion);
                    Conexion.creaParametro(_cmd, "@Id_EmpleadoP", System.Data.SqlDbType.Int, prosp);

                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            string resuelto = dr["Resuelto"].ToString();
                            if (resuelto == "SI")
                            {
                                result = true;
                            }

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return result;

        }
        //retorna lista de preguntas y respuestas posibles para el examen en un range de 10 preguntas
        //Ejemplo 1 al 10, 11 al 20 etc
        public List<Exam> getQuestionsExamBarsit()
        {
            List<int> Range = getRangeExam();
            List<Exam> _references = new List<Exam>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Get_PreguntasExamen_Barsit", oConexion);
                    Conexion.creaParametro(_cmd, "@idIni", System.Data.SqlDbType.Int, Range[0]);
                    Conexion.creaParametro(_cmd, "@idFin", System.Data.SqlDbType.Int, Range[1]);

                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _references.Add(new Exam
                            {
                                Id = Convert.ToInt32(dr["Id_Pregunta"]),
                                Question = dr["Pregunta"].ToString(),
                                Options = new List<QuestionOption> {
                                    new QuestionOption { Index =1, Answer = dr["R1"].ToString() },
                                    new QuestionOption { Index =2, Answer = dr["R2"].ToString() },
                                    new QuestionOption { Index =3, Answer = dr["R3"].ToString() },
                                    new QuestionOption { Index =4, Answer = dr["R4"].ToString() },
                                    new QuestionOption { Index =5, Answer = dr["R5"].ToString() }
                                },
                                CorrectAnswer = dr["RC"].ToString()

                            });

                        }

                        User_Persistent_Data.QuestionsExam = _references;
                    }


                }
                catch (Exception ex)
                {

                }
            }

            return _references;
        }
        //retorno de inicio y fin de las preguntas segun etapa
        //Etapa 1: 1 al 10, etapa 2 : 11 al 20,etapa 3 : 21 al 30,etapa 4 : 31 al 40,etapa 5 : 41 al 50,etapa 6 : 51 al 60, etapa 7 ; finalizado
        private List<int> getRangeExam()
        {
            List<int> _range = new List<int>();
            switch (User_Persistent_Data.StageExam)
            {
                case 1:
                    {
                        _range.Add(1);
                        _range.Add(10);
                        break;
                    }
                case 2:
                    {
                        _range.Add(11);
                        _range.Add(20);
                        break;
                    }
                case 3:
                    {
                        _range.Add(21);
                        _range.Add(30);
                        break;
                    }
                case 4:
                    {
                        _range.Add(31);
                        _range.Add(40);
                        break;
                    }
                case 5:
                    {
                        _range.Add(41);
                        _range.Add(50);
                        break;
                    }
                case 6:
                    {
                        _range.Add(51);
                        _range.Add(60);
                        break;
                    }
                default:
                    {
                        _range.Add(0);
                        _range.Add(0);
                        break;
                    }
            }

            return _range;
        }
        //pendiente
        public List<Exam> getQuestionsExamBarsitResult()
        {
            List<int> Range = getRangeExam();
            List<Exam> _references = new List<Exam>();
            SqlCommand _cmd;
            using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))
            {
                try
                {

                    _cmd = Conexion.creaComando("Get_PreguntasExamen_Barsit_Result", oConexion);
                    Conexion.creaParametro(_cmd, "@idIni", System.Data.SqlDbType.Int, Range[0]);
                    Conexion.creaParametro(_cmd, "@idFin", System.Data.SqlDbType.Int, Range[1]);
                    Conexion.creaParametro(_cmd, "@idexam", System.Data.SqlDbType.Int, User_Persistent_Data.IdExam);

                    _cmd.Connection.Open();

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            _references.Add(new Exam
                            {
                                Id = Convert.ToInt32(dr["Id_Pregunta"]),
                                Question = dr["Pregunta"].ToString(),
                                Options = new List<QuestionOption> {
                                    new QuestionOption { Index =1, Answer = dr["R1"].ToString() },
                                    new QuestionOption { Index =2, Answer = dr["R2"].ToString() },
                                    new QuestionOption { Index =3, Answer = dr["R3"].ToString() },
                                    new QuestionOption { Index =4, Answer = dr["R4"].ToString() },
                                    new QuestionOption { Index =5, Answer = dr["R5"].ToString() }
                                },
                                ChosenAnswer = dr["R"].ToString(),
                                CorrectAnswer = dr["RC"].ToString(),
                                isCorrect = Convert.ToBoolean(dr["Correcta"])
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


    }
}

