using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathTile : MazePath3x3
{
    private readonly GameObject tile = null;

    private GameObject pathFrmWrk = null;

    public MazePathTile(MazeData mazeData) : base(mazeData)
    {
        tile = mazeData.mazeTilePathPreFab;
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
