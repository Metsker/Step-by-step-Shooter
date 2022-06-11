using _Scripts.Stage;
using _Scripts.Utility;
using _Scripts.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.EnemyLogic
{
    [RequireComponent(typeof(RagDollSwitcher))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private Slider healthBar;
        
        [HideInInspector]
        public bool isKilled;
        private int _maxHeath;
        private RagDollSwitcher _ragDollSwitcher;

        private void Awake()
        {
            _ragDollSwitcher = GetComponent<RagDollSwitcher>();
            healthBar.maxValue = health;
            healthBar.value = health;
            _maxHeath = health;
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Bullet>(out var b)) return;

            TakeDamage(b.damage);

            if (health != 0) return;
            OnKilled();
        }

        private void TakeDamage(int damage)
        {
            health = Mathf.Clamp(health - damage, 0, _maxHeath);
            healthBar.value = health;
        }
        
        private void OnKilled()
        {
            isKilled = true;
            healthBar.gameObject.SetActive(false);
            _ragDollSwitcher.EnableRagDoll();
            if (StagesManager.Instance.IsAnyEnemiesOnStage()) return;
            GameStateManager.CurrentGameState = GameStateManager.GameState.Running;
        }
    }
}
