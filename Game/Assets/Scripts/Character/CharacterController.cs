using UnityEngine;
using System.Collections;
using Blurift;

[RequireComponent(typeof(MovementController), typeof(Health))]
public class CharacterController : MonoBehaviour, EntityEventListener  {

    MovementController moveController;
    CameraController camera;
	public GameObject shot;

	private Weapon currentWeapon;

    public Transform WeaponPlacement;

    private EntityEventManager events;

	void Start () {
        moveController = GetComponent<MovementController>();
        camera = FindObjectOfType<CameraController>();
        camera.Target = transform;

        events = GetComponent<EntityEventManager>();
        events.AddListener("HealthDamage", this);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameObject.GetComponent<Health>().isAlive == Health.State.Alive) 
		{
			if (Input.GetKey(KeyCode.R))
			{
				currentWeapon.Reload();
			}
			if (Input.GetButton ("Fire1")) 
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

    void FixedUpdate()
    {
		if (gameObject.GetComponent<Health> ().isAlive == Health.State.Alive) 
		{
			float x = Input.GetAxisRaw ("Horizontal");
			float y = Input.GetAxisRaw ("Vertical");
			Vector2 moveV = new Vector2 (x, y);
			if (moveV != Vector2.zero) 
			{
				moveController.Move (moveV);
			}
			Vector2 mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			moveController.RotateTowards (Camera.main.ScreenToWorldPoint (Input.mousePosition));
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

    public void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        currentWeapon.transform.SetParent(WeaponPlacement);
        currentWeapon.transform.localPosition = new Vector3(0, 0, 0);
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}
