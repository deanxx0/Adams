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
    public class LabelService : ILabelService
    {
        HttpClient _http;
        Project _project;
        public LabelService(HttpClient http, Project project)
        {
            _http = http;
            _project = project;
        }

        public void Add(Label entity)
        {
            try
            {
                var res = _http.PostAsJsonAsync<Label>($"/projects/{_project.Id}/labels", entity).Result;
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
                var res = _http.GetFromJsonAsync<int>($"/projects/{_project.Id}/labels/count").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(Label entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Label> Find(Expression<Func<Label, bool>> predicate)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<Label>>($"/projects/{_project.Id}/labels").Result;
                var findRes = res.AsQueryable().Where(predicate).ToList();
                return findRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Label entity)
        {
            try
            {
                var res = _http.PutAsJsonAsync<Label>($"/projects/{_project.Id}/labels", entity).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
