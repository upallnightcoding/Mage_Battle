using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathRoom : MazePath3x3
{
    private readonly GameObject tile = null;

    private GameObject pathFrmWrk = null;

    public MazePathRoom(MazeData mazeData) : base(mazeData)
    {
        tile = mazeData.mazeTileFloor;
        pathFrmWrk = mazeData.mazePathFloorFw;
    }

    protected override GameObject CreateBase()
    {
        GameObject startTile = new Framework()
            .Blueprint(pathFrmWrk)
            .Assemble(tile, CENTER_ANCHOR)
            .Build();

        return (startTile);
    }
}
