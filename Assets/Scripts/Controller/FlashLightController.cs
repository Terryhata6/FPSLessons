using UnityEngine;

namespace Game
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        private FlashLightModel _flashLightModel; //ссылка на модель фонарика
        
        public void Initialization()
        {
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();            
        }

        public override void On()
        {
            if (IsActive) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On();
            _flashLightModel.Switch(FlashLightActiveType.On);            
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off);            
        }

        public void Execute()
        {
            UiInterface.FlashLightUiText.Text = _flashLightModel.BatteryChargeCurrent;
            UiInterface.FlashLightUiBar.SetBar = _flashLightModel.BatteryChargeBar();
            if (!IsActive)
            {
                _flashLightModel.BatteryChargeEdit(BatteryEditType.Charge);                
                return;
            }
            _flashLightModel.Rotation();
            _flashLightModel.BatteryChargeEdit(BatteryEditType.Discharge);
            if (_flashLightModel.BatteryIsEmpty()) Off();
        }
    }
}