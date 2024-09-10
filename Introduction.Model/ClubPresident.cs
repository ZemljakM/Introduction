namespace Introduction.Model
{
    public class ClubPresident
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public List<Club>? Clubs { get; set; } = new List<Club>();

    }
}
