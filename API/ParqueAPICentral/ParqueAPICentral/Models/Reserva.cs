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
        [Key]
        public long ReservaID { get; set; }

        public long ReservaAPI { get; set; }

        [ForeignKey("ParqueID")]
        public long ParqueID { get; set; }
        public Parque Parque { get; set; }       

        [ForeignKey("UserID")]
        public string UserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public long LugarID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataFim { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataReserva { get; set; }

        public bool ParaSubAluguer { get; set; }

        public float Preco { get; set; }

        public Reserva(long parqueID, long reservaAPI, string userId, long lugarId, DateTime inicio, DateTime fim, DateTime reserva)
        {
            ParqueID = parqueID;
            ReservaAPI = reservaAPI;
            UserID = userId;
            LugarID = lugarId;
            DataInicio = inicio;
            DataFim = fim;
            DataReserva = reserva;
        }

        public Reserva()
        {
        }        
    }
}