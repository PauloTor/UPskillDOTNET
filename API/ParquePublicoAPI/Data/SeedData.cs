using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParqueAPI.Models;

namespace ParqueAPI.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ParqueAPIContext(serviceProvider.GetRequiredService<DbContextOptions<ParqueAPIContext>>()))
            {
                // Look for any Zonas.
                if (context.Cliente.Any())
                {
                    return;   // DB has been seeded
                }

                context.Cliente.AddRange(
                new Cliente { ClienteID = 1, NomeCliente = "Pedro", EmailCliente = "pedro1@hotmail.com", NifCliente = 123456789, MetodoPagamento = "CartaoCredito", Credito = 5 },
                new Cliente { ClienteID = 2, NomeCliente = "Pedro2", EmailCliente = "pedro2@hotmail.com", NifCliente = 22222189, MetodoPagamento = "DD", Credito = 115 },
                new Cliente { ClienteID = 3, NomeCliente = "Pedro3", EmailCliente = "pedro3@hotmail.com", NifCliente = 3333456789, MetodoPagamento = "CartaoCredito", Credito = 25 },
                new Cliente { ClienteID = 4, NomeCliente = "Pedro4", EmailCliente = "pedro4@hotmail.com", NifCliente = 444456789, MetodoPagamento = "Paypal", Credito = 544 },
                new Cliente { ClienteID = 5, NomeCliente = "Pedro5", EmailCliente = "pedro5@hotmail.com", NifCliente = 5555556789, MetodoPagamento = "Mbway", Credito = 65 },
                new Cliente { ClienteID = 6, NomeCliente = "Pedro6", EmailCliente = "pedro6@hotmail.com", NifCliente = 66666789, MetodoPagamento = "Paypal", Credito = 5 },
                new Cliente { ClienteID = 7, NomeCliente = "Pedro7", EmailCliente = "pedro7@hotmail.com", NifCliente = 777756789, MetodoPagamento = "MBWay", Credito = 11 },
                new Cliente { ClienteID = 8, NomeCliente = "Pedro8", EmailCliente = "pedro8@hotmail.com", NifCliente = 888856789, MetodoPagamento = "PayPal", Credito = 15 }
                );

                context.Reserva.AddRange(

                    new Reserva
                    {
                        ReservaID = 1,
                        DataReserva = DateTime.Parse("2020-2-12 15:00:00"),
                        DataInicio = DateTime.Parse("2020-01-05 15:00:00"),
                        DataFim = DateTime.Parse("2020-01-05 16:00:00"),
                        MetodoPagamentoReserva = "PayPal",
                        PrePagamento = true,
                        ClienteID = 1,
                        LugarID = 1
                    },

    new Reserva
    {
        ReservaID = 2,
        DataReserva = DateTime.Parse("2018-4-12 15:00:00"),
        DataInicio = DateTime.Parse("2019-01-05 15:00:00"),
        DataFim = DateTime.Parse("2019-01-05 16:00:00"),
        MetodoPagamentoReserva = "PayPal",
        PrePagamento = true,
        ClienteID = 2,
        LugarID = 3
    },

    new Reserva
    {
        ReservaID = 3,
        DataReserva = DateTime.Parse("2018-4-10 15:00:00"),
        DataInicio = DateTime.Parse("2019-05-05 15:00:00"),
        DataFim = DateTime.Parse("2019-05-05 16:00:00"),
        MetodoPagamentoReserva = "PayPal",
        PrePagamento = false,
        ClienteID = 3,
        LugarID = 4
    },

        new Reserva
        {
            ReservaID = 4,
            DataReserva = DateTime.Parse("2020-4-10 15:00:00"),
            DataInicio = DateTime.Parse("2020-02-09 11:00:00"),
            DataFim = DateTime.Parse("2020-02-09 18:00:00"),
            MetodoPagamentoReserva = "CartaoCredito",
            PrePagamento = false,
            ClienteID = 4,
            LugarID = 3
        },

        new Reserva
        {
            ReservaID = 5,
            DataReserva = DateTime.Parse("2019-12-11 15:00:00"),
            DataInicio = DateTime.Parse("2020-02-09 11:00:00"),
            DataFim = DateTime.Parse("2020-02-09 19:00:00"),
            MetodoPagamentoReserva = "CartaoCredito",
            PrePagamento = false,
            ClienteID = 2,
            LugarID = 2
        },

        new Reserva
        {
            ReservaID = 5,
            DataReserva = DateTime.Parse("2019-11-11 15:00:00"),
            DataInicio = DateTime.Parse("2020-02-09 16:00:00"),
            DataFim = DateTime.Parse("2020-02-09 18:00:00"),
            MetodoPagamentoReserva = "CartaoCredito",
            PrePagamento = false,
            ClienteID = 6,
            LugarID = 2
        },

        new Reserva
        {
            ReservaID = 6,
            DataReserva = DateTime.Parse("2019-4-10 15:00:00"),
            DataInicio = DateTime.Parse("2020-02-09 11:00:00"),
            DataFim = DateTime.Parse("2020-02-09 18:00:00"),
            MetodoPagamentoReserva = "CartaoCredito",
            PrePagamento = false,
            ClienteID = 7,
            LugarID = 2
        }


    );
                context.Lugar.AddRange(

                new Lugar { LugarID = 1, Fila = 1, Sector = 1, Preço = 10, ParqueID = 1 },
                new Lugar { LugarID = 2, Fila = 1, Sector = 2, Preço = 11, ParqueID = 2 },
                new Lugar { LugarID = 3, Fila = 2, Sector = 3, Preço = 13, ParqueID = 1 },
                new Lugar { LugarID = 4, Fila = 1, Sector = 4, Preço = 10, ParqueID = 2 },
                new Lugar { LugarID = 5, Fila = 2, Sector = 3, Preço = 1, ParqueID = 3 },
                new Lugar { LugarID = 6, Fila = 3, Sector = 2, Preço = 12, ParqueID = 1 },
                new Lugar { LugarID = 7, Fila = 4, Sector = 7, Preço = 12, ParqueID = 2 },
                new Lugar { LugarID = 8, Fila = 5, Sector = 8, Preço = 1, ParqueID = 3 }
                     );


                context.SubAluguer.AddRange(


                new SubAluguer { SubAluguerID = 1, ReservaID = 1, ClienteID = 1, Preço = 50, Data = DateTime.Parse("2019-5-10 15:00:00") },

                new SubAluguer { SubAluguerID = 2, ReservaID = 2, ClienteID = 1, Preço = 30, Data = DateTime.Parse("2019-3-10 11:00:00") },

                new SubAluguer { SubAluguerID = 3, ReservaID = 3, ClienteID = 1, Preço = 20, Data = DateTime.Parse("2018-1-10 15:00:00") },

                new SubAluguer { SubAluguerID = 4, ReservaID = 2, ClienteID = 1, Preço = 50, Data = DateTime.Parse("2020-7-10 12:00:00") },

                new SubAluguer { SubAluguerID = 5, ReservaID = 3, ClienteID = 2, Preço = 54, Data = DateTime.Parse("2019-6-10 11:00:00") },

                new SubAluguer { SubAluguerID = 6, ReservaID = 4, ClienteID = 2, Preço = 53, Data = DateTime.Parse("2018-9-10 10:00:00") },

                new SubAluguer { SubAluguerID = 7, ReservaID = 4, ClienteID = 3, Preço = 51, Data = DateTime.Parse("2017-1-10 01:00:00") },

                new SubAluguer { SubAluguerID = 8, ReservaID = 7, ClienteID = 2, Preço = 10, Data = DateTime.Parse("2011-1-10 05:00:00") }

    );

                context.Morada.AddRange(

                new Rua
                {
                    MoradaID = 1,
                    Rua = " Rua da Boavista",
                    CodigoPostal = "5002 - 456"
                },
                new Rua
                {
                    MoradaID = 2,
                    Rua = " Rua do Orvalho",
                    CodigoPostal = "5102 - 275"
                },
                new Rua
                {
                    MoradaID = 3,
                    Rua = " Rua Central",
                    CodigoPostal = "5022 - 436"
                },
                new Rua
                {
                    MoradaID = 4,
                    Rua = " Rua Augusta",
                    CodigoPostal = "5112 - 456"
                },
                    new Rua
                    {
                        MoradaID = 5,
                        Rua = " Rua do Ouro",
                        CodigoPostal = "5201 - 456"
                    },
                    new Rua
                    {
                        MoradaID = 6,
                        Rua = " Rua do Carmo",
                        CodigoPostal = "5211 - 256"
                    },
                    new Rua
                    {
                        MoradaID = 7,
                        Rua = " Rua de Sta Catarina",
                        CodigoPostal = "3221 - 156"
                    }

                    );
                context.Parque.AddRange(

                new Parque { ParqueID = 1, NomeParque = " Boavista Park", TipoParque = (TipoParque)0, MoradaID = 1 },
                new Parque { ParqueID = 2, NomeParque = " Nice Parking", TipoParque = (TipoParque)0, MoradaID = 2 },
                new Parque { ParqueID = 3, NomeParque = " Parking da cidade", TipoParque = (TipoParque)1, MoradaID = 2 },
                new Parque { ParqueID = 4, NomeParque = " West Parking", TipoParque = (TipoParque)1, MoradaID = 3 },
                new Parque { ParqueID = 5, NomeParque = " North Parking", TipoParque = (TipoParque)0, MoradaID = 4 },
                new Parque { ParqueID = 6, NomeParque = " Souith Parking", TipoParque = (TipoParque)1, MoradaID = 2 },
                new Parque { ParqueID = 7, NomeParque = " East Parking", TipoParque = (TipoParque)1, MoradaID = 1 }
                );

                context.Fatura.AddRange(
                new Fatura { FaturaID = 1, DataFatura = DateTime.Parse("2018-9-10 10:00:00"), MetodoPagamentoFatura = "Paypal", PrecoFatura = 10, ReservaID = 1 },
                new Fatura { FaturaID = 2, DataFatura = DateTime.Parse("2018-9-11 10:00:00"), MetodoPagamentoFatura = "CartaoCredito", PrecoFatura = 15, ReservaID = 2 },
                new Fatura { FaturaID = 3, DataFatura = DateTime.Parse("2018-9-12 10:00:00"), MetodoPagamentoFatura = "CartaoCredito", PrecoFatura = 115, ReservaID = 3 },
                new Fatura { FaturaID = 4, DataFatura = DateTime.Parse("2018-9-13 10:00:00"), MetodoPagamentoFatura = "CartaoCredito", PrecoFatura = 15, ReservaID = 4 }
    );

                context.SaveChanges();
            }
        }
    }
}




















