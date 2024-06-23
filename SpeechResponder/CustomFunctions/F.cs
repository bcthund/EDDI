using Cottle;
using EddiSpeechResponder.Service;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EddiSpeechResponder.CustomFunctions
{
    [UsedImplicitly]
    public class F : RecursiveFunction, ICustomFunction
    {
        public string name => "F";
        public FunctionCategory Category => FunctionCategory.Utility;
        public string description => Properties.CustomFunctions_Untranslated.F;
        public Type ReturnType => typeof( string );
        public IFunction function => Function.CreateNative1( ( runtime, scriptName, writer ) =>
        {
            // Use a cascading context consisting of variables set in the current document and variables set prior to resolving 
            var latestGlobalsContext =
                Cottle.Context.CreateCustom( runtime.Globals.ToDictionary( g => g.Key, g => g.Value ) );
            var latestStateContext =
                Cottle.Context.CreateBuiltin(
                    new Dictionary<Value, Value> { [ "state" ] = ScriptResolver.buildState() } );
            var originalContext = Context;
            var joinedContexts = Cottle.Context.CreateCascade(latestGlobalsContext, Cottle.Context.CreateCascade(latestStateContext, originalContext));
            
            var result = scriptName.AsString;
            return ScriptResolver.resolveFromName( result, Scripts, joinedContexts, false )?.Trim();
        });

        [UsedImplicitly]
        public F ( IContext context, Dictionary<string, Script> scripts ) : base( context, scripts )
        { }
    }
}
