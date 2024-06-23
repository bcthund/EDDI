using Cottle;
using System;
using System.Collections.Generic;

namespace EddiSpeechResponder.Service
{
    public interface ICustomFunction
    {
        string name { get; }
        FunctionCategory Category { get; }
        string description { get; }
        Type ReturnType { get; }
        IFunction function { get; }
    }

    public class RecursiveFunction
    {
        public IContext Context { get; }

        public Dictionary<string, Script> Scripts { get; }

        public RecursiveFunction ( IContext context, Dictionary< string, Script > scripts )
        {
            this.Context = context;
            this.Scripts = scripts;
        }
    }

    public enum FunctionCategory
    {
        Details,
        Dynamic,
        Galnet,
        Hidden,
        Phonetic,
        Tempo,
        Utility,
        Voice
    }
}
