using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.DTO
{
    public class PrecoLugar
    {
        [Key]
        public long PrecoLugarID { get; set; }

        [ForeignKey("LugarID")]
        public long LugarID { get; set; }
        public float Preco { get; set; }

        public Lugar_ Lugar_ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFim { get; set; }

        public PrecoLugar(long lugarID, float preco, DateTime dataInicio, DateTime dataFim)
        {
            LugarID = lugarID;
            Preco = preco;
            DataInicio = dataInicio;
            DataFim = dataFim;
        }
    }
}
