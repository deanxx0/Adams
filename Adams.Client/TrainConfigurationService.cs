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
    public class TrainConfigurationService : ITrainConfigurationService
    {
        HttpClient _http;
        Project _project;

        public TrainConfigurationService(HttpClient http, Project project)
        {
            _http = http;
            _project = project;
        }

        public void Add(TrainConfiguration configuration)
        {
            try
            {
                var res = _http.PostAsJsonAsync<TrainConfiguration>($"/projects/{_project.Id}/trainconfigurations", configuration).Result;
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
                var res = _http.GetFromJsonAsync<int>($"/projects/{_project.Id}/trainconfigurations/count").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TrainConfiguration> Find(Expression<Func<TrainConfiguration, bool>> predicate)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<TrainConfiguration>>($"/projects/{_project.Id}/trainconfigurations").Result;
                var findRes = res.AsQueryable().Where(predicate).ToList();
                return findRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TrainConfiguration> FindAll()
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<TrainConfiguration>>($"/projects/{_project.Id}/trainconfigurations").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(TrainConfiguration configuration)
        {
            try
            {
                var res = _http.PutAsJsonAsync<TrainConfiguration>($"/projects/{_project.Id}/trainconfigurations", configuration).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
