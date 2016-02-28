using UnityEngine;
using System.Collections;

public class EFactor : MonoBehaviour {

    float lifetime = 8f;
    // Use this for initialization
    void Start()
    {
        GameObject.Destroy(this.gameObject, lifetime);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
