using System;
namespace CassandraWebExam.Models
{
	public interface IDbContext
	{
		bool LoginUser(LoginModels loginModels);
		List<GetUsersModels> UserList();
		bool UserAdd(UserModel userModel);
		bool UserDelete(UserDeleteModel userDeleteModel);
		bool UserUpdate(UserUpdateModels userUpdateModels);
	}
}

