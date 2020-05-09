using UnityEngine;

namespace Game
{
    public class MentalBullet : BaseAmmuObject
    {
        protected override void Awake()
        {
            base.Awake();
            Type = AmmunitionType.ScalebleMentalBullet;
            _projectileDamage.BulletEffect = BulletEffects.MentalBurn;

        }

        private void OnCollisionEnter(Collision collision)
        {
            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_projectileDamage, Rigidbody.velocity));
            }

            DestroyAmmunition();
        }


    }
}

