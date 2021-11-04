using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class IdListBO : PageInfo
    {
        public string Id { get; set; }
        public string Key { get; set; }
    }

    public class IdStatusListBO : PageInfo
    {
        public string Status { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 0学生订场，1内部订场
        /// </summary>
        public string CategoryId { get; set; }
    }


    public class IdStudentStatusListBO : PageInfo
    {
        public string Status { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 0学生订场，1内部订场
        /// </summary>
        public string CategoryId { get; set; }
        public string StudnetId { get; set; }
    }

    public class IdCategoryListBO : PageInfo
    {
        public string ToId { get; set; }
        public string Id { get; set; }
        public int Category { get; set; }
    }

    public class IdActiStatusListBO : PageInfo
    {
        /// <summary>
        /// 空全部，1正常，2取消，3完成
        /// </summary>
        public string Status { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 按日常性
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// 0忽略，1 报名中活动），2报名或者未开始3不能报名活动
        /// </summary>
        public string StatusInfo { get; set; }
        
    }
}
