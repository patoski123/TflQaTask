﻿using BoDi;
using TflQaTask.Models;
using TflQaTask.Reports;
using TflQaTask.WebDriver;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

[assembly: Parallelizable(ParallelScope.Fixtures)]
namespace TflQaTask.Hooks
{
    [Binding]
    public class Hook
    {
        public static TestConfig Config;
        private Webdriver _webDriver;
        private readonly IObjectContainer _objectContainer;
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;
        private readonly Reporting _reporting;



        public Hook(IObjectContainer objectContainer, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _featureContext = featureContext;
            _reporting = new Reporting();
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var config = new ConfigurationBuilder()
              .AddJsonFile("Appsettings.json")
              .Build();
            Config = new TestConfig();
            config.GetSection("TestConfig").Bind(Config);
            SetEnvironmentVariables();
            Reporting.StartReport();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _webDriver = new Webdriver();
            _webDriver.SetDriver();
            _objectContainer.RegisterInstanceAs(_webDriver.Driver);
            _reporting.Test = Reporting.Extent.CreateTest(TestContext.CurrentContext.Test.Name);
            _reporting.Test.AssignCategory(_featureContext.FeatureInfo.Tags);
        }

        [AfterStep]
        public void AfterStep() =>
            _reporting.AfterStepTask(_scenarioContext);



        [AfterScenario]
        public void AfterScenario()
        {
            _reporting.AfterScenarioTask(_webDriver.Driver);
            _webDriver.Driver.Quit();
            _objectContainer.Dispose();
        }

        [AfterTestRun]
        public static void AfterTestRun() =>
            Reporting.EndReport();


       
        private static void SetEnvironmentVariables()
        {
            var browser = Environment.GetEnvironmentVariable("browser");

            if (!string.IsNullOrEmpty(browser))
                Config.Browser = browser;


        }
    }
}