using System.Collections;
using _Scripts.Stage;
using _Scripts.Utility;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace _Scripts.PlayerLogic
{
    public abstract class PlayerNavMeshController : PlayerAnimationController
    {
        public Controls PlayerControls;
        private NavMeshAgent _agent;

        protected new void Awake()
        {
            base.Awake();
            PlayerControls = new Controls();
        }
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            Player.Instance.PlayerControls.Actions.Fire.performed += SetPath;
        }
        
        private void OnEnable()
        {
            PlayerControls.Enable();
        }

        private void OnDisable()
        {
            PlayerControls.Disable();
        }

        private void SetPath(InputAction.CallbackContext context)
        {
            if (StagesManager.Instance.CurrentStage == -1)
            {
                StartCoroutine(MoveToNextWayPoint());
                return;
            }
            switch (StagesManager.Instance.IsAnyEnemiesOnStage())
            {
                case false when StagesManager.Instance.CurrentStage < StagesManager.Instance.stages.Count -1:
                    StartCoroutine(MoveToNextWayPoint());
                    break;
                case false:
                    StartCoroutine(GoToWaypoint(StagesManager.Instance.finishWaypoint.position));
                    break;
            }
        }
        
        private IEnumerator MoveToNextWayPoint()
        {
            if (_agent.hasPath) yield break;
            StagesManager.Instance.CurrentStage++;

            var waypoint = StagesManager.Instance.GetWaypoint(StagesManager.Instance.CurrentStage);
            yield return GoToWaypoint(waypoint.position);

            #region LookAtEnemy
            
            var stageEnemies = StagesManager.Instance.GetCurrentStage().enemies;
            if (stageEnemies.Count > 0)
            {
                transform.DOLookAt(stageEnemies[0].transform.position, 0.5f);
            }
            
            #endregion

            GameStateManager.CurrentGameState = GameStateManager.GameState.Shooting;
        }

        private IEnumerator GoToWaypoint(Vector3 waypointPosition)
        {
            SetWalkAnimation(1);
            
            _agent.SetDestination(waypointPosition);
            
            yield return new WaitUntil(() => _agent.hasPath);
            yield return new WaitUntil(() => _agent.remainingDistance < 0.1f);
            
            SetWalkAnimation(0);
        }
    }
}
