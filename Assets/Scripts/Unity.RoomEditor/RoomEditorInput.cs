// GENERATED AUTOMATICALLY FROM 'Assets/Misc/RoomEditor.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Object = UnityEngine.Object;

namespace AChildsCourage.RoomEditor
{

    public class RoomEditorInput : IInputActionCollection, IDisposable
    {

        // Mouse
        private readonly InputActionMap mMouse;
        private readonly InputAction mMouseDelete;
        private readonly InputAction mMouseMove;
        private readonly InputAction mMousePlace;

        // Movement
        private readonly InputActionMap mMovement;
        private readonly InputAction mMovementHorizontal;
        private readonly InputAction mMovementVertical;

        // Zoom
        private readonly InputActionMap mZoom;
        private readonly InputAction mZoomFocus;
        private readonly InputAction mZoomScroll;
        private IMouseActions mMouseActionsCallbackInterface;
        private IMovementActions mMovementActionsCallbackInterface;
        private IZoomActions mZoomActionsCallbackInterface;

        public InputActionAsset Asset { get; }

        public MouseActions Mouse => new MouseActions(this);

        public MovementActions Movement => new MovementActions(this);

        public ZoomActions Zoom => new ZoomActions(this);

        public RoomEditorInput()
        {
            Asset = InputActionAsset.FromJson(@"{
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
            mMouse = Asset.FindActionMap("Mouse", true);
            mMousePlace = mMouse.FindAction("Place", true);
            mMouseDelete = mMouse.FindAction("Delete", true);
            mMouseMove = mMouse.FindAction("Move", true);

            // Movement
            mMovement = Asset.FindActionMap("Movement", true);
            mMovementHorizontal = mMovement.FindAction("Horizontal", true);
            mMovementVertical = mMovement.FindAction("Vertical", true);

            // Zoom
            mZoom = Asset.FindActionMap("Zoom", true);
            mZoomScroll = mZoom.FindAction("Scroll", true);
            mZoomFocus = mZoom.FindAction("Focus", true);
        }

        public void Dispose()
        {
            Object.Destroy(Asset);
        }

        public InputBinding? bindingMask { get => Asset.bindingMask; set => Asset.bindingMask = value; }

        public ReadOnlyArray<InputDevice>? devices { get => Asset.devices; set => Asset.devices = value; }

        public ReadOnlyArray<InputControlScheme> controlSchemes => Asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return Asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return Asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            Asset.Enable();
        }

        public void Disable()
        {
            Asset.Disable();
        }

        public struct MouseActions
        {

            private readonly RoomEditorInput mWrapper;

            public MouseActions(RoomEditorInput wrapper)
            {
                mWrapper = wrapper;
            }

            public InputAction Place => mWrapper.mMousePlace;

            public InputAction Delete => mWrapper.mMouseDelete;

            public InputAction Move => mWrapper.mMouseMove;

            public InputActionMap Get()
            {
                return mWrapper.mMouse;
            }

            public void Enable()
            {
                Get()
                    .Enable();
            }

            public void Disable()
            {
                Get()
                    .Disable();
            }

            public bool Enabled =>
                Get()
                    .enabled;

            public static implicit operator InputActionMap(MouseActions set)
            {
                return set.Get();
            }

            public void SetCallbacks(IMouseActions instance)
            {
                if (mWrapper.mMouseActionsCallbackInterface != null)
                {
                    Place.started -= mWrapper.mMouseActionsCallbackInterface.OnPlace;
                    Place.performed -= mWrapper.mMouseActionsCallbackInterface.OnPlace;
                    Place.canceled -= mWrapper.mMouseActionsCallbackInterface.OnPlace;
                    Delete.started -= mWrapper.mMouseActionsCallbackInterface.OnDelete;
                    Delete.performed -= mWrapper.mMouseActionsCallbackInterface.OnDelete;
                    Delete.canceled -= mWrapper.mMouseActionsCallbackInterface.OnDelete;
                    Move.started -= mWrapper.mMouseActionsCallbackInterface.OnMove;
                    Move.performed -= mWrapper.mMouseActionsCallbackInterface.OnMove;
                    Move.canceled -= mWrapper.mMouseActionsCallbackInterface.OnMove;
                }

                mWrapper.mMouseActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Place.started += instance.OnPlace;
                    Place.performed += instance.OnPlace;
                    Place.canceled += instance.OnPlace;
                    Delete.started += instance.OnDelete;
                    Delete.performed += instance.OnDelete;
                    Delete.canceled += instance.OnDelete;
                    Move.started += instance.OnMove;
                    Move.performed += instance.OnMove;
                    Move.canceled += instance.OnMove;
                }
            }

        }

        public struct MovementActions
        {

            private readonly RoomEditorInput mWrapper;

            public MovementActions(RoomEditorInput wrapper)
            {
                mWrapper = wrapper;
            }

            public InputAction Horizontal => mWrapper.mMovementHorizontal;

            public InputAction Vertical => mWrapper.mMovementVertical;

            public InputActionMap Get()
            {
                return mWrapper.mMovement;
            }

            public void Enable()
            {
                Get()
                    .Enable();
            }

            public void Disable()
            {
                Get()
                    .Disable();
            }

            public bool Enabled =>
                Get()
                    .enabled;

            public static implicit operator InputActionMap(MovementActions set)
            {
                return set.Get();
            }

            public void SetCallbacks(IMovementActions instance)
            {
                if (mWrapper.mMovementActionsCallbackInterface != null)
                {
                    Horizontal.started -= mWrapper.mMovementActionsCallbackInterface.OnHorizontal;
                    Horizontal.performed -= mWrapper.mMovementActionsCallbackInterface.OnHorizontal;
                    Horizontal.canceled -= mWrapper.mMovementActionsCallbackInterface.OnHorizontal;
                    Vertical.started -= mWrapper.mMovementActionsCallbackInterface.OnVertical;
                    Vertical.performed -= mWrapper.mMovementActionsCallbackInterface.OnVertical;
                    Vertical.canceled -= mWrapper.mMovementActionsCallbackInterface.OnVertical;
                }

                mWrapper.mMovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Horizontal.started += instance.OnHorizontal;
                    Horizontal.performed += instance.OnHorizontal;
                    Horizontal.canceled += instance.OnHorizontal;
                    Vertical.started += instance.OnVertical;
                    Vertical.performed += instance.OnVertical;
                    Vertical.canceled += instance.OnVertical;
                }
            }

        }

        public struct ZoomActions
        {

            private readonly RoomEditorInput mWrapper;

            public ZoomActions(RoomEditorInput wrapper)
            {
                mWrapper = wrapper;
            }

            public InputAction Scroll => mWrapper.mZoomScroll;

            public InputAction Focus => mWrapper.mZoomFocus;

            public InputActionMap Get()
            {
                return mWrapper.mZoom;
            }

            public void Enable()
            {
                Get()
                    .Enable();
            }

            public void Disable()
            {
                Get()
                    .Disable();
            }

            public bool Enabled =>
                Get()
                    .enabled;

            public static implicit operator InputActionMap(ZoomActions set)
            {
                return set.Get();
            }

            public void SetCallbacks(IZoomActions instance)
            {
                if (mWrapper.mZoomActionsCallbackInterface != null)
                {
                    Scroll.started -= mWrapper.mZoomActionsCallbackInterface.OnScroll;
                    Scroll.performed -= mWrapper.mZoomActionsCallbackInterface.OnScroll;
                    Scroll.canceled -= mWrapper.mZoomActionsCallbackInterface.OnScroll;
                    Focus.started -= mWrapper.mZoomActionsCallbackInterface.OnFocus;
                    Focus.performed -= mWrapper.mZoomActionsCallbackInterface.OnFocus;
                    Focus.canceled -= mWrapper.mZoomActionsCallbackInterface.OnFocus;
                }

                mWrapper.mZoomActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Scroll.started += instance.OnScroll;
                    Scroll.performed += instance.OnScroll;
                    Scroll.canceled += instance.OnScroll;
                    Focus.started += instance.OnFocus;
                    Focus.performed += instance.OnFocus;
                    Focus.canceled += instance.OnFocus;
                }
            }

        }

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