using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Domain
{
    public class Snippet
    {
        public Snippet()
        {
            Keys = new HashSet<SearchKey>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Website { get; set; }
        public int ProgrammingLanguageID { get; set; }
        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }
        public virtual ICollection<SearchKey> Keys { get; set; }
    }
}
