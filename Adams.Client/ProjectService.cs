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
        private AdamsClient client;
        public ProjectService(AdamsClient client, Project entity)
        {
            this.client = client;
            this.Entity = entity;
        }

        public Project Entity { get; init; }
        public bool IsMultiChannel => throw new NotImplementedException();
        public IAugmentationService Augmentations => throw new NotImplementedException();

        public IClassInfoService ClassInfos => throw new NotImplementedException();

        public IDatasetService Datasets => throw new NotImplementedException();

        public IInputChannelService InputChannels => throw new NotImplementedException();

        public IItemService Items => throw new NotImplementedException();

        public IMetadataValueService MetadataValues => throw new NotImplementedException();

        public IMetadataKeyService MetadataKeys => throw new NotImplementedException();

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
