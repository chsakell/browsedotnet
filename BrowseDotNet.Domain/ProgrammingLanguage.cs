using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Domain
{
    public class ProgrammingLanguage
    {
        public ProgrammingLanguage()
        {
            Snippets = new HashSet<Snippet>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<Snippet> Snippets { get; set; }
    }
}
