using System;
namespace CassandraWebExam.Models
{
	public class UsersRequestModels
	{
		public Guid id { get; set; }
		public string? name { get; set; }
		public int age { get; set; }
		public string? password { get; set; }
		public string? email { get; set; }
		public string? address { get; set; }

	}
}

