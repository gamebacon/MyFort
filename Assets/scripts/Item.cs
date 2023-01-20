using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Item : MonoBehaviour
    {

        [SerializeField] private ItemType type;
        [SerializeField] private Tag[] tags;
        [HideInInspector] public Rigidbody _rb;

        private List<Tag> tagList = new List<Tag>();

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _rb = GetComponent<Rigidbody>();


            return;
            foreach(Tag tag in tags)
            {
               tagList.Add(tag); 
            }
            
        }

        public bool isOfType(ItemType type)
        {
            return type == type;
        }

        public void AddTag(Tag tag)
        {
            tagList.Add(tag);
        }
        
        public void SetType(ItemType type)
        {
            this.type = type;
        }


        public Collider GetGrabPoint()
        {
            return _collider;
        }

        public bool IsGrabbable()
        {
            return tagList.Contains(Tag.Grabbable);
        }

        public void SetLayer(int nameToLayer)
        {
            gameObject.layer = nameToLayer;
        }

        public void Parent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public ItemType getItemType()
        {
            return type;
        }
    }
    
    public enum ItemType
    {
        Wood,
        Stone
    }

    public enum Tag
    {
        Grabbable,
    }
}