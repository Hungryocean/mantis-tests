using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        public RegistrationHelper Registration { get; private set; }
        public FtpHelper Ftp { get; set; }
        public JamesHelper James { get; set; }
        public MailHelper Mail { get; set; }
        public LoginHelper Login { get; set; }
        public ManagementMenuHelper Menu { get; set; }
        public ProjectManagementHelper Project { get; set; }

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager() 
        {  
            driver = new ChromeDriver();
            baseURL = "http://localhost";
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);

            Login = new LoginHelper(this, baseURL);
            Menu = new ManagementMenuHelper(this, baseURL);
            Project = new ProjectManagementHelper(this);


        }

         ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url= "http://localhost/mantisbt-2.25.4";
                app.Value = newInstance;
            }
            return app.Value;
        }
        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

    }
}
