using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Data;

//CONTROLLER PARA TESTE DE ENVIO DE EMAIL - a funcionar

namespace ParqueAPICentral.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [EnableCors]
        [HttpGet]
        public void EnviarEmail()
        {
            //var cliente = _context.Cliente.Where(c => c.ClienteID == clienteID).FirstOrDefault();

            string remetente = "pseudocompany2020@gmail.com";

            string destinatario = "leandro555eagle@gmail.com";

            //var qrcode = new Attachment(new MemoryStream(qr), "QRCode", "imagem/png");

            using MailMessage mail = new MailMessage(remetente, destinatario)
            {
                Subject = "Comfirmação da reserva nº ",
                Body = "Código QR em anexo."
            };

           // mail.Attachments.Add(qrcode);
            mail.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                EnableSsl = true
            };

            NetworkCredential networkCredential = new NetworkCredential(remetente, "PseudoPark255");
            smtp.Credentials = networkCredential;
            smtp.Port = 587;
            smtp.Send(mail);
        }
    }
}