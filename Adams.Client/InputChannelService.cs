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
    public class InputChannelService : IInputChannelService
    {
        HttpClient _http;
        Project _project;

        public InputChannelService(HttpClient http, Project project)
        {
            _http = http;
            _project = project;
        }

        public void Add(InputChannel entity)
        {
            try
            {
                var res = _http.PostAsJsonAsync<InputChannel>($"/projects/{_project.Id}/inputchannels", entity).Result;
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
                var res = _http.GetFromJsonAsync<int>($"/projects/{_project.Id}/inputchannels/count").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<InputChannel> Find(Expression<Func<InputChannel, bool>> predicate)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<InputChannel>>($"/projects/{_project.Id}/inputchannels").Result;
                var findRes = res.AsQueryable().Where(predicate).ToList();
                return findRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<InputChannel> FindAll()
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<InputChannel>>($"/projects/{_project.Id}/inputchannels").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(InputChannel entity)
        {
            try
            {
                var res = _http.PutAsJsonAsync<InputChannel>($"/projects/{_project.Id}/inputchannels", entity).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
