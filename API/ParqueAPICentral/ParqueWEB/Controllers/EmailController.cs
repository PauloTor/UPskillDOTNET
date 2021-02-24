using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Data;

namespace ParqueAPICentral.Controllers
{
    public class EmailController : ControllerBase
    {
        private readonly APICentralContext _context;

        public EmailController(APICentralContext context)
        {
            _context = context;
        }

        public void EnviarEmail(byte[] qr, long clienteID, long reservaID)
        {
            var cliente = _context.Cliente.Where(c => c.ClienteID == clienteID).FirstOrDefault();

            string remetente = "pseudocompany2020@gmail.com";

            string destinatario = cliente.EmailCliente;

            var qrcode = new Attachment(new MemoryStream(qr), "QRCode", "imagem/png");

            using MailMessage mail = new MailMessage(remetente, destinatario)
            {
                Subject = "Comfirmação da reserva nº " + reservaID,
                Body = "Código QR em anexo."
            };

            mail.Attachments.Add(qrcode);
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