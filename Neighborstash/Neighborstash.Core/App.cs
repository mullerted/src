using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using Neighborstash.Core.Contracts;

namespace Neighborstash.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            // bulk service registration (with service locator, IoC)
            CreatableTypes()
                .EndingWith("Repository")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            // bulk service registration (with service locator)
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            //Mvx.RegisterSingleton<IMvxTextProvider>(new ResxTextProvider(new Strings.ResouceManager));

            //Resolving instance example
            var userService = Mvx.Resolve<IUserDataService>();

            RegisterNavigationServiceAppStart<ViewModels.FirstViewModel>();
        }
    }
}
