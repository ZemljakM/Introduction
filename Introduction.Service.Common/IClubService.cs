using Introduction.Common;
using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service.Common
{
    public interface IClubService
    {
        public Task<bool> DeleteClubAsync(Guid id);

        public Task<bool> InsertClubAsync(Club club);

        public Task<bool> UpdateClubAsync(Guid id, Club club);

        public Task<Club> GetClubByIdAsync(Guid id);

        public Task<List<Club>> GetAllClubsAsync(Sorting sorting, Paging paging, ClubFilter filter);
    }
}
