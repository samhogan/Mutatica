using UnityEngine;
using System.Collections;

public class EVector : EFactor
{
    public vType type;

    // Update is called once per frame
    void Update() {

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 9)//if not colliding with another efactor, deactivate the object(which is a module)
        {
            

            //if colliding not colliding with a resistor of the same color, hide that module
            if ((collision.gameObject.tag != "redres" && type == vType.RED) ||
                (collision.gameObject.tag != "blueres" && type == vType.BLUE))
            {
                collision.gameObject.SetActive(false);
            }

            Destroy(this.gameObject);

        }


    }
}

//the types of vectors 
public enum vType{RED, BLUE};