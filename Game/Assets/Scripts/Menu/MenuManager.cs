using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public GameObject LevelPrefab;
    public GameObject LevelGrid;

    public GameObject MainScreen;
    public GameObject LevelScreen;
    public GameObject OptionsScreen;

	// Use this for initialization
	void Start () {
        GameSave gameSave = SaveManager.Instance.GetSave();
        int completedLevels = gameSave.CompletedLevels();

        for (int i = 0; i < gameSave.Levels.Length; i++)
        {
            Level lvl = gameSave.Levels[i];
            GameObject goLevel = (GameObject)Instantiate(LevelPrefab);

            LevelMenuItem levelMenuItem = goLevel.GetComponent<LevelMenuItem>();
            levelMenuItem.SetLevel(null, lvl.LevelName, lvl.ScoreFromSurvival, lvl.ConquerCompleted, (lvl.LevelsToUnlock <= completedLevels));
            goLevel.transform.SetParent(LevelGrid.transform);

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void HideScreens()
    {
        MainScreen.SetActive(false);
        LevelScreen.SetActive(false);
        OptionsScreen.SetActive(false);
    }

    public void ShowLevelScreen()
    {
        HideScreens();

        LevelScreen.SetActive(true);
    }
}
