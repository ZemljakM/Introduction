using Introduction.Model;

namespace Introduction.WebAPI.Models
{
    public class GetClubPresident
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<GetClub>? Clubs { get; set; } = new List<GetClub>();
    }
}
