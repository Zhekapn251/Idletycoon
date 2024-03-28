using System.IO;
using UnityEngine;

namespace Services
{
    public static class JsonDataHandler
    {
        // Путь к файлу сохранения

        // Сохранение данных в JSON файл
        public static void SaveToJson<T>(T dataToSave, string path)
        {
            string SaveFilePath =
                Path.Combine(Application.persistentDataPath, path);
            string jsonData = JsonUtility.ToJson(dataToSave, true);
            File.WriteAllText(SaveFilePath, jsonData);
        }

        // Загрузка данных из JSON файла
        public static T LoadFromJson<T>(string path)
        {
            string SaveFilePath =
                Path.Combine(Application.persistentDataPath, path);
            if (File.Exists(SaveFilePath))
            {
                string jsonData = File.ReadAllText(SaveFilePath);
                return JsonUtility.FromJson<T>(jsonData);
            }

            Debug.LogError("Save file not found");
            return default;
        }
    }

}