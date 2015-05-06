using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelMenuItem : MonoBehaviour {

    public Image LevelImage;
    public Text LevelName;
    public Text LevelScore;
    public Toggle ConqueredToggle;

    public Button Button;

	public void SetLevel(Sprite image, string name, int score, bool conquered, bool unlocked)
    {
        LevelImage.sprite = image;
        LevelName.text = name;
        LevelScore.text = "Score: " + score;
        ConqueredToggle.isOn = conquered;
        Button.interactable = unlocked;
    }

    public void OnClick()
    {

    }
}
