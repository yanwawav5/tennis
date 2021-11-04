using school.Common;
using school.IDAL.Base;
using school.Model.BO;
using school.Model.Enum;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace school.DAL.Base
{
    public class BaseDAL: IBaseDAL
    {
        #region 属性
        /// <summary>
        /// 事务隔离级别
        /// </summary>
        public IsolationLevel TranIsolationLevel
        {
            get { return Config.IsolationLevel; }
        }
        /// <summary>
        /// 获取当前线程的数据库链接
        /// </summary>
        public string ConnectionString
        {
            get { return Config.ConnectionString; }
        }
        public virtual IDbConnectionFactory DbFactory
        {
            get
            {
                //return new OrmLiteConnectionFactory(ConnectionString, SqlServer2012Dialect.Provider);//2012 驱动
                return new OrmLiteConnectionFactory(ConnectionString, SqlServerDialect.Provider);
            }
        }
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            var conn = DbFactory.OpenDbConnectionString(ConnectionString);
            return conn;
        }
        #endregion

        #region 增删改

        /// <summary>
        /// 创建实体
        /// </summary>
        public int Create<T>(T model)
        {
            using (var db = GetConnection())
            {
                var rowCnt = db.Insert(model);
                db.Close();
                return Convert.ToInt32(rowCnt);
            }
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        public int Create<T>(IEnumerable<T> models)
        {
            using (var db = GetConnection())
            {
                var rowCnt = models.Count();
                db.InsertAll(models);
                db.Close();
                return rowCnt;
            }
        }
        /// <summary>
        /// 单个更新
        /// </summary>
        public int Update<T>(T model)
        {
            using (var db = GetConnection())
            {
                var rowCnt = db.Update(model);
                db.Close();
                return rowCnt;
            }
        }
        /// <summary>
        /// 单个删除
        /// </summary>
        public int Delete<T>(T model)
        {
            using (var db = GetConnection())
            { 
                var rowCnt = db.Delete(model);
                db.Close();
                return rowCnt;
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        public int Delete<T>(IEnumerable<T> models)
        {
            using (var db = GetConnection())
            {
                var rowCnt = db.DeleteAll(models);
                db.Close();
                return rowCnt;
            }
        }

        #endregion

        #region 查询
        /// <summary>
        /// 行数
        /// </summary>
        public int Count<T>(Expression<Func<T, bool>> lambda) where T : class, new()
        {
            using (var db = GetConnection())
            {
                var rowCnt = db.Count(lambda);
                db.Close();
                return Convert.ToInt32(rowCnt);
            }
        }

        /// <summary>
        /// 加载数据(不分页)
        /// </summary>
        public List<T> LoadEntities<T>(Expression<Func<T, bool>> lambda, bool isReference = true, Expression<Func<T, object>> include = null)
        {
            using (var db = GetConnection())
            {
                List<T> entities = null;
                if (isReference && include != null)
                {
                    entities = db.LoadSelect<T>(lambda, include);
                }
                else
                {
                   // var aa = db.GetLastSql();
                    entities = db.Select<T>(lambda);
                }
                db.Close();
                return entities;
            }

        }
        /// <summary>
        /// 获取一个实体(如果没有就新建)
        /// </summary>
        public T LoadFirstOrDefault<T>(Expression<Func<T, bool>> lambda) where T : class, new()
        {
            var entity = LoadFirst(lambda);
            return entity ?? new T();
        }
        /// <summary>
        /// 获取一个实体
        /// </summary>
        public T LoadFirst<T>(Expression<Func<T, bool>> lambda) where T : class, new()
        {
            using (var db = GetConnection())
            {
                var entity = db.Single<T>(lambda);
                db.Close();
                return entity;
            }
        }
        /// <summary>
        /// 执行查询
        /// </summary>
        public TKey Scalar<T, TKey>(Expression<Func<T, object>> field, Expression<Func<T, bool>> predicate)
        {
            using (var db = GetConnection())
            {
                TKey obj = db.Scalar<T, TKey>(field, predicate);
                db.Close();
                return obj;
            }
        }
        /// <summary>
        /// 按sql查询
        /// </summary>
        public IEnumerable<dynamic> Query(string sqlStr, object param)
        {
            using (var db = GetConnection())
            {
                var obj = db.Query(sqlStr, param);
                db.Close();
                return obj;
            }
        }
        /// <summary>
        /// 按sql查询
        /// </summary>
        public IEnumerable<T> Query<T>(string sqlStr, object param) where T : class, new()
        {
            using (var db = GetConnection())
            {
                var obj = db.Query<T>(sqlStr, param);
                db.Close();
                return obj;
            }
        }
        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        public List<T> LoadPageEntities<T>(int skipSize, int pageSize, out int total,Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, object>> orderByLambda, bool isReference = true, Expression<Func<T, object>> include = null) where T : class, new()
        {
            using (var db = GetConnection())
            {
                var count = db.Count(whereLambda);
                total = Convert.ToInt32(count);
                List<T> result = null;
                var sqlExpression = db.From<T>();
                if (isAsc)
                {
                    sqlExpression = sqlExpression.Where(whereLambda).OrderBy(orderByLambda).Limit(skip: skipSize, rows: pageSize);
                }
                else
                {
                    sqlExpression = sqlExpression.Where(whereLambda).OrderByDescending(orderByLambda).Limit(skip: skipSize, rows: pageSize);
                }
                if (isReference && include != null)
                {
                    result = db.LoadSelect<T>(sqlExpression, include);
                }
                else
                {
                    result = db.Select<T>(sqlExpression);
                }
                db.Close();
                return result;
            }
        }
        public List<T> LoadPageEntities<T>(int skipSize, int pageSize, out int total, Expression<Func<T, bool>> whereLambda,  bool isAsc, Expression<Func<T, object>> orderByLambda, Expression<Func<T, object>> TwhereLambda, bool isReference = true, Expression<Func<T, object>> include = null) where T : class, new()
        {
            using (var db = GetConnection())
            {
                var count = db.Count(whereLambda);
                total = Convert.ToInt32(count);
                List<T> result = null;
                var sqlExpression = db.From<T>();
                if (isAsc)
                {
                    sqlExpression = sqlExpression.Where(whereLambda).OrderBy(orderByLambda).ThenBy(TwhereLambda).Limit(skip: skipSize, rows: pageSize);
                }
                else
                {
                    sqlExpression = sqlExpression.Where(whereLambda).OrderByDescending(orderByLambda).ThenByDescending(TwhereLambda).Limit(skip: skipSize, rows: pageSize);
                }
                if (isReference && include != null)
                {
                    result = db.LoadSelect<T>(sqlExpression, include);
                }
                else
                {
                    result = db.Select<T>(sqlExpression);
                }
                db.Close();
                return result;
            }
        }

        ///// <summary>
        ///// 实现对数据的分页查询
        ///// </summary>
        //public List<T> LoadPageEntities<T>(int skipSize, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, bool isAsc,List<Expression<Func<T, object>>> orderByLambda, bool isReference = true, Expression<Func<T, object>> include = null) where T : class, new()
        //{
        //    using (var db = GetConnection())
        //    {
        //        var count = db.Count(whereLambda);
        //        total = Convert.ToInt32(count);
        //        List<T> result = null;
        //        var sqlExpression = db.From<T>();
        //        if (isAsc)
        //        {
        //            sqlExpression = sqlExpression.Where(whereLambda).OrderBy(orderByLambda).Limit(skip: skipSize, rows: pageSize);
        //        }
        //        else
        //        {
        //            sqlExpression = sqlExpression.Where(whereLambda).OrderByDescending(orderByLambda).Limit(skip: skipSize, rows: pageSize);
        //        }
        //        if (isReference && include != null)
        //        {
        //            result = db.LoadSelect<T>(sqlExpression, include);
        //        }
        //        else
        //        {
        //            result = db.Select<T>(sqlExpression);
        //        }
        //        db.Close();
        //        return result;
        //    }
        //}


        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        public List<T> LoadPageEntities<T, Q>(int skipSize, int pageSize, out int total, Expression<Func<T, Q, bool>> whereLambda, bool isAsc, Expression<Func<T, object>> orderByLambda, bool isReference = true, Expression<Func<T, object>> include = null)
            where T : class, new()
            where Q : class, new()
        {
            using (var db = GetConnection())
            {
                List<T> result = null;
                var sqlExpression = db.From<T, Q>(whereLambda);
                var count = db.Count(sqlExpression);
                total = Convert.ToInt32(count);
                if (isAsc)
                {
                    sqlExpression = sqlExpression.Where(whereLambda).OrderBy(orderByLambda).Limit(skip: skipSize, rows: pageSize);
                }
                else
                {
                    sqlExpression = sqlExpression.Where(whereLambda).OrderByDescending(orderByLambda).Limit(skip: skipSize, rows: pageSize);
                }
                if (isReference && include != null)
                {
                    result = db.LoadSelect<T>(sqlExpression, include);
                }
                else
                {
                    result = db.Select<T>(sqlExpression);
                }
                db.Close();
                return result;
            }
        }


        #endregion

        #region 执行SQL

        /// <summary>
        /// SQL执行
        /// </summary>
        public int ExecuteSqlTran(params SqlItem[] sqlItems)
        {
            bool checkNum = true;
            using (SqlConnection myCn = new SqlConnection(ConnectionString))
            {
                if (myCn.State == ConnectionState.Closed)
                {
                    myCn.Open();
                }
                using (SqlTransaction myTrans = myCn.BeginTransaction(Config.IsolationLevel))
                {
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandTimeout = 0;
                    int executeCount = 0;
                    string parStr = "";
                    var execSql = string.Empty;
                    try
                    {
                        myCmd.Connection = myCn;
                        myCmd.Transaction = myTrans;
                        foreach (var item in sqlItems)
                        {
                            if (item?.SqlValue == null)  continue; 
                            if (item.IsBatch)
                            {
                                executeCount += ExecuteSqlBulkCopy(myCn, item.SourseData, item.TableName, myTrans);
                            }
                            else
                            {
                                myCmd.CommandText = item.SqlValue;
                                execSql = item.SqlValue;
                                AddParams(myCmd, item.CustomType, item.Params);
                                int cnt = myCmd.ExecuteNonQuery();
                                if (item.IsCheckCount && cnt == 0)
                                {
                                    foreach (var par in item.Params)
                                    {
                                        parStr += par.ParameterName + ":" + par.Value + " | ";
                                    }
                                    checkNum = false;
                                    throw new schoolException(SubCode.NoAccessControl.GetHashCode()); 
                                }
                                else
                                {
                                    executeCount += cnt;
                                }
                                myCmd.Parameters.Clear();
                            }
                        }
                        myTrans.Commit();
                    }
                    catch (Exception exp)
                    {
                        myTrans.Rollback();
                        Log.Error(MethodBase.GetCurrentMethod().Name, exp);
                        SubCode errorCode = SubCode.UnifiedError;
                        
                    }
                    myCmd.Dispose();
                    myTrans.Dispose();
                    myCn.Dispose();
                    return executeCount;
                }
            }
        }

        /// <summary>
        /// SQL执行事务 
        /// </summary>
        private int ExecuteSqlBulkCopy(SqlConnection conn, DataTable sourceData, String tableName, SqlTransaction sqlTransaction)
        {
            if (sourceData != null && sourceData.Rows.Count > 0)
            {
                int count = sourceData.Rows.Count;
                using (SqlBulkCopy sbc = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, sqlTransaction))
                {   
                    sbc.DestinationTableName = tableName;
                    sbc.BatchSize = 500000;
                    sbc.BulkCopyTimeout = 500000;
                    for (int i = 0; i < sourceData.Columns.Count; i++)
                    {  
                        sbc.ColumnMappings.Add(sourceData.Columns[i].ColumnName, sourceData.Columns[i].ColumnName);
                    }
                    sbc.WriteToServer(sourceData);
                    sourceData.Dispose();
                    return count;
                }
            }
            return 0;

        }

        /// <summary>
        /// 添加参数
        /// </summary>
        private void AddParams(SqlCommand cmd, String customType, params IDataParameter[] sqlParams)
        {
            if (sqlParams != null)
            {
                foreach (SqlParameter parameter in sqlParams)
                {
                    if (!string.IsNullOrEmpty(customType))
                    {
                        parameter.SqlDbType = SqlDbType.Structured;
                        parameter.TypeName = customType;
                    }
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }

                    cmd.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// 根据SQL查询
        /// </summary>
        public DataSet ExecuteDataSet(string sql, SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            using (var conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    conn.Close();
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    throw new Exception("数据查询失败：" + ex.ToString());
                }
            }
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        public DataTable ExecuteDataTable(SqlItem sqlItem)
        {
            var dataSet = ExecuteDataSet(sqlItem);
            if (dataSet != null && dataSet.Tables.Count > 0)
                return dataSet.Tables[0];
            return null;
        }

        /// <summary>
        /// 根据SQL查询
        /// </summary>
        public DataSet ExecuteDataSet(SqlItem sqlItem)
        {
            DataSet ds = new DataSet();
            using (var conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand(sqlItem.SqlValue, conn))
                    {
                        cmd.CommandType = sqlItem.CmdType;
                        cmd.CommandTimeout = 0;
                        AddParams(cmd, sqlItem.CustomType, sqlItem.Params);
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        da.Dispose();
                    }
                }
                catch (Exception exp)
                {
                    Log.Error(MethodBase.GetCurrentMethod().Name, exp);
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }
        /// <summary>
        /// EntitiesToDataTable
        /// </summary>
        public DataTable EntitiesToDataTable<Q>(List<Q> modelList) where Q : class, new()
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            DataTable dt = EntityToDataTable(modelList[0]);

            foreach (Q model in modelList)
            {
                DataRow dataRow = dt.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(Q).GetProperties().Where(p => p.GetMethod.IsVirtual == false && p.PropertyType != typeof(byte[])))
                {
                    var value = propertyInfo.GetValue(model);
                    dataRow[propertyInfo.Name] = value ?? DBNull.Value;
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }
        /// <summary>
        /// 单个实体转dataTable
        /// </summary>
        private DataTable EntityToDataTable<Q>(Q model) where Q : class, new()
        {
            DataTable dataTable = new DataTable(typeof(Q).Name);
            foreach (PropertyInfo propertyInfo in typeof(Q).GetProperties().Where(p => p.GetMethod.IsVirtual == false && p.PropertyType != typeof(byte[])))
            {
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, typeof(object)));
            }
            return dataTable;
        }
        /// <summary>
        /// DataTableToEntities
        /// </summary>
        /// 
        public List<Q> DataTableToEntities<Q>(DataTable dataTable, bool isStringproperty = false) where Q : class, new()
        {
            var list = new List<Q>();
            if (dataTable != null)
            {
                var properties = typeof(Q).GetProperties();
                foreach (DataRow item in dataTable.Rows)
                {
                    var q = new Q();
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        var colName = dataTable.Columns[j].ColumnName.ToLower();
                        var property = properties.FirstOrDefault(p => p.Name.ToLower() == colName);
                        if (property != null)
                        {
                            if (isStringproperty)
                            {
                                property.SetValue(q, item[colName].ToString());
                            } 
                            else if (colName.ToLower()== "ftimestemp")
                            {
                                object value = item[colName];
                                if (property != null)
                                {
                                    string st = string.Empty;
                                    byte[] b = (byte[])value;
                                    var len = b.Length;
                                    long a = 0;
                                    for (int i = len - 1; i >= 0; i--)
                                    {
                                        a += b[i] << (7 - i) * 8;
                                    }
                                    st = a.ToString();
                                    if (property.PropertyType.Name.ToUpper() == "INT64")
                                    {
                                        property.SetValue(q, a, null);
                                    }
                                    else
                                    {
                                        property.SetValue(q, st, null);
                                    }
                                }
                            }

                            else
                            {
                                property.SetValue(q, item[colName] is DBNull ? null : item[colName]);
                            }
                        }
                    }
                    list.Add(q);
                }
            }
            return list;
        }

        
        /// <summary>
        /// 获取sql参数
        /// </summary>
        public SqlParameter[] GetParameters(params KeyValuePair<String, object>[] keyValuePairs)
        {
            return keyValuePairs.Select(p => new SqlParameter(p.Key, p.Value)).ToArray();
        }
        /// <summary>
        /// 分片表名
        /// </summary>
        public string GetTableName(string tableName, int value)
        {
            return tableName + "_" + value % Config.ShardingCount;
        }
        #endregion

        #region 批量执行
        /// <summary>
        /// sql批量执行
        /// </summary>
        public int ExcuteSqlTranByBatch(params SqlItem[] sqlItems)
        {
            var count = 0;
            using (SqlConnection myCn = new SqlConnection(ConnectionString))
            {
                StringBuilder sb = new StringBuilder();
                try
                { 
                    sb.Append(IsolationLevelToSql(Config.IsolationLevel));
                    sb.Append("begin tran                                                                                                                                                             \n");
                    sb.Append("begin try                                                                                                                                                              \n");
                    sb.Append(GetSqlString(sqlItems) + "\n");
                    sb.Append("commit tran                                                                                                                                                            \n");
                    sb.Append("Select 1 CODE, 0 ERRORNUMBER,0 ERRORSEVERITY,0 ERRORSTATE,0 ERRORLINE,'' ERRORMESSAGE                                                                                  \n");
                    sb.Append("end try                                                                                                                                                                \n");
                    sb.Append("begin catch                                                                                                                                                            \n");
                    sb.Append("rollback tran                                                                                                                                                          \n");
                    sb.Append("Select 0 CODE, ERROR_NUMBER() as ERRORNUMBER,ERROR_SEVERITY() as ERRORSEVERITY,ERROR_STATE() as ERRORSTATE, ERROR_LINE() as ERRORLINE ,ERROR_MESSAGE() as ERRORMESSAGE  \n");
                    sb.Append("end catch    \n");
                    if (myCn.State == ConnectionState.Closed)
                    {
                        myCn.Open();
                    }
                    SqlCommand cmd = new SqlCommand(sb.ToString(), myCn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 0;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            count = Convert.ToInt32(reader.GetInt32(0));
                            Log.Error(new[] { MethodBase.GetCurrentMethod().Name, count == 0 ? reader.GetString(5) : "Success." });
                        }
                        reader.Close();
                    }
                }
                catch(Exception e)
                {
                    if (myCn.State == ConnectionState.Open)
                    {
                        myCn.Close();
                    } 
                    Log.Error(sb.ToString(), e);
                }
            }
            return count;
        }

        private string GetSqlString(params SqlItem[] sqlItems)
        {
            var sqlList = new List<string>();
            foreach (var item in sqlItems)
            {
                if (item != null)
                {
                    var sql = new StringBuilder();
                    if (item != null)
                    {
                        sql.Append("exec sp_executesql N'" + item.SqlValue.Replace("'", "''") + "'," + GetSqlParams(item.Params));
                    }
                    sqlList.Add(sql.ToString());
                }
            }
            return string.Join(";\n", sqlList);
        }

        private string GetSqlParams(params IDataParameter[] sqlParameters)
        {
            var paramList = new List<string>();
            var valueList = new List<string>();
            foreach (var item in sqlParameters)
            {
                var arr = GetParamString((SqlParameter)item);

                paramList.Add(arr[0]);
                valueList.Add(arr[1]);
            }
            var param1 = "N'" + string.Join(",", paramList) + "'";
            var param2 = string.Join(",", valueList);
            return param1 + "," + param2;
        }

        private string[] GetParamString(SqlParameter item)
        {
            var paramArr = new string[2];
            try
            {
                SqlDbType sqlDbType = item.SqlDbType;
                paramArr[0] = item.ParameterName + " " + sqlDbType.ToString();
                paramArr[1] = item.ParameterName + "=" + item.SqlValue;

                #region SqlDbType

                switch (sqlDbType)
                {
                    case SqlDbType.BigInt:
                        break;
                    case SqlDbType.Binary:
                        break;
                    case SqlDbType.Bit:
                        paramArr[1] = item.ParameterName + "=" + (Convert.ToBoolean(item.Value) ? 1 : 0);
                        break;
                    case SqlDbType.Char:
                        paramArr[0] += "(" + item.Value.ToString().Length + ")";
                        paramArr[1] = item.ParameterName + "=" + ((item.Value == null || item.Value is DBNull) ? "NULL" : "'" + item.Value + "'");
                        break;
                    case SqlDbType.Date:
                        break;
                    case SqlDbType.DateTime:
                        paramArr[1] = item.ParameterName + "=" + ((item.Value == null || item.Value is DBNull) ? "NULL" : "'" + item.Value + "'");
                        break;
                    case SqlDbType.DateTime2:
                        paramArr[0] += "(7)";
                        paramArr[1] = item.ParameterName + "=" + ((item.Value == null || item.Value is DBNull) ? "NULL" : "'" + item.Value + "'");
                        break;
                    case SqlDbType.DateTimeOffset:
                        break;
                    case SqlDbType.Decimal:
                        paramArr[0] += "(18,4)";
                        break;
                    case SqlDbType.Float:
                        break;
                    case SqlDbType.Image:
                        break;
                    case SqlDbType.Int:
                        break;
                    case SqlDbType.Money:
                        break;
                    case SqlDbType.NChar:
                        paramArr[0] += "(max)";
                        paramArr[1] = item.ParameterName + "=" + ((item.Value == null || item.Value is DBNull) ? "NULL" : "N'" + item.Value + "'");
                        break;
                    case SqlDbType.NText:
                        break;
                    case SqlDbType.NVarChar:
                        paramArr[0] += "(max)";
                        paramArr[1] = item.ParameterName + "=" + ((item.Value == null || item.Value is DBNull) ? "NULL" : "N'" + item.Value + "'");
                        break;
                    case SqlDbType.Real:
                        break;
                    case SqlDbType.SmallDateTime:
                        break;
                    case SqlDbType.SmallInt:
                        break;
                    case SqlDbType.SmallMoney:
                        break;
                    case SqlDbType.Structured:
                        break;
                    case SqlDbType.Text:
                        break;
                    case SqlDbType.Time:
                        paramArr[0] += "(7)";
                        paramArr[1] = item.ParameterName + "='" + item.Value + "'";
                        break;
                    case SqlDbType.Timestamp:
                        break;
                    case SqlDbType.TinyInt:
                        break;
                    case SqlDbType.Udt:
                        break;
                    case SqlDbType.UniqueIdentifier:
                        break;
                    case SqlDbType.VarBinary:
                        break;
                    case SqlDbType.VarChar:
                        paramArr[0] += "(max)";
                        paramArr[1] = item.ParameterName + "=" + ((item.Value == null || item.Value is DBNull) ? "NULL" : "'" + item.Value + "'");
                        break;
                    case SqlDbType.Variant:
                        break;
                    case SqlDbType.Xml:
                        break;
                    default:
                        break;
                }

                #endregion
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return paramArr;
        }
        /// <summary>
        /// 获取事务隔离级别
        /// </summary>
        private string IsolationLevelToSql(IsolationLevel? isolationLevel)
        {
            var levelStr = "READ COMMITTED";
            switch (isolationLevel)
            {
                case IsolationLevel.ReadCommitted:
                    {
                        levelStr = "READ COMMITTED";
                        break;
                    }
                case IsolationLevel.ReadUncommitted:
                    {
                        levelStr = "READ UNCOMMITTED";
                        break;
                    }
                case IsolationLevel.RepeatableRead:
                    {
                        levelStr = "REPEATABLE READ";
                        break;
                    }
                case IsolationLevel.Snapshot:
                    {
                        levelStr = "SNAPSHOT";
                        break;
                    }
                case IsolationLevel.Serializable:
                    {
                        levelStr = "SERIALIZABLE";
                        break;
                    }
            }
            return string.Format("set transaction isolation level {0} \n", levelStr);
        }

        #endregion

        #region 获取SQL语句
        /// <summary>
        /// 获取插入语句
        /// </summary>
        public SqlItem InsertSqlItem<Q>(Q t, string tableName = "") where Q : class, new()
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                var propertys = typeof(Q).GetProperties().Where(p => p.GetMethod.IsVirtual == false && p.PropertyType != typeof(byte[]));
                stringBuilder.AppendFormat("Insert Into [{0}]", string.IsNullOrEmpty(tableName) ? typeof(Q).Name : tableName);
                stringBuilder.AppendFormat("({0})Values", String.Join(",", propertys.Select(p => p.Name)));
                stringBuilder.AppendFormat("({0})", String.Join(",", propertys.Select(q => "@" + q.Name)));
                return new SqlItem { SqlValue = stringBuilder.ToString(), Params = propertys.Select(p => new SqlParameter("@" + p.Name, p.GetValue(t))).ToArray() };
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取更新语句
        /// </summary>
        public SqlItem UpdateSqlItem(String tableName, BaseCore baseCore)
        {
            if (baseCore != null && baseCore.ChangedDictionary.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Update " + tableName + " WITH (ROWLOCK) Set ");
                sb.Append(string.Join(",", baseCore.ChangedDictionary.Keys.Select(p => p + "=@" + p)));
                sb.Append(" where Id=@Id");
                var list = new List<SqlParameter>();
                if (!baseCore.ChangedDictionary.Keys.Contains("Id"))
                {
                    list.Add(new SqlParameter("@Id", baseCore.Id));
                }
                list.AddRange(baseCore.ChangedDictionary.Select(p => new SqlParameter("@" + p.Key, p.Value)));
                return new SqlItem
                {
                    SqlValue = sb.ToString(),
                    Params = list.ToArray()
                };
            }
            return null;
        }
        #endregion

      

        #region
        /// <summary>
        /// 获取删除语句
        /// </summary>
        public SqlItem DeleteSqlItem<T>(T t, string tableName = "", bool isCheckCount = false) where T : class, new()
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                var properties = typeof(T).GetProperties().Where(p => p.GetMethod.IsVirtual == false && p.PropertyType != typeof(byte[]));
                if (properties.Where(p => p.Name.ToLower() == "id") == null) return null;
                stringBuilder.AppendFormat("DELETE FROM [{0}]", string.IsNullOrEmpty(tableName) ? typeof(T).Name : tableName);
                stringBuilder.AppendFormat("WHERE Id= @Id");
                return new SqlItem
                {
                    SqlValue = stringBuilder.ToString(),
                    Params = properties.Where(p => p.Name.ToLower() == "id").Select(p => new SqlParameter("@Id", p.GetValue(t))).ToArray(),
                    IsCheckCount = isCheckCount
                };
            }
            catch
            {
                return null;
            }
        }
        #endregion


        public int DeleteById<T>(string id)
        {
            using (var db = GetConnection())
            {
                var rowCnt = db.DeleteById<T>(id);
                db.Close();
                return rowCnt;
            }
        }
    }
}
