using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathStart : MazePath3x3
{
    private readonly GameObject tile = null;

    public MazePathStart(MazeData mazeData, MazeCell mazeCell, Vector3 position) 
        : base(mazeData, mazeCell, position)
    {
        tile = mazeData.mazeStartPathPreFab;
    }

    protected override GameObject CreateBase()
    {
        GameObject startTile = new Framework()
            .Blueprint(GetPathFrmWrk())
            .Assemble(tile, CENTER_ANCHOR)
            .Build();

        return (startTile);
    }
}
