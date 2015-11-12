﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBrowser.Generator.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkspaceModel : IProjectItem
    {
        public ICollection<IProjectItem> Children { get; set; }

        public string Name { get; set; }

        public string BasePath { get; set; }

        public string RelativePath { get; set; }

        //The WorkspaceModel has no parent. It is the top level item.
        public IProjectItem Parent
        {
            get
            {
                return null;
            }
        }

        public WorkspaceModel(string name, string basePath)
        {
            Name = name;
            BasePath = basePath;
            RelativePath = "";
            Children = new List<IProjectItem>();
        }
    }
}
