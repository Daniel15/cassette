﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Cassette
{
    public static class BootstrapperLocator
    {
        public static IBootstrapper Bootstrapper;

        static BootstrapperLocator()
        {
            var defaultBootstrapperType = typeof(DefaultBootstrapper);
            var customBootstrappers = CustomBootstrappers(defaultBootstrapperType);

            var bootstrapperType = customBootstrappers.FirstOrDefault() ?? defaultBootstrapperType;

            Bootstrapper = (IBootstrapper)Activator.CreateInstance(bootstrapperType);
        }

        static IEnumerable<Type> CustomBootstrappers(Type defaultBootstrapperType)
        {
            return from type in AppDomainAssemblyTypeScanner.Types
                   where typeof(IBootstrapper).IsAssignableFrom(type)
                   where type != defaultBootstrapperType
                   select type;
        }
    }
}