using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Principal;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        public void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
            String url = GetConfirmationUrl(account);
            FillPasswordForm(url, account);
            SubmitPasswordForm();

        }

        private void SubmitPasswordForm()
        {
            driver.FindElement(By.XPath("//form[@id='account-update-form']/fieldset/span/button/span")).Click();
        }

        private void FillPasswordForm(string url, AccountData account)
        {
            driver.Url = url;
            driver.FindElement(By.Id("realname")).SendKeys(account.Name);
            driver.FindElement(By.Id("password")).SendKeys(account.Password);
            driver.FindElement(By.Id("password-confirm")).SendKeys(account.Password);
        }

        private string GetConfirmationUrl(AccountData account)
        {
            String message = manager.Mail.GetLastMail(account);
            Match match = Regex.Match(message, @"http://\S*");
            return match.Value;
        }
        private void OpenRegistrationForm()
        {
            driver.FindElement(By.LinkText("Signup for a new account")).Click();
        }
        private void SubmitRegistration()
        {
            driver.FindElement(By.XPath("//input[@value='Signup']")).Click();
        }
        private void FillRegistrationForm(AccountData account)
        {
            driver.FindElement(By.Id("username")).SendKeys(account.Name);
            driver.FindElement(By.Id("email-field")).SendKeys(account.Email);
        }
        private void OpenMainPage()
        {
            manager.Driver.Url = "http://localhost/mantisbt-2.25.6/login_page.php";
        }
    }
}
