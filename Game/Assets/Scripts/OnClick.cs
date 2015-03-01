using UnityEngine;
using System.Collections;

namespace Maniac
{
    public class OnClick : MonoBehaviour
    {

        public void LoadScene(int level)
        {
            Application.LoadLevel(level);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void LoadOptions(GameObject textObject)
        {
            textObject.SetActive(true);
        }
    }
}