using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParqueAPI.Models;

namespace ParqueAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ParqueAPIContext context)
        {
            //context.Database.EnsureCreated();

            //Look for any Doencas.
            if (context.Reserva.Any())
            {
                return;   // DB has been seeded
            }


            var morada = new Morada[]

                {
                    new Morada
                    {

                        Rua = " Rua da Boavista",
                        CodigoPostal = "5002 - 456"
                    },
                    new Morada
                    {

                        Rua = " Rua do Orvalho",
                        CodigoPostal = "5102 - 275"
                    },
                    new Morada
                    {

                        Rua = " Rua Central",
                        CodigoPostal = "5022 - 436"
                    },
                    new Morada
                    {

                        Rua = " Rua Augusta",
                        CodigoPostal = "5112 - 456"
                    },
                        new Morada
                        {

                            Rua = " Rua do Ouro",
                            CodigoPostal = "5201 - 456"
                        },
                        new Morada
                        {

                            Rua = " Rua do Carmo",
                            CodigoPostal = "5211 - 256"
                        },
                        new Morada
                        {

                            Rua = " Rua de Sta Catarina",
                            CodigoPostal = "3221 - 156"
                        }
                };


            foreach (Morada s in morada)
            {
                context.Morada.Add(s);
            }
            context.SaveChanges();

           // ===============================================================
            var parque = new Parque[]

                {


            new Parque { NomeParque = " Boavista Park", TipoParque = (TipoParque)0, MoradaID = 1 },
            new Parque { NomeParque = " Nice Parking", TipoParque = (TipoParque)0, MoradaID = 2 },
            new Parque { NomeParque = " Parking da cidade", TipoParque = (TipoParque)1, MoradaID = 2 },
            new Parque { NomeParque = " West Parking", TipoParque = (TipoParque)1, MoradaID = 3 },
            new Parque { NomeParque = " North Parking", TipoParque = (TipoParque)0, MoradaID = 4 },
            new Parque { NomeParque = " Souith Parking", TipoParque = (TipoParque)1, MoradaID = 2 },
            new Parque { NomeParque = " East Parking", TipoParque = (TipoParque)1, MoradaID = 1 }
             };

            foreach (Parque s in parque)
            {
                context.Parque.Add(s);
            }
            context.SaveChanges();

            //=================================================================


            var lugar = new Lugar[]

                {

                    new Lugar { Fila = 1, Sector = 1, Preço = 10, ParqueID = 1 },
                    new Lugar { Fila = 1, Sector = 2, Preço = 11, ParqueID = 2 },
                    new Lugar { Fila = 2, Sector = 3, Preço = 13, ParqueID = 1 },
                    new Lugar { Fila = 1, Sector = 4, Preço = 10, ParqueID = 2 },
                    new Lugar { Fila = 2, Sector = 3, Preço = 1, ParqueID = 3 },
                    new Lugar { Fila = 3, Sector = 2, Preço = 12, ParqueID = 1 },
                    new Lugar { Fila = 4, Sector = 7, Preço = 12, ParqueID = 2 },
                    new Lugar { Fila = 5, Sector = 8, Preço = 1, ParqueID = 3 }
                };

            foreach (Lugar s in lugar)
            {
                context.Lugar.Add(s);
            }
            context.SaveChanges();
//===========================================================================================0

            var reserva = new Reserva[]
{
                new Reserva{
                            DataReserva = DateTime.Parse("2020-2-12 15:00:00"),
                            DataInicio = DateTime.Parse("2020-01-05 15:00:00"),
                            DataFim = DateTime.Parse("2020-01-05 16:00:00"),
                            MetodoPagamentoReserva = "PayPal",
                            PrePagamento = true,
                            
                            LugarID = 1
                        },

                new Reserva
        {

            DataReserva = DateTime.Parse("2018-4-12 15:00:00"),
            DataInicio = DateTime.Parse("2019-01-05 15:00:00"),
            DataFim = DateTime.Parse("2019-01-05 16:00:00"),
            MetodoPagamentoReserva = "PayPal",
            PrePagamento = true,
            LugarID = 3
        },

        new Reserva
        {

            DataReserva = DateTime.Parse("2018-4-10 15:00:00"),
            DataInicio = DateTime.Parse("2019-05-05 15:00:00"),
            DataFim = DateTime.Parse("2019-05-05 16:00:00"),
            MetodoPagamentoReserva = "PayPal",
            PrePagamento = false,
            LugarID = 4
        },

            new Reserva
            {

                DataReserva = DateTime.Parse("2020-4-10 15:00:00"),
                DataInicio = DateTime.Parse("2020-02-09 11:00:00"),
                DataFim = DateTime.Parse("2020-02-09 18:00:00"),
                MetodoPagamentoReserva = "CartaoCredito",
                PrePagamento = false,
                LugarID = 3
            },

            new Reserva
            {

                DataReserva = DateTime.Parse("2019-12-11 15:00:00"),
                DataInicio = DateTime.Parse("2020-02-09 11:00:00"),
                DataFim = DateTime.Parse("2020-02-09 19:00:00"),
                MetodoPagamentoReserva = "CartaoCredito",
                PrePagamento = false,
                LugarID = 2
            },

            new Reserva
            {

                DataReserva = DateTime.Parse("2019-11-11 15:00:00"),
                DataInicio = DateTime.Parse("2020-02-09 16:00:00"),
                DataFim = DateTime.Parse("2020-02-09 18:00:00"),
                MetodoPagamentoReserva = "CartaoCredito",
                PrePagamento = false,
                LugarID = 2
            },

            new Reserva
            {

                DataReserva = DateTime.Parse("2019-4-10 15:00:00"),
                DataInicio = DateTime.Parse("2020-02-09 11:00:00"),
                DataFim = DateTime.Parse("2020-02-09 18:00:00"),
                MetodoPagamentoReserva = "CartaoCredito",
                PrePagamento = false,
                LugarID = 2
            }
        };
            foreach (Reserva s in reserva)
            {
                context.Reserva.Add(s);
            }
            context.SaveChanges();





            

            
        }


    }
}






















