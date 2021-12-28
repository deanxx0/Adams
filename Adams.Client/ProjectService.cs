using NAVIAIServices;
using NAVIAIServices.Entities;
using NAVIAIServices.ProjectService;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool IsMultiChannel => throw new NotImplementedException();
        public IAugmentationService Augmentations => throw new NotImplementedException();

        public IClassInfoService ClassInfos => new ClassInfoService(_http, Entity);

        public IDatasetService Datasets => throw new NotImplementedException();

        public IInputChannelService InputChannels => new InputChannelService(_http, Entity);

        public IItemService Items => new ItemService(_http, Entity);

        public IMetadataValueService MetadataValues => throw new NotImplementedException();

        public IMetadataKeyService MetadataKeys => new MetadataKeyService(_http, Entity);

        public IImageInfoService ImageInfos => throw new NotImplementedException();

        public ITrainConfigurationService TrainConfigurations => throw new NotImplementedException();

        public ITrainService Trains => throw new NotImplementedException();

        public ILabelService Labels => throw new NotImplementedException();

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
