using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class ClubPresidentService : IClubPresidentService
    {
        public bool InsertClubPresident(ClubPresident clubPresident)
        {
            ClubPresidentRepository repository = new();
            return repository.InsertClubPresident(clubPresident);
        }

    }
}
