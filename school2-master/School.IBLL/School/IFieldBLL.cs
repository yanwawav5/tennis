using school.IBLL.Base;
using school.Model;
using school.Model.BO;
using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace school.IBLL
{
    public interface IFieldBLL : IBaseBLL
    {
        /// <summary>
        /// 保存或更新
        /// </summary>
        AmountBo CreateOrUpdate(FieldBO modelBo);
        AmountBo DeleteField(string Id);
        M_Field GetOne(string Id);

        List<M_Field> GetList(string schoolId);
        AmountBo CreateTableLog();

        List<M_School> GetListSchool();
        M_School GetSchool(string id);

        AmountBo CreateOrUpdateSchool(SchoolBO modelBo);
        AmountBo DeleteSchool(string Id);
        AmountBo CreateOrUpdateFilePrice(FilePriceBO modelBo);
        M_FilePrice GetFilePrice();
    }
}
