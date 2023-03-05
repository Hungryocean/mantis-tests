using System;
using System.Collections.Generic;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [SetUp]
        public void Init()
        {
            app.Project.DeleteExistingProject(new ProjectData("New project"));
        }

        [Test]
        public void ProjectCreationTest()
        {
            List<ProjectData> oldProjectsList = app.Project.GetProjectsList();

            ProjectData project = new ProjectData("Test Project 2 ")
            {
                Status = "в разработке",
                Visibility = "публичный",
                Enabled = "true",
                Description = "Test Description",
            };

            app.Project.CreateNewProject(project);

            Assert.AreEqual(oldProjectsList.Count + 1, app.Project.GetCountProjects());

            List<ProjectData> newProjectsList = app.Project.GetProjectsList();

            project.Visibility = "публичный";
            oldProjectsList.Add(project);
            oldProjectsList.Sort();
            newProjectsList.Sort();

            Assert.AreEqual(oldProjectsList, newProjectsList);
        }
    }
}
