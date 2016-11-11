using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebVansSite.Models;

namespace WebVansSite
{
    /// <summary>
    /// Summary description for AprovaUsuario
    /// </summary>
    [WebService(Namespace = "http://www.webvans.com.br/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AprovaUsuario : System.Web.Services.WebService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [WebMethod]
        public string Aprova(string email, string senha)
        {
            if (senha != ConfigurationManager.AppSettings["senhaAprovacao"].ToString())
            {
                return "ERRO: Senha incorreta!";
            }

            var user = db.Users.Include(u => u.UserProfileInfo).FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return "ERRO: Usuário inexistente";
            }

            if (user.UserProfileInfo.UsuarioValidado)
            {
                return "ERRO: Usuário [" + email + "] já está validado.";
            }

            try
            {
                user.UserProfileInfo.UsuarioValidado = true;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return "OK: Usuário [" + email + "] validado com sucesso.";
            }
            catch (Exception ex)
            {
                return "ERRO: " + ex.Message;
            }
        }
    }
}
