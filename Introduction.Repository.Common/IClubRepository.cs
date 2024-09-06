using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Repository.Common
{
    public interface IClubRepository
    {
        public bool DeleteClub(Guid id);

        public bool InsertClub(Club club);

        public bool UpdateClub(Guid id, ClubUpdate club);

        public List<Club> GetAllClubs();

        public Club GetClubById(Guid id);
    }
}
