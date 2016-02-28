using UnityEngine;
using System.Collections;

public class Nucleus : Module {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    

    void OnCollisionEnter2D(Collision2D collision)
    {
        //print("collision!!!!");
        if (collision.gameObject.tag == "vect")
        {
            mine.die();
        }

    }
}
