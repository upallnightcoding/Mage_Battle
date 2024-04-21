using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathRoom : MazePath3x3
{
    private readonly GameObject tile = null;
    private readonly GameObject door = null;

    private GameObject pathFrmWrk = null;

    public MazePathRoom(MazeData mazeData, MazeCell mazeCell, Vector3 position) 
        : base(mazeData, mazeCell, position)
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
            .Build();

        return (wall);
    }

    protected override GameObject CreateNorthPath(MazeCell mazeCell) => CreateDoorWay(0.0f);
    protected override GameObject CreateSouthPath(MazeCell mazeCell) => CreateDoorWay(180.0f);
    protected override GameObject CreateEastPath(MazeCell mazeCell) => CreateDoorWay(90.0f);
    protected override GameObject CreateWestPath(MazeCell mazeCell) => CreateDoorWay(90.0f);

    protected override GameObject CreateNorthWall(MazeCell mazeCell) => CreateNorthSouthWall(wallPreFab);
    protected override GameObject CreateSouthWall(MazeCell mazeCell) => CreateNorthSouthWall(wallPreFab);
    protected override GameObject CreateEastWall(MazeCell mazeCell) => CreateEastWestWall(wallPreFab);
    protected override GameObject CreateWestWall(MazeCell mazeCell) => CreateEastWestWall(wallPreFab);
}
