using school.IBLL;
using school.BLL.Base;
using System;
using System.Collections.Generic;
using school.IDAL;
using school.Model.BO;
using school.Model.DAO;
using school.Common.Tools;
using System.Linq;
using school.Common;
using school.Model.Enum;

namespace school.BLL
{
    public class ShareInfoBLL : BaseBLL<IShareInfoDAL>, IShareInfoBLL
    {
        public string CreateOrUpdate(ShareInfoBO modelBo)
        {

            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_ShareInfo>(p => p.Id == modelBo.Id);
            if (queryModel.FtimeStemp == null)
            {
                queryModel = mServiceDAL.LoadFirstOrDefault<M_ShareInfo>(p => p.ShareDay == modelBo.ShareDay && p.UnionId == modelBo.UnionId&&p.Types==10);
            }
            ObjectHelper.AutoMapping(modelBo, queryModel, new string[] { "id" });
            queryModel.Types = 10;
            if (queryModel.FtimeStemp == null)
            {
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateParse.GetDateTime();
                queryModel.CreateUserId = queryModel.Id;
                amount = mServiceDAL.Create(queryModel);
            }
            else
            {
                queryModel.UpdateTime = DateParse.GetDateTime();
                queryModel.UpdateUserId = queryModel.Id;
                amount = mServiceDAL.Update(queryModel);
            }
            return queryModel.Id;
            //return base.ReturnBo(1);
        }

        public string CreateOrUpdate2(ShareInfoBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_ShareInfo>(p => p.Id == modelBo.Id);

          

            if (queryModel.FtimeStemp == null)
            {
                if (modelBo.Types == 2)
                {
                    queryModel = mServiceDAL.LoadFirstOrDefault<M_ShareInfo>(p => p.StudentCourseId == modelBo.StudentCourseId && p.StudentId == modelBo.StudentId && p.Types == modelBo.Types);
                    //var vibo = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == id);
                    //modelBo.Url = vibo?.CourseVideoUrl;
                }
                else
                {

                    queryModel = mServiceDAL.LoadFirstOrDefault<M_ShareInfo>(p => p.StudentCourseClassId == modelBo.StudentCourseClassId && p.StudentId == modelBo.StudentId && p.Types == modelBo.Types);
                    var vibo = mServiceDAL.LoadFirstOrDefault<M_StudentCourseClass>(p => p.Id == modelBo.StudentCourseClassId);
                    if (modelBo.Types == 1)
                    {
                        modelBo.Url = vibo?.StudentJobUrl;
                        if(string.IsNullOrEmpty(modelBo.Url))
                            throw new schoolException(SubCode.Failure.GetHashCode(), "请先上传作业视频！");
                    }
                    else {
                        var tec= mServiceDAL.LoadFirstOrDefault<M_TeacherCourse>(p => p.Id == vibo.TeacherCourseId);
                        var planbo = mServiceDAL.LoadFirstOrDefault<M_TeachingPlan>(p => p.Id == tec.TeachingPlanId);
                        /*var urls = mServiceDAL.LoadFirstOrDefault<M_Video>(p => p.Id == planbo.VideoId);*/
                        //var urls = ;
                        modelBo.Url = vibo?.CourseVideoUrl;
                        if (string.IsNullOrEmpty(modelBo.Url))
                            throw new schoolException(SubCode.Failure.GetHashCode(), "请先上传视频！");
                    }
                    
                }
            }
            ObjectHelper.AutoMapping(modelBo, queryModel, new string[] { "id" });
            if (queryModel.FtimeStemp == null)
            {
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateParse.GetDateTime();
                queryModel.CreateUserId = queryModel.Id;
                amount = mServiceDAL.Create(queryModel);
            }
            else
            {
                queryModel.UpdateTime = DateParse.GetDateTime();
                queryModel.UpdateUserId = queryModel.Id;
                amount = mServiceDAL.Update(queryModel);
            }
            return queryModel.Id;
            //return base.ReturnBo(1);
        }



        public List<ShareInfoVO> GetList(PageInfo bo)
        {
            int totNum = 0;
            var list = new List<ShareInfoVO>();
            var whereQl = BuildExpression<M_ShareInfo>(p => p.Deleted == 0);
            IEnumerable<M_ShareInfo> noumena = null;
            noumena = mServiceDAL.LoadEntities(whereQl, true, x => new { x.M_Student }).OrderByDescending(x => x.CreateTime);
            totNum = noumena.Count();

            if (!noumena.Any()) return list;
            foreach (var item in noumena)
            {
                var vo = new ShareInfoVO
                {
                    Id = item.Id,
                    NickName = item.M_Student?.NickName,
                    Click = item.Click,
                    Pic = item.Pic,
                    ShareDay = item.ShareDay,
                    StudentId = item.StudentId,
                    Title = item.Title,
                    Url = item.Url
                };
                list.Add(vo);
            }

            return list;
            //list = noumena.Select
            //    ({

            //}).OrderByDescending(x =>x.CreateTime).ThenByDescending(x => x.Click).ToList();

        }
        public M_ShareInfo GetOne(string Id)
        {
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_ShareInfo>(p => p.Id == Id);
            var student = mServiceDAL.LoadFirstOrDefault<M_Student>(p => p.Id == queryModel.StudentId);
            queryModel.NickName = student?.NickName; 
            return queryModel;
        }

        public bool CreateOrUpdateClick(string Id, string unionId)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_ShareInfo>(p => p.Id == Id);//查询分享的数据
            if (queryModel.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误！");
            var count = base.Count<M_ShareInfoClick>(p => p.ShareInfoId == Id && p.UnionId == unionId);
            if (count > 0)
                throw new schoolException(SubCode.Failure.GetHashCode(), "不能重复点赞！");
            M_ShareInfoClick mode = new M_ShareInfoClick()
            {
                Id = Utils.GetGuid(),
                CreateTime = DateParse.GetDateTime(),
                CreateUserId = unionId,
                ShareInfoId = Id,
                UnionId = unionId
            };
            amount = mServiceDAL.Create<M_ShareInfoClick>(mode);
            return amount > 0;
        }

        /// <summary>
        /// 获取点赞
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int GetNum(string Id)
        {
            var count = base.Count<M_ShareInfoClick>(p => p.ShareInfoId == Id);
            return count;
        }

        /// <summary>
        /// 获取训练次数
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int GetXLNum(string Id, int types)
        {
            var count = base.Count<M_ShareInfo>(p => p.UnionId == Id&&p.Types==types);
            return count;
        }
    }
}
