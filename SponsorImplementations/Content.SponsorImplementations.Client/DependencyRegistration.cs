using Content.Corvax.Interfaces.Shared;
using Robust.Shared.IoC;

namespace Content.SponsorImplementations.Client;

internal static class DependencyRegistration
{
    public static void Register(IDependencyCollection dependencies)
    {
        dependencies.Register<ISharedSponsorsManager, ClientSponsorManager>();
    }
}
