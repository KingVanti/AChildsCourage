// GENERATED AUTOMATICALLY FROM 'Assets/Misc/RoomEditor.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace AChildsCourage.RoomEditor
{
    public class @RoomEditorInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @RoomEditorInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""RoomEditor"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""4960f215-c838-49bd-87dc-e480b9c0eb33"",
            ""actions"": [
                {
                    ""name"": ""Place"",
                    ""type"": ""Button"",
                    ""id"": ""d9286359-014b-4818-bb64-de9d41ad180e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Delete"",
                    ""type"": ""Button"",
                    ""id"": ""892da7af-b281-41a6-bfb0-bbfe8bbe5e36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ae3aa284-2e7d-4234-80a2-0709f72f8e8b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""512491cd-cd98-4215-8d47-101a467e1d65"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d56ec763-0666-407a-aee6-afc6ab26bb11"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Delete"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc3a11d6-b60a-4b3e-acb5-76b3bc712476"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Mouse
            m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
            m_Mouse_Place = m_Mouse.FindAction("Place", throwIfNotFound: true);
            m_Mouse_Delete = m_Mouse.FindAction("Delete", throwIfNotFound: true);
            m_Mouse_Move = m_Mouse.FindAction("Move", throwIfNotFound: true);
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

        // Mouse
        private readonly InputActionMap m_Mouse;
        private IMouseActions m_MouseActionsCallbackInterface;
        private readonly InputAction m_Mouse_Place;
        private readonly InputAction m_Mouse_Delete;
        private readonly InputAction m_Mouse_Move;
        public struct MouseActions
        {
            private @RoomEditorInput m_Wrapper;
            public MouseActions(@RoomEditorInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Place => m_Wrapper.m_Mouse_Place;
            public InputAction @Delete => m_Wrapper.m_Mouse_Delete;
            public InputAction @Move => m_Wrapper.m_Mouse_Move;
            public InputActionMap Get() { return m_Wrapper.m_Mouse; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
            public void SetCallbacks(IMouseActions instance)
            {
                if (m_Wrapper.m_MouseActionsCallbackInterface != null)
                {
                    @Place.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnPlace;
                    @Place.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnPlace;
                    @Place.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnPlace;
                    @Delete.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnDelete;
                    @Delete.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnDelete;
                    @Delete.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnDelete;
                    @Move.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnMove;
                }
                m_Wrapper.m_MouseActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Place.started += instance.OnPlace;
                    @Place.performed += instance.OnPlace;
                    @Place.canceled += instance.OnPlace;
                    @Delete.started += instance.OnDelete;
                    @Delete.performed += instance.OnDelete;
                    @Delete.canceled += instance.OnDelete;
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                }
            }
        }
        public MouseActions @Mouse => new MouseActions(this);
        public interface IMouseActions
        {
            void OnPlace(InputAction.CallbackContext context);
            void OnDelete(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
        }
    }
}
