using UnityEngine;

namespace _Scripts.Utility
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float lerpSpeed = 10f;
        private Vector3 _offset;

        private void Start()
        {
            _offset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            var desPos = target.position + _offset;
            var lerpPos = Vector3.Lerp(transform.position, desPos, lerpSpeed * Time.deltaTime);
            transform.position = lerpPos;
        }
    }
}
