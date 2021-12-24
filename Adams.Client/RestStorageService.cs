using NAVIAIServices.RepositoryService.Interfaces;
using NAVIAIServices.StorageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adams.Client
{
    internal class RestStorageService : IStorageService
    {
        public void DownloadImage(IProjectService projectService, string itemId, string imageInfoId, string downloadPath)
        {

        }

        public void DownloadModelFile(IProjectService projectService, string trainId, string downloadPath)
        {

        }

        public void UploadImage(IProjectService projectService, string origialPath, string itemId, string imageInfoId)
        {

        }
    }
}
