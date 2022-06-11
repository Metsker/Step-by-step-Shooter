using UnityEngine;

namespace _Scripts.Weapon.Commands
{
    public class ShootCommand : Command
    {
        private readonly Gun _gun;
        private readonly Ray _ray;
        
        public ShootCommand(Gun gun, Ray ray)
        {
            _gun = gun;
            _ray = ray;
        }
        public override void Execute()
        {
            _gun.Shoot(_ray);
        }
        public override bool IsInProcess => _gun.isGunShooting;
    }
}