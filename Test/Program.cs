using Adams.Client;
using NAVIAIServices.Entities;
using NAVIAIServices.Enums;
using System.Linq.Expressions;


Thread.Sleep(3000);

var adams = new AdamsClient("https://localhost:5000", "1", "1");

//var project = new Project(NAVIAITypes.Mercury, "p1", "p1");
//adams.Projects.Add(project);
var projects = adams.Projects.GetProjectPage(1, 10).ToList();
var projectService = Extensions.GetService(projects[0], adams);

Console.WriteLine();
