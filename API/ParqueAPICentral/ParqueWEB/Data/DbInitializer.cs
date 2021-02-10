using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParqueAPICentral.Models;


namespace ParqueAPICentral.Data
{

    public static class DbInitializer
    {

        public static void Initialize(APICentralContext context)
        {

            if (context.Parque.Any())
            {
                return;
            }

            IList<Parque> defaultParque = new List<Parque>();

            defaultParque.Add(new Parque("Standard 1"));
            defaultParque.Add(new Parque("Standard 2"));
            defaultParque.Add(new Parque("Standard 3"));
            defaultParque.Add(new Parque("Standard 4"));
            defaultParque.Add(new Parque("Standard 5"));

            //            _context.Fatura.Add(fatura);
            //          await _context.SaveChangesAsync();
            context.Parque.AddRange(defaultParque);

            context.SaveChanges();
            // var defaultUser = new List<User>();

            //{
            //    defaultCliente.Add(new Cliente ( "Cliente1", "cliente1@hotmail.com", 111111111, "DD", 1, "test", "test" );
            //    defaultCliente.Add(new Cliente { NomeCliente = "Cliente2", EmailCliente = "cliente2@hotmail.com", NifCliente = 222222222, MetodoPagamento = "CartaoCredito", Credito = 0, Username = "test", Password = "test" }) ;
            //    defaultCliente.Add(new Cliente { NomeCliente = "Cliente3", EmailCliente = "cliente3@hotmail.com", NifCliente = 333333333, MetodoPagamento = "CartaoCredito", Credito = 0, Username = "test", Password = "test" } );
            //    defaultCliente.Add(new Cliente { NomeCliente = "Cliente4", EmailCliente = "cliente4@hotmail.com", NifCliente = 444444444, MetodoPagamento = "CartaoCredito", Credito = 0, Username = "test", Password = "test" });
            //    defaultCliente.Add(new Cliente { NomeCliente = "Cliente5", EmailCliente = "cliente5@hotmail.com", NifCliente = 555555555, MetodoPagamento = "Paypall", Credito = 0, Username = "test", Password = "test" });
            //    defaultCliente.Add(new Cliente { NomeCliente = "Cliente6", EmailCliente = "cliente6@hotmail.com", NifCliente = 666666666, MetodoPagamento = "DD", Credito = 0, Username = "test", Password = "test" });
            //    defaultCliente.Add(new Cliente { NomeCliente = "Cliente6", EmailCliente = "cliente7@hotmail.com", NifCliente = 777777777, MetodoPagamento = "Paypall", Credito = 0, Username = "test", Password = "test" });
            //    defaultCliente.Add(new Cliente { NomeCliente = "Cliente6", EmailCliente = "cliente8@hotmail.com", NifCliente = 888888888, MetodoPagamento = "Paypall", Credito = 2, Username = "test", Password = "test" });
            //    defaultCliente.Add(new Cliente { NomeCliente = "Cliente9", EmailCliente = "cliente9@hotmail.com", NifCliente = 999999999, MetodoPagamento = "DD", Credito = 1, Username = "test", Password = "test" });          
            //};


            // eliminar subalugado

            IList<Reserva> defaultReserva = new List<Reserva>();

            defaultReserva.Add(new Reserva(1, 2));
            defaultReserva.Add(new Reserva(2, 1));
            defaultReserva.Add(new Reserva(3, 2));
            defaultReserva.Add(new Reserva(2, 2));
            defaultReserva.Add(new Reserva(2, 4));
            defaultReserva.Add(new Reserva(4, 2));

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
