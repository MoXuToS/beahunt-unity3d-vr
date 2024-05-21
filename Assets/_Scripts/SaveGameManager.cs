using UnityEngine;
using System.IO;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager instance;

    [System.Serializable]
    public class SaveData
    {
        public int playerLevel;
        public int playerScore;
        public string[] learnedSpells;
    }

    private SaveData saveData = new SaveData();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        string jsonData = JsonUtility.ToJson(saveData);
        string filePath = Application.persistentDataPath + "/saveData.json";
        
        // Создание или перезапись файла и запись данных в него
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(jsonData);
            }
        }
    }


    public void LoadGame()
    {
        if(File.Exists(Application.persistentDataPath + "/saveData.json"))
        {
            string jsonData = File.ReadAllText(Application.persistentDataPath + "/saveData.json");
            saveData = JsonUtility.FromJson<SaveData>(jsonData);
        }
    }

    public void SetPlayerLevel(int level)
    {
        saveData.playerLevel = level;
    }

    public void SetPlayerScore(int score)
    {
        saveData.playerScore = score;
    }

    public void SetLearnedSpells(string[] LearnedSpells)
    {
        saveData.learnedSpells = LearnedSpells;
    }

    public int GetPlayerLevel()
    {
        return saveData.playerLevel;
    }

    public int GetPlayerScore()
    {
        return saveData.playerScore;
    }

    public string[] GetLearnedSpells()
    {
        return saveData.learnedSpells;
    }
}