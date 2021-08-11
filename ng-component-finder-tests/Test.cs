using NUnit.Framework;
using ng_component_finder;
using System;

namespace ng_component_finder_tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestIsHtmlFileTest()
        {
            string htmlFile = "../test-folder/app/hero.component.html";
            string tsFile = "../test-folder/app/hero.component.ts";
            Assert.True(Program.IsHtmlFile(htmlFile));
            Assert.False(Program.IsHtmlFile(tsFile));
        }

        [Test]
        public void TestIsTsFileTest()
        {
            string htmlFile = "../test-folder/app/hero.component.html";
            string tsFile = "../test-folder/app/hero.component.ts";
            Assert.False(Program.IsTsFile(htmlFile));
            Assert.True(Program.IsTsFile(tsFile));
        }

        [Test]
        public void TestIsSpecTsFileTest()
        {
            string specFile = "../test-folder/app/hero.component.spec.ts";
            string tsFile = "../test-folder/app/hero.component.ts";
            Assert.False(Program.IsSpecTsFile(tsFile));
            Assert.True(Program.IsSpecTsFile(specFile));
        }

        [Test]
        public void IsComponentFileTest()
        {
            string templateFile = "../test-folder/app/hero.component.html";
            string componentFile = "../test-folder/app/hero.component.ts";
            Assert.True(Program.IsComponentFile(componentFile));
            Assert.False(Program.IsComponentFile(templateFile));
        }

        [Test]
        public void IsSelectorUsedInDirectoryTest()
        {
            const string rootDirectory = "../../../../ng-component-finder/test-folder/app/";
            Assert.True(Program.IsSelectorUsedInDirectory(rootDirectory, "app-heroes"));
            Assert.False(Program.IsSelectorUsedInDirectory(rootDirectory, "app-villains"));
        }

        [Test]
        public void IsSelectorUsedInFilesTest()
        {
            const string file = "../../../../ng-component-finder/test-folder/app/layout.component.html";
            string[] files = new String[] { file };
            Assert.True(Program.IsSelectorUsedInFiles(files, "app-heroes"));
            Assert.False(Program.IsSelectorUsedInFiles(files, "app-villains"));
        }

        [Test]
        public void IsComponentNameUsedInFilesTest()
        {
            const string file = "../../../../ng-component-finder/test-folder/app/app.module.ts";
            string[] files = new String[] { file };
            Assert.True(Program.IsComponentNameUsedInRouteConfigInFiles(files, "HeroComponent"));
            Assert.False(Program.IsComponentNameUsedInRouteConfigInFiles(files, "VillianComponent"));
        }

        [Test]
        public void GetComponentSelectorInFileTest()
        {
            const string file = "../../../../ng-component-finder/test-folder/app/hero.component.ts";
            Assert.AreEqual("app-hero", Program.GetComponentSelectorInFile(file));
        }

        [Test]
        public void GetComponentNameInFileTest()
        {
            const string file = "../../../../ng-component-finder/test-folder/app/hero.component.ts";
            Assert.AreEqual("HeroComponent", Program.GetComponentNameInFile(file));
        }
    }
}