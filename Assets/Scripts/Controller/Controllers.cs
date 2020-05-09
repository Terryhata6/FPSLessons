using UnityEngine;

namespace Game
{
    public sealed class Controllers : IInitialization
    {
        //контроллеры с индексатором
        private readonly IExecute[] _executeControllers;

        public int Length => _executeControllers.Length;

        public IExecute this[int index] => _executeControllers[index];

        public Controllers()
        {
            IMotor motor = default;
            if (Application.platform == RuntimePlatform.PS4)
            {
                //
            }
            else
            {
                motor = new UnitMotor(
                    ServiceLocatorMonoBehaviour.GetService<CharacterController>());
            }
            //в сервислокатор добавляются все сервисы и отправляются в _executeControllers
            
            ServiceLocator.SetService(new TimeRemainingController());
            ServiceLocator.SetService(new Inventory());
            ServiceLocator.SetService(new PlayerController(motor));
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new SelectorController());
            ServiceLocator.SetService(new BulletController());

            _executeControllers = new IExecute[6];

            _executeControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();

            _executeControllers[1] = ServiceLocator.Resolve<PlayerController>();

            _executeControllers[2] = ServiceLocator.Resolve<FlashLightController>();

            _executeControllers[3] = ServiceLocator.Resolve<InputController>();

            _executeControllers[4] = ServiceLocator.Resolve<SelectorController>();

            _executeControllers[5] = ServiceLocator.Resolve<BulletController>();
            
        }

        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }

            ServiceLocator.Resolve<Inventory>().Initialization();
            ServiceLocator.Resolve<InputController>().On();
            
            ServiceLocator.Resolve<PlayerController>().On();
        }
    }
}
