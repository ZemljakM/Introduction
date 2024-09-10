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


        public async Task<bool> InsertClubPresidentAsync(ClubPresident clubPresident)
        {
            return await _repository.InsertClubPresidentAsync(clubPresident);
        }

    }
}
