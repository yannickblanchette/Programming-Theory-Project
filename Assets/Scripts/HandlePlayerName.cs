using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UtilityScripts;

namespace GameLogic
{
    public class HandlePlayerName : MonoBehaviour
    {
        public static HandlePlayerName instance { get; private set; }

        private TMP_InputField playerNameField;

        //Called first
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            // end of new code

            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("HandlePlayerName.Awake called");
        }


        //Called third
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == ScreenManager.sceneNumberTitleScreen)
            {
                Debug.Log("HandlePlayerName.OnSceneLoaded: " + scene.name + " mode " + mode);
                InitializeGameObjectReferences();
                AddAllListeners();
            }
        }


        private void InitializeGameObjectReferences()
        {
            playerNameField = GameObject.Find("PlayerNameField").GetComponent<TMP_InputField>();
        }


        private void AddAllListeners()
        {
            playerNameField.onEndEdit.AddListener(HandlePlayerNameEdited);
        }


        // ABSTRACTION
        //Once the player entered or modified the name in the input field then save it in the Game Manager
        public void HandlePlayerNameEdited(string text)
        {
            if (!string.IsNullOrEmpty(playerNameField.text))
            {
                Debugging.instance.DebugLog(playerNameField.text);
                GameManager.instance.playerName = playerNameField.text;
            }
        }
    }

}

