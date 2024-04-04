using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathCntrl : MonoBehaviour
{
    private MazeData mazeData = null;
    private MazeCell mazeCell = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(MazeData mazeData, MazeCell mazeCell)
    {
        this.mazeCell = mazeCell;
        this.mazeData = mazeData;

        Vector3 p = transform.position;
        GameObject fx = Instantiate(mazeData.FxPathDirection, new Vector3(p.x, p.y+0.3f, p.z), Quaternion.identity);
        fx.transform.localScale = new Vector3(3.0f, 1.0f, 3.0f);


        switch(mazeCell.MazePathDir)
        {
            case MazePathDirection.NORTH:
                fx.transform.Rotate(0.0f, 0.0f, 0.0f);
                break;
            case MazePathDirection.SOUTH:
                fx.transform.Rotate(0.0f, 180.0f, 0.0f);
                break;
            case MazePathDirection.EAST:
                fx.transform.Rotate(0.0f, 90.0f, 0.0f);
                break;
            case MazePathDirection.WEST:
                fx.transform.Rotate(0.0f, 270.0f, 0.0f);
                break;
        }
    }
}
