using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Maniac
{
    public class GameEndScreen : MonoBehaviour
    {
        private static GameEndScreen _instance;
        private static GameEndScreen Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<GameEndScreen>();
                return _instance;
            }
        }


        [SerializeField]
        private Text LastMessage;
        [SerializeField]
        private GameObject screen;

        public static void SetMessage(string message)
        {
            Instance.LastMessage.text = message;
        }

        public static void Show()
        {
            Instance.screen.SetActive(true);
            Instance.LastMessage.fontSize = Screen.height / 30;
        }

        public static void Hide()
        {
            Instance.screen.SetActive(false);
        }

        public void ButtonMainMenu()
        {
            Application.LoadLevel("MainMenu");
        }

        public void ButtonRestartLevel()
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
}
