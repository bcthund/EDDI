using EddiConfigService.Configurations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo( "Tests" )]
namespace EddiInaraService
{
    public interface IInaraService
    {
        // Background Sync
        void Start(bool eddiIsBeta = false);
        void Stop();

        // API Event Queue Management
        void EnqueueAPIEvent(InaraAPIEvent inaraAPIEvent);
        List<InaraResponse> SendEventBatch(List<InaraAPIEvent> events, InaraConfiguration inaraConfiguration = null);

        // Commander Profiles
        InaraCmdr GetCommanderProfile(string cmdrName = null);
        List<InaraCmdr> GetCommanderProfiles(IList<string> cmdrNames);

        // API Credentials
        bool checkAPIcredentialsOk ( InaraConfiguration inaraConfiguration );
    }
}
