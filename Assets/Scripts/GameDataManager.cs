using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{

    [System.Serializable]
    public class SaveData
    {
        public BestScoreManager.BestScoreEntry[] bestScoreArray;
    }


    public SaveData saveData { get; private set; }

    public static GameDataManager instance { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        }
        else
        {
            //The file does not exists so init the save data
            InitializeSaveData();
        }
    }


    public void SaveGameData()
    {
        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }


    private void InitializeSaveData()
    {
        saveData.bestScoreArray = new BestScoreManager.BestScoreEntry[BestScoreManager.bestScoreArrayLength];
        for (int i = 0; i < BestScoreManager.bestScoreArrayLength; i++)
        {
            BestScoreManager.BestScoreEntry entry = new BestScoreManager.BestScoreEntry();
            entry.name = string.Empty;
            entry.score = GameManager.invalidScore;
            saveData.bestScoreArray[i] = entry;
        }
    }

}
