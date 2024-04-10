using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathRoom : MazePath3x3
{
    private readonly GameObject tile = null;
    private readonly GameObject door = null;

    private GameObject pathFrmWrk = null;

    public MazePathRoom(MazeData mazeData) : base(mazeData)
    {
        tile = mazeData.mazeTileFloor;
        door = mazeData.willTileDoorPreFab;
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

    private GameObject CreateDoorWay(float turn)
    {
        GameObject wall = new Framework()
            .Blueprint(wallFrmWrk)
            .Assemble(door, COLUMN_ANCHOR, turn)
            //.Rotate(new Vector3(0.0f, 0.0f, 0.0f))
            .Build();

        return (wall);
    }

    protected override GameObject CreateNorthWall(MazeCell mazeCell)
    {
        return (mazeCell.HasNorthWall() ? CreateNorthSouthWall(wallPreFab) : CreateDoorWay(0.0f));
    }

    protected override GameObject CreateSouthWall(MazeCell mazeCell)
    {
        return (mazeCell.HasSouthWall() ? CreateNorthSouthWall(wallPreFab) : CreateDoorWay(180.0f));
    }

    protected override GameObject CreateEastWall(MazeCell mazeCell)
    {
        return (mazeCell.HasEastWall() ? CreateEastWestWall(wallPreFab) : CreateDoorWay(90.0f));
    }

    protected override GameObject CreateWestWall(MazeCell mazeCell)
    {
        return (mazeCell.HasWestWall() ? CreateEastWestWall(wallPreFab) : CreateDoorWay(90.0f));
    }
}
