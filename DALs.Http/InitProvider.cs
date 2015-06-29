using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DALs.Model.Interfaces;

namespace DALs.Http
{
    public class InitializationRepository : IInitializationRepository
    {
        private Lazy<HttpClient> httpClient = new Lazy<HttpClient>(()=>new HttpClient());
        public HttpClient HttpClient {
            get { return httpClient.Value; }
        }
    }
}
