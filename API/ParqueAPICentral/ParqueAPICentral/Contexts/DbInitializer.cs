using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaqueAPICentral.Models;

using ParqueAPICentral.Contexts;
//using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;


namespace ParqueAPICentral.Data
{

    public static class DbInitializer
    {

        public static void Initialize(ApplicationDbContext context)
        {

            if (context.Parque.Any())
            {
                return;
            }


            var morada = new List<Morada>
            {
                new Morada("Rua da Boavista 184", "4400-224 Porto"),
                new Morada("Praça da Vida 83", "4428-108 Porto"),
                new Morada("Rua do Poste", "5267-123 Maia")
            };

            foreach (Morada m in morada)
            {
                context.Morada.Add(m);
            }
            context.SaveChanges();


            var parque = new List<Parque>
            {
                new Parque("Aparkai 1", 1234, 10, "https://localhost:44365/api/", 1),
                new Parque("Aparkai 2", 1234, 4, "https://localhost:44365/api/", 2),
                new Parque("Municipal", 4444, 22, "https://localhost:44339/api/", 3)
            };

            foreach (Parque p in parque)
            {
                context.Parque.Add(p);
            }
            context.SaveChanges();

            /*
            IList<Reserva> defaultReserva = new List<Reserva>
            {
                new Reserva(1, 1, "1",1,true),
                new Reserva(2, 1, "2",2,false),
                new Reserva(3, 2, "1",3,true),
                new Reserva(1, 1, "2",5,false),
                new Reserva(1, 2, "1",4,false),
                new Reserva(2, 2, "4",2,true)
            };

            context.Reserva.AddRange(defaultReserva);

            context.SaveChanges();
            */
            /*
            IList<Fatura> defaultFatura = new List<Fatura>
            {
                new Fatura(DateTime.Parse("2020-01-04 21:00:00"), 33, 1),
                new Fatura(DateTime.Parse("2021-01-01 20:00:00"), 13, 2),
                new Fatura(DateTime.Parse("2019-02-11 21:00:00"), 2, 3),
                new Fatura(DateTime.Parse("2120-11-04 21:00:00"), 3, 4),
                new Fatura(DateTime.Parse("2021-02-01 05:00:00"), 11, 5),
                new Fatura(DateTime.Parse("2019-02-11 21:00:00"), 23, 6)
            };

            context.Fatura.AddRange(defaultFatura);

            context.SaveChanges();
            */
            /*
            IList<SubAluguer> defaultSubAluguer = new List<SubAluguer>
            {
                new SubAluguer(3, 12,false),
                new SubAluguer(5, 8,false),
            };

            context.SubAluguer.AddRange(defaultSubAluguer);

            context.SaveChanges();
            */
        }
    }
}
