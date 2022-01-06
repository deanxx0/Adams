using Adams.Client;
using NAVIAIServices.Entities;
using NAVIAIServices.Enums;
using System.Linq.Expressions;


Thread.Sleep(3000);

var adams = new AdamsClient("http://localhost:5000", "1", "1");

//var project = new Project(NAVIAITypes.Mercury, "p1", "p1");
//adams.Projects.Add(project);
var projects = adams.Projects.GetProjectPage(1, 10).ToList();
var projectService = Extensions.GetService(projects[0], adams);

var items = projectService.Items.FindAll().ToList();
var item = items[0];

var imageInfos = projectService.ImageInfos.FindAll().ToList();
var imageInfo = imageInfos[0];

var downloadPath = @"D:\2022\adams\project\DownloadFileStorage";
adams.Storages.DownloadImage(projectService, item.Id, imageInfo.Id, downloadPath);

Console.WriteLine();
