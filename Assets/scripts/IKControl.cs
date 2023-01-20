using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
public class IKControl : MonoBehaviour {
    
    public Transform lookTarget;
    private Animator _animator;

    private Transform _handTarget;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if(lookTarget) {
            _animator.SetLookAtWeight(1);
            _animator.SetLookAtPosition(lookTarget.position);
        }


        if (_handTarget)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _handTarget.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _handTarget.rotation);
        }
    }
            
    
    public void Ignore()
    {
        _handTarget = null;
        // _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
    }

    public void SetHandPos(Transform pos)
    {
        _handTarget = pos;
    }
    
}