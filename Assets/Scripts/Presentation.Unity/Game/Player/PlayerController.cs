using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AChildsCourage.Game.Input {
    public class PlayerController : MonoBehaviour {

        #region Fields

        [SerializeField] private Transform playerTransform;

        private Vector3 movement;

        public UserControls controls;

        private float _movementSpeed;
        private bool _isMoving;


        #endregion

        #region Properties

        public float MovementSpeed {
            get { return _movementSpeed; }
            set { _movementSpeed = value; }
        }

        public bool IsMoving {
            get { return _isMoving; }
            set { _isMoving = value; }
        }


        #endregion

        #region Methods

        private void Awake() {

            controls = new UserControls();

            controls.Player.Move.performed += _ => movement = _.ReadValue<Vector2>();
            controls.Player.Move.canceled += _ => movement = Vector3.zero;

        }

        private void Update() {
            Vector3 move = new Vector3(movement.x, 0, movement.z) * Time.deltaTime * MovementSpeed;
            transform.Translate(move, Space.World);
        }

        private void OnEnable() {
            controls.Player.Enable();
        }

        private void OnDisable() {
            controls.Player.Disable();
        }

        #endregion



    }

}
