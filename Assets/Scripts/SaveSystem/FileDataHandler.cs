using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string fullPath;
    private bool encrpyData;
    private string codeWord = "unityalexdev.com";

    public FileDataHandler(string dataDirPath, string dataFileNmae, bool encryptData)
    {
        fullPath = Path.Combine(dataDirPath, dataFileNmae);
        this.encrpyData = encryptData;
    }

    public void SaveData(GameData gameData)
    {
        try
        {
            //1.Create directory if it doesn's exist.
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //2.Convert gameData to JSON string.
            string dataToSave = JsonUtility.ToJson(gameData, true);

            if(encrpyData)
                dataToSave = EncryptDecrypt(dataToSave);

            //3.Open/create a new file.
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                //4.Write the JSON text to the file.
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToSave);
                }

            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error on trying save data to file: " + fullPath + "\n" + e);
        }
    }

    public GameData LoadData()
    {
        GameData loadData = null;

        //1.Check if the save file exist.
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                //2.Open the file.
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    //3.Read file's text content.
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (encrpyData)
                    dataToLoad = EncryptDecrypt(dataToLoad);

                //4.Convert the JSON string back into to GameData object.
                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error on trying load data to file: " + fullPath + "\n" + e);
            }
        }
        return loadData;
    }

    public void Delete()
    {
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ codeWord[i % codeWord.Length]);
        }

        return modifiedData;
    }
}
