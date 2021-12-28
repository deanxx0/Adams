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
    public class AugmentationService : IAugmentationService
    {
        HttpClient _http;
        Project _project;

        public AugmentationService(HttpClient http, Project project)
        {
            _http = http;
            _project = project;
        }

        public void Add(Augmentation augmentation)
        {
            try
            {
                var res = _http.PostAsJsonAsync<Augmentation>($"/projects/{_project.Id}/augmentations", augmentation).Result;
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
                var res = _http.GetFromJsonAsync<int>($"/projects/{_project.Id}/augmentations/count").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Augmentation> Find(Expression<Func<Augmentation, bool>> predicate)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<Augmentation>>($"/projects/{_project.Id}/augmentations").Result;
                var findRes = res.AsQueryable().Where(predicate).ToList();
                return findRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Augmentation> FindAll()
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<Augmentation>>($"/projects/{_project.Id}/augmentations").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Augmentation augmentation)
        {
            try
            {
                var res = _http.PutAsJsonAsync<Augmentation>($"/projects/{_project.Id}/augmentations", augmentation).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
