using NAVIAIServices;
using NAVIAIServices.Entities;
using NAVIAIServices.ProjectService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Adams.Client
{
    public class ProjectService : IProjectService
    {
        private HttpClient _http;
        public ProjectService(AdamsClient client, Project entity)
        {
            _http = client._http;
            this.Entity = entity;
        }

        public Project Entity { get; init; }
        public bool IsMultiChannel => GetInputChannelCount();


        private bool GetInputChannelCount()
        {
            try
            {
                var res = _http.GetFromJsonAsync<int>($"/projects/{Entity.Id}/inputchannels/count").Result;
                if (res > 1) return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IAugmentationService Augmentations => new AugmentationService(_http, Entity);

        public IClassInfoService ClassInfos => new ClassInfoService(_http, Entity);

        public IDatasetService Datasets => new DatasetService(_http, Entity);

        public IInputChannelService InputChannels => new InputChannelService(_http, Entity);

        public IItemService Items => new ItemService(_http, Entity);

        public IMetadataValueService MetadataValues => new MetadataValueService(_http, Entity);

        public IMetadataKeyService MetadataKeys => new MetadataKeyService(_http, Entity);

        public IImageInfoService ImageInfos => new ImageInfoService(_http, Entity);

        public ITrainConfigurationService TrainConfigurations => new TrainConfigurationService(_http, Entity);

        public ITrainService Trains => new TrainService(_http, Entity);

        public ILabelService Labels => new LabelService(_http, Entity);

        public void BeginTrans()
        {
            throw new NotImplementedException();
        }

        public void CommitTrans()
        {
            throw new NotImplementedException();
        }
    }
}
