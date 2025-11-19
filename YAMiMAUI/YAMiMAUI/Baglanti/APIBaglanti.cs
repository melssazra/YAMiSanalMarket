using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMiMAUI.Baglanti
{
    public class APIBaglanti
    {
        protected readonly HttpClient _httpClient;

        public APIBaglanti()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7170")
                
            };

            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }
    }
}
