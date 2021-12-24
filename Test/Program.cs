


using Adams.Client;
using NAVIAIServices.Entities;
using System.Linq.Expressions;

var adams = new AdamsClient("sss", "1234");

var adams1 = new AdamsClient("asdf", "1234");

var project = adams.Projects.Where(x => x.Id == "projcetId").FirstOrDefault();

var projectService = project.GetService(adams1);




