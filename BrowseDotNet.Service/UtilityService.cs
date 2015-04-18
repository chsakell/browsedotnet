using BrowseDotNet.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Service
{
    public class UtilityService : IUtilityService
    {
        private ISearchKeyService _searchKeyService;

        public UtilityService(ISearchKeyService searchKeyService)
        {
            _searchKeyService = searchKeyService;
        }

        public ICollection<SearchKey> ResolveKeys(string keys)
        {
            ICollection<SearchKey> searchKeys = new HashSet<SearchKey>();
            ICollection<SearchKey> currentKeys = new HashSet<SearchKey>();

            List<string> listKeys = keys.Split(',').ToList();
            listKeys = listKeys.ConvertAll(k => k.Trim().ToLower());
            List<string> removeKeys = new List<string>();

            currentKeys = _searchKeyService.GetSearchKeys(s => listKeys.Contains(s.Term.ToLower())).ToList();

            foreach (SearchKey searchKey in currentKeys)
            {
                searchKeys.Add(searchKey);
                removeKeys.Add(searchKey.Term);
            }

            foreach (string key in listKeys)
            {
                if (!removeKeys.Contains(key))
                {
                    searchKeys.Add(new SearchKey()
                    {
                        Term = key.ToLower(),
                        DateCreated = DateTime.Now
                    });
                }
            }

            return searchKeys;
        }

        public void OpenDotNETSolution(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    Process.Start(filePath);
                }
                else
                    throw new Exception("Solution file path " + filePath + " doesn't exists. Update solution's info if moved.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetCommaSeparatedKeys(ICollection<SearchKey> keys)
        {
            string keysResult = string.Empty;

            foreach (SearchKey key in keys)
            {
                keysResult += key.Term + ",";
            }

            return keysResult.Substring(0, keysResult.Length - 1);
        }

        public List<string> ResolveDirectories(string[] directories)
        {
            List<string> _directories = new List<string>();

            try
            {
                foreach (string dir in directories)
                {
                    if (dir.ToLower().EndsWith("packages\\") || dir.ToLower().EndsWith("packages"))
                        continue;
                    else
                        _directories.Add(dir.Substring(6, dir.Length - 6));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _directories;
        }

        public bool HasDirectoryDotNETSolution(string directory, out string solutionFile)
        {
            bool _hasDotNETSolution = new bool();
            solutionFile = string.Empty;

            try
            {
                if (Directory.Exists(directory))
                {
                    solutionFile = Directory.GetFiles(directory).Where(file => file.EndsWith(".sln")).FirstOrDefault();
                    if (solutionFile != null)
                    {
                        solutionFile = Path.GetFileNameWithoutExtension(solutionFile);
                        _hasDotNETSolution = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _hasDotNETSolution = false;
            }

            return _hasDotNETSolution;
        }
    }

    public interface IUtilityService
    {
        ICollection<SearchKey> ResolveKeys(string keys);

        void OpenDotNETSolution(string filePath);

        string GetCommaSeparatedKeys(ICollection<SearchKey> keys);

        List<string> ResolveDirectories(string[] directories);

        bool HasDirectoryDotNETSolution(string directory, out string solutionFile);
    }
}
