using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Repository.Common
{
    public interface IClubPresidentRepository
    {
        public bool InsertClubPresident(ClubPresident clubPresident);
    }
}
