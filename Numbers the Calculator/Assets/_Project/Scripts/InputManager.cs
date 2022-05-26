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
        public bool hasBeenCalled;
        public InputAction userControls;

        private void Awake()
        {
            _userInput = new UserInput();
            userControls = _userInput.User.Numpad;
            _camera = Camera.main;
            hasBeenCalled = false;
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
           // _userInput.User.Numpad.started += ctx => KeyboardPressed(ctx);
            //_userInput.User.Numpad.canceled += ctx => KeyboardReleased(ctx);
        }
        private void StartTouch(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(_userInput.User.TouchPosition.ReadValue<Vector2>());
            RaycastHit hit;

            hasBeenCalled = true;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    _pressedKeyswitch = hit.collider.GetComponent<Keyswitch>();
                    _pressedKeyswitch.onPress.Invoke();
                }
            }
        }
        private void EndTouch(InputAction.CallbackContext context)
        {
             if (_pressedKeyswitch != null && _pressedKeyswitch.isPressed)
             {
                _pressedKeyswitch.onRelease.Invoke();
                _pressedKeyswitch = null;
            }
        }
        private void KeyboardPressed(InputAction.CallbackContext context)
        {

        }
        private void KeyboardReleased(InputAction.CallbackContext context)
        {

        }
    }
}
