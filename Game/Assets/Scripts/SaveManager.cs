using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour {
    private static SaveManager _instance;
    public static SaveManager Instance
    {
        get { return _instance; }
    }

    private XmlSerializer serializer;

	// Use this for initialization
	void Awake () {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        serializer = new XmlSerializer(typeof(GameSave));

        if (File.Exists(GetSavePath()))
        {
            Load();
        }
        else
            saveInstance = GameSave.Default();

	}

    private GameSave saveInstance;

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "unlock.dat");
    }

    public GameSave GetSave()
    {
        return saveInstance;
    }

    public void ResetAll()
    {

    }

    public void Save()
    {
        string path = GetSavePath();

        StreamWriter writer = new StreamWriter(path);
        serializer.Serialize(writer, saveInstance);
        writer.Close();
    }

    private void Load()
    {
        StreamReader reader = new StreamReader(GetSavePath());
        saveInstance = (GameSave)serializer.Deserialize(reader);
    }
}
