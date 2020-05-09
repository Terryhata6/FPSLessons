using UnityEngine;

namespace Game
{
    public sealed class InputController : BaseController, IExecute
    {
        private KeyCode _activeFlashLight = KeyCode.F; //позволяет изменить хоткей не декомпилируя код
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private int _mouseButton = (int)MouseButton.LeftButton;
        private int _weaponCount = -1;
        private int _weaponCountRangeMin = -1;
        private int _weaponCountRangeMax = 1;
        private float _getWheelMove;
        
        
        
        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;//Лочит 
        }


        public void Execute()
        {
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch(); //обращаемся к сервис локатору за выбранным сервисом и просим свичнуть
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _weaponCount = 0;
                SelectWeapon(_weaponCount);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _weaponCount = 1;
                SelectWeapon(_weaponCount);
            }
            _getWheelMove = (Input.GetAxis("Mouse ScrollWheel"));
            if (_getWheelMove != 0)
            {
                if (_getWheelMove > 0) _weaponCount++;
                else if (_getWheelMove < 0) _weaponCount--;
                if (_weaponCount < _weaponCountRangeMin) _weaponCount = _weaponCountRangeMin;
                if (_weaponCount > _weaponCountRangeMax) _weaponCount = _weaponCountRangeMax;
                SelectWeapon(_weaponCount);
            }
            if (Input.GetMouseButton(_mouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().Fire();
                }
            }

            if (Input.GetKeyDown(_cancel))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<FlashLightController>().Off();
            }

            if (Input.GetKeyDown(_reloadClip))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().ReloadClip();
                }
            }           
        }

        /// <summary>
        /// Выбор оружия
        /// </summary>
        /// <param name="i">Номер оружия</param>
        private void SelectWeapon(int i)
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            if (i < 0) return;
            var tempWeapon = ServiceLocator.Resolve<Inventory>().Weapons[i]; //todo инкапсулировать
            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
            }
        }
    }
}
