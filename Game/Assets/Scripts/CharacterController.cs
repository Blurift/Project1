using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MovementController))]
public class CharacterController : MonoBehaviour {

    MovementController moveController;
	private float nextFire;
	public GameObject shot;
	public Transform shotSpawn; 
	public float fireRate;
	// Use this for initialization

	void Start () {
        moveController = GetComponent<MovementController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

		}
	}

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 moveV = new Vector2(x, y);
        if(moveV != Vector2.zero)
        {
            moveController.Move(moveV);
        }

        Vector2 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        moveController.RotateTowards(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
