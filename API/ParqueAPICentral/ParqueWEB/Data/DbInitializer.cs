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
            //context.Database.EnsureCreated();

            //Look for any Pagamento.
            if (context.Pagamento.Any())
            {
                return;   // DB has been seeded
            }

            // ===============================================================

            var cliente = new Cliente[]
            {
                new Cliente { NomeCliente = "Cliente1", EmailCliente = "cliente1@hotmail.com", NifCliente = 111111111, MetodoPagamento = "DD", Credito = 1 },
                new Cliente { NomeCliente = "Cliente2", EmailCliente = "cliente2@hotmail.com", NifCliente = 222222222, MetodoPagamento = "CartaoCredito", Credito = 0 },
                new Cliente { NomeCliente = "Cliente3", EmailCliente = "cliente3@hotmail.com", NifCliente = 333333333, MetodoPagamento = "CartaoCredito", Credito = 0 },
                new Cliente { NomeCliente = "Cliente4", EmailCliente = "cliente4@hotmail.com", NifCliente = 444444444, MetodoPagamento = "CartaoCredito", Credito = 0 },
                new Cliente { NomeCliente = "Cliente5", EmailCliente = "cliente5@hotmail.com", NifCliente = 555555555, MetodoPagamento = "Paypall", Credito = 0 },
                new Cliente { NomeCliente = "Cliente6", EmailCliente = "cliente6@hotmail.com", NifCliente = 666666666, MetodoPagamento = "DD", Credito = 0 },
                new Cliente { NomeCliente = "Cliente6", EmailCliente = "cliente7@hotmail.com", NifCliente = 777777777, MetodoPagamento = "Paypall", Credito = 0 },
                new Cliente { NomeCliente = "Cliente6", EmailCliente = "cliente8@hotmail.com", NifCliente = 888888888, MetodoPagamento = "Paypall", Credito = 2 },
                new Cliente { NomeCliente = "Cliente9", EmailCliente = "cliente9@hotmail.com", NifCliente = 999999999, MetodoPagamento = "DD", Credito = 1 }
            };

            foreach (Cliente c in cliente)
            {
                context.Cliente.Add(c);
            }
            context.SaveChanges();

            // ===============================================================

            // ===============================================================
           /* var parque = new Parque[]

                {
                    new Parque { NomeParque = "Boavista Park", Lotacao = 5, MoradaID = 1 },
                    new Parque { NomeParque = "Lisboa Park", Lotacao = 30, MoradaID = 2},
                    new Parque { NomeParque = "Aveiro Park", Lotacao = 100, MoradaID = 3},
                    new Parque { NomeParque = "Gaia Park", Lotacao = 10, MoradaID = 4},
                    new Parque { NomeParque = "Porto Park", Lotacao = 6, MoradaID = 5}
                 };

            foreach (Parque p in parque)
            {
                context.Parque.Add(p);
            }
            context.SaveChanges();
           */
            //=================================================================
            
            //===========================================================================================0
          /*  var reserva = new Reserva[]
{
            new Reserva
            {
                DataReserva = DateTime.Parse("2020-01-01 15:00:00"),
                DataInicio = DateTime.Parse("2020-01-05 15:00:00"),
                DataFim = DateTime.Parse("2020-01-05 18:00:00"),
                DataSaida = DateTime.Parse("2020-01-05 18:00:00"),
                LugarID = 2,
                ClienteID = 1,
                SubAlugado = false
                
            },

            new Reserva
            {
                DataReserva = DateTime.Parse("2020-01-04 11:00:00"),
                DataInicio = DateTime.Parse("2020-01-04 18:00:00"),
                DataFim = DateTime.Parse("2020-01-04 20:00:00"),
                DataSaida = DateTime.Parse("2020-01-04 21:00:00"),
                LugarID = 3,
                ClienteID = 2,
                SubAlugado = false
            },

            new Reserva
            {
                DataReserva = DateTime.Parse("2020-01-11 09:30:00"),
                DataInicio = DateTime.Parse("2020-01-13 15:00:00"),
                DataFim = DateTime.Parse("2020-01-14 14:00:00"),
                DataSaida = DateTime.Parse("2020-01-14 14:00:00"),
                LugarID = 1,
                ClienteID = 3,
                SubAlugado = false
            },

            new Reserva
            {
                DataReserva = DateTime.Parse("2020-01-18 15:00:00"),
                DataInicio = DateTime.Parse("2020-02-03 08:00:00"),
                DataFim = DateTime.Parse("2020-02-03 19:00:00"),
                DataSaida = DateTime.Parse("2020-02-03 19:00:00"),
                LugarID = 3,
                ClienteID = 4,
                SubAlugado = false
            }
        };
            foreach (Reserva r in reserva)
            {
                context.Reserva.Add(r);
            }
            context.SaveChanges();
          */
            //-----------------------------------------------------------------------------------------------------------------

            var fatura = new Fatura[]

                {
                    new Fatura { DataFatura = DateTime.Parse("2020-01-05 18:00:00"), PrecoFatura = 33, ReservaID = 1 },
                    new Fatura { DataFatura = DateTime.Parse("2020-01-04 21:00:00"), PrecoFatura = 30, ReservaID = 2 },
                    new Fatura { DataFatura = DateTime.Parse("2020-01-14 15:00:00"), PrecoFatura = 230, ReservaID = 3 },
                    new Fatura { DataFatura = DateTime.Parse("2020-02-03 19:00:00"), PrecoFatura = 110, ReservaID = 4 }
                };

            foreach (Fatura f in fatura)
            {
                context.Fatura.Add(f);
            }
            context.SaveChanges();

            //=================================================================

            var subaluguer = new SubAluguer[]
                {
                    new SubAluguer { PrecoSubAluguer = 10, DataSubAluguer = DateTime.Parse("2020-01-01 17:00:00"), DataInicio = DateTime.Parse("2020-01-05 15:00:00"), DataFim = DateTime.Parse("2020-01-05 18:00:00"), ReservaID = 4 },
                    new SubAluguer { PrecoSubAluguer = 15, DataSubAluguer = DateTime.Parse("2020-01-04 12:00:00"), DataInicio = DateTime.Parse("2020-01-04 18:00:00"), DataFim = DateTime.Parse("2020-01-04 20:00:00"), ReservaID = 3 },
                    new SubAluguer { PrecoSubAluguer = 20, DataSubAluguer = DateTime.Parse("2020-01-14 14:00:00"), DataInicio = DateTime.Parse("2020-01-13 15:00:00"), DataFim = DateTime.Parse("2020-01-14 14:00:00"), ReservaID = 2 },
                    new SubAluguer { PrecoSubAluguer = 15, DataSubAluguer = DateTime.Parse("2020-01-25 19:00:00"), DataInicio = DateTime.Parse("2020-02-03 08:00:00"), DataFim = DateTime.Parse("2020-02-03 19:00:00"), ReservaID = 1 },
                };

            foreach (SubAluguer s in subaluguer)
            {
                context.SubAluguer.Add(s);
            }
            context.SaveChanges();

            //=================================================================

            var Pagamento = new Pagamento[]

               {

                    new Pagamento { FaturaID = 1 },
                    new Pagamento { FaturaID = 1 },
                    new Pagamento { FaturaID = 2 },
                    new Pagamento { FaturaID = 3 },
                    new Pagamento { FaturaID = 4 },
                    new Pagamento { FaturaID = 3},

               };

            foreach (Pagamento s in Pagamento)
            {
                context.Pagamento.Add(s);
            }
            context.SaveChanges();





        }
    }
}
