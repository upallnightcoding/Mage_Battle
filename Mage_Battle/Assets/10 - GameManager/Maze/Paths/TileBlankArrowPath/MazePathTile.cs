using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathTile : MazePath3x3
{
    private readonly GameObject tile = null;

    public MazePathTile(MazeData mazeData, MazeCell mazeCell, Vector3 position) 
        : base(mazeData, mazeCell, position)
    {
        tile = mazeData.mazeTilePathPreFab;
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
