using UnityEngine;

namespace GameLogic
{
    public class Player : MovingBody
    {
        [SerializeField] private GameObject projectile;

        private float m_horizontalSpeed = 0f;
        private float m_verticalSpeed = 5f;
        private int m_scoreIncrement = 0;
        private int numProjectiles;
        private float initialPositionX = -7f;
        private float initialPositionZ = -0.2f;
        private float verticalInput;

        public override float horizontalSpeed { get { return m_horizontalSpeed; } }
        public override float verticalSpeed { get { return m_verticalSpeed; } }
        public override int scoreIncrement { get { return m_scoreIncrement; } }


        protected override void Update()
        {
            if (GameStateManager.instance.currentGameState == GameStates.InProgress)
            {
                CheckSpaceBar();
                base.Update();
            }
        }


        protected override void MoveVertically()
        {
            GetVerticalInput();
            transform.Translate(Vector3.up * verticalSpeed * verticalInput * Time.deltaTime);
        }


        private void GetVerticalInput()
        {
            verticalInput = Input.GetAxis("Vertical");
        }


        public void CreateProjectile()
        {
            Vector3 projectilePosition = new Vector3(initialPositionX, gameObject.transform.position.y, initialPositionZ);

            Instantiate(projectile, projectilePosition, projectile.transform.rotation);
            numProjectiles++;

            if (numProjectiles >= 5)
            {
                GameScreenManager.HandleGameOver();
            }
        }
    
    
        private void CheckSpaceBar()
        {
            if (KeyboardManager.instance.IsSpacebarPressed())
            {
                CreateProjectile();
            }
        }
    }
}


