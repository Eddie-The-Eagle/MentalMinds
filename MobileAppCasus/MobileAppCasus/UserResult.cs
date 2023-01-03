using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MobileAppCasus
{
	class UserResult
	{
		[AutoIncrement,PrimaryKey]
		public int id { get; set; }
		public string levelName { get; set; }
		public int levelId { get; set; }
		public int userId { get; set; }
		public int score { get; set; }
	}
}
