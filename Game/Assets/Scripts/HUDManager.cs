using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManager : MonoBehaviour {
    private static HUDManager _instance;
    public static HUDManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<HUDManager>();
            return _instance;
        }
    }

    #region variables

    [SerializeField]
    private RectTransform healthBar;

    [SerializeField]
    private Text ammoText;

    [SerializeField]
    private Text scoreText;

    #endregion

    public void SetHealth(float health, float total)
    {
        float p = (float)health / total;
        healthBar.anchorMax = new Vector2(p, healthBar.anchorMax.y);
        healthBar.offsetMax = new Vector2(0, 0);
    }

    public void SetAmmo(int clip, int remaining)
    {
        ammoText.text = clip + " / " + remaining;
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
