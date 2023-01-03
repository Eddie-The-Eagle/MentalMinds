using SQLite;

namespace MobileAppCasus
{
	public class Level
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }

		[MaxLength(25)]

		public string name { get; set; }

		public string description { get; set; }

		public int number { get; set; }
	}
}