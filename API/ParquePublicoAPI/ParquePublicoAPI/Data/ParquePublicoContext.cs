using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParquePublicoAPI.Models;
using ParquePublicoAPI.Data;

namespace ParquePublicoAPI.Data
{
    public class ParquePublicoContext : DbContext
    {
        public ParquePublicoContext (DbContextOptions<ParquePublicoContext> options)
            : base(options)
        {

        }


    }
}
