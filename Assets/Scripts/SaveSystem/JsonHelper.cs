using UnityEngine;
using System.IO;

public static class JsonHelper
{
    private static readonly string filePath = Path.Combine(Application.persistentDataPath, "saveData.json");

    public static void SaveData(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    public static SaveData LoadData()
    {
        if (File.Exists(filePath) == false)
            return new SaveData(); // Возвращает новый объект, если файл не найден

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
}