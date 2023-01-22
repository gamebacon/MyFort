using System.Threading;
using UnityEngine;

namespace DefaultNamespace
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private Transform _handPos;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private HumanoidLandInput _input;
        [SerializeField] private Animator _animator;
        [SerializeField] private IKControl _ikControl;

        private float attachCooldown = 0.3f;

        private bool canAttack = true;
        

        private Item _heldItem;
        private Camera _cam; 

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            if (_heldItem)
            {
                if (!_input.MouseLeft)
                {
                    DropItem();
                } 
            }
            else if(_input.MouseLeft){
                Item item = GetItemInReach();

                if (item)
                {
                    UseItem(item);
                }
                else if(canAttack)
                {
                    Attack();
                } 
            }
        }

        private void Attack()
        {
            canAttack = false;
            _animator.SetTrigger("attack");
            Invoke(nameof(ResetAttack), attachCooldown);
                
            if(Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, 2, _mask))
            {

                Entity entity = hit.collider.GetComponentInParent<Entity>();
                    
                if (entity)
                {
                    entity.TakeDamage(Random.Range(5f, 10f));
                }
            }
            
        }
        
        public void ResetAttack()
        {
            canAttack = true;
        }

        private void UseItem(Item item)
        {
            
            if (item.IsGrabbable())
            {
                PickUpItem(item);
            }
            
        }

        private void PickUpItem(Item item)
        {
            _heldItem = item;
            
            item._rb.isKinematic = true;
            item.SetLayer(LayerMask.NameToLayer("HeldObject"));
            
            SetUpPosition();
            _ikControl.SetHandPos(item.transform);
        }

        public void SetUpPosition()
        {
            if (!_heldItem)
            {
                return;
            }
            
            Transform itemTransform = _heldItem.transform;
            _heldItem.Parent(_handPos.transform);
            itemTransform.position = _handPos.position;
            itemTransform.localRotation = Quaternion.identity;
        }

        private void DropItem(bool placeItem = false)
        {
            _ikControl.Ignore();
            
            _heldItem.Parent(null);
            
            _heldItem._rb.isKinematic = false;

            _heldItem.transform.localRotation = Quaternion.Euler(Vector3.up * _heldItem.transform.localEulerAngles.y);
            
            ResetValues();
        }

        private void ResetValues()
        {
            _heldItem.SetLayer(LayerMask.NameToLayer("Default"));
            _heldItem = null;
        }

        private Item GetItemInReach()
        {
            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, 10, _mask))
            {
                if (hit.collider.TryGetComponent(out Item item))
                {
                    return item;
                }
            }

            return null;
        }
    }
}