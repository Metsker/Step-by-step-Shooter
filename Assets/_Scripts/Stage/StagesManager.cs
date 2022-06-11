using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Stage
{
    public class StagesManager : MonoBehaviour
    {
        public List<StageData> stages = new List<StageData>();
        [Space]
        public Transform finishWaypoint;
        public int CurrentStage { get; set; } = -1;
        public static StagesManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            } 
            else 
            {
                Destroy(gameObject);
            }
        }

        public Transform GetWaypoint(int stage)
        {
            return stages[stage].waypoint;
        }

        public StageData GetCurrentStage()
        {
            return stages[CurrentStage];
        }

        public bool IsAnyEnemiesOnStage()
        {
            return GetCurrentStage().enemies.Any(e => !e.isKilled);
        }
        
        #region Editor

        private void OnValidate()
        {
            for (var i = 0; i < stages.Count; i++)
            {
                stages[i].name = $"Stage {i + 1}";
            }
        }

        #endregion
    }
}