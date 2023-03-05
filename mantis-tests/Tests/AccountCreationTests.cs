using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;



namespace mantis_tests 
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp]
        public void setUpConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "testuser4",
                Password = "password",
                Email = "testuser4@localhost.localdomain"


            };
            app.James.Delete(account);
            app.James.Add(account);
            app.Registration.Register(account);

        }

        [TestFixtureTearDown]
        public void restoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }
    }
}