using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class WServerMainEvaluationBO
    {
        /// <summary>
        /// 服务Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 星星
        /// </summary>
        public int EvaluationStars { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string EvaluationMain { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
       public  List<WServerMainEvaluationVO> List { get; set; }
    }
}
