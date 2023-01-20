using System;
using UnityEngine;

namespace controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private HumanoidLandInput _input;
        [SerializeField] private CameraController _camController;
        [SerializeField] private Transform CameraFollow; 
        [SerializeField] private Animator _animator;

        private Vector3 _playerMoveInput;
    
        private Vector3 _playerLookInput;
        private Vector3 _previousLookInput;

        private float _cameraPitch;
        
        [SerializeField] private MovementConstraints _movementConstraints;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            _playerLookInput = GetLookInput();
            PlayerLook();
            PitchCamera();
            
            _playerMoveInput = GetMoveInput();
            CheckAnimation();
            PlayerMove();
        
            _rb.AddRelativeForce(_playerMoveInput);
        }

        private Vector3 GetLookInput()
        {
            _previousLookInput = _playerLookInput;
            _playerLookInput = new Vector3(_input.LookInput.x, (_input.InvertMouseY ? -_input.LookInput.y : _input.LookInput.y), 0f);
            return Vector3.Lerp(_previousLookInput, _playerLookInput * Time.deltaTime, _movementConstraints._lookInputLerpTime);
        }

        private void PlayerLook()
        {
            _rb.rotation = Quaternion.Euler(0, _rb.rotation.eulerAngles.y + (_playerLookInput.x * _movementConstraints._rotationSpeedMultiplier), 0);
        }

        private void PitchCamera()
        {
            Vector3 rotationValues = CameraFollow.rotation.eulerAngles;
            _cameraPitch += _playerLookInput.y * _movementConstraints._pitchSpeedMultiplier;
            _cameraPitch = Mathf.Clamp(_cameraPitch, _movementConstraints.cameraPitchClamp.min, _movementConstraints.cameraPitchClamp.max);

            CameraFollow.rotation = Quaternion.Euler(_cameraPitch, rotationValues.y, rotationValues.z);
        }
        
        private void CheckAnimation()
        {
            bool isMoving = _playerMoveInput != Vector3.zero;
            
            _animator.SetBool("isWalking", isMoving && !_input.SprintInput);
            _animator.SetBool("isRunning", isMoving && _input.SprintInput);
        }
    
        private Vector3 GetMoveInput()
        {
            return new Vector3(_input.MoveInput.x, 0, _input.MoveInput.y);
        }
    
        private void PlayerMove()
        {
            
            float speed = _movementConstraints._movementMultiplier * _rb.mass * 
                          (_input.SprintInput ? _movementConstraints._movementSprintMultiplier : 1);
            
            _animator.SetFloat("speed", speed * 0.0045f);
            
            _playerMoveInput = new Vector3(
                _playerMoveInput.x * speed, 
                _playerMoveInput.y,
                _playerMoveInput.z * speed 
            );
        }
    

        [Serializable]
        private class MovementConstraints 
        {
            public float _movementSprintMultiplier = 1.7f;
            public float _movementMultiplier = 20f;
            
            public float _pitchSpeedMultiplier = 300f;
            public float _rotationSpeedMultiplier = 300f;
            public float _lookInputLerpTime = 0.35f;

            [SerializeField] public CameraPitchClamp cameraPitchClamp = new CameraPitchClamp();
        }

        [Serializable]
        private class CameraPitchClamp 
        {
            [Range(-89.9f, -70f)] public float min = -89.9f;
            [Range(25f, 89.9f)] public float max = 25f;
        }

    }
}
