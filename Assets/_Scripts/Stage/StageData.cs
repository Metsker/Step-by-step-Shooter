using System.Collections.Generic;
using _Scripts.EnemyLogic;
using UnityEngine;

namespace _Scripts.Stage
{
    [System.Serializable]
    public class StageData
    {
        [HideInInspector] public string name;
        public Transform waypoint;
        public List<Enemy> enemies = new List<Enemy>();
    }
}
