using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service
{
    public class ClubService: IClubService
    {

        public bool DeleteClub(Guid id)
        {
            ClubRepository repository = new();
            return repository.DeleteClub(id);
        }

        public bool InsertClub(Club club)
        {
            ClubRepository repository = new();
            return repository.InsertClub(club);
        }

        public bool UpdateClub(Guid id, ClubUpdate club)
        {
            ClubRepository repository = new();
            var currentClub = repository.GetClubById(id);
            if (currentClub == null)
            {
                return false;
            }
            return repository.UpdateClub(id, club);
        }

        public Club GetClubById(Guid id)
        {
            ClubRepository repository = new();
            var club = repository.GetClubById(id);
            if (club == null)
            {
                return null;
            }
            return club;
        }

        public List<Club> GetAllClubs()
        {
            ClubRepository repository = new();
            var clubs = repository.GetAllClubs();
            if (clubs is null)
            {
                return null;
            }
            return clubs;
        }

    }
}
