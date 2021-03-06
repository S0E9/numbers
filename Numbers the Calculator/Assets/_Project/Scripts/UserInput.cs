//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/_Project/Scripts/UserInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @UserInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @UserInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UserInput"",
    ""maps"": [
        {
            ""name"": ""User"",
            ""id"": ""6c88cc32-fbfc-47b9-9cc8-611ce32bb682"",
            ""actions"": [
                {
                    ""name"": ""TouchPress"",
                    ""type"": ""Button"",
                    ""id"": ""b2999e0f-4e3d-4bf1-9b74-f5eb69bce7a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d98c7752-4af0-4a43-89bf-300f29de965a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Numpad"",
                    ""type"": ""Button"",
                    ""id"": ""5f16365e-894b-4358-bb87-c82f6fec7dfc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9c147148-68fa-442f-b30b-451914a31e1b"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c81bc08-94f1-47ff-afba-8488a286f02e"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""515dca3c-3a75-4349-9ec6-1f9b5df8a1ed"",
                    ""path"": ""<Keyboard>/numpad0"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dde06314-71fe-4393-8dcb-06aace554cb8"",
                    ""path"": ""<Keyboard>/numpadPeriod"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b41ec4de-0e69-43fa-9956-fabc4875b3c6"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b6396f5-4942-4d39-8c81-17fb1a827747"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa41e8a2-9e48-450b-9a8c-d13d00453b46"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dba63579-42d4-40fd-8383-725cb8bf9119"",
                    ""path"": ""<Keyboard>/numpadEnter"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69865600-c78d-4537-86ba-747e227f87a8"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22b1cf8c-1597-4dbf-9251-fabff12bd852"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63f0b3dd-2ffe-48c0-af3e-7731af77f773"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe455596-07bc-43e9-8b5c-18455f010fde"",
                    ""path"": ""<Keyboard>/numpad7"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5160586a-abde-4205-a376-588f794bc597"",
                    ""path"": ""<Keyboard>/numpad8"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7c53f7d-cd1d-4312-acb7-2bf35f5aa23d"",
                    ""path"": ""<Keyboard>/numpad9"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""caa27373-4588-4367-ba87-136b48a2dbf4"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21c70d0f-3909-48e8-98a5-64fe051b1138"",
                    ""path"": ""<Keyboard>/numLock"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9919f8f-647b-4c3a-9aa0-d47fcd19071c"",
                    ""path"": ""<Keyboard>/numpadDivide"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8133b439-1268-4f11-b988-57ae4f7d6f4e"",
                    ""path"": ""<Keyboard>/numpadMultiply"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afa4c434-6b91-4d49-b4a9-d44fe2e4e3bf"",
                    ""path"": ""<Keyboard>/numpadMinus"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Numpad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // User
        m_User = asset.FindActionMap("User", throwIfNotFound: true);
        m_User_TouchPress = m_User.FindAction("TouchPress", throwIfNotFound: true);
        m_User_TouchPosition = m_User.FindAction("TouchPosition", throwIfNotFound: true);
        m_User_Numpad = m_User.FindAction("Numpad", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // User
    private readonly InputActionMap m_User;
    private IUserActions m_UserActionsCallbackInterface;
    private readonly InputAction m_User_TouchPress;
    private readonly InputAction m_User_TouchPosition;
    private readonly InputAction m_User_Numpad;
    public struct UserActions
    {
        private @UserInput m_Wrapper;
        public UserActions(@UserInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchPress => m_Wrapper.m_User_TouchPress;
        public InputAction @TouchPosition => m_Wrapper.m_User_TouchPosition;
        public InputAction @Numpad => m_Wrapper.m_User_Numpad;
        public InputActionMap Get() { return m_Wrapper.m_User; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UserActions set) { return set.Get(); }
        public void SetCallbacks(IUserActions instance)
        {
            if (m_Wrapper.m_UserActionsCallbackInterface != null)
            {
                @TouchPress.started -= m_Wrapper.m_UserActionsCallbackInterface.OnTouchPress;
                @TouchPress.performed -= m_Wrapper.m_UserActionsCallbackInterface.OnTouchPress;
                @TouchPress.canceled -= m_Wrapper.m_UserActionsCallbackInterface.OnTouchPress;
                @TouchPosition.started -= m_Wrapper.m_UserActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.performed -= m_Wrapper.m_UserActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.canceled -= m_Wrapper.m_UserActionsCallbackInterface.OnTouchPosition;
                @Numpad.started -= m_Wrapper.m_UserActionsCallbackInterface.OnNumpad;
                @Numpad.performed -= m_Wrapper.m_UserActionsCallbackInterface.OnNumpad;
                @Numpad.canceled -= m_Wrapper.m_UserActionsCallbackInterface.OnNumpad;
            }
            m_Wrapper.m_UserActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchPress.started += instance.OnTouchPress;
                @TouchPress.performed += instance.OnTouchPress;
                @TouchPress.canceled += instance.OnTouchPress;
                @TouchPosition.started += instance.OnTouchPosition;
                @TouchPosition.performed += instance.OnTouchPosition;
                @TouchPosition.canceled += instance.OnTouchPosition;
                @Numpad.started += instance.OnNumpad;
                @Numpad.performed += instance.OnNumpad;
                @Numpad.canceled += instance.OnNumpad;
            }
        }
    }
    public UserActions @User => new UserActions(this);
    public interface IUserActions
    {
        void OnTouchPress(InputAction.CallbackContext context);
        void OnTouchPosition(InputAction.CallbackContext context);
        void OnNumpad(InputAction.CallbackContext context);
    }
}
