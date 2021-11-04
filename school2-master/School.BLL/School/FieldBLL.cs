using school.IBLL;
using school.BLL.Base;
using System;
using System.Collections.Generic;
using school.IDAL;
using school.Model.BO;
using school.Model.DAO;
using school.Common.Tools;

namespace school.BLL
{
    public class FieldBLL : BaseBLL<IFieldDAL>, IFieldBLL
    {
        private const int MaxNum = 2000000;
        #region 场地

        public AmountBo CreateOrUpdate(FieldBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Field>(p => p.Id == modelBo.Id);

            ObjectHelper.AutoMapping(modelBo, queryModel, new string[] { "id", "CreateTime", "CreateUserId", "FTimeStemp" });
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
            return base.ReturnBo(1);
        }

        public AmountBo DeleteField(string Id)
        {
            var mo = base.LoadFirstOrDefault<M_Field>(p => p.Id == Id);
            mo.Deleted = 1;
            mServiceDAL.Update(mo);
            return base.ReturnBo(1);
        }

        public AmountBo CreateTableLog()
        {

            return base.ReturnBo(1);
        }
        SqlItem CreateSqlIte()
        {
            string endName = DateTime.Now.ToString("yyMMddHH");
            string tbname = "M_Log" + endName;
            string sql = "select * into " + tbname + " from M_Log";
            var sqlItem = new SqlItem()
            {
                SqlValue = sql,
                Params = null
            };
            return sqlItem;
        }
        SqlItem DeleteSqlItem()
        {
            string sql = "TRUNCATE TABLE  [dbo].[M_Log] ";//删除  
            var sqlItem = new SqlItem()
            {
                SqlValue = sql,
                Params = null
            };
            return sqlItem;
        }

        public M_Field GetOne(string Id)
        {
            var query = base.LoadFirstOrDefault<M_Field>(p => p.Id == Id);
            return query;
        }

        public List<M_Field> GetList(string schoolId)
        {
            if (!string.IsNullOrEmpty(schoolId))
            {
                var list = base.LoadEntities<M_Field>(p => p.SchoolId == schoolId && p.Deleted == 0, true, p => p.Sort);
                return list;
            }
            var list2 = base.LoadEntities<M_Field>(p => p.SchoolId != null && p.Deleted == 0, true, p => p.Sort);
            return list2;
        }
        #endregion


        #region //学校
        public List<M_School> GetListSchool()
        {
            var list = base.LoadEntities<M_School>(p => p.Id != null && p.Deleted == 0, true, p => p.Sort);
            return list;
        }
        public M_School GetSchool(string id)
        {
            var mo = base.LoadFirstOrDefault<M_School>(p => p.Id == id);
            return mo;
        }
        /// <summary>
        /// 添加修改
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        public AmountBo CreateOrUpdateSchool(SchoolBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_School>(p => p.Id == modelBo.Id);

            ObjectHelper.AutoMapping(modelBo, queryModel);
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
            return base.ReturnBo(1);
        }

        public AmountBo DeleteSchool(string Id)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_School>(p => p.Id == Id);
            queryModel.Deleted = 1;
            queryModel.UpdateTime = DateParse.GetDateTime();
            queryModel.UpdateUserId = queryModel.Id;
            amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(1);
        }

        #endregion


        #region //场地价格
        public AmountBo CreateOrUpdateFilePrice(FilePriceBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_FilePrice>(p => p.Id == "123");
            ObjectHelper.AutoMapping(modelBo, queryModel);
            queryModel.Id = "123";
            queryModel.Price = modelBo.Price;
            queryModel.PriceSec = modelBo.PriceSec;
            queryModel.sorttime = modelBo.sorttime;
            queryModel.UpdateTime = DateParse.GetDateTime();
            queryModel.UpdateUserId = queryModel.Id;
            amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(1);
        }

        public M_FilePrice GetFilePrice()
        {
            return mServiceDAL.LoadFirstOrDefault<M_FilePrice>(p => p.Id == "123");
        }

        #endregion

    }
}
