using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Domain
{
    public class Solution
    {
        public Solution()
        {
            Keys = new HashSet<SearchKey>();
        }

        public int ID { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public DateTime DateRegistered { get; set; }
        public Nullable<DateTime> LastTimeOpened { get; set; }
        public int SolutionTypeID { get; set; }
        public virtual SolutionType SolutionType { get; set; }
        public virtual ICollection<SearchKey> Keys { get; set; }
    }
}
