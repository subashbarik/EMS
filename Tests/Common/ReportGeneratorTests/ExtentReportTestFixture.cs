using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System.Reflection;

namespace ReportGeneratorTests
{
    public class ExtentReportTestFixture : IDisposable
    {
        private readonly ExtentReports _extentReport;
        private readonly ExtentHtmlReporter _extentHtmlReporter;
        private ExtentTest _extentTest;
        private readonly string _reportPath;
        private readonly string _documentTitle;
        private readonly string _reportName;
      
        public ExtentReportTestFixture()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            var currentDir = Directory.GetCurrentDirectory();
            var config = ConfigurationSetup.SetupJsonConfigFile();
            
            _reportPath = config["ReportPath"] != null ? config["ReportPath"] : currentDir;
            _documentTitle = config["DocumentTitle"] != null ? config["DocumentTitle"] : assemblyName;
            _reportName = config["ReportName"] != null ? config["ReportName"] : assemblyName;

            _extentHtmlReporter = new ExtentHtmlReporter(_reportPath);
            _extentHtmlReporter.Config.Theme = Theme.Dark;
            _extentHtmlReporter.Config.DocumentTitle = _documentTitle;
            _extentHtmlReporter.Config.ReportName = _reportName;

            _extentReport = new ExtentReports();
            _extentReport.AttachReporter(_extentHtmlReporter);
        }
        public void LogReport(string status, string message)
        {
            var testStatus = Status.Pass;
            switch (status)
            {
                case "Fail":
                    testStatus = Status.Fail;
                    TestStatus(status.ToString());
                    break;
                case "Pass":
                    testStatus = Status.Pass;
                    TestStatus(status.ToString());
                    break;
                case "Error":
                    testStatus = Status.Error;
                    TestStatus(status.ToString());
                    break;
                default:
                    break;
            }
            _extentTest.Log(testStatus, message);
        }
        public void CreateTest(string testName)
        {
            _extentTest = _extentReport.CreateTest(testName);
        }
        public void TestStatus(string status)
        {
            if (status.Equals("Pass"))
            {
                _extentTest.Pass("Test case passed");
            }
            else
            {
                _extentTest.Fail("Test case failed");
            }
        }
        public void Dispose()
        {
            _extentReport.Flush();
        }
    }
}
