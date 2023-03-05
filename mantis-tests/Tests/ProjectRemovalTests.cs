using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests : AuthTestBase
    {
        [SetUp]
        public void Init()
        {
            app.Project.CreateIfNotExist();
        }

        [Test]
        public void RemoveProject()
        {
            List<ProjectData> oldProjectsList = app.Project.GetProjectsList();

            app.Project.RemoveProject(0);

            List<ProjectData> newProjectsList = app.Project.GetProjectsList();

            Assert.AreEqual(oldProjectsList.Count - 1, app.Project.GetCountProjects());

            oldProjectsList.RemoveAt(0);
            oldProjectsList.Sort();
            newProjectsList.Sort();
            Assert.AreEqual(oldProjectsList, newProjectsList);
        }
    }
}
