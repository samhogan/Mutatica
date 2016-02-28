using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Organism : MonoBehaviour
{
    //a reference to the moderator
    public Moderator moderator;

    public Vector2 startpos;

    //the list that contains all the module data
    public Dictionary<Pos, modType> modData = new Dictionary<Pos, modType>();

    //the list that contains the actual modules
    public Dictionary<Pos, GameObject> modules = new Dictionary<Pos, GameObject>();

    public GameObject nucleus;
    public GameObject redres;
    public GameObject blueres;
    public GameObject feeder;//unfinished
    public GameObject thruster;//likewise unfinished
    /*
        Module codes:
        0 - Nucleus
        1 - blue resistor
        2 - red resistor
        3 - feeder
        4 - thruster

    */

    //initialize the organism
    public void init()
    {

        //startpos = new Vector2(5, 0);
        //add the nucleus
        modData.Add(new Pos(0, 0), 0);
        //modData.Add(new Pos(1, 0), modType.BLUERES);
        //modData.Add(new Pos(0, 1), modType.BLUERES);
        //construct();
    }


    //builds the organism with its modules in the world
    public void construct()
    {
        foreach (KeyValuePair<Pos, modType> mod in modData)
        {
            constructModule(mod.Key, mod.Value);
        }

        makeJoints();
    }

    //construct a single module
    void constructModule(Pos pos, modType kind)
    {
        GameObject mod = null;
        if (kind == modType.NUCLEUS)
            mod = GameObject.Instantiate(nucleus);
        else if (kind == modType.BLUERES)
            mod = GameObject.Instantiate(blueres);
        else if (kind == modType.REDRES)
            mod = GameObject.Instantiate(redres);
        else if (kind == modType.FEEDER)
            mod = GameObject.Instantiate(feeder);
        else if (kind == modType.THRUSTER)
            mod = GameObject.Instantiate(thruster);

        mod.transform.position = new Vector3(pos.x+startpos.x, pos.y+startpos.y, 0);
        Module comp = mod.GetComponent<Module>();
        comp.mine = this;
        mod.transform.parent = this.transform;

        //add to list
        modules.Add(pos, mod);
    }

    //properly joins all modules together
    void makeJoints()
    {
        foreach (KeyValuePair<Pos, GameObject> mod in modules)
        {
            GameObject mod2 = null;
            //if there is a module to the right of this one 
            if (modules.TryGetValue(new Pos(mod.Key.x+1, mod.Key.y), out mod2))
                makeJoint(mod.Value, mod2, 1);
            

            //if there is a module above
            if (modules.TryGetValue(new Pos(mod.Key.x, mod.Key.y + 1), out mod2))
                makeJoint(mod.Value, mod2, 2);


        }
    }

    //joins two modules
    void makeJoint(GameObject g1, GameObject g2, int dir)
    {
        FixedJoint2D joint = g1.AddComponent<FixedJoint2D>();
        joint.connectedBody = g2.GetComponent<Rigidbody2D>();


        if(dir == 1)
            joint.connectedAnchor = new Vector2(-1, 0);
        else
            joint.connectedAnchor = new Vector2(0, -1);


    }

    //*****MUTATIONS*******\\

    public void mutate()
    {

        if (modData.Count == 1)
        {
            addModule();
            return;
        }

        int num = Random.Range(0, 3);
        if (num == 0)
            addModule();
        else if (num == 1)
            editModule();
        else
            removeModule();

    }
    
    void addModule()
    {

        while (true)
        {
            //first get a module to build off of
            Pos newPos = randomModule(0);
            int dc = Random.Range(0, 4);
            if (dc == 0)
                newPos.x++;
            else if (dc == 1)
                newPos.x--;
            else if (dc == 2)
                newPos.y++;
            else if (dc == 3)
                newPos.y--;

            if (!modData.ContainsKey(newPos))
            {

                modData.Add(newPos, randomModType());
                break;
            }


        }
    }

    //removes a single module
    void removeModule()
    {
        modData.Remove(randomModule(1));
    }

    //changes the identity of an already existing module
    void editModule()
    {
        modData[randomModule(1)] = randomModType();

    }

    //returns the pos of a random module
    Pos randomModule(int start)
    {
        return modData.ElementAt(Random.Range(start, modData.Count)).Key;
    }

    //returns a random module type
    modType randomModType()
    {
        return (modType)(Random.Range(1, 3));
    }


    //called when this organism's nucleus is destroyed
    public void die()
    {
        moderator.reset(this);
    }

    //destroys all modules in the modules dictionary
    public void destroyModules()
    {
        foreach (KeyValuePair<Pos, modType> entry in modData)
        {
            GameObject mod = null;
            modules.TryGetValue(entry.Key, out mod);
            Destroy(mod);
            modules.Remove(entry.Key);
        }

    }
}


//the position of a module
public struct Pos
{
    public int x, y;
    public Pos(int xp, int yp)
    {
        x = xp;
        y = yp;
    }
}

//the types of modules
public enum modType {NUCLEUS, BLUERES, REDRES, FEEDER, THRUSTER};