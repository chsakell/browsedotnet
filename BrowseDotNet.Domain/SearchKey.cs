using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Domain
{
    public class SearchKey
    {
        public SearchKey()
        {
            Solutions = new HashSet<Solution>();
            Snippets = new HashSet<Snippet>();
        }

        public int ID { get; set; }
        public string Term { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Solution> Solutions { get; set; }
        public virtual ICollection<Snippet> Snippets { get; set; }
    }
}
