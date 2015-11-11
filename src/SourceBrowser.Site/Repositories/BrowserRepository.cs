﻿namespace SourceBrowser.Site.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using SourceBrowser.Generator;
    using SourceBrowser.Site.Models;
    using SourceBrowser.Site.Utilities;
    using SourceBrowser.SolutionRetriever;

    internal static class BrowserRepository
    {
        private static readonly string StaticHtmlAbsolutePath = System.Web.Hosting.HostingEnvironment.MapPath("~/SB_Files/");

        // TODO: DO NOT LOAD THIS INTO MEMORY.
        // This string could be really big. We should probably be distributing this
        // from a CDN. However, we're not saving them to a CDN, so we'll have to
        // figure that out first.
        internal static string GetDocumentHtml(string username, string repository, string path)
        {
            var fullPath = Path.Combine(StaticHtmlAbsolutePath, username, repository, path);

            using(var sr = new StreamReader(fullPath))
            {
                var rawHtml = sr.ReadToEnd();
                return rawHtml;
            }
        }

        internal static JObject GetMetaData(string username, string repository, string path)
        {
            var fullPath = Path.Combine(StaticHtmlAbsolutePath, username, repository, path);
            var metadataPath = fullPath + ".json";

            using(var sr = new StreamReader(metadataPath))
            {
                var metadata = JObject.Parse(sr.ReadToEnd());
                return metadata;
            }
        }

        /// <summary>
        /// Returns a list of all Github users on file.
        /// </summary>
        /// <returns>
        /// All Github users.
        /// </returns>
        public static List<GithubUserStructure> GetAllGithubUsers()
        {
            // If, for some reason, we have no data at all, just return an empty list
            if (!Directory.Exists(StaticHtmlAbsolutePath))
            {
                return new List<GithubUserStructure>();
            }

            // Otherwise, find them all
            var directories = Directory.GetDirectories(StaticHtmlAbsolutePath);
            var users = new List<GithubUserStructure>(directories.Length);
            foreach (var directoryName in directories)
            {
                var userName = Path.GetFileName(directoryName);
                users.Add(GetUserStructure(userName));
            }
            return users;
        }

        /// <summary>
        /// Returns a structure containing information on user's github repositories available at Source Browser.
        /// If the structure does not exist, creates it.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        internal static GithubUserStructure GetUserStructure(string userName)
        {
            var userDataFile = Path.Combine(StaticHtmlAbsolutePath, userName, "user.data");
            if (!File.Exists(userDataFile))
            {
                var userData = SetUpUserStructure(userName);
                FileUtilities.SerializeData(userData, userDataFile);
                return userData;
            }
            try
            {
                return FileUtilities.DeserializeData<GithubUserStructure>(userDataFile);
            }
            catch
            {
                // There was some problem. Recreate the data.
                var userData = SetUpUserStructure(userName);
                FileUtilities.SerializeData(userData, userDataFile);
                return userData;
            }
            finally
            {
                // if user.data file is stale, asynchronously update it.
                if (!FileUtilities.FileIsFresh(userDataFile))
                {
                    Task.Run(() =>
                    {
                        var updatedData = SetUpUserStructure(userName);
                        FileUtilities.SerializeData(updatedData, userDataFile);
                    });
                }
            }
        }
    
        /// <summary>
        /// Creates a structure containing information on user's github repositories available at Source Browser.
        /// </summary>
        /// <param name="userName">The username.</param>
        /// <returns>The github structure.</returns>
        internal static GithubUserStructure SetUpUserStructure(string userName)
        {
            var repoPath = Path.Combine(StaticHtmlAbsolutePath, userName);

            List<GithubRepoStructure> repos;
            if (Directory.Exists(repoPath))
            {
                var directories = Directory.GetDirectories(repoPath);
                repos = new List<GithubRepoStructure>(directories.Length);
                foreach (var directory in directories)
                {
                    var repoName = Path.GetFileName(directory);
                    repos.Add(GetRepoStructure(userName, repoName));
                }
            }
            else
            {
                // If, for some reason the directory doesn't exist, just supply an empty list
                repos = new List<GithubRepoStructure>();
            }

            var userData = new GithubUserStructure()
            {
                Username = userName,
                Repos = repos,
                Path = repoPath
            };
            //userData.UseLiveData();

            return userData;
        }

        /// <summary>
        /// Returns a structure containing information on a repository available at Source Browser.
        /// If the structure does not exist, creates it.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="repoName"></param>
        /// <returns></returns>
        internal static GithubRepoStructure GetRepoStructure(string userName, string repoName)
        {
            var repoDataFile = Path.Combine(StaticHtmlAbsolutePath, userName, repoName, "repo.data");
            if (!File.Exists(repoDataFile))
            {
                var repoData = SetUpRepoStructure(userName, repoName);
                FileUtilities.SerializeData(repoData, repoDataFile);
                return repoData;
            }
            try
            {
                return FileUtilities.DeserializeData<GithubRepoStructure>(repoDataFile);
            }
            catch
            {
                // There was some problem. Recreate the data.
                var repoData = SetUpRepoStructure(userName, repoName);
                FileUtilities.SerializeData(repoData, repoDataFile);
                return repoData;
            }
            finally
            {
                // if repo.data file is stale, asynchronously update it.
                if (!FileUtilities.FileIsFresh(repoDataFile))
                {
                    Task.Run(() =>
                    {
                        var updatedData = SetUpRepoStructure(userName, repoName);
                        FileUtilities.SerializeData(updatedData, repoDataFile);
                    });
                }
            }
        }

        /// <summary>
        /// Creates a structure containing information on a repository available at Source Browser.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="repoName"></param>
        /// <returns></returns>
        internal static GithubRepoStructure SetUpRepoStructure(string userName, string repoName)
        {
            var solutionPath = Path.Combine(StaticHtmlAbsolutePath, userName, repoName);

            List<string> solutionNames;
            if (Directory.Exists(solutionPath))
            {
                var directories = Directory.GetDirectories(solutionPath);
                solutionNames = new List<string>(directories.Length);
                solutionNames.AddRange(directories.Select(Path.GetFileName));
            }
            else
            {
                // If, for some reason the directory doesn't exist, just supply an empty list
                solutionNames = new List<string>();
            }

            var repoData = new GithubRepoStructure()
            {
                Name = repoName,
                Solutions = solutionNames,
                ParentUserName = userName
            };
            //repoData.UseLiveData();

            return repoData;
        }

        internal static GithubSolutionStructure SetUpSolutionStructure(string userName, string repoName, string solutionName)
        {
    var viewModel = new GithubSolutionStructure()
            {
                Name = solutionName,
                RelativePath = CreatePath(userName, repoName, solutionName),
                RelativeRootPath = CreatePath(userName, repoName, solutionName),
                ParentRepo = SetUpRepoStructure(userName, repoName)
            };

            return viewModel;
        }

        internal static GithubFileStructure SetUpFileStructure(string userName, string repoName, string path, string html, int numLines)
        {
            var viewModel = new GithubFileStructure
            {
                FileName = Path.GetFileName(path),
                Directory = GetRelativeDirectory(path),
                RelativePath = CreatePath(userName, repoName, GetRelativeDirectory(path)), // Used to expand nodes leading to this file
                RelativeRootPath = CreatePath(userName, repoName, path), // Points to the root of the treeview
                SourceCode = html,
                NumberOfLines = numLines
            };

            return viewModel;
        }

        public static string GetRelativeDirectory(string path)
        {
            var fileName = Path.GetFileName(path);
            if (fileName != null)
            {
                var baseDirectory = path.Substring(0, path.Length - fileName.Length);
                return baseDirectory;
            }
            return null;
        }

        public static List<string> FindFiles(string path)
        {
            var fullPath = Path.Combine(StaticHtmlAbsolutePath, path);
            var files = new List<string>();
            if (!Directory.Exists(fullPath))
            {
                return files;
            }

            var filePaths = Directory.GetFiles(fullPath);
            files = new List<string>(filePaths);
            files = files.Where(n => n != "solutionInfo.json").ToList();
            return files;
        }

        internal static List<string> FindFolders(string path)
        {
            var fullPath = Path.Combine(StaticHtmlAbsolutePath, path);
            var directories = new List<string>();
            if (!Directory.Exists(fullPath))
            {
                return directories;
            }

            var directoryPaths = Directory.GetDirectories(fullPath);
            directories = new List<string>(directoryPaths);
            return directories;
        }


                                                                                                                        

        private static string CreatePath(string part1, string part2 = null, string part3 = null, string part4 = null)
        {
            string[] pathParts;
            if (part4 != null)
            {
                pathParts = new[] { part1, part2, part3, part4 };
            }
            else if (part3 != null)
            {
                pathParts = new[] { part1, part2, part3 };
            }
            else if (part2 != null)
            {
                pathParts = new[] { part1, part2 };
            }
            else
            {
                pathParts = new[] { part1 };
            }

            return string.Join("/", pathParts);
        }
    }
}