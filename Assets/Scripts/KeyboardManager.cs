using UnityEngine;


namespace GameLogic
{
    public class KeyboardManager : MonoBehaviour
    {
        public static KeyboardManager instance { get; private set; }


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            // end of new code

            instance = this;
        }


        public bool IsSpacebarPressed()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}

