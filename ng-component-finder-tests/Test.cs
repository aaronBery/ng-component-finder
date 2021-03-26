using NUnit.Framework;
using ng_component_in_template_finder;
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
            const string rootDirectory = "../../../../ng-component-in-template-finder/test-folder/app/";
            Assert.True(Program.IsSelectorUsedInDirectory(rootDirectory, "app-heroes"));
            Assert.False(Program.IsSelectorUsedInDirectory(rootDirectory, "app-villains"));
        }

        [Test]
        public void IsSelectorUsedInFilesTest()
        {
            const string file = "../../../../ng-component-in-template-finder/test-folder/app/layout.component.html";
            string[] files = new String[] { file };
            Assert.True(Program.IsSelectorUsedInFiles(files, "app-heroes"));
            Assert.False(Program.IsSelectorUsedInFiles(files, "app-villains"));
        }

        [Test]
        public void GetComponentSelectorInFileTest()
        {
            const string file = "../../../../ng-component-in-template-finder/test-folder/app/hero.component.ts";
            Assert.AreEqual("app-hero", Program.GetComponentSelectorInFile(file));
        }
    }
}