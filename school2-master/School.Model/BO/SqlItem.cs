using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class SqlItem
    {
        /// <summary>
        /// sql语句
        /// </summary>
        public string SqlValue { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public IDataParameter[] Params { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public DataTable SourseData { get; set; }
        /// <summary>
        /// 批量插入
        /// </summary>
        public bool IsBatch { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 是否检查数量
        /// </summary>
        public bool IsCheckCount { get; set; }
        /// <summary>
        /// 自定义类型
        /// </summary>
        public string CustomType { get; set; }
        /// <summary>
        /// 执行类型
        /// </summary>
        public CommandType CmdType { get; set; }
        public SqlItem()
        {
            this.CmdType = CommandType.Text;
        }
    }
}
