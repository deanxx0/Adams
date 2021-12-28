using NAVIAIServices;
using NAVIAIServices.Entities;
using System.Collections;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Json;

namespace Adams.Client
{
    public class ProjectCollection : ICollection<Project>
    {
        HttpClient _http;

        public ProjectCollection(HttpClient http)
        {
            _http = http;
        }

        public int Count => GetCount();
        public bool IsReadOnly => throw new NotImplementedException();

        public int GetCount()
        {
            try
            {
                var res = _http.GetFromJsonAsync<int>($"/projects/count").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Project> GetProjectPage(int pageNo, int perPage = 10)
        {
            try
            {
                var res = _http.GetFromJsonAsync<List<Project>>($"/projects/{pageNo}/{perPage}").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Project GetProject(Project item)
        {
            try
            {
                var res = _http.GetFromJsonAsync<Project>($"/projects/{item.Id}").Result;
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Add(Project item)
        {
            try
            {
                var res = _http.PostAsJsonAsync<Project>("/projects", item).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Clear()
        {
            try
            {
                var res = _http.DeleteAsync("/projects").Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Contains(Project item)
        {
            try
            {
                var res = _http.GetFromJsonAsync<bool>($"/projects/contains/{item.Id}").Result;
                if (res) return true;
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public void CopyTo(Project[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Project> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Project item)
        {
            try
            {
                var res = _http.DeleteAsync($"/projects/{item.Id}").Result;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}