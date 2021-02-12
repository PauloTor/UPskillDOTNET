using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;


namespace ParqueAPICentral.Data
{

    public static class DbInitializer
    {

        public static void Initialize(APICentralContext context)
        {

            if (context.Cliente.Any())
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


            var user = new User[]
            {
              new User { FirstName = "Test1", LastName = "User11", Username = "test1", Password = "test1" },
              new User { FirstName = "Test2", LastName = "User22", Username = "test2", Password = "test2" },
              new User { FirstName = "Test3", LastName = "User33", Username = "test3", Password = "test3" },
              new User { FirstName = "Test4", LastName = "User44", Username = "test4", Password = "test4" },
              new User { FirstName = "Test5", LastName = "User55", Username = "test5", Password = "test5" }
            };

            foreach (User s in user)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();



            var defaultCliente = new List<Cliente>
            {
                new Cliente("Cliente1", "cliente1@hotmail.com", 111111111, "DD", 10, 1),
                new Cliente("Cliente2", "cliente2@hotmail.com", 222222222, "CartaoCredito", 20, 2),
                new Cliente("Cliente3", "cliente3@hotmail.com", 333333333, "CartaoCredito", 30, 3),
                new Cliente("Cliente4", "cliente4@hotmail.com", 444444444, "DD", 40, 2),
                new Cliente("Cliente6", "cliente5@hotmail.com", 555555555, "CartaoCredito", 50, 1),
                new Cliente("Cliente6", "cliente6@hotmail.com", 666666666, "CartaoCredito", 60, 5)
            };

            context.Cliente.AddRange(defaultCliente);

            context.SaveChanges();


            IList<Reserva> defaultReserva = new List<Reserva>
            {
                new Reserva(2, 1, 3),
                new Reserva(3, 1, 2),
                new Reserva(3, 2, 3),
                new Reserva(1, 1, 5),
                new Reserva(1, 2, 1),
                new Reserva(2, 2, 4)
            };

            context.Reserva.AddRange(defaultReserva);

            context.SaveChanges();


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


            IList<SubAluguer> defaultSubAluguer = new List<SubAluguer>
            {
                new SubAluguer(100, DateTime.Parse("2020-01-01 17:00:00"), DateTime.Parse("2020-01-05 15:00:00"), DateTime.Parse("2020-01-05 18:00:00"), 4),
                new SubAluguer(15, DateTime.Parse("2020-01-04 12:00:00"), DateTime.Parse("2020-01-04 18:00:00"), DateTime.Parse("2020-01-04 20:00:00"), 3),
                new SubAluguer(20, DateTime.Parse("2020-01-14 14:00:00"), DateTime.Parse("2020-01-13 15:00:00"), DateTime.Parse("2020-01-14 14:00:00"), 2),
                new SubAluguer(15, DateTime.Parse("2020-01-25 19:00:00"), DateTime.Parse("2020-02-03 08:00:00"), DateTime.Parse("2020-02-03 19:00:00"), 1)
            };

            context.SubAluguer.AddRange(defaultSubAluguer);

            context.SaveChanges();
        }
    }
}
