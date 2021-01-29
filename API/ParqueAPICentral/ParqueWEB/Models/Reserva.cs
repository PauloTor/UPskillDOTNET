using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParqueWEB.Models
{
    public class Reserva
    {

        
        
        public long ReservaID { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataReserva { get; set; }

       
        
            // initialize in ctor so behaviour is the same for freshly created and loaded entities
            
        

        
        [DataType(DataType.DateTime)]
        
        [DisplayFormat(DataFormatString = "{0:ddd, d MMMM yyyy, hh}", ApplyFormatInEditMode = true)]

        public DateTime DataInicio { get; set; }


        
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:ddd, d MMMM yyyy, hh}", ApplyFormatInEditMode = true)]
        public DateTime DataFim { get; set; }

        [ForeignKey("LugarID")]
        public long LugarID { get; set; }        
        public Lugar Lugar { get; set; }
    }
}
