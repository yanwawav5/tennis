using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using school.IBLL;
namespace school.IBLL.Session
{
    public class SessionService
    {
        public IUserBLL mUserBLL { get; set; }

        #region
        public IWProductBLL mWproductBLL { get; set; }
        public IWTokenBLL mWtokenBLL { get; set; }

        public IWLoginKeyBLL mWloginKeyBLL { get; set; }
        public IWUserInfoBLL mWuserInfoBLL { get; set; }
        public IWUserAddressBLL mWuserAddressBLL { get; set; }

        public IWServerItemBLL mWserverItemBLL { get; set; }
        public IWServerMainBLL mWserverMainBLL { get; set; }

        public ITopPicBLL mTopPicBLL { get; set; }
        public IProductItemBLL mProductItemBLL { get; set; }
        public IWProductItemPriceBLL mWProductItemPriceBLL { get; set; }

        public IWServerMainEvaluationBLL mWServerMainEvaluationBLL { get; set; }
        #endregion

        #region School
        public IStudentBLL mstudentBLL { get; set; }
        public IShareInfoBLL mshareInfoBLL { get; set; }

        public IStudentPrizeBLL mStudentPrizeBLL { get; set; }
        public ICourseBLL mCourseBLL { get; set; }

        public IStudentCourseBLL mStudentCourseBLL { get; set; }

        public ITeacherBLL mTeacherBLL { get; set; }

        public IFieldBLL mFieldBLL { get; set; }
        #endregion
    }
}
