using UnityEngine;

namespace _Scripts.PlayerLogic
{
    [RequireComponent(typeof(Animator))]
    public abstract class PlayerAnimationController : MonoBehaviour
    {
        [HideInInspector] public Animator animator;
        [SerializeField] private AnimationClip shootingAnim;

        private readonly int _walkingId = Animator.StringToHash("isWalking");
        public readonly int ShootId = Animator.StringToHash("Shoot");

        protected void Awake()
        {
            animator = GetComponent<Animator>();
        }

        protected void SetWalkAnimation(int state)
        {
            animator.SetInteger(_walkingId, state);
        }

        public float GetShootingAnimLenght()
        {
            return shootingAnim.length;
        }
    }
}