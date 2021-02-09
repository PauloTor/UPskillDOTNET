using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Repositories
{
    public class RepositoryBase
    {
        protected APICentralContext RepContext { get; set; }

        public RepositoryBase(APICentralContext context)
        {
            this.RepContext = context;
        }


    }

}
