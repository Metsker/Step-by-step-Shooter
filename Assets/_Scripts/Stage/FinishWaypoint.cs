using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Stage
{
    public class FinishWaypoint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadScene(0);
        }
    }
}
