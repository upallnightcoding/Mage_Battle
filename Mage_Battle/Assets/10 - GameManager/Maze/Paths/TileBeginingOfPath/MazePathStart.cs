using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathStart : MazePath3x3
{
    private readonly GameObject tile = null;

    private GameObject pathFrmWrk = null;

    public MazePathStart(MazeData mazeData) : base(mazeData)
    {
        tile = mazeData.mazeStartPathPreFab;
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
