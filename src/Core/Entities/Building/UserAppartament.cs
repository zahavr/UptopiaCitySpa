namespace Core.Entities
{
	public class UserAppartament : BaseEntity
    {
		public string UserId { get; set; }
		public int AppartamentId { get; set; }
		public Appartament Appartament { get; set; }
	}
}
