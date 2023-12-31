﻿using System;
using System.Threading;
using GameFramework.Impl.Core;
using GameFramework.UI.WPF.Core;
using Implementation.Module;
using Microsoft.Extensions.DependencyInjection;

namespace GameFramework.ManualTests.Desktop.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override IServiceProvider LoadModules(ServiceCollection collection)
        {            var source = new CancellationTokenSource();

            var core = new CoreModule(collection, source);

            core.RegisterServices("gf-manual-tests");
            core.RegisterOtherServices(new GameFrameworkCore(collection, source));
            core.RegisterOtherServices(new WpfGameFramework(collection, source));
            
            return collection.BuildServiceProvider();
        }
    }
}
