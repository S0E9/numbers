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
        // private Keyswitch[] _pressedKeyswitches; // NKRO UNIMPLIMENTED
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
            _userInput.User.ClickPress.started += ctx => StartClick(ctx);
            _userInput.User.ClickPress.canceled += ctx => EndTouch(ctx);
            _userInput.User.Numpad.started += ctx => NumpadPressed(ctx);
            _userInput.User.Numpad.canceled += ctx => NumpadReleased(ctx);
            _userInput.User.Keyboard.started += ctx => KeyboardPressed(ctx);
            _userInput.User.Keyboard.canceled += ctx => KeyboardReleased(ctx);
        }
        private void StartTouch(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(_userInput.User.TouchPosition.ReadValue<Vector2>());
            RaycastHit hit;

            hasBeenCalled = true;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && _pressedKeyswitch == null)
                {
                    _pressedKeyswitch = hit.collider.GetComponent<Keyswitch>();
                    _pressedKeyswitch.onPress.Invoke();
                }
            }
        }
        private void StartClick(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(_userInput.User.ClickPosition.ReadValue<Vector2>());
            RaycastHit hit;

            hasBeenCalled = true;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && _pressedKeyswitch == null)
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
        private void NumpadPressed(InputAction.CallbackContext context)
        {
            if (!hasBeenCalled)
            {
                hasBeenCalled = true;
            }    
            else if (_pressedKeyswitch == null && hasBeenCalled)
            {
                _pressedKeyswitch = context.control.name switch
                {
                    "numpad0" =>        GameObject.Find("0 Key").GetComponent<Keyswitch>(),
                    "numpadPeriod" =>   GameObject.Find("1 Key").GetComponent<Keyswitch>(),
                    "numpad1" =>        GameObject.Find("2 Key").GetComponent<Keyswitch>(),
                    "numpad2" =>        GameObject.Find("3 Key").GetComponent<Keyswitch>(),
                    "numpad3" =>        GameObject.Find("4 Key").GetComponent<Keyswitch>(),
                    "numpadEnter" =>    GameObject.Find("5 Key").GetComponent<Keyswitch>(),
                    "numpad4" =>        GameObject.Find("6 Key").GetComponent<Keyswitch>(),
                    "numpad5" =>        GameObject.Find("7 Key").GetComponent<Keyswitch>(),
                    "numpad6" =>        GameObject.Find("8 Key").GetComponent<Keyswitch>(),
                    "numpad7" =>        GameObject.Find("9 Key").GetComponent<Keyswitch>(),
                    "numpad8" =>        GameObject.Find("10 Key").GetComponent<Keyswitch>(),
                    "numpad9" =>        GameObject.Find("11 Key").GetComponent<Keyswitch>(),
                    "numpadPlus" =>     GameObject.Find("12 Key").GetComponent<Keyswitch>(),
                    "numpadDivide" =>   GameObject.Find("14 Key").GetComponent<Keyswitch>(),
                    "numpadMultiply" => GameObject.Find("15 Key").GetComponent<Keyswitch>(),
                    "numpadMinus" =>    GameObject.Find("16 Key").GetComponent<Keyswitch>(),
                    _ =>                GameObject.Find("13 Key").GetComponent<Keyswitch>()
                };
                // check multiple inputs here
                _pressedKeyswitch.onPress.Invoke();
            }
        }
        private void NumpadReleased(InputAction.CallbackContext context)
        {
            if (_pressedKeyswitch != null && _pressedKeyswitch.isPressed)
            {
                _pressedKeyswitch.onRelease.Invoke();
                _pressedKeyswitch = null;
            }
        }
        private void KeyboardPressed(InputAction.CallbackContext context)
        {
            if (!hasBeenCalled)
            {
                hasBeenCalled = true;
            }
            else if (_pressedKeyswitch == null && hasBeenCalled)
            {
                _pressedKeyswitch = context.control.name switch
                {
                    "0" => GameObject.Find("0 Key").GetComponent<Keyswitch>(),
                    "period" => GameObject.Find("1 Key").GetComponent<Keyswitch>(),
                    "1" => GameObject.Find("2 Key").GetComponent<Keyswitch>(),
                    "2" => GameObject.Find("3 Key").GetComponent<Keyswitch>(),
                    "3" => GameObject.Find("4 Key").GetComponent<Keyswitch>(),
                    "enter" => GameObject.Find("5 Key").GetComponent<Keyswitch>(),
                    "4" => GameObject.Find("6 Key").GetComponent<Keyswitch>(),
                    "5" => GameObject.Find("7 Key").GetComponent<Keyswitch>(),
                    "6" => GameObject.Find("8 Key").GetComponent<Keyswitch>(),
                    "7" => GameObject.Find("9 Key").GetComponent<Keyswitch>(),
                    "9" => GameObject.Find("11 Key").GetComponent<Keyswitch>(),
                    "slash" => GameObject.Find("14 Key").GetComponent<Keyswitch>(),
                    "minus" => GameObject.Find("16 Key").GetComponent<Keyswitch>(),
                    "backspace" => GameObject.Find("13 Key").GetComponent<Keyswitch>(),
                    _ => CheckModifier(context)
                };
                if (_pressedKeyswitch != null)
                {
                    Debug.Log(_pressedKeyswitch);
                    _pressedKeyswitch.onPress.Invoke();
                }                 
            }
        }
        private void KeyboardReleased(InputAction.CallbackContext context)
        {
            if (_pressedKeyswitch != null && _pressedKeyswitch.isPressed)
            {
                _pressedKeyswitch.onRelease.Invoke();
                _pressedKeyswitch = null;
            }
        }
        private Keyswitch CheckModifier(InputAction.CallbackContext context)
        {
            InputAction shiftAction = _userInput.FindAction("Shift");
            bool shiftHeld = shiftAction.ReadValue<float>() > .9f;

            if (context.control.name == "8")
                _pressedKeyswitch = shiftHeld ? _pressedKeyswitch = GameObject.Find("15 Key").GetComponent<Keyswitch>() : _pressedKeyswitch = GameObject.Find("10 Key").GetComponent<Keyswitch>();
            else
                _pressedKeyswitch = shiftHeld ? _pressedKeyswitch = GameObject.Find("12 Key").GetComponent<Keyswitch>() : _pressedKeyswitch = GameObject.Find("5 Key").GetComponent<Keyswitch>();

            return _pressedKeyswitch;
        }
    }
}
