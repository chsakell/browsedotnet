using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Domain
{
    public class SolutionType
    {
        public SolutionType()
        {
            Solutions = new HashSet<Solution>();
        }

        public int ID { get; set; }
        public string Type { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<Solution> Solutions { get; set; }
    }
}
