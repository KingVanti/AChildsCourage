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
        },
        {
            ""name"": ""Movement"",
            ""id"": ""abfd69af-1afc-424d-bffe-9a5005637b7c"",
            ""actions"": [
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Value"",
                    ""id"": ""55bddb43-cfea-4630-a431-0c55e414cdbb"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical"",
                    ""type"": ""Value"",
                    ""id"": ""65079ace-c069-4c70-9c8b-cc9ebf17ea13"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AD"",
                    ""id"": ""afacd57e-a565-4aa8-b141-10b6296d32b0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""23508df0-2e1c-4c4a-8186-a586c0eceb2e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e102a305-1bff-43d6-8bf3-a505bb88e7c5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WS"",
                    ""id"": ""a1e4eddd-cd52-4acb-bb4c-ac74156410f8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ff56a4af-8a75-4501-a837-4491dd6d880c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2026583e-5599-4485-a31b-dd10cd272410"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Zoom"",
            ""id"": ""190a35c5-ec46-40d0-80ae-d73c7e490554"",
            ""actions"": [
                {
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""7446b1ed-4d17-4492-b4d4-27c7313f529a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Focus"",
                    ""type"": ""Button"",
                    ""id"": ""80f6d094-4b52-4d6f-b3e9-5203b24a617a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f7f54864-e115-4bad-8711-a56d098a76e1"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc482a09-c260-4708-bf66-160048fe02fd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Focus"",
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
            // Movement
            m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
            m_Movement_Horizontal = m_Movement.FindAction("Horizontal", throwIfNotFound: true);
            m_Movement_Vertical = m_Movement.FindAction("Vertical", throwIfNotFound: true);
            // Zoom
            m_Zoom = asset.FindActionMap("Zoom", throwIfNotFound: true);
            m_Zoom_Scroll = m_Zoom.FindAction("Scroll", throwIfNotFound: true);
            m_Zoom_Focus = m_Zoom.FindAction("Focus", throwIfNotFound: true);
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

        // Movement
        private readonly InputActionMap m_Movement;
        private IMovementActions m_MovementActionsCallbackInterface;
        private readonly InputAction m_Movement_Horizontal;
        private readonly InputAction m_Movement_Vertical;
        public struct MovementActions
        {
            private @RoomEditorInput m_Wrapper;
            public MovementActions(@RoomEditorInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Horizontal => m_Wrapper.m_Movement_Horizontal;
            public InputAction @Vertical => m_Wrapper.m_Movement_Vertical;
            public InputActionMap Get() { return m_Wrapper.m_Movement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
            public void SetCallbacks(IMovementActions instance)
            {
                if (m_Wrapper.m_MovementActionsCallbackInterface != null)
                {
                    @Horizontal.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnHorizontal;
                    @Horizontal.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnHorizontal;
                    @Horizontal.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnHorizontal;
                    @Vertical.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnVertical;
                    @Vertical.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnVertical;
                    @Vertical.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnVertical;
                }
                m_Wrapper.m_MovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Horizontal.started += instance.OnHorizontal;
                    @Horizontal.performed += instance.OnHorizontal;
                    @Horizontal.canceled += instance.OnHorizontal;
                    @Vertical.started += instance.OnVertical;
                    @Vertical.performed += instance.OnVertical;
                    @Vertical.canceled += instance.OnVertical;
                }
            }
        }
        public MovementActions @Movement => new MovementActions(this);

        // Zoom
        private readonly InputActionMap m_Zoom;
        private IZoomActions m_ZoomActionsCallbackInterface;
        private readonly InputAction m_Zoom_Scroll;
        private readonly InputAction m_Zoom_Focus;
        public struct ZoomActions
        {
            private @RoomEditorInput m_Wrapper;
            public ZoomActions(@RoomEditorInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Scroll => m_Wrapper.m_Zoom_Scroll;
            public InputAction @Focus => m_Wrapper.m_Zoom_Focus;
            public InputActionMap Get() { return m_Wrapper.m_Zoom; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ZoomActions set) { return set.Get(); }
            public void SetCallbacks(IZoomActions instance)
            {
                if (m_Wrapper.m_ZoomActionsCallbackInterface != null)
                {
                    @Scroll.started -= m_Wrapper.m_ZoomActionsCallbackInterface.OnScroll;
                    @Scroll.performed -= m_Wrapper.m_ZoomActionsCallbackInterface.OnScroll;
                    @Scroll.canceled -= m_Wrapper.m_ZoomActionsCallbackInterface.OnScroll;
                    @Focus.started -= m_Wrapper.m_ZoomActionsCallbackInterface.OnFocus;
                    @Focus.performed -= m_Wrapper.m_ZoomActionsCallbackInterface.OnFocus;
                    @Focus.canceled -= m_Wrapper.m_ZoomActionsCallbackInterface.OnFocus;
                }
                m_Wrapper.m_ZoomActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Scroll.started += instance.OnScroll;
                    @Scroll.performed += instance.OnScroll;
                    @Scroll.canceled += instance.OnScroll;
                    @Focus.started += instance.OnFocus;
                    @Focus.performed += instance.OnFocus;
                    @Focus.canceled += instance.OnFocus;
                }
            }
        }
        public ZoomActions @Zoom => new ZoomActions(this);
        public interface IMouseActions
        {
            void OnPlace(InputAction.CallbackContext context);
            void OnDelete(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
        }
        public interface IMovementActions
        {
            void OnHorizontal(InputAction.CallbackContext context);
            void OnVertical(InputAction.CallbackContext context);
        }
        public interface IZoomActions
        {
            void OnScroll(InputAction.CallbackContext context);
            void OnFocus(InputAction.CallbackContext context);
        }
    }
}
