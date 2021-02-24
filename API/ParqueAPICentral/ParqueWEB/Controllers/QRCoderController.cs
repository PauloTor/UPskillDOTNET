using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.IO;
using QRCoder;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Models;
using Microsoft.AspNetCore.Cors;
using System.Net.Http;
using ParqueAPICentral.Data;

namespace ParqueAPICentral.Controllers
{
    public class QRCoderController : ControllerBase
    {
        private readonly APICentralContext _context;

        public QRCoderController(APICentralContext context)
        {
            _context = context;
        }


        public async Task<ActionResult<byte[]>> GerarQRcode(Reserva_ reserva)
        {
            long reservaByID = reserva.ReservaID;

            var reservaCentral = _context.Reserva.Where(f => f.ReservaAPI == reservaByID).FirstOrDefault();

            long parqueByID = reservaCentral.ParqueID;

            var parque = _context.Parque.FirstOrDefault(p => p.ParqueID == parqueByID);

            using HttpClient client = new HttpClient();

            string endpoint = parque.Url + "reservas/" + reserva;

            var reservaRes = await client.GetAsync(endpoint);

            var reservaDTO = await reservaRes.Content.ReadAsAsync<Reserva_>();

            var qrInfo = ("Parque: " + reservaCentral.Parque.NomeParque
                   + "\n Morada: " + reservaCentral.Parque.Morada.Rua
                   + "\n Reserva: " + reserva.ReservaID
                   + "\n Lugar: " + reservaDTO.LugarID
                   + "\n Data de Inicio: " + reservaDTO.DataInicio
                   + "\n Data de Fim: " + reservaDTO.DataFim);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrInfo, QRCodeGenerator.ECCLevel.Q);

            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return BitmapToBytes(qrCodeImage);
        }


        private static byte[] BitmapToBytes(Bitmap img)
        {
            using MemoryStream stream = new MemoryStream();

            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            return stream.ToArray();
        }
    }
}