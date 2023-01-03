using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MobileAppCasus
{
	public class Result
	{

		[PrimaryKey, AutoIncrement]
		public int id { get; set; }

		public string name { get; set; }

		public string description { get; set; }

		public int minScore { get; set; }

		public int maxScore { get; set; }

		public int levelId { get; set; }

		
	}
}
