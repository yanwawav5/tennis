using school.Model.Enum;
using school.Model.TO.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO
{
    public class ApiResult<T> where T : AbsResponse, new()
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public ApiCode Code { get; set; }
        /// <summary>
        ///返回信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Body { get; set; }

        /// <summary>
        ///是否成功
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return this.Code == 0 && this.SubCode == 0;
            }
        }

        /// <summary>
        /// 业务返回错误码
        /// </summary>
        public SubCode SubCode { get; set; }
        /// <summary>
        /// 业务返回错误信息
        /// </summary>
        public string SubMessage { get; set; }
    }
}
