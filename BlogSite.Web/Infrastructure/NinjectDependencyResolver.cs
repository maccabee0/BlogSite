using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using BlogSite.Web.Infrastructure.Abstract;
using BlogSite.Web.Infrastructure.Concrete;
using Ninject;
using Ninject.Syntax;

using BlogSite.Services.Abstract;
using BlogSite.Services.Concrete;

namespace BlogSite.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public IBindingToSyntax<T> Bind<T>()
        {
            return kernel.Bind<T>();
        }

        public IKernel Kernel { get { return kernel; } }

        private void AddBindings()
        {
            Bind<IAuthProvider>().To<FormsAuthProvider>();
            Bind<IService>().To<SiteService>();
            Bind<IBlogRepository>().To<BlogRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<ICommentRepository>().To<CommentRepository>();
        }
    }
}