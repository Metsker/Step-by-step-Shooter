namespace _Scripts.Weapon.Commands
{
    public abstract class Command
    {
        public abstract void Execute();
        public abstract bool IsInProcess { get; }
    }
}