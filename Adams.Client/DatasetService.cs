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
    public class DatasetService : IDatasetService
    {
        HttpClient _http;
        Project _project;
        public DatasetService(HttpClient http, Project project)
        {
            _http = http;
            _project = project;
        }

        public void Add(Dataset dataset)
        {
            try
            {
                var res = _http.PostAsJsonAsync<Dataset>($"/projects/{_project.Id}/datasets", dataset).Result;
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
                var res = _http.GetFromJsonAsync<int>($"/projects/{_project.Id}/datasets/count").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Dataset> Find(Expression<Func<Dataset, bool>> predicate)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<Dataset>>($"/projects/{_project.Id}/datasets").Result;
                var findRes = res.AsQueryable().Where(predicate).ToList();
                return findRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Dataset> FindAll()
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<Dataset>>($"/projects/{_project.Id}/datasets").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Dataset dataset)
        {
            try
            {
                var res = _http.PutAsJsonAsync<Dataset>($"/projects/{_project.Id}/datasets", dataset).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
