using NAVIAIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adams.Client
{
    internal class StorageService : IStorageService
    {
        public void DownloadImage(IProjectService projectService, string itemId, string imageInfoId, string downloadPath)
        {

        }

        public void DownloadModelFile(IProjectService projectService, string trainId, string downloadPath)
        {

        }

        public void UploadImage(IProjectService projectService, string origialPath, string itemId, string imageInfoId)
        {
            // upload image to server
            // add server image path to imageInfoId doc
        }
    }
}
