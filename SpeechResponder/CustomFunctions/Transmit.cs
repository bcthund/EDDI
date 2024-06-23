using Cottle;
using EddiSpeechResponder.Service;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EddiSpeechResponder.CustomFunctions
{
    [UsedImplicitly]
    public class Transmit : RecursiveFunction, ICustomFunction
    {
        public string name => "Transmit";
        public FunctionCategory Category => FunctionCategory.Phonetic;
        public string description => Properties.CustomFunctions_Untranslated.Transmit;
        public Type ReturnType => typeof( string );
        public IFunction function => Function.CreateNative1( ( runtime, input, writer ) =>
        {
            if (!string.IsNullOrEmpty( input.AsString) )
            {
                var result = @"<transmit>" + input.AsString + "</transmit>";
                var context = Cottle.Context.CreateCascade(Cottle.Context.CreateCustom(runtime.Globals.ToDictionary(g => g.Key, g => g.Value)), Context );
                return ScriptResolver.resolveFromValue( result, context, false );
            }
            return "The Transmit function is used improperly. Please review the documentation for correct usage.";
        });

        [UsedImplicitly]
        public Transmit ( IContext context, Dictionary<string, Script> scripts ) : base( context, scripts )
        { }
    }
}
