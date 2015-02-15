using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour {

    public float MoveForce;
    public float MaxRotation;
    public Rigidbody2D body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveForward()
    {
        MoveVertical(1);
    }

    public void MoveBackward()
    {
        MoveVertical(-1);
    }

    public void MoveVertical(float power)
    {
        if (power > 1) power = 1;
        if (power < -1) power = -1;
        Move(new Vector2(0, power));
    }

    public void MoveRight()
    {
        MoveHorizontal(1);
    }

    public void MoveLeft()
    {
        MoveHorizontal(-1);
    }

    public void MoveHorizontal(float power)
    {
        if (power > 1) power = 1;
        if (power < -1) power = -1;
        Move(new Vector2(power, 0));
    }

    public void Move(Vector2 direction)
    {
        direction.Normalize();
        body.AddForce(direction * MoveForce);
    }

    



    public void RotateTowards(Vector2 pos)
    {
        
    }


}
