using UnityEngine;

namespace GameLogic
{
    public class Player : MovingBody
    {
        [SerializeField] private GameObject projectile;

        private float m_horizontalSpeed = 0f;
        private float m_verticalSpeed = 10f;
        private int m_scoreIncrement = 0;
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
            ConstrainPositionY();
        }


        private void GetVerticalInput()
        {
            verticalInput = Input.GetAxis("Vertical");
        }


        public void CreateProjectile()
        {
            Vector3 projectilePosition = new Vector3(initialPositionX, gameObject.transform.position.y, initialPositionZ);

            Instantiate(projectile, projectilePosition, projectile.transform.rotation);
        }
    
    
        private void CheckSpaceBar()
        {
            if (KeyboardManager.instance.IsSpacebarPressed())
            {
                CreateProjectile();
            }
        }


        protected override void OnTriggerEnter(Collider other)
        {
            //Player has been hit so it is Game Over
            GameScreenManager.HandleGameOver();
            base.OnTriggerEnter(other);
        }


        private void ConstrainPositionY()
        {
            //If the position y would be further than the y-range, then modify vertical input to be so that the final position is the limit of y-range
            if (transform.position.y > GameScreenManager.verticalRange)
            {
                Vector3 position = transform.position;
                position.y = GameScreenManager.verticalRange;
                transform.position = position;
            }
            else if (transform.position.y < -GameScreenManager.verticalRange)
            {
                Vector3 position = transform.position;
                position.y = -GameScreenManager.verticalRange;
                transform.position = position;
            }
        }
    }
}


