using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour
{
    public GameObject wallObj;


    public static float wallWidth = 0.2f;
    public static Vector2 envSize = new Vector2(16, 9);//size of the environment


    void Start()
    {
        buildWalls();
    }

    //builds walls, nice
    public void buildWalls()
    {
        buildWall(new Vector2(envSize.x / 2, 0), new Vector2(wallWidth, envSize.y + wallWidth));
        buildWall(new Vector2(envSize.x / -2, 0), new Vector2(wallWidth, envSize.y + wallWidth));
        buildWall(new Vector2(0, envSize.y / 2), new Vector2(envSize.x, wallWidth));
        buildWall(new Vector2(0, envSize.y / -2), new Vector2(envSize.x, wallWidth));

    }

    public void buildWall(Vector2 pos, Vector2 scale)
    {
        GameObject wall = GameObject.Instantiate(wallObj);
        wall.transform.position = new Vector3(pos.x, pos.y, 0);
        wall.transform.localScale = new Vector3(scale.x, scale.y, 0);
        // print("cmon");
    }
}
