using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MovementController))]
public class AI : MonoBehaviour {

    private GameObject target;
    private MovementController movement;

	// Use this for initialization
	void Start () {
        movement = GetComponent<MovementController>();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float distance = float.MaxValue;

        foreach (GameObject go in players)
        {
            float d = Vector2.Distance(transform.position, go.transform.position);
            if(d < distance)
            {
                distance = d;
                target = go;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        //Temp until AI manager
        movement.MoveForward();
        movement.RotateTowards(target.transform.position);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
