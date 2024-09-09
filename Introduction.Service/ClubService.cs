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

        public async Task<bool> DeleteClubAsync(Guid id)
        {
            ClubRepository repository = new();
            return await repository.DeleteClubAsync(id);
        }

        public async Task<bool> InsertClubAsync(Club club)
        {
            ClubRepository repository = new();
            return await repository.InsertClubAsync(club);
        }

        public async Task<bool> UpdateClubAsync(Guid id, ClubUpdate club)
        {
            ClubRepository repository = new();
            var currentClub = await repository.GetClubByIdAsync(id);
            if (currentClub == null)
            {
                return false;
            }
            return await repository.UpdateClubAsync(id, club);
        }

        public async Task<Club> GetClubByIdAsync(Guid id)
        {
            ClubRepository repository = new();
            var club = await repository.GetClubByIdAsync(id);
            if (club == null)
            {
                return null;
            }
            return club;
        }

        public async Task<List<Club>> GetAllClubsAsync()
        {
            ClubRepository repository = new();
            var clubs = await repository.GetAllClubsAsync();
            if (clubs is null)
            {
                return null;
            }
            return clubs;
        }

    }
}
