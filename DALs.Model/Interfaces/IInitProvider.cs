using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DALs.Model.Interfaces
{
    public interface IInitializationRepository
    {
        HttpClient HttpClient { get; }
    }
}
