

namespace Game 
{
    public sealed class GangstaMpdel : BaseEnemyObject
    {
        protected override void Awake()
        {
            base.Awake();
            _mentalResistance = 0.75f;
        }
    }
}



