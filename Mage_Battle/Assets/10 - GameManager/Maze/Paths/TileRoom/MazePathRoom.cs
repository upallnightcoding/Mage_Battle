using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathRoom : MazePath3x3
{
    private readonly GameObject floorPrefab = null;
    private readonly GameObject doorPrefab = null;

    private GameObject pathFrmWrk = null;

    public MazePathRoom(MazeData mazeData, MazeCell mazeCell, Vector3 position) 
        : base(mazeData, mazeCell, position)
    {
        floorPrefab = mazeData.mazeTileFloor;
        doorPrefab = mazeData.willTileDoorPreFab;
        pathFrmWrk = mazeData.mazePathFloorFw;
    }

    protected override GameObject CreateBase()
    {
        GameObject floor = new Framework()
            .Blueprint(pathFrmWrk)
            .Assemble(floorPrefab, CENTER_ANCHOR)
            .Build();

        return (floor);
    }

    private GameObject CreateDoorWay(float turn)
    {
        GameObject door = new Framework()
            .Blueprint(wallFrmWrk)
            .Assemble(doorPrefab, COLUMN_ANCHOR, turn)
            .Build();

        return (door);
    }

    protected override GameObject RenderSouthPassage(MazeCell mazeCell)
    {
        return (mazeCell.HasSouthPath() ? CreateSouthPath(mazeCell) : CreateSouthWall(mazeCell));
    }

    protected override GameObject RenderWestPassage(MazeCell mazeCell)
    {
        return (mazeCell.HasWestPath() ? CreateWestPath(mazeCell) : CreateWestWall(mazeCell));
    }

    protected override GameObject CreateNorthPath(MazeCell mazeCell) => CreateDoorWay(0.0f);
    protected override GameObject CreateSouthPath(MazeCell mazeCell) => CreateDoorWay(180.0f);
    protected override GameObject CreateEastPath(MazeCell mazeCell) => CreateDoorWay(90.0f);
    protected override GameObject CreateWestPath(MazeCell mazeCell) => CreateDoorWay(90.0f);

    protected override GameObject CreateNorthWall(MazeCell mazeCell) => CreateNorthSouthWall(wallPreFab, 0.0f);
    protected override GameObject CreateSouthWall(MazeCell mazeCell) => CreateNorthSouthWall(wallPreFab, 180.0f);
    protected override GameObject CreateEastWall(MazeCell mazeCell) => CreateEastWestWall(wallPreFab, 90.0f);
    protected override GameObject CreateWestWall(MazeCell mazeCell) => CreateEastWestWall(wallPreFab, 90.0f);
}
