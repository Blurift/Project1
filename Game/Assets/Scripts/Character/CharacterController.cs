using UnityEngine;
using System.Collections;
using Blurift;

namespace Maniac
{
    [RequireComponent(typeof(MovementController), typeof(Health))]
    public class CharacterController : MonoBehaviour, EntityEventListener
    {

        MovementController moveController;
        CameraController camera;
       

        private Weapon currentWeapon;
		private Weapon secondaryWeapon;
        public Transform WeaponPlacement;

        private EntityEventManager events;

        void Start()
        {
            moveController = GetComponent<MovementController>();
            camera = FindObjectOfType<CameraController>();
            camera.Target = transform;

            events = GetComponent<EntityEventManager>();
            events.AddListener("HealthDamage", this);
            events.AddListener("HealthDead", this);
        }

        // Update is called once per frame
        void Update()
        {
            if (gameObject.GetComponent<Health>().isAlive == Health.State.Alive)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    currentWeapon.Reload();
                }
				if (Input.GetKeyDown(KeyCode.E))
				{
					SwapWeapons();
					currentWeapon.UpdateAmmo();
				}
                if (Input.GetButton("Fire1"))
                {
                    if (!(currentWeapon.CheckForReload()))
                    {

                    }
                    else
                    {
                        currentWeapon.Fire();

                    }


                    //Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
                }

                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");
                Vector2 moveV = new Vector2(x, y);
                if (moveV != Vector2.zero)
                {
                    moveController.Move(moveV);
                }

                moveController.RotateTowards(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
		void SwapWeapons()
		{
            Debug.Log("Swap");
            if (currentWeapon != null) Debug.Log("Current Weapon");
            if (secondaryWeapon != null) Debug.Log("Second Weapon");
			Weapon temp = currentWeapon;
			currentWeapon = secondaryWeapon;
			secondaryWeapon = temp;

		}

        public void PushEvent(object sender, string type, EntityEvent e)
        {
            //Debug.Log("Player Event: (" + type + ")");
            switch (type)
            {
                case "HealthDamage":
                    camera.Shake();
                    break;
                case "HealthDead":
                    WorldEventManager.Instance.PushEvent(this, "PlayerDied", new WorldEvent(transform.position));
                    break;
            }
        }

        public void SetMainWeapon(Weapon weapon)
        {
			currentWeapon = weapon;
			currentWeapon.transform.SetParent (WeaponPlacement);
			currentWeapon.transform.localPosition = new Vector3 (0, 0, 0);
			currentWeapon.transform.localRotation = Quaternion.identity;
			
        }
		public void SetSecondaryWeapon(Weapon weapon)
		{
			secondaryWeapon = weapon;
			secondaryWeapon.transform.SetParent (WeaponPlacement);
			secondaryWeapon.transform.localPosition = new Vector3 (0, 0, 0);
			secondaryWeapon.transform.localRotation = Quaternion.identity;
		}
    }
}
