namespace SourceBrowser.Site.Models
{
    using SourceBrowser.SolutionRetriever;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class GitRepoStructure
    {
        public string Name { get; set; }

        public string ParentUserName { get; set; }

        public DateTime UploadTime { get; set; } // todo: populate.

        public override string ToString()
        {
            return string.Format(
                "{0}'s repository {1}",
                ParentUserName,
                Name);
        }
    }
}