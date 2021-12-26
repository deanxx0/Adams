using Adams.Client;
using NAVIAIServices.Entities;
using NAVIAIServices.Enums;
using System.Linq.Expressions;


Thread.Sleep(3000);

var adams = new AdamsClient("1", "1");

var project1 = new Project(NAVIAITypes.Mercury, "p1", "p1p1");

adams.Projects.Add(project1);

//var count = adams.Projects.GetCount();

//var containsTest = adams.Projects.Contains(project1);

//var removeTest = adams.Projects.Remove(project1);

//var containsTest2 = adams.Projects.Contains(project1);

//adams.Projects.Clear();

var projectService = new ProjectService(adams, project1);

Console.WriteLine();
