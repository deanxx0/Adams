using NAVIAIServices.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adams.Client
{
    public class AdamsClient
    {
        public ProjectCollection Projects;
        string _token { get; set; }
        public AdamsClient(string id, string pw)
        {


            Projects = new ProjectCollection(_token);
        }

        private void Login(string asdfs, string pw)
        {

        }
    }
}
