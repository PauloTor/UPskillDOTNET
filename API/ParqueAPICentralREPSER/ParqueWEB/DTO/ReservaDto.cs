using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParqueAPICentral.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ParqueAPICentral.DTO
{
    public class ReservaPrivateDTO
    {
        [Key]
        public long ReservaID { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataReserva { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [ForeignKey("LugarID")]
        public long LugarID { get; set; }        
        public LugarDTO LugarDTO { get; set; }

        public long ParqueID { get; set; }
        public Parque Parque { get; set; }

        public long ClienteID { get; set; }
        public Cliente Cliente { get; set; }



        public ReservaPrivateDTO(DateTime datareserva, DateTime datainicio, DateTime datafim,long lugarid)
        {
            DataReserva = datareserva;
            DataInicio = datainicio;
            DataFim = datafim;
            LugarID = lugarid;

        }



    }




}
