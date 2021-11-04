using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class OrderVO
    { 
        public string SchoolId { get; set; }
        /// <summary>
        /// 场地姓名
        /// </summary>
        public string SchoolName { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 学生编号
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 场地编号
        /// </summary>
        public string FieldId { get; set; }
        /// <summary>
        /// 学生
        /// </summary>
        public string StudentName { get; set; } 
        /// <summary>
        /// 学校电话
        /// </summary>
        public string SchoolTel { get; set; }
        /// <summary>
        /// 场地图片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
       /// <summary>
       /// 场地
       /// </summary>
        public string FieldName { get; set; }
         
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 小时列表
        /// </summary>
        public List<int> HourList { get; set; }
        /// <summary>
        ///姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        
        /// <summary>
        /// 用户手机
        /// </summary>
        public string StudentTel { get; set; }

        public string X { get; set; }
        public string Y { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 0学生1内部
        /// </summary>
        public int Category { get; set; }

    }
}
