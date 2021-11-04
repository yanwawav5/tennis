using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class M_Order : BaseModel
    {
        [References(typeof(M_Student))]
        public string StudentId { get; set; }
        [Reference]
        public virtual M_Student M_Student { get; set; }

        [References(typeof(M_Field))]
        public string FieldId { get; set; }
        [Reference]
        public virtual M_Field M_Field { get; set; }
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
        ///姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; } 

        /// <summary>
        /// 0.学生订单，1内部订场
        /// </summary>
        public int Category { get; set; }
    }
}
