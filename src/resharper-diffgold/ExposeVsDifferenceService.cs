using JetBrains.VsIntegration.Application;
using Microsoft.VisualStudio.Shell.Interop;

namespace CitizenMatt.ReSharper.Plugins.DiffGold
{
    [WrapVsInterfaces]
    public class ExposeVsDifferenceService : IExposeVsServices
    {
        public void Register(VsServiceProviderResolver.VsServiceMap map)
        {
            if (map.Resolve(typeof (IVsDifferenceService)) == null)
            {
                map.QueryService<SVsDifferenceService>().As<IVsDifferenceService>();
            }
        }
    }
}