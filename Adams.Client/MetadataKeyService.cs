using NAVIAIServices.Entities;
using NAVIAIServices.ProjectService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Adams.Client
{
    public class MetadataKeyService : IMetadataKeyService
    {
        HttpClient _http;
        Project _project;

        public MetadataKeyService(HttpClient http, Project project)
        {
            _http = http;
            _project = project;
        }

        public void Add(MetadataKey metadataKey)
        {
            try
            {
                var res = _http.PostAsJsonAsync<MetadataKey>($"/projects/{_project.Id}/metadatakeys", metadataKey).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Count()
        {
            try
            {
                var res = _http.GetFromJsonAsync<int>($"/projects/{_project.Id}/metadatakeys/count").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<MetadataKey> Find(Expression<Func<MetadataKey, bool>> predicate)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<MetadataKey>>($"/projects/{_project.Id}/metadatakeys").Result;
                var findRes = res.AsQueryable().Where(predicate).ToList();
                return findRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<MetadataKey> FindAll()
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<MetadataKey>>($"/projects/{_project.Id}/metadatakeys").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(MetadataKey metadataKey)
        {
            try
            {
                var res = _http.PutAsJsonAsync<MetadataKey>($"/projects/{_project.Id}/metadatakeys", metadataKey).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
