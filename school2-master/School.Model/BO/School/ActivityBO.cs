using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class ActivityBO
    {
        public string UserId { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BegTime { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 报名开始时间
        /// </summary>
        public DateTime EnrollBegTime { get; set; }
        /// <summary>
        /// 报名结束时间
        /// </summary> 
        public DateTime EnrollEndTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int Num { get; set; } 
        /// <summary>
        /// 名称
        /// </summary>
        public string Main { get; set; }

        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchooId { get; set; }
        /// <summary>
        /// 学校名称
        /// </summary>

        public string ShcoolName { get; set; }
        /// <summary>
        /// 场地编号
        /// </summary>
        public string FieldId { get; set; }
        /// <summary>
        /// 场地名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 老师编号
        /// </summary>

        public string StudentId { get; set; }
        /// <summary>
        /// 老师编号
        /// </summary>

        public string TeacherName { get; set; }
        /// <summary>
        /// 老师编号
        /// </summary>

        public string TeacherTel { get; set; }
        /// <summary>
        /// 价格
        /// </summary>

        public int Price { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 咨询电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 插入订场数据
        /// </summary>
        public List<int> List { get; set; }
    }

}
