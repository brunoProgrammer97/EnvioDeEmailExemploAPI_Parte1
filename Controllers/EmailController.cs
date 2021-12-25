using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EnvioDeEmailExemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        /// <summary>
        /// Rota de envio de e-mails
        /// </summary>
        /// <param name="email"></param>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpGet("enviar/{email}/{nome}")]
        public async Task<IActionResult> EnviarEmail(string email, string nome)
        {
            string dominio = Environment.GetEnvironmentVariable("DOMINIO_PRIMARIO");
            int porta = Convert.ToInt32(Environment.GetEnvironmentVariable("PORTA"));
            string emailOrigem = Environment.GetEnvironmentVariable("EMAIL");
            string senha = Environment.GetEnvironmentVariable("SENHA");

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(emailOrigem)
            };

            mail.To.Add(new MailAddress(email));

            mail.Subject = "Exemplo de Email .Net Core";
            mail.Body = "Olá " + nome;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using (SmtpClient smtp = new SmtpClient(dominio, porta))
            {
                smtp.Credentials = new NetworkCredential(emailOrigem, senha);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }

            return Ok();
        }
    }
}
