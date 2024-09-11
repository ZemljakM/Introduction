using Introduction.Common;
using Introduction.Model;
using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class ClubPresidentService : IClubPresidentService
    {
        private IClubPresidentRepository _repository;

        public ClubPresidentService(IClubPresidentRepository repository)
        {
            _repository = repository;
        }


        public async Task<ClubPresident> GetClubPresidentByIdAsync(Guid id)
        {
            return await _repository.GetClubPresidentByIdAsync(id);
             
        }

        public async Task<List<ClubPresident>> GetAllClubPresidentsAsync(Sorting sorting, Paging paging, ClubPresidentFilter filter)
        {
            return  await _repository.GetAllClubPresidentsAsync(sorting, paging, filter);
            
        }


        public async Task<bool> InsertClubPresidentAsync(ClubPresident clubPresident)
        {
            return await _repository.InsertClubPresidentAsync(clubPresident);
        }

        public async Task<bool> UpdateClubPresidentAsync(Guid id, ClubPresident clubPresident)
        {
            var currentPresident = await _repository.GetClubPresidentByIdAsync(id);
            if (currentPresident == null)
            {
                return false;
            }
            return await _repository.UpdateClubPresidentAsync(id, clubPresident);
        }
    }
}
