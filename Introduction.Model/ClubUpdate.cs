namespace Introduction.Model
{
    public class ClubUpdate
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public string? Sport { get; set; }

        public DateOnly? DateOfEstablishment { get; set; }

        public int? NumberOfMembers { get; set; }

        public ClubPresident? ClubPresident { get; set; }

        public Guid? ClubPresidentId { get; set; }
    }
}
