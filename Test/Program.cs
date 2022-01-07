using Adams.Client;
using NAVIAIServices.Entities;
using NAVIAIServices.Enums;
using System.Linq.Expressions;


Thread.Sleep(5000);

var adams = new AdamsClient("http://localhost:5000", "1", "1");

var project = new Project(NAVIAITypes.Mercury, "p4", "desc");
adams.Projects.Add(project);

var projects = adams.Projects.GetProjectPage(1, 10).ToList();
var projectService = Extensions.GetService(projects[1], adams); // p2

var classInfo = new ClassInfo("class1", "desc", "", 0, 0, 0);
projectService.ClassInfos.Add(classInfo);

var inputChannel = new InputChannel("inputchannel1", true, "desc", "regex");
projectService.InputChannels.Add(inputChannel);

var metaKey = new MetadataKey("metakey1", "desc", MetadataTypes.String);
projectService.MetadataKeys.Add(metaKey);

var configuration = new TrainConfiguration("config1", "desc", 512, 512, 10, "pretrainPath", 100, 1, 1, 1, false, 0, false, 0.1);
projectService.TrainConfigurations.Add(configuration);

var augmentation = new Augmentation("a1", "desc", true, true, true, 0.1, 0.1, 0.1, 0.1, BorderModes.Replicate, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 1);
projectService.Augmentations.Add(augmentation);

var item = new Item("tag1");
projectService.Items.Add(item);

var originalpath = @"D:\2022\adams\project\imageinfo_originalpath\testimg.png";
var imageInfo = new ImageInfo(item.Id, inputChannel.Id, originalpath);
projectService.ImageInfos.Add(imageInfo);

var label = new Label(item.Id, classInfo.Id, "labeldata");
projectService.Labels.Add(label);

var metaValue = new MetadataValue(item.Id, metaKey.Id, MetadataTypes.String, "valueobject");
projectService.MetadataValues.Add(metaValue);

var train_dataset = new Dataset("train_dataset1", "desc", DatasetTypes.Training);
projectService.Datasets.Add(train_dataset);

var validation_dataset = new Dataset("validation_dataset1", "desc", DatasetTypes.Validation);
projectService.Datasets.Add(validation_dataset);

var trainsets = new List<string>() { train_dataset.Id };
var validationsets = new List<string>() { validation_dataset.Id };
var train = new Train("train1", "desc", configuration.Id, augmentation.Id, trainsets, validationsets);
projectService.Trains.Add(train);







// *** file storage service test

//var project = new Project(NAVIAITypes.Mercury, "p1", "p1");
//adams.Projects.Add(project);
//var projects = adams.Projects.GetProjectPage(1, 10).ToList();
//var projectService = Extensions.GetService(projects[0], adams);

//var items = projectService.Items.FindAll().ToList();
//var item = items[0];

//var imageInfos = projectService.ImageInfos.FindAll().ToList();
//var imageInfo = imageInfos[0];

//var downloadPath = @"D:\2022\adams\project\DownloadFileStorage";
//adams.Storages.DownloadImage(projectService, item.Id, imageInfo.Id, downloadPath);

Console.WriteLine();
