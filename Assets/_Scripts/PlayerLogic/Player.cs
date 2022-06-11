namespace _Scripts.PlayerLogic
{
    public class Player : PlayerNavMeshController
    { 
        public static Player Instance { get; private set; }

        protected new void Awake()
        {
            base.Awake();

            if (Instance == null)
            {
                Instance = this;
            } 
            else 
            {
                Destroy(gameObject);
            }
        }
    }
}