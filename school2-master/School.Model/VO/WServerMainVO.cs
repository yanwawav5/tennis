using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model
{
    public class WServerMainVO
    {
        public string Id { get; set; }
        public int Status { get; set; } 
        public DateTime? ApplyTime { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal Price { get; set; }
        public string UserAddressId { get; set; }
        public string OtherAddress { get; set; }
        public string OtherTel { get; set; }
        public string ProductId { get; set; }
        /// <summary>
        /// 服务人员Id
        /// </summary>
        public string ServerUserId { get; set; }
        /// <summary>
        /// 第一个验证码
        /// </summary>
        public string ServerCode { get; set; }
        /// <summary>
        /// 第二个验证码
        /// </summary>
        public string ServerSecCode { get; set; }
        public List<WServerItemVO> List { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public W_UserAddress AddressModel { get; set; }

        public WUserInfoVO UserModel { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 服务类型1。元/H    2.元/平   3 ，元/台
        /// </summary>
        public int ServerType { get; set; }
        public string Pic { get; set; }
        /// <summary>
        /// 服务人员
        /// </summary>
        public int ServerNum { get; set; }
    }


    public class WServerMainNewVO
    {
        public string Id { get; set; }
        public int Status { get; set; }
        public DateTime ApplyTime { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal Price { get; set; }
        public string UserAddressId { get; set; }
        public string OtherAddress { get; set; }
        public string OtherTel { get; set; }
        public string ProductId { get; set; }

        public List<WServerItemVO> List { get; set; }
    }
}
