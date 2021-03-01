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

        [ForeignKey("ClienteID")]
        public long ClienteID { get; set; }
        public Cliente Cliente { get; set; }

        public long LugarID { get; set; }

        public bool ParaSubAluguer { get; set; }


        //public Reserva(long parqueID, long reservaAPI, long clienteID)
        //{
        //    ParqueID = parqueID;
        //    ReservaAPI = reservaAPI;
        //    ClienteID = clienteID;
        //}
        public Reserva( long parqueID,long reservaAPI, long clienteID, long lugarId)
        {
            ParqueID = parqueID;
            ReservaAPI = reservaAPI;
            ClienteID = clienteID;
            LugarID = lugarId;
        }

        public Reserva()
        {
        }

        
    }
}