using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ParqueAPICentral.Services
{
    public interface IHttpClientHelper
    {
        Task<TResult> GetAsync<TResult>(string requestUrl);
        HttpClient HttpClient { get; set; }
    }
}
