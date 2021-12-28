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
    public class TrainService : ITrainService
    {
        HttpClient _http;
        Project _project;
        public TrainService(HttpClient http, Project project)
        {
            _http = http;
            _project = project;
        }

        public void Add(Train train)
        {
            try
            {
                var res = _http.PostAsJsonAsync<Train>($"/projects/{_project.Id}/trains", train).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Train> Find(Expression<Func<Train, bool>> predicate)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<Train>>($"/projects/{_project.Id}/trains").Result;
                var findRes = res.AsQueryable().Where(predicate).ToList();
                return findRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Train> FindAll()
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<Train>>($"/projects/{_project.Id}/trains").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Train train)
        {
            try
            {
                var res = _http.PutAsJsonAsync<Train>($"/projects/{_project.Id}/trains", train).Result;
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
                var res = _http.GetFromJsonAsync<int>($"/projects/{_project.Id}/trains/count").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
