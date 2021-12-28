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
    public class ItemService : IItemService
    {
        HttpClient _http;
        Project _project;

        public ItemService(HttpClient http, Project project)
        {
            _http = http;
            _project = project;
        }

        public void Add(Item item)
        {
            try
            {
                var res = _http.PostAsJsonAsync<Item>($"/projects/{_project.Id}/items", item).Result;
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
                var res = _http.GetFromJsonAsync<int>($"/projects/{_project.Id}/items/count").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Item> Find(Expression<Func<Item, bool>> predicate)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<Item>>($"/projects/{_project.Id}/items").Result;
                var findRes = res.AsQueryable().Where(predicate).ToList();
                return findRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Item> Find(Expression<Func<Item, bool>> predicate, int page, int perPage = 30)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<Item>>($"/projects/{_project.Id}/items").Result;
                var findRes = res.AsQueryable().Where(predicate).ToList();
                return findRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Item> FindAll()
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<Item>>($"/projects/{_project.Id}/items").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Item entity)
        {
            try
            {
                var res = _http.PutAsJsonAsync<Item>($"/projects/{_project.Id}/items", entity).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
