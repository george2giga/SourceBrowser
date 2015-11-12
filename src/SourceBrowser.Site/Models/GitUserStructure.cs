namespace SourceBrowser.Site.Models
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using SolutionRetriever;

    [Serializable]
    public class GitUserStructure
    {
        public string Username;
        public string Path;
        public IList<GitRepoStructure> Repos;

        public string AvatarURL;

       //public override string ToString()
        //{
        //    return String.Format("User {0} with {1} repo{2}", Username, Repos.Count, Repos.Count == 1 ? "" : "s");
        //}
    }
}