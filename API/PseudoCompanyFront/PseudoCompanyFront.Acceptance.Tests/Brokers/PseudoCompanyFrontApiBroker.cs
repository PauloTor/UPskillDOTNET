using Microsoft.AspNetCore.Mvc.Testing;
using RESTFulSense.Clients;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PseudoCompanyFront.Acceptance.Tests.Brokers
{
    public partial class PseudoCompanyFrontApiBroker
    {
        public readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly HttpClient baseClient;
        private readonly IRESTFulApiFactoryClient apiFactoryClient;
    }
}
