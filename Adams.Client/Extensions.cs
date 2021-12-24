using NAVIAIServices;
using NAVIAIServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adams.Client
{
    public static class Extensions
    {
        public static IProjectService GetService(this Project project, AdamsClient client)
        {
            return new ProjectService(client, project);
        }
    }
}
