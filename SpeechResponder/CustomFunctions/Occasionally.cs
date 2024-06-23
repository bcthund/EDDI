using Cottle;
using EddiSpeechResponder.Service;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EddiSpeechResponder.CustomFunctions
{
    [UsedImplicitly]
    public class Occasionally : RecursiveFunction, ICustomFunction
    {
        public string name => "Occasionally";
        public FunctionCategory Category => FunctionCategory.Dynamic;
        public string description => Properties.CustomFunctions_Untranslated.Occasionally;
        public Type ReturnType => typeof( string );

        private static readonly Random random =
            new Random( new { n = nameof(Occasionally), dt = DateTime.UtcNow }.GetHashCode() );

        public IFunction function => Function.CreateNative2( ( runtime, n, input, writer ) =>
        {
            Value result;
            if ( random.Next( Convert.ToInt32( n.AsNumber) ) == 0 )
            {
                result = input.AsString;
            }
            else
            {
                result = "";
            }
            var context = Cottle.Context.CreateCascade(Cottle.Context.CreateCustom(runtime.Globals.ToDictionary(g => g.Key, g => g.Value)), Context );
            return ScriptResolver.resolveFromValue( result.AsString, context, false );
        });

        [UsedImplicitly]
        public Occasionally ( IContext context, Dictionary<string, Script> scripts ) : base( context, scripts )
        { }
    }
}
