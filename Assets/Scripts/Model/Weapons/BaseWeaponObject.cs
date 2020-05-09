﻿using UnityEngine;


namespace Game
{
    public abstract class BaseWeaponObject : BaseObjectScene
    {
        
        public BaseAmmuObject Ammunition;
        public Clip Clip; //Запас патронов и объем обоймы
        
        public AmmunitionType[] AmmunitionTypes =
        { 
            AmmunitionType.Bullet
        };

        [SerializeField] protected Transform _barrel; //Ссылка на позицию источника выстрела
        [SerializeField] protected float _force = 999.0f; //Сила запущенного снаряда
        [SerializeField] protected float _rechergeTime = 0.2f; //Время между выстрелами 
        [SerializeField] protected int _startClip = 180;
        [SerializeField] protected int _clipVolume = 30;
        [SerializeField] protected bool _unlimitedCharge = false; 
        protected bool _isReady = true;
        protected ITimeRemaining _timeRemaining;
        
        private void Start()
        {
            _timeRemaining = new TimeRemaining(ReadyShoot, _rechergeTime);
            if(!_unlimitedCharge) Clip.TotalAmmunition = _startClip;            
            else Clip.TotalAmmunition = 999;            
            Clip.VolumeAmmunition = _clipVolume;
            Clip.CountAmmunition = 0;
            ReloadClip();
        }


        /// <summary>
        /// Выстрел
        /// </summary>
        public abstract void Fire();


        /// <summary>
        /// Готовность стрелять
        /// </summary>
        protected void ReadyShoot()
        {
            _isReady = true;
        }
                
        /// <summary>
        /// Перезаряжает обойму новыми снарядами
        /// </summary>
        public void ReloadClip()
        {
            if (!_unlimitedCharge)
            {
                if (Clip.TotalAmmunition < 1) return;                   //при пустом общем запасе не можем зарядиться  #todo вывод звука 
                Clip.TotalAmmunition += Clip.CountAmmunition;           //возвращаем запас ищ обоймы обратно в тотал
                if (Clip.TotalAmmunition >= Clip.VolumeAmmunition)      //если хватает на полную перезарядку, полностью перезаряжаем
                {
                    Clip.CountAmmunition = Clip.VolumeAmmunition;
                    Clip.TotalAmmunition -= Clip.VolumeAmmunition;
                }
                else                                                    //загружаем остаток 
                {
                    Clip.CountAmmunition = Clip.TotalAmmunition;
                }
            }
            else
            {
                Clip.CountAmmunition = Clip.VolumeAmmunition;
            }
                
        }

        public void AddAmmo(int value)
        {
            if (!_unlimitedCharge)
                Clip.TotalAmmunition += value;
        }
        
    }
}
