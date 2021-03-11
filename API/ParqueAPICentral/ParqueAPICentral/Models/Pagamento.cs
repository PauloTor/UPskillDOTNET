using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Models
{
    public class Pagamento
    {
        public long PagamentoID { get; set; }

        [ForeignKey("FaturaID")]
        public long FaturaID { get; set; }
        public Fatura Fatura { get; set; }
    }
}
