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

            //IList<Parque> defaultParque = new List<Parque>();

            //defaultParque.Add(new Parque("Standard 1"));
            //defaultParque.Add(new Parque("Standard 2"));
            //defaultParque.Add(new Parque("Standard 3"));
            //defaultParque.Add(new Parque("Standard 4"));
            //defaultParque.Add(new Parque("Standard 5"));

            ////            _context.Fatura.Add(fatura);
            ////          await _context.SaveChangesAsync();
            //context.Parque.AddRange(defaultParque);

            //context.SaveChanges();



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



            var defaultCliente = new List<Cliente>();

            defaultCliente.Add(new Cliente("Cliente1", "cliente1@hotmail.com", 111111111, "DD", 10,1));
            defaultCliente.Add(new Cliente("Cliente2", "cliente2@hotmail.com", 222222222, "CartaoCredito", 20,2));
            defaultCliente.Add(new Cliente("Cliente3", "cliente3@hotmail.com", 333333333, "CartaoCredito", 30,3));
            defaultCliente.Add(new Cliente("Cliente4", "cliente4@hotmail.com", 444444444, "DD", 40,2));
            defaultCliente.Add(new Cliente("Cliente6", "cliente5@hotmail.com", 555555555, "CartaoCredito", 50,1));
            defaultCliente.Add(new Cliente("Cliente6", "cliente6@hotmail.com", 666666666, "CartaoCredito", 60,5));

            context.Cliente.AddRange(defaultCliente);

            context.SaveChanges();





            // eliminar subalugado

            IList<Reserva> defaultReserva = new List<Reserva>();

            defaultReserva.Add(new Reserva(11111225222, 2,1));
            defaultReserva.Add(new Reserva(22222115111, 1,2));
            defaultReserva.Add(new Reserva(33333341111, 2,3));
            defaultReserva.Add(new Reserva(22222131111, 2,4));
            defaultReserva.Add(new Reserva(22223232355, 4,5));
            defaultReserva.Add(new Reserva(23423445645, 2,2));

            context.Reserva.AddRange(defaultReserva);

            context.SaveChanges();



            IList<Fatura> defaultFatura = new List<Fatura>();

            defaultFatura.Add(new Fatura(DateTime.Parse("2020-01-04 21:00:00"), 33, 1));
            defaultFatura.Add(new Fatura(DateTime.Parse("2021-01-01 20:00:00"), 131, 2));
            defaultFatura.Add(new Fatura(DateTime.Parse("2019-02-11 21:00:00"), 232, 3));
            defaultFatura.Add(new Fatura(DateTime.Parse("2120-11-04 21:00:00"), 33, 1));
            defaultFatura.Add(new Fatura(DateTime.Parse("2021-02-01 05:00:00"), 1131, 2));
            defaultFatura.Add(new Fatura(DateTime.Parse("2019-02-11 21:00:00"), 2232, 4));

            context.Fatura.AddRange(defaultFatura);

            context.SaveChanges();


            IList<SubAluguer> defaultSubAluguer = new List<SubAluguer>();

            defaultSubAluguer.Add(new SubAluguer(100, DateTime.Parse("2020-01-01 17:00:00"), DateTime.Parse("2020-01-05 15:00:00"), DateTime.Parse("2020-01-05 18:00:00"), 4));
            defaultSubAluguer.Add(new SubAluguer(15, DateTime.Parse("2020-01-04 12:00:00"), DateTime.Parse("2020-01-04 18:00:00"), DateTime.Parse("2020-01-04 20:00:00"), 3));
            defaultSubAluguer.Add(new SubAluguer(20, DateTime.Parse("2020-01-14 14:00:00"), DateTime.Parse("2020-01-13 15:00:00"), DateTime.Parse("2020-01-14 14:00:00"), 2));
            defaultSubAluguer.Add(new SubAluguer(15, DateTime.Parse("2020-01-25 19:00:00"), DateTime.Parse("2020-02-03 08:00:00"), DateTime.Parse("2020-02-03 19:00:00"), 1));

            context.SubAluguer.AddRange(defaultSubAluguer);
            context.SaveChanges();




        }
    }
}
