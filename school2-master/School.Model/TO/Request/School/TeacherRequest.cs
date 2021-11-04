using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class TeacherRequest
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 用户表Id
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 评价
        /// </summary>
        public int Valuation { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 教龄
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string SubMain { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }

        public string Tel { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
    }
}
