﻿using Adams.Client;
using NAVIAIServices.Entities;
using NAVIAIServices.Enums;
using System.Linq.Expressions;


Thread.Sleep(3000);

var adams = new AdamsClient("https://localhost:5000", "1", "1");

//for(int i = 0; i < 32; i++)
//{
//    var project = new Project(NAVIAITypes.Mercury, $"p{i}", $"p{i}");

//    adams.Projects.Add(project);
//}


//var count = adams.Projects.GetCount();

//var containsTest = adams.Projects.Contains(project1);

//var removeTest = adams.Projects.Remove(project1);

//var containsTest2 = adams.Projects.Contains(project1);

//adams.Projects.Clear();

//var projectGet = adams.Projects.GetProject(project1);

//var projectPage = adams.Projects.GetProjectPage(1).ToList();

////var projectService = new ProjectService(adams, projectGet);
//var projectService = Extensions.GetService(projectPage[0], adams);



//var aug = new Augmentataion($"tag{i}");


//var count = projectService.Items.Count();
//var list = projectService.Items.Find(x => true);
//var page = projectService.Items.Find(x => true, 2, 10);






Console.WriteLine();
