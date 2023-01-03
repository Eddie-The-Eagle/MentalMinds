using SQLite;

namespace MobileAppCasus
{
	public class Question
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }

		[MaxLength(25)]
		public string question { get; set; }
		
		public int levelId { get; set; }
	}
}
