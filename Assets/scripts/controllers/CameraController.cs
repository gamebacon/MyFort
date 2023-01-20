using System;
using System.Security.Authentication.ExtendedProtection;
using Cinemachine;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField] private HumanoidLandInput _input;
    
    private CinemachineVirtualCamera _activeCamera;
    private int _activeCameraPriorityModifier = 31337;

    [SerializeField] private Camera MainCamera;
    [SerializeField] private CinemachineVirtualCamera camThirdPerson;
    [SerializeField] private CinemachineVirtualCamera camFirstPerson;

    private void Start()
    {
        _activeCamera = camFirstPerson;
        _activeCamera.Priority = _activeCameraPriorityModifier;
        ChangeCamera();
    }

    private void Update()
    {
        if (_input.ChangeCameraWasPressThisFrame)
        {
            ChangeCamera();
        }
    }

    private void ChangeCamera()
    {
        _activeCamera.Priority -= _activeCameraPriorityModifier;
        
        if (_activeCamera == camFirstPerson)
        {
            _activeCamera = camThirdPerson;
        }
        else
        {
            _activeCamera = camFirstPerson;
        }

        _activeCamera.Priority += _activeCameraPriorityModifier;
        return;
        if (camThirdPerson == _activeCamera)
        {
            SetCamerPriority(camThirdPerson, camFirstPerson);
        } else if (camFirstPerson == _activeCamera)
        {
            SetCamerPriority(camFirstPerson, camThirdPerson);
        }
    }

    private void SetCamerPriority(CinemachineVirtualCamera currentMode, CinemachineVirtualCamera newMode)
    {
        currentMode.Priority -= _activeCameraPriorityModifier;
        newMode.Priority += _activeCameraPriorityModifier;
        _activeCamera = newMode;
    }
}
