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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParqueAPICentral.Controllers
{
    public class QRCoderController : ControllerBase
    {

        public ActionResult<byte[]> QRcoder(Reserva_ reserva_, Reserva reserva)
        {
            var qrInfo = ("Parque: " + reserva.Parque.NomeParque
                + "\n Morada: " + reserva.Parque.Morada.Rua
                + "\n Lugar: " + reserva_.LugarID
                + "\n Data de Inicio: " + reserva_.DataInicio
                + "\n Data de Fim: " + reserva_.DataFim);

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
