using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePath3x3 : MazePathBuild
{
    private GameObject[] tileListPrefab = null;
    protected GameObject wallPreFab = null;
    private GameObject pathFrmWrk = null;
    private GameObject tileGratePrefab = null;
    protected GameObject wallFrmWrk = null;

    public MazePath3x3(MazeData mazeData, MazeCell mazeCell, Vector3 position) 
        : base(mazeData, mazeCell, position)
    {
        this.wallPreFab = mazeData.buildingColumnPreFab;
        this.tileListPrefab = mazeData.tile3x3ListPrefab;
        this.wallFrmWrk = mazeData.mazeWallFw;
        this.pathFrmWrk = mazeData.mazePathFloorFw;
        this.tileGratePrefab = mazeData.tileGratePrefab;
    }

    protected override GameObject CreateBase()
    {
        GameObject baseTile = new Framework()
           .Blueprint(pathFrmWrk)
           .Assemble(SelectCenterTile(), CENTER_ANCHOR, RandomRotate())
           .Assemble(tileListPrefab, NORTH_TILE_ANCHOR, RandomRotate())
           .Assemble(tileListPrefab, SOUTH_TILE_ANCHOR, RandomRotate())
           .Assemble(tileListPrefab, EAST_TILE_ANCHOR, RandomRotate())
           .Assemble(tileListPrefab, WEST_TILE_ANCHOR, RandomRotate())
           .Assemble(tileListPrefab, NORTH_EAST_TILE_ANCHOR, RandomRotate())
           .Assemble(tileListPrefab, NORTH_WEST_TILE_ANCHOR, RandomRotate())
           .Assemble(tileListPrefab, SOUTH_EAST_TILE_ANCHOR, RandomRotate())
           .Assemble(tileListPrefab, SOUTH_WEST_TILE_ANCHOR, RandomRotate())
           .Build();

        return (baseTile);
    }

    private GameObject SelectCenterTile()
    {
        GameObject selection = null;

        if (Random.Range(0, 4) == 0)
        {
            selection = tileGratePrefab;
        } else
        {
            selection = tileListPrefab[Random.Range(0, tileListPrefab.Length)];
        }

        return (selection);
    }

    private float RandomRotate()
    {
        return (90.0f * Random.Range(0, 4));
    }

    protected override GameObject CreateNorthPath(MazeCell mazeCell) => null;
    protected override GameObject CreateSouthPath(MazeCell mazeCell) => null;
    protected override GameObject CreateEastPath(MazeCell mazeCell) => null;
    protected override GameObject CreateWestPath(MazeCell mazeCell) => null;

    protected override GameObject CreateNorthWall(MazeCell mazeCell) 
        => CreateNorthSouthWall(wallPreFab, 0.0f);

    protected override GameObject CreateSouthWall(MazeCell mazeCell)
        => CreateNorthSouthWall(wallPreFab, 0.0f);

    protected override GameObject CreateEastWall(MazeCell mazeCell)
        => CreateEastWestWall(wallPreFab, 90.0f);

    protected override GameObject CreateWestWall(MazeCell mazeCell)
        => CreateEastWestWall(wallPreFab, 90.0f);

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