using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//generates organsims
public class Moderator : MonoBehaviour {

    //the two organisms
    public Organism o1;
    public Organism o2;


    // Use this for initialization
    void Start () {
        initialize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //initialize the sim with two plain organisms
    void initialize()
    {
        o1.init();
        o2.init();

        //o1.addModule();
        //o1.addModule();
        //o1.addModule();

        o1.construct();
        o2.construct();
    }


    //resets both organisms (one with a mutation) and the environment
    public void reset(Organism dead)
    {
        //find the living organism
        Organism live = (o1 == dead ? o2 : o1);

        live.destroyModules();
        dead.destroyModules();

        removeObjectsWithTag("vect");

        dead.modData = new Dictionary<Pos, modType>(live.modData);
        dead.mutate();

        live.construct();
        dead.construct();

    }

    //destroys all gameobjects with a certain tag
    void removeObjectsWithTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        for(var i = 0; i < gameObjects.Length; i++)
            Destroy(gameObjects[i]);
    }
    

}
