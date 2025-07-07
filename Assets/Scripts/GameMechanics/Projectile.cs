using GameLogic;
using UnityEngine;


namespace GameLogic
{
    public class Projectile : MovingBody
    {
        private float m_horizontalSpeed = 1f;
        private float m_verticalSpeed = 0f;

        public override float horizontalSpeed { get { return m_horizontalSpeed; } }
        public override float verticalSpeed { get { return m_verticalSpeed; } }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        //void Update()
        //{
        //    base.Update();
        //}


        protected void OnTriggerEnter(Collider other)
        {
 
            
            ////Check if the projectile hit an enemy or the player
            //if (other.CompareTag("Enemy"))
            //{
            //    //GameManager.instance.playerScore += 
            //}
            //else if (other.CompareTag("Player"))
            //{
            //    //Player has been hit so it is Game Over
            //    GameStateManager.instance.ProcessEvent(GameEvents.GameOver);
            //}
            ////Destroy the object that has been hit, as well as our own object
            //Destroy(gameObject);
        }
    }

}

