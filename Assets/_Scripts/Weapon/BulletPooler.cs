using MyPooler;
using UnityEngine;

namespace _Scripts.Weapon
{
    public abstract class BulletPooler : MonoBehaviour
    {
        [SerializeField] private Transform bulletSpawnPoint;
        
        private const string Tag = "Bullet";
        
        protected GameObject GetBullet()
        {
            return ObjectPooler.Instance.GetFromPool(Tag, bulletSpawnPoint.position, Quaternion.identity);
        }

        protected void ReleaseBullet(GameObject bul)
        {
            ObjectPooler.Instance.ReturnToPool(Tag, bul);
        }
    }
}
