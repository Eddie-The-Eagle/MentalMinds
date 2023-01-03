using SQLite;

namespace MobileAppCasus
{
	class Answer
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }

		[MaxLength(25)]
		public string answer { get; set; }

		public int score { get; set; }

		public int questionId { get; set; }
	}
}
