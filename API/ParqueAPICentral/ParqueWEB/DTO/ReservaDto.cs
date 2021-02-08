using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParqueAPICentral.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ParqueAPICentral.Models
{
    public class Reserva_
    {
        [Key]
        public long ReservaID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataReserva { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFim { get; set; }

        [ForeignKey("LugarID")]
        public long LugarID { get; set; }        
        public Lugar Lugar { get; set; }



        public Reserva_(DateTime datareserva, DateTime datainicio, DateTime datafim,long lugarid)
        {
            DataReserva = datareserva;
            DataInicio = datainicio;
            DataFim = datafim;
            LugarID = lugarid;

        }



    }




}
