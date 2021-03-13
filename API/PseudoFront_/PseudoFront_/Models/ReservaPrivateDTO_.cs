using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PseudoFront_.Models;


namespace PseudoFront_.Models
{
    public class ReservaPrivateDTO_
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
        public LugarDTO LugarDTO { get; set; }
        public long ParqueID { get; set; }
        public ParqueDTO ParqueDTO { get; set; }
        public string UserID { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public ReservaPrivateDTO_(DateTime datar, DateTime dataini, DateTime datafim, string user, long parqueid, long lugarid)
        {
            DataReserva = datar;
            DataInicio = dataini;
            DataFim = datafim;
            UserID = user;
            LugarID = lugarid;
            ParqueID = parqueid;

        }
    }
}
