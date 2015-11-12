using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBrowser.Search.ViewModels
{
    public struct TokenViewModel
    {
        public string Id { get; set; }

        public string Path { get; set; }
        public string Username { get; set; }
        public string Repository { get; set; }
        public string FullyQualifiedName { get; set; }
        public string DisplayName
        {
            get
            {
                if (FullyQualifiedName == null)
                    return null;
                string currentName = FullyQualifiedName;
                var parensIndex = FullyQualifiedName.IndexOf("(");
                if(parensIndex != -1)
                {
                    currentName = currentName.Remove(parensIndex, currentName.Length - parensIndex);
                }

                var splitName = currentName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                return splitName.Last();
            }
        }
        public int LineNumber { get; set; }

        public TokenViewModel(string username, string repository, string path, string fullName, int lineNumber):this()
        {
            Id = username + "/" + repository + "/" + fullName;
            Username = username;
            Repository = repository;
            Path = path;
            FullyQualifiedName = fullName;
            LineNumber = lineNumber;
        }
    }
}
