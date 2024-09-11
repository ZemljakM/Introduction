using Introduction.Common;
using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service.Common
{
    public interface IClubPresidentService
    {
        public Task<ClubPresident> GetClubPresidentByIdAsync(Guid id);

        public Task<List<ClubPresident>> GetAllClubPresidentsAsync(Sorting sorting, Paging paging, ClubPresidentFilter filter);

        public Task<bool> InsertClubPresidentAsync(ClubPresident clubPresident);

        public Task<bool> UpdateClubPresidentAsync(Guid id, ClubPresident clubPresident);
    }
}
