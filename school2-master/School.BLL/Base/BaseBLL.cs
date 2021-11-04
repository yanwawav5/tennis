
using school.IBLL.Base;
using school.IDAL.Base;
using school.Model.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//using school.DAL.IOC;
using ServiceStack.OrmLite;
using school.Model.TO;

namespace school.BLL.Base
{
    public class BaseBLL<Q>:IBaseBLL where Q:IBaseDAL
    {
        public Q mServiceDAL { get; set; }
        /// <summary>
        /// API请求Head携带信息
        /// </summary>
        protected ApiHeader mHeader { get; set; }
        public BaseBLL()
        {
            //mServiceDAL = DALRegister.Instance.GetObject<Q>();
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        public int Create<T>(T model)
        {
            return mServiceDAL.Create(model);
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        public int Create<T>(IEnumerable<T> models)
        {
            return mServiceDAL.Create(models);
        }
        /// <summary>
        /// 单个更新
        /// </summary>
        public int Update<T>(T model)
        {
            return mServiceDAL.Update(model);
        }
        /// <summary>
        /// 单个删除
        /// </summary>
        public int Delete<T>(T model)
        {
            return mServiceDAL.Delete(model);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        public int Delete<T>(IEnumerable<T> models)
        {
            return mServiceDAL.Delete(models);
        }
        /// <summary>
        /// 行数
        /// </summary>
        public int Count<T>(Expression<Func<T, bool>> lambda) where T : class, new()
        {
            return mServiceDAL.Count(lambda);
        }

        /// <summary>
        /// 加载数据(不分页)
        /// </summary>
        public List<T> LoadEntities<T>(Expression<Func<T, bool>> lambda, bool isReference = true, Expression<Func<T, object>> include = null)
        {
            return mServiceDAL.LoadEntities(lambda, isReference, include);
        }
        /// <summary>
        /// 获取一个实体(如果没有就新建)
        /// </summary>
        public T LoadFirstOrDefault<T>(Expression<Func<T, bool>> lambda) where T : class, new()
        {
            return mServiceDAL.LoadFirstOrDefault(lambda);
        }
        /// <summary>
        /// 获取一个实体
        /// </summary>
        public T LoadFirst<T>(Expression<Func<T, bool>> lambda) where T : class, new()
        {
            return mServiceDAL.LoadFirst(lambda);
        }
        /// <summary>
        /// 执行查询
        /// </summary>
        public TKey Scalar<T, TKey>(Expression<Func<T, object>> field, Expression<Func<T, bool>> predicate)
        {
            return mServiceDAL.Scalar<T, TKey>(field, predicate);
        }
        /// <summary>
        /// 按sql查询
        /// </summary>
        public IEnumerable<dynamic> Query(string sqlStr, object param)
        {
            return mServiceDAL.Query(sqlStr, param);
        }
        /// <summary>
        /// 按sql查询
        /// </summary>
        public IEnumerable<T> Query<T>(string sqlStr, object param) where T : class, new()
        {
            return mServiceDAL.Query<T>(sqlStr, param);
        }
        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        public List<T> LoadPageEntities<T>(int skipSize, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, object>> orderByLambda, bool isReference = true, Expression<Func<T, object>> include = null) where T : class, new()
        {
            return mServiceDAL.LoadPageEntities(skipSize, pageSize,out total, whereLambda, isAsc, orderByLambda, isReference, include);
        }
        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        public List<T> LoadPageEntities<T, Q>(int skipSize, int pageSize, out int total, Expression<Func<T, Q, bool>> whereLambda, bool isAsc, Expression<Func<T, object>> orderByLambda, bool isReference = true, Expression<Func<T, object>> include = null)
            where T : class, new()
            where Q : class, new()
        {
            return mServiceDAL.LoadPageEntities(skipSize, pageSize, out total, whereLambda, isAsc, orderByLambda, isReference, include);
        }
        /// <summary>
        /// SQL执行
        /// </summary>
        public int ExecuteSqlTran(params SqlItem[] sqlItems)
        {
            var amount = sqlItems.Where(item => item != null).Count(item => !string.IsNullOrEmpty(item.SqlValue));
            if (amount == 0)
                return 1;
            return mServiceDAL.ExecuteSqlTran(sqlItems);
        }
        /// <summary>
        /// 根据SQL查询
        /// </summary>
        public DataSet ExecuteDataSet(string sql, SqlParameter[] parameters)
        {
            return mServiceDAL.ExecuteDataSet(sql, parameters);
        }
        /// <summary>
        /// 执行查询返回DataTable
        /// </summary>
        public DataTable ExecuteDataTable(SqlItem sqlItem)
        {
            return mServiceDAL.ExecuteDataTable(sqlItem);
        }
        /// <summary>
        /// 根据SQL查询
        /// </summary>
        public DataSet ExecuteDataSet(SqlItem sqlItem)
        {
            return mServiceDAL.ExecuteDataSet(sqlItem);
        }
        /// <summary>
        ///实体集合 转 DataTable
        /// </summary>
        public DataTable EntitiesToDataTable<T>(List<T> modelList) where T : class, new()
        {
            return mServiceDAL.EntitiesToDataTable(modelList);
        }
        /// <summary>
        /// DataTable 转 实体集合
        /// </summary>
        public List<T> DataTableToEntities<T>(DataTable dataTable, bool isStringproperty = false) where T : class, new()
        {
            return mServiceDAL.DataTableToEntities<T>(dataTable, isStringproperty);
        }
        /// <summary>
        /// sql批量执行
        /// </summary>
        public int ExcuteSqlTranByBatch(params SqlItem[] sqlItems)
        {
            return mServiceDAL.ExcuteSqlTranByBatch(sqlItems);
        }
        /// <summary>
        /// 获取插入语句
        /// </summary>
        public SqlItem InsertSqlItem<T>(T t, string tableName = "") where T : class, new()
        {
            return mServiceDAL.InsertSqlItem(t, tableName);
        }
        /// <summary>
        /// 获取更新语句
        /// </summary>
        public SqlItem UpdateSqlItem(string tableName, BaseCore baseCore)
        {
            return mServiceDAL.UpdateSqlItem(tableName, baseCore);
        }
        /// <summary>
        /// 获取分片的数据库名称
        /// </summary>
        public string GetTableName(string tableName, int value)
        {
            return mServiceDAL.GetTableName(tableName, value);
        }

        #region 构建表达式树
        /// <summary>
        /// 构建表达式树
        /// </summary>
        public Expression<Func<T, bool>> BuildExpression<T>(params Expression<Func<T, bool>>[] lambdas)
        {
            Expression<Func<T, bool>> whereLambda = PredicateBuilder.True<T>();
            foreach (var item in lambdas)
            {
                whereLambda = whereLambda.And(item);
            }
            return whereLambda;
        }

        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<T, bool>> BuildLambda<T>(object value, Expression<Func<T, bool>> lambda)
        {
            if (value == null || "".Equals(value)||value.ToString().ToLower()=="false")
            {
                return (T p) => true;
            }
            return lambda;
        }

       
        #endregion

        public AmountBo ReturnBo(int amount)
        {
            if (amount > 0)
                return new AmountBo { IsSuccess = true, Amount = amount };
            return new AmountBo { IsSuccess = false, Amount = amount };
        }

        //public AmountLogoutBo ReturnLogoutBo(int amount, bool needLogout)
        //{
        //    if (amount > 0)
        //        return new AmountLogoutBo { IsSuccess = true, Amount = amount, NeedLogout = needLogout };
        //    return new AmountLogoutBo { IsSuccess = false, Amount = amount, NeedLogout = needLogout };
        //} 

    }
}
