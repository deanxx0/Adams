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
    public class ImageInfoService : IImageInfoService
    {
        HttpClient _http;
        Project _project;
        public ImageInfoService(HttpClient http, Project project)
        {
            _http = http;
            _project = project;
        }

        public void Add(ImageInfo entity)
        {
            try
            {
                var res = _http.PostAsJsonAsync<ImageInfo>($"/projects/{_project.Id}/imageinfos", entity).Result;
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
                var res = _http.GetFromJsonAsync<int>($"/projects/{_project.Id}/imageinfos/count").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ImageInfo> Find(Expression<Func<ImageInfo, bool>> predicate, int page, int perPage = 30)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<ImageInfo>>($"/projects/{_project.Id}/imageinfos/{page}/{perPage}").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ImageInfo> Find(Expression<Func<ImageInfo, bool>> predicate)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<ImageInfo>>($"/projects/{_project.Id}/imageinfos").Result;
                var findRes = res.AsQueryable().Where(predicate).ToList();
                return findRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ImageInfo> FindAll()
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<ImageInfo>>($"/projects/{_project.Id}/imageinfos").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(ImageInfo entity)
        {
            try
            {
                var res = _http.PutAsJsonAsync<ImageInfo>($"/projects/{_project.Id}/imageinfos", entity).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
