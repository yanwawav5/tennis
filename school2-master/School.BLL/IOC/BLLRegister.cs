using school.IBLL;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using System.Web.Compilation;
using school.IBLL.Session;
using school.Model.DAO;

namespace school.BLL.IOC
{
    public class BLLRegister
    {
        private static readonly BLLRegister instance = new BLLRegister();
        //private readonly IUnityContainer container;
        private readonly IContainer container;
        private BLLRegister()
        {
            var builder = new ContainerBuilder();
            //container = new UnityContainer();
            builder.RegisterType<SessionService>().PropertiesAutowired();
            builder.RegisterType<schoolInterceptor>();
            //var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            var assemblies = new[]
            {
                Assembly.Load("school.BLL"),
                Assembly.Load("school.IBLL"),
                Assembly.Load("school.IDAL"),
                Assembly.Load("school.DAL")
            }; 

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Where(p => typeof(IDependency).IsAssignableFrom(p) && !p.IsAbstract)
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .InterceptedBy(typeof(schoolInterceptor))
                .EnableInterfaceInterceptors(); 
            container = builder.Build();
            

        }
        /// <summary>
        /// 获取实例
        /// </summary>
        public static BLLRegister Instance
        {
            get { return instance; }
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public object GetObject(Type t)
        {
            return container.Resolve(t);
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetObject<T>()
        {
            return container.Resolve<T>();
        }

        public T GetObject<T>(string name)
        {
            return container.ResolveNamed<T>(name);
        } 
    }
}
