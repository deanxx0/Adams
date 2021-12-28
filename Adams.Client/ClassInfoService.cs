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
    public class ClassInfoService : IClassInfoService
    {
        HttpClient _http;
        Project _project;
        public ClassInfoService(HttpClient http, Project project)
        {
            _http = http;
            _project = project;
        }

        public void Add(ClassInfo entity)
        {
            try
            {
                var res = _http.PostAsJsonAsync<ClassInfo>($"/projects/{_project.Id}/classinfos", entity).Result;
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
                var res = _http.GetFromJsonAsync<int>($"/projects/{_project.Id}/classinfos/count").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ClassInfo> Find(Expression<Func<ClassInfo, bool>> predicate)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<ClassInfo>>($"/projects/{_project.Id}/classinfos").Result;
                var findRes = res.AsQueryable().Where(predicate).ToList();
                return findRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ClassInfo> FindAll()
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<ClassInfo>>($"/projects/{_project.Id}/classinfos").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(ClassInfo entity)
        {
            try
            {
                var res = _http.PutAsJsonAsync<ClassInfo>($"/projects/{_project.Id}/classinfos", entity).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
