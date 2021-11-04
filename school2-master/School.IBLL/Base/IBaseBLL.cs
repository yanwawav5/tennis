using school.Model.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using school.Model.DAO;

namespace school.IBLL.Base
{
    public interface IBaseBLL : IDependency
    {
        /// <summary>
        /// 创建实体
        /// </summary>
        int Create<T>(T model);
        /// <summary>
        /// 批量添加
        /// </summary>
        int Create<T>(IEnumerable<T> models);
        /// <summary>
        /// 单个更新
        /// </summary>
        int Update<T>(T model);
        /// <summary>
        /// 单个删除
        /// </summary>
        int Delete<T>(T model);
        /// <summary>
        /// 批量删除
        /// </summary>
        int Delete<T>(IEnumerable<T> models);
        /// <summary>
        /// 行数
        /// </summary>
        int Count<T>(Expression<Func<T, bool>> lambda) where T : class, new();

        /// <summary>
        /// 加载数据(不分页)
        /// </summary>
        List<T> LoadEntities<T>(Expression<Func<T, bool>> lambda, bool isReference = true, Expression<Func<T, object>> include = null);
        /// <summary>
        /// 获取一个实体(如果没有就新建)
        /// </summary>
        T LoadFirstOrDefault<T>(Expression<Func<T, bool>> lambda) where T : class, new();
        /// <summary>
        /// 获取一个实体
        /// </summary>
        T LoadFirst<T>(Expression<Func<T, bool>> lambda) where T : class, new();
        /// <summary>
        /// 执行查询
        /// </summary>
        TKey Scalar<T, TKey>(Expression<Func<T, object>> field, Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 按sql查询
        /// </summary>
        IEnumerable<dynamic> Query(string sqlStr, object param);
        /// <summary>
        /// 按sql查询
        /// </summary>
        IEnumerable<T> Query<T>(string sqlStr, object param) where T : class, new();
        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        List<T> LoadPageEntities<T>(int skipSize, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, object>> orderByLambda, bool isReference = true, Expression<Func<T, object>> include = null) where T : class, new();
        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        List<T> LoadPageEntities<T, Q>(int skipSize, int pageSize, out int total, Expression<Func<T, Q, bool>> whereLambda, bool isAsc, Expression<Func<T, object>> orderByLambda, bool isReference = true, Expression<Func<T, object>> include = null)
            where T : class, new()
            where Q : class, new();
        /// <summary>
        /// SQL执行
        /// </summary>
        int ExecuteSqlTran(params SqlItem[] sqlItems);
        /// <summary>
        /// 根据SQL查询
        /// </summary>
        DataSet ExecuteDataSet(string sql, SqlParameter[] parameters);
        /// <summary>
        /// 执行查询返回DataTable
        /// </summary>
        DataTable ExecuteDataTable(SqlItem sqlItem);
        /// <summary>
        /// 根据SQL查询
        /// </summary>
        DataSet ExecuteDataSet(SqlItem sqlItem);
        /// <summary>
        ///实体集合 转 DataTable
        /// </summary>
        DataTable EntitiesToDataTable<Q>(List<Q> modelList) where Q : class, new();
        /// <summary>
        /// DataTable 转 实体集合
        /// </summary>
        List<Q> DataTableToEntities<Q>(DataTable dataTable, bool isStringproperty = false) where Q : class, new();
        /// <summary>
        /// sql批量执行
        /// </summary>
        int ExcuteSqlTranByBatch(params SqlItem[] sqlItems);
        /// <summary>
        /// 获取插入语句
        /// </summary>
        SqlItem InsertSqlItem<Q>(Q t, string tableName = "") where Q : class, new();
        /// <summary>
        /// 获取更新语句
        /// </summary>
        SqlItem UpdateSqlItem(string tableName, BaseCore baseCore);
        /// <summary>
        /// 获取分片的数据库名称
        /// </summary>
        string GetTableName(string tableName, int value);
         
    }
}
