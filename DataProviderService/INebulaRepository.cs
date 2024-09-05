using EddiDataDefinitions;
using System.Collections.Generic;

namespace EddiDataProviderService
{
    public interface INebulaRepository
    {
        bool GetNebulaVisited(int? nebulaId);
        
        void SaveNebulaVisited(int? nebulaId, bool visited);

        void ToggleNebulaVisited(int? nebulaId);

        // Nothing to do when we leave a nebula
        //void LeaveNebula(int nebulaId);

        void GetNebulaeVisited ( ref List<Nebula> nebulae );
    }
}
