using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WebVansSite.Helpers.Util
{
    public class MailHelper
    {
        public static void EnviarEmail(string emailPara, string nomePara, string assunto, string mensagem)
        {
            var fromAddress = new MailAddress(ConfigurationManager.AppSettings["emailWebVans"].ToString(), ConfigurationManager.AppSettings["nomeEmailWebVans"].ToString());
            var toAddress = new MailAddress(emailPara, nomePara);
            var fromPassword = ConfigurationManager.AppSettings["senhaEmailWebVans"].ToString();
            var subject = assunto;
            var body = mensagem;

            var smtp = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["smtpEmailWebVans"].ToString(),
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}