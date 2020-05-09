using UnityEngine;
using System;

namespace Game
{
    public sealed class SelectorController : BaseController, IExecute
    {
        //private readonly float _dedicateDistance = 20.0f;
        private GameObject _dedicatedObj;
        private ISelectObj _selectedObj;
        private Vector2 _aim;
        private Camera _cameraMain;
        private bool _isSelectedObj;
        private bool _nullString;

        public SelectorController()
        {
            _cameraMain = Camera.main;
            _aim = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        }

        public void Execute()
        {
            //if (!IsActive) return;
            if (Physics.Raycast(_cameraMain.ScreenPointToRay(_aim), out var hit))
            {
                CheckHit(hit.collider.gameObject);
                _nullString = false;
            }
            else if (!_nullString)
            {
                UiInterface.SelectorUi.Text = String.Empty;
                _nullString = true;
                _dedicatedObj = null;
                _isSelectedObj = false;
            }
        }

        public void CheckHit(GameObject obj)
        {
            
            if (obj == _dedicatedObj) return;
            _selectedObj = obj.GetComponent<ISelectObj>();
            if (_selectedObj != null)
            {
                UiInterface.SelectorUi.Text = _selectedObj.GetMessage();                
                _isSelectedObj = true;
            }
            else
            {
                UiInterface.SelectorUi.Text = String.Empty;
                _isSelectedObj = false;
            }
            _dedicatedObj = obj;
        }
    }
}
