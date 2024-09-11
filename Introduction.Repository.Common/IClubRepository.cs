using Introduction.Common;
using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Repository.Common
{
    public interface IClubRepository
    {
        public Task<bool> DeleteClubAsync(Guid id);

        public Task<bool> InsertClubAsync(Club club);

        public Task<bool> UpdateClubAsync(Guid id, ClubUpdate club);

        public Task<List<Club>> GetAllClubsAsync(Sorting sorting, Paging paging, ClubFilter filter);

        public Task<Club> GetClubByIdAsync(Guid id);
    }
}
