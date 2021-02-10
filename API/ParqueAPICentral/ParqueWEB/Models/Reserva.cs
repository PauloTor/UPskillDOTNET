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

       //// unique
        public long NifParqueAPI { get; set; }

        //// unique
        public long ReservaAPI { get; set; }


        [ForeignKey("ClienteID")]
        public long ClienteID { get; set; }
        public Cliente Cliente { get; set; }

        
       
        
        public Reserva(long nifParqueAPI, long reservaAPI, long clienteID)
        {
            NifParqueAPI = nifParqueAPI;

            ReservaAPI = reservaAPI;

            ClienteID = clienteID;

        }

    }
}