using Cottle;
using EddiConfigService;
using EddiDataDefinitions;
using EddiSpeechResponder.Service;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EddiSpeechResponder.CustomFunctions
{
    [UsedImplicitly]
    public class HaulageDetails : ICustomFunction
    {
        public string name => "HaulageDetails";
        public FunctionCategory Category => FunctionCategory.Details;
        public string description => Properties.CustomFunctions_Untranslated.HaulageDetails;
        public Type ReturnType => typeof( List<Haulage> );
        public IFunction function => Function.CreateNative1( ( runtime, missionID, writer ) =>
        {
            var cargo = ConfigService.Instance.cargoMonitorConfiguration?.cargo;
            var result = cargo
                ?.FirstOrDefault( c => c.haulageData.Any( h => h.missionid == Convert.ToInt64( missionID.AsNumber ) ) )
                ?.haulageData;
            return result is null ? Value.EmptyMap : Value.FromReflection( result, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
        });
    }
}
