using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NumbersTheCalculator
{
    public class InputManager : MonoBehaviour
    {
        public delegate void StartTouchEvent(Vector2 position, float timeStarted);
        public event StartTouchEvent OnStartTouch;
        public delegate void EndTouchEvent(Vector2 position, float timeStarted);
        public event EndTouchEvent OnEndTouch;

        private Camera _camera;
        private UserInput _userInput;
        private Keyswitch _pressedKeyswitch;

        private void Awake()
        {
            _userInput = new UserInput();
            _camera = Camera.main;
            //_pressedKeyswitch = new Keyswitch();
        }

        private void OnEnable()
        {
            _userInput.Enable();
        }
        private void OnDisable()
        {
            _userInput.Disable();
        }
        private void Start()
        {
            _userInput.User.TouchPress.started += ctx => StartTouch(ctx);
            _userInput.User.TouchPress.canceled += ctx => EndTouch(ctx);
        }
        private void StartTouch(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(_userInput.User.TouchPosition.ReadValue<Vector2>());
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    _pressedKeyswitch = hit.collider.GetComponent<Keyswitch>();
                    PressKey(_pressedKeyswitch);
                }
            }
        }
        private void EndTouch(InputAction.CallbackContext context)
        {
             if (_pressedKeyswitch != null)
             {
                ReleaseKey();
             }
        }
        private void PressKey(Keyswitch clickedKeyswitch)
        {
            clickedKeyswitch.onPress.Invoke();
        }
        private void ReleaseKey()
        {
            if (_pressedKeyswitch.isPressed)
            {
                _pressedKeyswitch.onRelease.Invoke();
                _pressedKeyswitch = null;
            }    
        }
    }
}
