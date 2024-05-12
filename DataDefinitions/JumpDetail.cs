using Utilities;

namespace EddiDataDefinitions
{
    public class JumpDetail
    {
        [PublicAPI]
        public double distance { get; private set; }

        [PublicAPI]
        public int jumps { get; private set; }

        public JumpDetail() { }

        public JumpDetail(double distance, int jumps)
        {
            this.distance = distance;
            this.jumps = jumps;
        }
    }
}
