using school.Model.DAO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class W_ServerMain : BaseEntity
    {
        public int Status { get; set; }
        public string ServerCode { get; set; }
        public string ServerSecCode { get; set; }
        public DateTime? ApplyTime { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal Price { get; set; }
        public string UserAddressId { get; set; }
        public string OtherAddress { get; set; }
        public string OtherTel { get; set; }
        public string UserInfoId { get; set; }
        public string ProductId { get; set; }
        /// <summary>
        /// 星星
        /// </summary>
        public int EvaluationStars { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string EvaluationMain { get; set; }
        /// <summary>
        /// 服务人员Id
        /// </summary>
        public string ServerUserId { get; set; }

        /// <summary>
        /// 服务类型1。元/H    2.元/平   3 ，元/台
        /// </summary>
        public int ServerType { get; set; }

        /// <summary>
        /// 服务人员
        /// </summary>
        public int ServerNum { get; set; }
    }
}
