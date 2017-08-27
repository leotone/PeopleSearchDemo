using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace PeopleSearchDemo.DependencyInjection
{
    public class Bootstrapper
    {
        private static IUnityContainer objectContainer;

        public static IUnityContainer Container
        {
            get { return objectContainer; }

            private set
            {
                objectContainer = value;
            }
        }

        public static void Initialize(IUnityContainer unityContainer)
        {
            Container = unityContainer;
            Container.LoadConfiguration(); // Read unity configurations from config file 
        }
    }
}