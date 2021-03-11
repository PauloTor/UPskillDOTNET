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
        public bool ParaSubAluguer { get; set; }

        

        public Reserva(long parqueID, long reservaAPI, string userID, long lugarId, bool paras)
        {
            ParqueID = parqueID;
            ReservaAPI = reservaAPI;
            UserID = userID;
            LugarID = lugarId;
            paras = false;
        }

        public Reserva()
        {
        }

        
    }
}