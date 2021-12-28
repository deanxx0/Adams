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
    public class MetadataValueService : IMetadataValueService
    {
        HttpClient _http;
        Project _project;
        public MetadataValueService(HttpClient http, Project project)
        {
            _http = http;
            _project = project;
        }

        public void Add(MetadataValue metadataValue)
        {
            try
            {
                var res = _http.PostAsJsonAsync<MetadataValue>($"/projects/{_project.Id}/metadatavalues", metadataValue).Result;
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
                var res = _http.GetFromJsonAsync<int>($"/projects/{_project.Id}/metadatavalues/count").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<MetadataValue> Find(Expression<Func<MetadataValue, bool>> predicate)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<MetadataValue>>($"/projects/{_project.Id}/metadatavalues").Result;
                var findRes = res.AsQueryable().Where(predicate).ToList();
                return findRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<MetadataValue> FindAll()
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<MetadataValue>>($"/projects/{_project.Id}/metadatavalues").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(MetadataValue entity)
        {
            try
            {
                var res = _http.PutAsJsonAsync<MetadataValue>($"/projects/{_project.Id}/metadatavalues", entity).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
