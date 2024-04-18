using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathEnd : MazePath3x3
{
    private readonly GameObject tile = null;

    private GameObject pathFrmWrk = null;

    public MazePathEnd(MazeData mazeData) : base(mazeData)
    {
        tile = mazeData.mazeEndPathPreFab;
        pathFrmWrk = mazeData.mazePathFloorFw;
    }

    protected override GameObject CreateBase()
    {
        float rotate = (mazeCell.PathValue & 12) > 0 ? 90.0f : 0.0f;

        GameObject endTile = new Framework()
            .Blueprint(pathFrmWrk)
            .Assemble(tile, CENTER_ANCHOR, rotate)
            .Rotate(new Vector3(0.0f, rotate, 0.0f))
            .Build();

        endTile.name = "End Tile";

        return (endTile);
    }
}
