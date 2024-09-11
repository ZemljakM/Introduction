using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Common
{
    public class ClubFilter
    {
        public string Name {  get; set; }
        
        public string Sport { get; set; }

        public DateOnly? DateFrom { get; set; }

        public DateOnly? DateTo { get; set; }

        public int MembersFrom { get; set; }

        public int MembersTo { get; set; }

        public string President { get; set; }

    }
}
