//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;
//using Axb.ActiveAlumni.Nit.Services;
//using Ninject;

//namespace Axb.ActiveAlumni.Nit
//{
//    public class ActiveAlumniControlFactory : DefaultControllerFactory
//    {
//        private IKernel _kernel;

//        /// <summary>
//        /// ctor
//        /// </summary>
//        public ActiveAlumniControlFactory()
//        {
//            _kernel = new StandardKernel();
//            AddBindings();
//        }

//        protected override IController GetControllerInstance(RequestContext requestContext,
//                                    Type controllerType)
//        {

//            return controllerType == null
//                ? null
//                : (IController)_kernel.Get(controllerType);
//        }

//        private void AddBindings()
//        {
//            _kernel.Bind<IAutenticationService>().To<AutenticationService>();
//            _kernel.Bind<IUserService>().To<UserService>();
//        }
//    }
//}