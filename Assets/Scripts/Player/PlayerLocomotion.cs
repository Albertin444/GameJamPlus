using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace GameJamPlus.FinalCharacterController
{

    [DefaultExecutionOrder(-2)]
    public class PlayerLocomotion : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
    {
        public PlayerControls playerControls {  get; private set; }
        public Vector2 MovementInput { get; private set; }
        public Vector2 LookInput { get; private set; }

        private void OnEnable()
        {
            playerControls = new PlayerControls();
            playerControls.Enable();

            playerControls.PlayerLocomotionMap.Enable();
            playerControls.PlayerLocomotionMap.SetCallbacks(this);
        }

        private void OnDisable()
        {
            playerControls.PlayerLocomotionMap.Disable();
            playerControls.PlayerLocomotionMap.RemoveCallbacks(this);
        }


        public void OnMovement(InputAction.CallbackContext context)
        {
           MovementInput = context.ReadValue<Vector2>();
            print(MovementInput);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            LookInput = context.ReadValue<Vector2>();
        }
    }
}