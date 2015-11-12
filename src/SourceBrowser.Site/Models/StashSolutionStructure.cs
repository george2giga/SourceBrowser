﻿namespace SourceBrowser.Site.Models
{
    using System;

    using Newtonsoft.Json.Linq;

    public class StashSolutionStructure
    {
        public string Name { get; set; }

        public string RelativePath { get; set; }

        public string RelativeRootPath { get; set; }

        public StashRepoStructure ParentRepo { get; set; }

        public override string ToString()
        {
            return String.Format("'{0}' from {1}'s repo {2}", Name, ParentRepo.ParentUserName, ParentRepo.Name);
        }
    }
}