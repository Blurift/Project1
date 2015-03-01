using UnityEngine;
using System.Collections;

namespace Maniac
{
    public class DeathScreen : MonoBehaviour
    {

        public void LoadLevel(int level)
        {
            Application.LoadLevel(level);
        }
    }
}
