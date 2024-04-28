using System;
namespace CassandraWebExam.Models
{
	public class GetUsersModels
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public string password { get; set; }
		public string email { get; set; }
		public string address { get; set; }
	}
}

