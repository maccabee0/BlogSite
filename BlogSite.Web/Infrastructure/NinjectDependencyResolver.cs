using System.Web.Http.Dependencies;

using Ninject;

namespace BlogSite.Web.Infrastructure
{
    public class NinjectDependencyResolver : NinjectScope, IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
            :base(kernel)
        {
            _kernel = kernel;
            //AddBindings();
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectScope(_kernel.BeginBlock());
        }

        //public object GetService(Type serviceType)
        //{
        //    return _kernel.TryGet(serviceType);
        //}

        //public IEnumerable<object> GetServices(Type serviceType)
        //{
        //    return _kernel.GetAll(serviceType);
        //}

        //public IBindingToSyntax<T> Bind<T>()
        //{
        //    return _kernel.Bind<T>();
        //}

        //public IKernel Kernel { get { return _kernel; } }

        //private void AddBindings()
        //{
        //    Bind<IAuthProvider>().To<FormsAuthProvider>();
        //    Bind<IService>().To<SiteService>();
        //    Bind<IBlogRepository>().To<BlogRepository>();
        //    Bind<IUserRepository>().To<UserRepository>();
        //    Bind<ICommentRepository>().To<CommentRepository>();
        //}
    }
}