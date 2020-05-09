using UnityEngine;

namespace Game
{
    public abstract class BaseAmmuObject : BaseObjectScene
    {
        [SerializeField] private float _timeToDestruct = 10.0f;
        [SerializeField] private float _physicalDamage = 10.0f;
        [SerializeField] private float _coldDamage = 0.0f;
        [SerializeField] private float _mentalDamage = 0.0f;
        protected DamageCapsule _projectileDamage; // todo доделать свой урон        
        private ITimeRemaining _timeRemaining;

        public AmmunitionType Type = AmmunitionType.Bullet;
        

        protected override void Awake()
        {
            base.Awake();
            _projectileDamage.PhysicalDamageValue = _physicalDamage;
            _projectileDamage.MentalDamageValue = _mentalDamage;
            _projectileDamage.ColdDamageValue = _coldDamage;
            _projectileDamage.BulletEffect = 0;
        }

        private void Start()
        {
            Destroy(gameObject, _timeToDestruct);
            
        }

        public void AddForce(Vector3 dir)
        {
            if (!Rigidbody) return;
            Rigidbody.AddForce(dir);
        }
               

        protected void DestroyAmmunition()
        {
            Destroy(gameObject);
            _timeRemaining.RemoveTimeRemaining();
            // Вернуть в пул
        }
    }
}
