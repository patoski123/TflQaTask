using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using TflQaTask.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Reflection;
using TechTalk.SpecFlow;

namespace TflQaTask.Reports
{
    public class Reporting
    {
        public static ExtentReports Extent;
        public ExtentTest Test;
        private static Uri BaseReportsDirectory;

        public static void StartReport()
        {
            var actualPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location)!.Parent!.Parent!.Parent!.FullName;
            BaseReportsDirectory = new Uri(actualPath + "\\Reports\\");
            var reportPath = BaseReportsDirectory.LocalPath;
            var reporter = new ExtentHtmlReporter(reportPath);
            Extent = new ExtentReports();
            Extent.AttachReporter(reporter);
            Extent.AddSystemInfo("Task", "TflQaTask");
            Extent.AddSystemInfo("Browser", Hook.Config.Browser);
        }

        public void AfterStepTask(ScenarioContext scenarioContext)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stepDefType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepText = scenarioContext.StepContext.StepInfo.Text;

            switch (status)
            {
                case NUnit.Framework.Interfaces.TestStatus.Skipped:
                    Test.Log(Status.Skip, $"{stepDefType} : {stepText}");
                    Test.Skip("Test was marked as Skipped");
                    break;
                case NUnit.Framework.Interfaces.TestStatus.Warning:
                    Test.Log(Status.Warning, $"{stepDefType} : {stepText}");
                    Test.Warning("Test was marked as Warning");
                    break;
                case NUnit.Framework.Interfaces.TestStatus.Failed:
                    Test.Log(Status.Fail, $"{stepDefType} : {stepText}");
                    Test.Fail($"Assertion Result: {TestContext.CurrentContext.Result.Message}");
                    break;
                default:
                    Test.Log(Status.Pass, $"{stepDefType} : {stepText}");
                    break;
            }
        }

        public void AfterScenarioTask(IWebDriver driver)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == NUnit.Framework.Interfaces.TestStatus.Passed) return;
            var testName = TestContext.CurrentContext.Test.MethodName;
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(testName + ".png", ScreenshotImageFormat.Png);
            var screenShotPath = new Uri(BaseReportsDirectory + $"\\Screenshots\\{testName}.png").LocalPath;
            File.Move($"{testName}.png", screenShotPath, true);
            Test.AddScreenCaptureFromPath(screenShotPath);
        }


        public static void EndReport() =>
            Extent.Flush();

    }
}