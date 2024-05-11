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

        public bool LoginUser(LoginModels loginModels)
        {
            var resultprepare = _session.Prepare("SELECT * FROM my_keyspace.users WHERE email = ? AND password = ? ALLOW FILTERING");

            var boundStatement = resultprepare.Bind(loginModels.email, loginModels.password);

            var rs = _session.Execute(boundStatement);

            var row = rs.FirstOrDefault();

            if (row != null)
            {
                return true;
            }
            else
                return false;


        }

        public bool UserAdd(UserModel userModel)
        {
            var selectStatement = new SimpleStatement("SELECT * FROM my_keyspace.users WHERE username = ? ALLOW FILTERING");

            selectStatement.Bind(userModel.Name);

            var rs = _session.Execute(selectStatement);

            var row = rs.FirstOrDefault();

            if (row != null)
                return false;


            _session.Execute($@"insert into my_keyspace.users  (id, username, password , age , email , address)
                              values ({Guid.NewGuid()}, '{userModel.Name}', '{userModel.password}',
                              {userModel.Age}, '{userModel.email}' , '{userModel.address}')");

            return true;
        }

        public bool UserDelete(UserDeleteModel userDeleteModel)
        {

            var selectStatement = new SimpleStatement("SELECT * FROM my_keyspace.users WHERE username = ? ALLOW FILTERING");


            selectStatement.Bind(userDeleteModel.name);

            var resultSet= _session.Execute(selectStatement);

            var datas = resultSet.FirstOrDefault();

            if(datas is null)
            {
                return false;
            }

           Guid deletedUserId= datas.GetValue<Guid>("id");


            var deleteStatement = new SimpleStatement("DELETE FROM my_keyspace.users WHERE id = ?");

            deleteStatement.Bind(deletedUserId);

            _session.Execute(deleteStatement);
            return true;
        }

        public bool UserUpdate(UserUpdateModels userUpdateModels)
        {
            var selectStatement = new SimpleStatement("SELECT * FROM my_keyspace.users WHERE username = ? ALLOW FILTERING");


            selectStatement.Bind(userUpdateModels.Name);

            var resultSet = _session.Execute(selectStatement);

            Row? data= resultSet.FirstOrDefault();

            if (data is null)
                return false;
            Guid deletedUserId = data.GetValue<Guid>("id");

            var updateStatement = new SimpleStatement("UPDATE my_keyspace.users SET age = ? WHERE id = ?");

            updateStatement.Bind(Convert.ToInt32(userUpdateModels.Age), deletedUserId);

            _session.Execute(updateStatement);
            return true;
        }
    }
}

