using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class WserverCodeRequest : AbsRequest
    {
        public string Code { get; set; }
        public string Id { get; set; }
    }

    public class WserverSecCodeRequest : AbsRequest
    {
        /// <summary>
        /// 服务码
        /// </summary>
        public string Code { get; set; }
        //public string Id { get; set; }

        ///// <summary>
        ///// 服务类型
        ///// </summary>
        //public int TypeInfo { get; set; }

        public int num { get; set; }
        ///// <summary>
        ///// 价格
        ///// </summary>
        //public decimal Price { get; set; } 

        /// <summary>
        /// 编号
        /// </summary>
        public string ServerMainId { get; set; }
        /// <summary>
        /// 计算方式(按W_ProductItemPrice配置的NUM来确定，)
        /// </summary>
        public int TypeInfo { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// 1。元/H    2.元/平   3 ，元/台
        /// </summary>
        public int ServerType { get; set; }
    }




}
