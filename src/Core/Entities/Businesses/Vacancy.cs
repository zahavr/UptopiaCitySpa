namespace Core.Entities
{
	public class Vacancy : BaseEntity
    {
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Salary { get; set; }
		public int BusinessesId { get; set; }
		public virtual Business Businesses{ get; set; }
	}
}
