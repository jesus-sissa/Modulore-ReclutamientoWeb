using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
namespace Modulo_Reclutamiento_Web.Models
{
    public  class Email
    {
        private static SqlCommand? _cmd;
        private static SqlDataReader? _dr;
        private static DataTable? destinationEmails;

        private string _from = string.Empty;
        private string _password = string.Empty;
        private string _port = string.Empty;
        private string _smtp = string.Empty;


        public Email() 
        {
            try
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                _from = builder.GetSection("EmailConfiguration:From").Value;
                _password = builder.GetSection("EmailConfiguration:Pass").Value;
                _port = builder.GetSection("EmailConfiguration:Port").Value;
                _smtp = builder.GetSection("EmailConfiguration:SmtpServer").Value;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SendingLeadApprovalMail(int ClaveMail, string tittle,string prospecto ) 
        {
            bool _resp = false;
            var htmlbody = "<html><style>body{font-family:'Lucida Console';}</style><body>"
                       + "<table style='border-radius: 25px; background:rgba(178, 186, 187 ); border: solid 0px ;  width:100%;'>"
                       + "<tr><td colspan='4' align='center'><b style='color: #D68910; font-size:26PX;'>Boletín Informativo</b></td></tr>"
                       + "<tr><td colspan='4' align='center'><b style='color: #D68910; font-size:26PX;'>" + tittle + "</b></td></tr>"
                       + "<tr><td colspan='4'><br><hr /></td></tr>"
                       + "<tr><td align='right'><label><b>Nuevo Prospecto Listo Para ser validado:</b></label></td><td>"+ prospecto + "</td><td></td><td></td></tr>"
                       + "<tr><td align='right'><label><b>Usuario Solicita:</b></label></td><td>" + User_Persistent_Data.Name + "</td></tr>"
                       + "<tr><td align='right'><label><b>Departamento:</b></label></td><td>RH</td></tr>"
                       + "<tr><td align='right'><label><b>Fecha Solicitud:</b></label></td><td>" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + "<br></td></tr>"
                       + "<tr><td colspan='4'><hr /></td></tr><tr><td colspan='3' align='center'>"
                       + "<b style='color: #D68910; font-size: 26PX;'>Agente de Servicios SIAC</b></td></tr></table><br/><br/></body></html>";
            try
            {
                _resp = EmailPreparation(ClaveMail, tittle, htmlbody);
                
            }
            catch (Exception)
            {

               
            }

            return _resp;
        }

        public bool SendEmailCandidateApproval(int ClaveMail, string tittle, string prospecto)
        {
            bool _resp = false;
            var htmlbody = "<html><style>body{font-family:'Lucida Console';}</style><body>"
                       + "<table style='border-radius: 25px; background:rgba(178, 186, 187 ); border: solid 0px ;  width:100%;'>"
                       + "<tr><td colspan='4' align='center'><b style='color: #D68910; font-size:26PX;'>Boletín Informativo</b></td></tr>"
                       + "<tr><td colspan='4' align='center'><b style='color: #D68910; font-size:26PX;'>" + tittle + "</b></td></tr>"
                       + "<tr><td colspan='4'><br><hr /></td></tr>"
                       + "<tr><td align='right'><label><b>Nuevo Prospecto aprobado:</b></label></td><td>" + prospecto + "</td><td></td><td></td></tr>"
                       + "<tr><td align='right'><label><b>Usuario Aprobo:</b></label></td><td>" + User_Persistent_Data.Name + "</td></tr>"
                       //+ "<tr><td align='right'><label><b>Departamento:</b></label></td><td>RH</td></tr>"
                       + "<tr><td align='right'><label><b>Fecha Aprobacion:</b></label></td><td>" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + "<br></td></tr>"
                       + "<tr><td colspan='4'><hr /></td></tr><tr><td colspan='3' align='center'>"
                       + "<b style='color: #D68910; font-size: 26PX;'>Agente de Servicios SIAC</b></td></tr></table><br/><br/></body></html>";
            try
            {
                _resp = EmailPreparation(ClaveMail, tittle, htmlbody);

            }
            catch (Exception)
            {

           
            }

            return _resp;
        }


        private bool EmailPreparation(int NumeroAlerta, string Subject, string htmlbody)
        {
            bool _resp = false;
            try
            {
               SmtpClient smtp = new SmtpClient();

                smtp.Host = _smtp;
                smtp.Port = int.Parse(_port);
                smtp.Credentials = new System.Net.NetworkCredential(_from, _password);
                MailMessage _email = new MailMessage();
                _email.From = new MailAddress("SIAC@SISSASEGURIDAD.COM", " SIAC ALERTAS");
                destinationEmails = getDestinationEmails(NumeroAlerta);
                foreach (DataRow row in destinationEmails.Rows)
                {
                    if (destinationEmails.Rows.Count > 0)
                    {
                        _email.To.Add(new MailAddress((string)row["Mail"]));
                    }
                }
                _email.Subject = Subject;
                _email.Body = htmlbody;
                _email.IsBodyHtml = true;
                smtp.Send(_email);
                _resp = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return _resp;
        }

        private DataTable getDestinationEmails(int NumeroAlerta)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (var oConexion = Conexion.creaConexion(Conexion.getClaveConexion(User_Persistent_Data.Connection)))

                {
                    _cmd = Conexion.creaComando("Cat_AlertasDestinos_Get", oConexion);
                    Conexion.creaParametro(_cmd, "@Clave_AlertaTipo", SqlDbType.Int, NumeroAlerta);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Connection.Open();

                    using (_dr = _cmd.ExecuteReader())
                    {
                        dataTable.Load(_dr);
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }

    }
}
