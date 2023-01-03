using SQLite;

namespace MobileAppCasus
{
	public class User
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }

		[MaxLength(25)]
		public string name { get; set; }

		public string password { get; set; }

		

		public bool isAdmin { get; set; }

		
	}
}
