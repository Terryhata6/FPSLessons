using UnityEngine;

namespace Game
{
    public class Bullet : BaseAmmuObject
    {
        private void OnCollisionEnter(Collision collision) // todo своя обработка полета и получения урона
        {
            // дописать доп урон
            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_projectileDamage, Rigidbody.velocity));
            }

            DestroyAmmunition();
        }
    }
}

