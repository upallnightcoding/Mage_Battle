using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathEnd : MazePath3x3
{
    private readonly GameObject tile = null;

    public MazePathEnd(MazeData mazeData, MazeCell mazeCell, Vector3 position) 
        : base(mazeData, mazeCell, position)
    {
        tile = mazeData.mazeEndPathPreFab;
    }

    protected override GameObject CreateBase()
    {
        float rotate = (GetMazeCell().PathValue & 12) > 0 ? 90.0f : 0.0f;

        GameObject endTile = new Framework()
            .Blueprint(GetPathFrmWrk())
            .Assemble(tile, CENTER_ANCHOR, rotate)
            .Rotate(new Vector3(0.0f, rotate, 0.0f))
            .Build();

        return (endTile);
    }
}
