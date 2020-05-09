using UnityEngine;

namespace Game
{
    public readonly struct InfoCollision
    {
        private readonly Vector3 _dir;
        private readonly DamageCapsule _damage;

        public InfoCollision(DamageCapsule damage, Vector3 dir = default)
        {
            _damage = damage;
            _dir = dir;
        }

        public Vector3 Dir => _dir;

        public DamageCapsule Damage => _damage;
    }
}
