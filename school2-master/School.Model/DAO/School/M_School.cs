using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{

    public class M_School : BaseModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        
        /// <summary>
        /// 场馆性质
        /// </summary>
        public string CgNature { get; set; }

        /// <summary>
        /// 场地性质
        /// </summary>
        public string DzNature { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// 灯光状态
        /// </summary>
        public string Lighting { get; set; }
        /// <summary>
        /// 停车情况
        /// </summary>
        public string Park { get; set; }
        /// <summary>
        /// 单双打
        /// </summary>
        public string DanShuang { get; set; }
        /// <summary>
        /// 教练情况
        /// </summary>
        public bool HaveTeacher { get; set; }
        /// <summary>
        /// 课间顾问
        /// </summary>
        public bool HaveAdviser { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// X坐标
        /// </summary>
        public string X { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public string Y { get; set; }
        
    }
}
