using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParqueAPICentral.Models
{
    public class Reserva
    {
        public long ReservaID { get; set; }

        public long ReservaAPI { get; set; }

        [ForeignKey("ParqueID")]
        public long ParqueID { get; set; }
        public Parque Parque { get; set; }       

        [ForeignKey("ClienteID")]
        public long ClienteID { get; set; }
        public Cliente Cliente { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataReserva { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        public long LugarID { get; set; }

        public bool ParaSubAluguer { get; set; }


        public Reserva(long parqueID,long reservaAPI, long clienteID, long lugarId, DateTime dRes, DateTime inicio, DateTime fim)
        {
            ParqueID = parqueID;
            ReservaAPI = reservaAPI;
            ClienteID = clienteID;
            LugarID = lugarId;
            DataReserva = dRes;
            DataInicio = inicio;
            DataFim = fim;
        }

        public Reserva()
        {
        }

        
    }
}