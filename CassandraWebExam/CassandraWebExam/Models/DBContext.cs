using System;
using Cassandra;

namespace CassandraWebExam.Models
{
	public class DBContext:IDbContext
	{
        private Cassandra.ISession _session;
        public DBContext(Cassandra.ISession session)
        {
            _session = session;
        }


        public List<GetUsersModels> UserList()
        {

           var result= _session.Execute("SELECT * FROM my_keyspace.users");

            List<GetUsersModels> userMailPasswordModels = new();

            foreach (var item in result)
            {
                userMailPasswordModels.Add(new GetUsersModels()
                {
                    email = item.GetValue<string>("email"),
                    Id = item.GetValue<Guid>("id"),
                    Age=item.GetValue<int>("age"),
                    address=item.GetValue<string>("address"),
                    Name=item.GetValue<string>("username")
                });
            }

            return userMailPasswordModels;
        }

        
    }
}

