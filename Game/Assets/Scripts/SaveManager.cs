using UnityEngine;
using System.Collections;
using System.IO;

public class SaveManager : MonoBehaviour {
    private static SaveManager _instance;
    public static SaveManager Instance
    {
        get { return _instance; }
    }

	// Use this for initialization
	void Awake () {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        if (File.Exists(GetSavePath()))
        {
            Debug.LogWarning("Not Implemented: Game Save Loading");
            saveInstance = GameSave.Default();
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

    }
}
