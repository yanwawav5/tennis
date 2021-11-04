using school.Model.DAO.Base;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class W_Product 
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建者Id
        /// </summary>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 更新者Id
        /// </summary>
        public string UpdateUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Compute]
        public byte[] FtimeStemp { get; set; }


        public int Sort { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Main { get; set; } 
        /// <summary>
        /// 简介
        /// </summary>
        public string SubMain { get; set; }
        /// <summary>
        /// 好评
        /// </summary>

        public int Praise { get; set; }
       
        /// <summary>
        ///排序号
        /// </summary>
        public int IsTop { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string ProductUnit { get; set; }
        /// <summary>
        /// 算式
        /// </summary>
        public int MatchFormula { get; set; }
        /// <summary>
        /// 服务价格图片
        /// </summary>
        public string ServerPic { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
