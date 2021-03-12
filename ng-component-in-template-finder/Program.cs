using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ng_component_in_template_finder
{
    class Program
    {
        private const string testRootDirectory = "/Users/aaronbery/Projects/ng-component-in-template-finder/ng-component-in-template-finder/test-folder/";

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Process(args[0], args[0]);
            } else
            {
                Process(testRootDirectory, testRootDirectory);
            }
        }

        static void ProcessFiles(string[] files, string rootDirectory)
        {
            try
            {
                foreach (string file in files)
                {
                    if (IsComponentFile(file))
                    {
                        var selector = GetComponentSelectorInFile(file);
                        if (selector != null && !IsSelectorUsed(selector, rootDirectory))
                        {
                            Console.WriteLine($"Selector {selector} is not in use");
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("There was an error processing file:", error.Message);
            }
        }

        static bool IsHtmlFile(string fileName)
        {
            return Regex.Match(fileName, "^.+\\.html$").Success;
        }

        static bool IsComponentFile(string fileName)
        {
            return Regex.Match(fileName, "^.+\\.component\\.ts").Success;
        }


        static void Process(string path, string rootDirectory)
        {
            try
            {
                string[] directories = Directory.GetDirectories(path);

                string[] files = Directory.GetFiles(path);
                
                if (files.Length > 0)
                {
                    ProcessFiles(files, rootDirectory);
                }

                foreach (string directory in directories)
                {
                    if (Directory.Exists(directory))
                    {
                        Process(directory, rootDirectory);
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("There was an error processing:", error.Message);
            }
        }

        static bool IsSelectorUsed(string selector, string rootDirectory)
        {
            bool isUsed = false;

            string[] directories = Directory.GetDirectories(rootDirectory);

            string[] files = Directory.GetFiles(rootDirectory);

            if (IsSelectorUsedInFiles(files, selector))
            {
                isUsed = true;
            }

            foreach (string directory in directories)
            {
                if (IsSelectorUsedInDirectory(directory, selector))
                {
                    isUsed = true;
                }
            }


            return isUsed;
        }

        static bool IsSelectorUsedInDirectory(string path, string selector)
        {
            bool isUsed = false;

            try
            {
                string[] directories = Directory.GetDirectories(path);

                string[] files = Directory.GetFiles(path);

                if (IsSelectorUsedInFiles(files, selector))
                {
                    isUsed = true;
                }

                foreach (string directory in directories)
                {
                    if (IsSelectorUsedInDirectory(directory, selector))
                    {
                        isUsed = true;
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("There was an error processing:", error.Message);
            }

            return isUsed;
        }

        static bool IsSelectorUsedInFiles(string[] files, string selector)
        {
            bool isUsed = false;

            try
            {

                foreach (string file in files)
                {
                    if (IsHtmlFile(file))
                    {
                        // Open the text file using a stream reader.
                        using (var sr = new StreamReader(file))
                        {
                            string closingTag = $"</{selector}>";
                            MatchCollection matchedSelectors = Regex.Matches(sr.ReadToEnd(), closingTag);

                            if (matchedSelectors.Count > 0)
                            {
                                isUsed = true;
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("There was an error in IsSelectorUsed:", error.Message);
            }

            return isUsed;
        }

        static string GetComponentSelectorInFile(string path)
        {
            string selector = null;

            try
            {
                // Open the text file using a stream reader.
                using (var sr = new StreamReader(path))
                {
                    MatchCollection matchedSelectors = Regex.Matches(sr.ReadToEnd(), "selector: '[^']+'");

                    if (matchedSelectors.Count > 0)
                    {
                        selector = Regex.Replace(matchedSelectors[0].ToString(), "selector: '([^']+)'", "$1");
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("There was an error in GetComponentSelectorInFile:", error.Message);
            }

            return selector;
        }


    }
}
