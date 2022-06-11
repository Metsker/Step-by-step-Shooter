using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.EnemyLogic
{
    public class RagDollSwitcher : MonoBehaviour
    {
        [SerializeField] private List<Collider> ragDollParts = new List<Collider>();
        private void Awake()
        {
            DisableRagDoll();
        }
        private void DisableRagDoll()
        {
            foreach (var c in ragDollParts)
            {
                c.enabled = false;
                if (c.attachedRigidbody == null) continue;
                c.attachedRigidbody.isKinematic = true;
            }
        }
        public void EnableRagDoll()
        {
            GetComponent<BoxCollider>().enabled = false;
            var animator = GetComponent<Animator>();
            animator.enabled = false;
            animator.avatar = null;
            foreach (var c in ragDollParts)
            {
                c.enabled = true;
                if (c.attachedRigidbody == null) continue;
                var rb = c.attachedRigidbody;
                rb.isKinematic = false;
                rb.velocity = Vector3.zero;
            }
        }
    }
}