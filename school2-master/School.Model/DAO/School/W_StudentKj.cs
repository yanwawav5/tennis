using school.Model.DAO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class W_StudentKj : BaseEntity
    {
        public string StudentId { get; set; }
        public string UnionId { get; set; }
        /// <summary>
        /// 状态0未使用1使用,2过期
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; } 
        /// <summary>
        /// 使用条件
        /// </summary>
        public string Condition { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string CategoryId { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }

    }

    public class StudentKjVO
    {
        public string StudentId { get; set; }
        public string UnionId { get; set; }

        public int Status { get; set; }
        public string Price { get; set; }
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
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 使用条件
        /// </summary>
        public string Condition { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
    }
}
