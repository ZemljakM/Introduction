using Introduction.Common;
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
        public Task<List<ClubPresident>> GetAllClubPresidentsAsync(Sorting sorting, Paging paging, ClubPresidentFilter filter);

        public Task<ClubPresident> GetClubPresidentByIdAsync(Guid id);

        public Task<bool> InsertClubPresidentAsync(ClubPresident clubPresident);

        public Task<bool> UpdateClubPresidentAsync(Guid id, ClubPresident clubPresident);
    }
}
