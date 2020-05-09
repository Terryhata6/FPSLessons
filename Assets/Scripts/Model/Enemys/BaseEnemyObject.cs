using System;
using UnityEngine;

namespace Game
{
    public abstract class BaseEnemyObject : BaseObjectScene, ICollision, ISelectObj
    {
        public event Action OnPointChange = delegate { };
        [SerializeField] protected float _healthPoint = 100;
        [SerializeField] protected float _physicalResistance = 0.0f;
        [SerializeField] protected float _coldResistance = 0.0f;
        [SerializeField] protected float _mentalResistance = 0.0f;
        private bool _isDead;
        private float _timeToDestroy = 10.0f;
        private string _hpToString;
        private ITimeRemaining _effectTimeRemaining; //таймер наложенных состояний
        private int _isMentalBurning = 0;
        public float Hp
        {
            get => _healthPoint;
            private set => _healthPoint = value;
        }

        public void CollisionEnter(InfoCollision info)
        {
            if (_isDead) return;
            if (Hp > 0)
            {
                Hp -= info.Damage.ColdDamageValue * (1 - _coldResistance);
                Hp -= info.Damage.MentalDamageValue * (1 - _mentalResistance) * (1 + _isMentalBurning);
                Hp -= info.Damage.PhysicalDamageValue * (1 - _physicalResistance);  
                if (info.Damage.BulletEffect != 0)
                {
                    GetEffect(info.Damage.BulletEffect);
                }
            }
            if (Hp <= 0)
            {
                if (!TryGetComponent<Rigidbody>(out _))
                {
                    gameObject.AddComponent<Rigidbody>();
                }
                Destroy(gameObject, _timeToDestroy);

                OnPointChange.Invoke();
                _isDead = true;
            }
            
        }

        public string GetMessage()
        {            
            return $"{gameObject.name} - {Hp:0}";//
        }

        public void GetEffect(BulletEffects effect)
        {
            ///Получил эффект
            switch(effect)
            {
                case BulletEffects.Freeze: Hp -= 33 * (1 - _coldResistance); break;
                case BulletEffects.MentalBurn: if (_isMentalBurning < 3) _isMentalBurning++; break;
                default: return;
            }
        }
    }
}
