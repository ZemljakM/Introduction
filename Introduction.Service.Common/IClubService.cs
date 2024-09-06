using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service.Common
{
    public interface IClubService
    {
        public bool DeleteClub(Guid id);

        public bool InsertClub(Club club);

        public bool UpdateClub(Guid id, ClubUpdate club);

        public Club GetClubById(Guid id);

        public List<Club> GetAllClubs();
    }
}
