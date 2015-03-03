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
			secondaryWeapon = null;
            moveController = GetComponent<MovementController>();
            camera = FindObjectOfType<CameraController>();
            camera.Target = transform;

            events = GetComponent<EntityEventManager>();
            events.AddListener("HealthDamage", this);
        }

        // Update is called once per frame
        void Update()
        {
            if (gameObject.GetComponent<Health>().isAlive == Health.State.Alive)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    currentWeapon.Reload();
                }
				if (Input.GetKey(KeyCode.E))
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
            }
        }
		void SwapWeapons()
		{
			Debug.Log("SWAPPING");
			Weapon temp = currentWeapon;
			currentWeapon = secondaryWeapon;
			secondaryWeapon = temp;

		}
        void FixedUpdate()
        {
            if (gameObject.GetComponent<Health>().isAlive == Health.State.Alive)
            {
                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");
                Vector2 moveV = new Vector2(x, y);
                if (moveV != Vector2.zero)
                {
                    moveController.Move(moveV);
                }
                Vector2 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                moveController.RotateTowards(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        public void PushEvent(object sender, string type, EntityEvent e)
        {
            switch (type)
            {
                case "HealthDamage":
                    camera.Shake();
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
