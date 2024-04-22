using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePath3x3 : MazePathBuild
{
    private GameObject[] tilePreFab = null;
    protected GameObject wallPreFab = null;
    private GameObject pathFrmWrk = null;
    protected GameObject wallFrmWrk = null;
    private GameObject grass01 = null;

    public MazePath3x3(MazeData mazeData, MazeCell mazeCell, Vector3 position) 
        : base(mazeData, mazeCell, position)
    {
        this.wallPreFab = mazeData.buildingColumnPreFab;
        this.tilePreFab = mazeData.tileList;
        this.wallFrmWrk = mazeData.mazeWallFw;
        this.pathFrmWrk = mazeData.mazePathFloorFw;
        this.grass01 = mazeData.grass01;
    }

    protected override GameObject CreateBase()
    {
        GameObject baseTile = new Framework()
           .Blueprint(pathFrmWrk)
           .Assemble(tilePreFab, CENTER_ANCHOR)
           .Decorate(grass01, 7, 7.0f, 7.0f, 90.0f)
           .Assemble(tilePreFab, NORTH_TILE_ANCHOR)
           .Assemble(tilePreFab, SOUTH_TILE_ANCHOR)
           .Assemble(tilePreFab, EAST_TILE_ANCHOR)
           .Assemble(tilePreFab, WEST_TILE_ANCHOR)
           .Assemble(tilePreFab, NORTH_EAST_TILE_ANCHOR)
           .Assemble(tilePreFab, NORTH_WEST_TILE_ANCHOR)
           .Assemble(tilePreFab, SOUTH_EAST_TILE_ANCHOR)
           .Assemble(tilePreFab, SOUTH_WEST_TILE_ANCHOR)
           .Build();

        return (baseTile);
    }

    protected override GameObject CreateNorthPath(MazeCell mazeCell) => null;
    protected override GameObject CreateSouthPath(MazeCell mazeCell) => null;
    protected override GameObject CreateEastPath(MazeCell mazeCell) => null;
    protected override GameObject CreateWestPath(MazeCell mazeCell) => null;

    protected override GameObject CreateNorthWall(MazeCell mazeCell)
    {
        return (CreateNorthSouthWall(wallPreFab, 0.0f));
    }

    protected override GameObject CreateSouthWall(MazeCell mazeCell)
    {
        return (CreateNorthSouthWall(wallPreFab, 180.0f));
    }

    protected override GameObject CreateEastWall(MazeCell mazeCell)
    {
        return (CreateEastWestWall(wallPreFab, 90.0f));
    }

    protected override GameObject CreateWestWall(MazeCell mazeCell)
    {
        return (CreateEastWestWall(wallPreFab, 90.0f));
    }

    protected GameObject CreateNorthSouthWall(GameObject wallGameObject, float turn)
    {
        GameObject wall = new Framework()
            .Blueprint(wallFrmWrk)
            .SetIsPreFab(true)
            .Assemble(wallGameObject, COLUMN_ANCHOR, turn)
            .Build();

        return (wall);
    }

    protected GameObject CreateEastWestWall(GameObject wallGameObject, float turn)
    {
        GameObject wall = new Framework()
            .Blueprint(wallFrmWrk)
            .SetIsPreFab(true)
            .Assemble(wallGameObject, COLUMN_ANCHOR, turn)
            .Build();

        return (wall);
    }
}