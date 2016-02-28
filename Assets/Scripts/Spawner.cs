using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    //references to the prefabs of efactors
    public GameObject vblue;
    public GameObject vred;
    public GameObject food;

    static int[] frequency = {1,1,1};
    static int totalf;
    //radius from which efactors are spawned
    // float spawnRadius = Mathf.Sqrt((Environment.envSize.x / 2) + Environment.envSize.y / 2);
    float spawnRadius = Vector2.Distance(Environment.envSize, Vector2.zero)/2;

    // Use this for initialization
    void Start ()
    {
        //add up efactor frequencies
        for (int i = 0; i < frequency.Length; i++)
            totalf += frequency[i];
	}

    int timer = 1;
	// Update is called once per frame
	void FixedUpdate ()
    {
        timer++;

        if(timer==2)
        {
            spawnEfactor(randomFactor());
            timer = 0;
        }

    }

    //returns a random efactor based on frequency
    GameObject randomFactor()
    {
        //float 
        int rand = Random.Range(0, 2);
        if (rand == 0)
            return vblue;
        else if (rand == 1)
            return vred;
        else
            return food;
    }

    static float skew = 1;

    void spawnEfactor(GameObject fact)
    {
        GameObject factor = GameObject.Instantiate(fact);

        Vector2 dir = Random.insideUnitCircle.normalized;
        Vector2 pos = dir * spawnRadius;

        dir.x += Random.Range(-skew, skew);
        dir.y += Random.Range(-skew, skew);
        dir.Normalize();

        float speed = 2;

        factor.transform.position = new Vector3(pos.x, pos.y, 0);

        Rigidbody2D rb = factor.GetComponent<Rigidbody2D>();
        rb.velocity = -dir*speed;
        rb.angularVelocity = Random.Range(-50, 50);

        //float rot = Mathf.Rad2Deg * Mathf.Atan(dir.y / dir.x);
        //factor.transform.rotation = Quaternion.Euler(0, 0, rot);
    }


}
