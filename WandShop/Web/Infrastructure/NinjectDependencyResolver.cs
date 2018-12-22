using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Logic.Concrete;
using Logic.Abstract;
using Logic.Entities;
using System.Configuration;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam; AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IGameRepository>().To<EfGameRepository>();
            kernel.Bind<IPlayerPartRepository>().To<EfPlayerPartRepository>();
            kernel.Bind<IPlayerRepository>().To<EfPlayerRepository>();
            kernel.Bind<IPlayerRoundRepository>().To<EfPlayerRoundRepository>();
            kernel.Bind<IRoundRepository>().To<EfRoundRepository>();
        }
    }
}