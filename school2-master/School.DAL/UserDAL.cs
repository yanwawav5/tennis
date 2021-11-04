using school.Model;
using school.IDAL;
using school.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using school.Model.BO;
using System.Data.SqlClient;

namespace school.DAL
{
    public class UserDAL : BaseDAL, IUserDAL
    {
        //public SqlItem SqlItemCreateUser(UserSyncBo modelBo)
        //{
        //    string strSql = @"INSERT INTO [dbo].[M_User] ([Id], [CreateUserId], [CreateTime], [Deleted], [Sort], [UserName], [FullName], [Email], [Enabled]) VALUES (@Id, @CreateUserId, @CreateTime, @Deleted, @Sort, @UserName, @FullName, @Email, @Enabled)";
        //    SqlParameter[] par =
        //    {
        //        new SqlParameter("@Id", modelBo.Id),
        //        new SqlParameter("@CreateTime", DateTime.Now),
        //        new SqlParameter("@CreateUserId", modelBo.UserId),
        //        new SqlParameter("@Deleted", modelBo.Deleted),
        //        new SqlParameter("@Sort", modelBo.Sort),
        //        new SqlParameter("@UserName", modelBo.UserName),
        //        new SqlParameter("@FullName", modelBo.FullName),
        //        new SqlParameter("@Email", modelBo.Email),
        //        new SqlParameter("@Enabled", modelBo.Enabled),
        //    };
        //    SqlItem sqlItem = new SqlItem
        //    {
        //        SqlValue = strSql,
        //        Params = par.ToArray()
        //    };
        //    return sqlItem;
        //}

        //public SqlItem SqlItemUpdateUser(UserSyncBo modelBo)
        //{
        //    string strSql = @"UPDATE [dbo].[M_User] SET [UpdateTime] =@UpdateTime, [UpdateUserId] =@UpdateUserId, [Deleted] =@Deleted, [UserName] =@UserName, [FullName] =@FullName, [Email] =@Email, [Enabled] =@Enabled  where [Id] =@Id";
        //    SqlParameter[] par =
        //    {
        //        new SqlParameter("@Id", modelBo.Id),
        //        new SqlParameter("@UpdateTime",DateTime.Now.ToString()),
        //        new SqlParameter("@UpdateUserId",modelBo.UserId),
        //        new SqlParameter("@Deleted", modelBo.Deleted),
        //        new SqlParameter("@UserName", modelBo.UserName),
        //        new SqlParameter("@FullName", modelBo.FullName),
        //        new SqlParameter("@Email", modelBo.Email),
        //        new SqlParameter("@Enabled", modelBo.Enabled),
        //    };
        //    SqlItem sqlItem = new SqlItem
        //    {
        //        SqlValue = strSql,
        //        Params = par.ToArray()
        //    };
        //    return sqlItem;
        //}

    }
}
