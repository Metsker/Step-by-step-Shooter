using System;
using System.Threading.Tasks;
using _Scripts.PlayerLogic;
using _Scripts.Utility;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Weapon
{
    public class Gun : BulletPooler
    {
        [Range(10, 100)] [SerializeField] private float bulletSpeed = 50;
        
        [HideInInspector] public bool isGunShooting;
        
        [Range(1, 1000)] private const int ShootDelayModifier = 300;
        private const int ShootRange = 100;

        public async void Shoot(Ray ray)
        {
            isGunShooting = true;

            #region Physics
            
            var point = 
                Physics.Raycast(ray, out var hit, ShootRange) ? hit.point : ray.direction * ShootRange;

            #endregion
            
            #region Animations
        
            Player.Instance.transform.DOLookAt(point, 0.5f, AxisConstraint.Y);
            Player.Instance.animator.SetTrigger(Player.Instance.ShootId);
            var shootDelay = (int) (Player.Instance.GetShootingAnimLenght() * ShootDelayModifier);
            
            await Task.Delay(shootDelay);
            
            #endregion

            #region Pooling
            
            var b = GetBullet();
            b.transform.DOMove(point, point.magnitude / bulletSpeed).OnComplete(() => ReleaseBullet(b));

            #endregion
            
            await Task.Delay((int)(Player.Instance.GetShootingAnimLenght() * 1000 - shootDelay));
            
            isGunShooting = false;
        }
    }
}