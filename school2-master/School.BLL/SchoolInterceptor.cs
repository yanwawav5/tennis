using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using school.Common;
using ServiceStack.OrmLite;


namespace school.BLL
{
    public class schoolInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                var dateTime = DateTime.Now;
                invocation.Proceed();
                //var types= invocation.ReturnValue.GetType().Name;
                //var dd = "dd";
                //var asx = ((school.Model.BO.SqlItem)((Castle.DynamicProxy.AbstractInvocation)invocation).ReturnValue).SqlValue;
                var executeTime = (DateTime.Now - dateTime).TotalMilliseconds;
            }
            catch (Exception exp)
            {
                var methodName = invocation.Method.ReflectedType.Name + "." + invocation.Method.Name;
                Log.Error(methodName, exp);
                throw exp;
            }
        }
    }
}
