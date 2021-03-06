using NAVIAIServices.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Adams.Client
{
    public class AdamsClient
    {
        public ProjectCollection Projects;
        public StorageService Storages;
        public HttpClient _http;

        public AdamsClient(string baseAddress, string id, string pw)
        {
            _http = new HttpClient();
            _http.BaseAddress = new Uri(baseAddress);
            if (!Login(id, pw))
                throw new Exception("Login fali");

            Projects = new ProjectCollection(_http);
            Storages = new StorageService(_http);
        }

        private bool Login(string id, string pw)
        {
            using (var res = _http.PostAsync($"/login/{id}/{pw}", null).Result)
            {
                if (HttpStatusCode.OK != res.StatusCode) return false;
                var token = res.Content.ReadAsStringAsync().Result;
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return true;
            }
        }
    }
}
