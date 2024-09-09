using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class ClubPresidentService : IClubPresidentService
    {
        public async Task<bool> InsertClubPresidentAsync(ClubPresident clubPresident)
        {
            ClubPresidentRepository repository = new();
            return await repository.InsertClubPresidentAsync(clubPresident);
        }

    }
}
