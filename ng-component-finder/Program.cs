using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ng_component_finder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the ng component finder. This utlity is designed to find unused Angular components by scanning your project however there are some assumptions. Please see the README.md for more details");
            if (args.Length > 0)
            {
                Process(args[0], args[0]);
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
                        var componentName = GetComponentNameInFile(file);

                        if (selector != null && !IsSelectorUsedInDirectory(rootDirectory, selector) && !IsComponentUsedInRoutes(rootDirectory, componentName))
                        {
                            Console.WriteLine($"Selector '{selector}' / Component '{componentName}', is not in use");
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("There was an error processing file:", error.Message);
            }
        }

        public static bool IsHtmlFile(string fileName)
        {
            return Regex.Match(fileName, "^.+\\.html$").Success;
        }

        public static bool IsComponentFile(string fileName)
        {
            return Regex.Match(fileName, "^.+\\.component\\.ts").Success;
        }

        public static bool IsTsFile(string fileName)
        {
            return Regex.Match(fileName, "^.+\\.ts").Success;
        }

        public static bool IsSpecTsFile(string fileName)
        {
            return Regex.Match(fileName, "^.+\\.spec\\.ts").Success;
        }


        public static void Process(string path, string rootDirectory)
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

        public static bool IsSelectorUsedInDirectory(string path, string selector)
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

        public static bool IsSelectorUsedInFiles(string[] files, string selector)
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


        public static bool IsComponentUsedInRoutes(string path, string componentName)
        {
            bool isUsed = false;

            try
            {
                string[] directories = Directory.GetDirectories(path);

                string[] files = Directory.GetFiles(path);

                if (IsComponentNameUsedInRouteConfigInFiles(files, componentName))
                {
                    isUsed = true;
                }

                foreach (string directory in directories)
                {
                    if (IsComponentUsedInRoutes(directory, componentName))
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

        public static bool IsComponentNameUsedInRouteConfigInFiles(string[] files, string componentName)
        {
            bool isUsed = false;

            try
            {

                foreach (string file in files)
                {
                    if (IsTsFile(file) && !IsSpecTsFile(file))
                    {
                        // Open the text file using a stream reader.
                        using (var sr = new StreamReader(file))
                        {
                            string matchingConfiguration = $"component: {componentName}";
                            MatchCollection matchedSelectors = Regex.Matches(sr.ReadToEnd(), matchingConfiguration);

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

        public static string GetComponentNameInFile(string path)
        {
            string selector = null;

            try
            {
                // Open the text file using a stream reader.
                using (var sr = new StreamReader(path))
                {
                    MatchCollection matchedSelectors = Regex.Matches(sr.ReadToEnd(), "@Component\\([^\\)]+\\)\n*export class ([A-Za-z0-9]+)");

                    if (matchedSelectors.Count > 0)
                    {
                        selector = Regex.Replace(matchedSelectors[0].ToString(), "@Component\\([^\\)]+\\)\n*export class ([A-Za-z0-9]+)", "$1");
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("There was an error in GetComponentNameInFile:", error.Message);
            }

            return selector;
        }

        public static string GetComponentSelectorInFile(string path)
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
