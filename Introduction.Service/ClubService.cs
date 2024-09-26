using Introduction.Common;
using Introduction.Model;
using Introduction.Repository;
using Introduction.Repository.Common;
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

        private IClubRepository _repository;

        public ClubService(IClubRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> DeleteClubAsync(Guid id)
        {
            return await _repository.DeleteClubAsync(id);
        }

        public async Task<bool> InsertClubAsync(Club club)
        {
            return await _repository.InsertClubAsync(club);
        }

        public async Task<bool> UpdateClubAsync(Guid id, Club club)
        {
            var currentClub = await _repository.GetClubByIdAsync(id);
            if (currentClub == null)
            {
                return false;
            }
            return await _repository.UpdateClubAsync(id, club);
        }

        public async Task<Club> GetClubByIdAsync(Guid id)
        {
            var club = await _repository.GetClubByIdAsync(id);
            return club;
        }

        public async Task<List<Club>> GetAllClubsAsync(Sorting sorting, Paging paging, ClubFilter filter)
        {
            var clubs = await _repository.GetAllClubsAsync(sorting, paging, filter);
            return clubs;
        }

        public async Task<int> CountClubs(ClubFilter filter)
        {
            return await _repository.CountClubs(filter);
        }

    }
}
