using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UtilityScripts;


namespace GameLogic
{
    public class BestScoreManager : MonoBehaviour
    {
        public static BestScoreManager instance { get; private set; }

        [System.Serializable]
        public class BestScoreEntry
        {
            public string name { get; set; }
            public int score { get; set; }
        }

        public const int bestScoreArrayLength = 5;
        public BestScoreEntry[] bestScoreArray { get; private set; }


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            // end of new code

            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }


        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == ScreenManager.sceneNumberTitleScreen)
            {
                FillBestScoreArrayFromGameData();
                Debugging.instance.DebugLog("BestScoreManager.OnSceneLoaded: " + scene.name + " mode " + mode);
            }
        }



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }


        // Update is called once per frame
        void Update()
        {

        }


        public void CompareScoreWithBestScores(string name, int score)
        {
            if (string.IsNullOrEmpty(name) || score < 0)
            {
                return;
            }

            //Go through the current list of best scores to see if the new score is better than one of the current best scores
            for (int i = 0; i < bestScoreArrayLength; i++)
            {
                if (score > bestScoreArray[i].score)
                {
                    //score is greater than current entry. Shift down by one all entries starting with the current entry
                    for (int j = (bestScoreArrayLength - 1); j > i; j--)
                    {
                        bestScoreArray[j].score = bestScoreArray[j - 1].score;
                        bestScoreArray[j].name = bestScoreArray[j - 1].name;
                    }

                    //Store the score at the current index
                    bestScoreArray[i].score = score;
                    bestScoreArray[i].name = name;
                    FillGameDataFromBestScoreArray();
                }
            }
        }


        private void FillBestScoreArrayFromGameData()
        {
            bestScoreArray = new BestScoreEntry[bestScoreArrayLength];
            for (int i = 0; i < bestScoreArrayLength; i++)
            {
                BestScoreEntry entry = new BestScoreEntry();

                entry.name = GameDataManager.instance.saveData.bestScoreArray[i].name;
                entry.score = GameDataManager.instance.saveData.bestScoreArray[i].score;
                bestScoreArray[i] = entry;
            }
        }


        private void FillGameDataFromBestScoreArray()
        {
            bestScoreArray = new BestScoreEntry[bestScoreArrayLength];
            for (int i = 0; i < bestScoreArrayLength; i++)
            {
                GameDataManager.instance.saveData.bestScoreArray[i] = bestScoreArray[i];
            }
        }

    }

}

