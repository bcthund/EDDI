using Cottle;
using EddiCore;
using EddiSpeechResponder.Service;
using JetBrains.Annotations;
using System;
using System.Reflection;

namespace EddiSpeechResponder.CustomFunctions
{
    [UsedImplicitly]
    public class GetState : ICustomFunction
    {
        public string name => "GetState";
        public FunctionCategory Category => FunctionCategory.Utility;
        public string description => Properties.CustomFunctions_Untranslated.GetState;
        public Type ReturnType => typeof( Value );
        public IFunction function => Function.CreateNative1((runtime, variableName, writer) =>
        {
            var varName = variableName.AsString.ToLowerInvariant().Replace(" ", "_");
            if ( EDDI.Instance.State.TryGetValue( varName, out var result )  )
            {
                return result is null ? Value.EmptyMap : Value.FromReflection( result, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
            }
            return "";
        });
    }
}
