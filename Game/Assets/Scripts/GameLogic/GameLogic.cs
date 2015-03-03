using UnityEngine;
using System.Collections;

namespace Maniac
{
    public class GameLogic
    {
        public virtual void Start()
        {
            
        }

        public virtual void Update()
        {

        }
    }

    public class GameLogicConquer : GameLogic
    {
        public override void Start()
        {
            AIManager.Instance.SetSpawnersInvincible(false);
        }

        public override void Update()
        {
            
        }
    }

    public class GameLogicWaves : GameLogic
    {
        public override void Start()
        {
            AIManager.Instance.SetSpawnersInvincible(true);
        }

        public override void Update()
        {
            
        }
    }
}