using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class M_ShareInfo : BaseModel
    {
        /// <summary>
        /// 学生Id
        /// </summary> 
        [References(typeof(M_Student))]
        public string StudentId { get; set; }
        [Reference]
        public virtual M_Student M_Student { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 点击
        /// </summary>
        public int Click { get; set; }

        /// <summary>
        /// 年月日（20200115）
        /// </summary>
        public string ShareDay { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// UnionId
        /// </summary>
        public string UnionId { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string NickName { get; set; }

        public string StudentCourseId { get; set; }
        public string StudentCourseClassId { get; set; }
        public int Types { get; set; }
    }

    public class ShareInfoVO{
        public string Id { get; set; }
        /// <summary>
        /// 学生Id
        /// </summary>  
        public string StudentId { get; set; } 

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 点击
        /// </summary>
        public int Click { get; set; }

        /// <summary>
        /// 年月日（20200115）
        /// </summary>
        public string ShareDay { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// UnionId
        /// </summary>
        public string UnionId { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string NickName { get; set; }

        public string StudentCourseId { get; set; }
        public string StudentCourseClassId { get; set; }
        public int Types { get; set; } 
    }
}
