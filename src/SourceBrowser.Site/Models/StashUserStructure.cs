﻿namespace SourceBrowser.Site.Models
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using SolutionRetriever;

    [Serializable]
    public class StashUserStructure
    {
        public string Username;
        public string Path;
        public IList<StashRepoStructure> Repos;

        public string FullName;
        public string AvatarURL;
        public string StashURL;

        public void UseLiveData()
        {
            //try
            //{
            //    GitHubInformationRetriever.GetUserInformation(Username, ref FullName, ref AvatarURL, ref GitHubURL, ref BlogURL);
            //}
            //catch (Exception ex)
            //{
            //    // Swallow. This information is not essential to operation of SourceBrowser.
            //}
        }

        public override string ToString()
        {
            return String.Format("User {0} with {1} repo{2}", Username, Repos.Count, Repos.Count == 1 ? "" : "s");
        }
    }
}