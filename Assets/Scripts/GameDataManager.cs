using UnityEngine;
using System.IO;

namespace GameLogic
{
    public class GameDataManager : MonoBehaviour
    {
        [System.Serializable]
        public class BestScoreEntry
        {
            public string name;
            public int score;
        }



        [System.Serializable]
        public class SaveData
        {
            public BestScoreEntry[] bestScoreArray;
        }


        public SaveData saveData;


        // ENCAPSULATION
        public static GameDataManager instance { get; private set; }


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            // end of new code

            instance = this;
            saveData = new SaveData();
            LoadGameData();
        }


        private void LoadGameData()
        {
            string path = Application.persistentDataPath + "/savefile.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                saveData = JsonUtility.FromJson<SaveData>(json);

                //The file exists but the content is empty, so init the save data
                if (saveData.bestScoreArray == null)
                {
                    InitializeSaveData();
                }
                else
                {
                    SanitizeGameData();
                }
            }
            else
            {
                //The file does not exists so init the save data
                InitializeSaveData();
            }
        }


        // ABSTRACTION
        public void SaveGameData()
        {
            string json = JsonUtility.ToJson(saveData, true);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }


        private void InitializeSaveData()
        {
            saveData.bestScoreArray = new BestScoreEntry[BestScoreManager.bestScoreArrayLength];
            for (int i = 0; i < BestScoreManager.bestScoreArrayLength; i++)
            {
                BestScoreEntry entry = new BestScoreEntry();
                entry.name = string.Empty;
                entry.score = GameManager.invalidScore;
                saveData.bestScoreArray[i] = entry;
            }
        }


        private void SanitizeGameData()
        {
            for (int i = 0; i < BestScoreManager.bestScoreArrayLength; i++)
            {
                if (saveData.bestScoreArray[i] == null)
                {
                    BestScoreEntry entry = new BestScoreEntry();
                    entry.name = string.Empty;
                    entry.score = GameManager.invalidScore;
                    saveData.bestScoreArray[i] = entry;
                }
                else
                {
                    if (saveData.bestScoreArray[i].name == null)
                    {
                        saveData.bestScoreArray[i].name = string.Empty;
                        saveData.bestScoreArray[i].score = GameManager.invalidScore;
                    }
                }
            }
        }

    }

}

