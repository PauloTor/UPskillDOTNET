﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ParqueAPICentral.Models
{
    public class SubAluguer
    {
        public long SubAluguerID { get; set; }

        public float PrecoSubAluguer { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataSubAluguer { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFim { get; set; }

        [ForeignKey("ReservaID")]
        public long ClienteID { get; set; }
        public Cliente Cliente { get; set; }


        public SubAluguer(float precoSubAluguer, DateTime dataSubAluguer, DateTime dataInicio, DateTime dataFim, long clienteID)
        {
            PrecoSubAluguer = precoSubAluguer;
            DataSubAluguer = dataSubAluguer;
            DataInicio = dataInicio;
            DataFim = dataFim;
            ClienteID = clienteID;
        }

    }
}
