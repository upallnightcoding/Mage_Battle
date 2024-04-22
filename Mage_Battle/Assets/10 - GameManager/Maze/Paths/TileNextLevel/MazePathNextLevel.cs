using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathNextLevel : MazePath
{
    private GameObject[] tilePreFab = null;
    //protected GameObject wallPreFab = null;
    private GameObject pathFrmWrk = null;
    protected GameObject wallFrmWrk = null;
    private GameObject balustradePrefab = null;
    private GameObject balustradePathPrefab = null;
    private GameObject balustradeWallPrefab = null;
    private Vector3 position;
    private MazeCell mazeCell;

    public MazePathNextLevel(MazeData mazeData, MazeCell mazeCell, Vector3 position) 
    {
        //this.wallPreFab = mazeData.buildingColumnPreFab;
        this.tilePreFab = mazeData.tileList;
        this.wallFrmWrk = mazeData.mazeWallFw;
        this.pathFrmWrk = mazeData.mazePathFloorFw;
        this.balustradePrefab = mazeData.balustradePrefab;
        this.balustradePathPrefab = mazeData.balustradePathPrefab;
        this.balustradeWallPrefab = mazeData.balustradeWallPrefab;
        this.position = position;
        this.mazeCell = mazeCell;

    }

    public override GameObject RenderPath()
    {
        return (CreateBase());
    }

    protected GameObject CreateBase()
    {
        GameObject baseTile = new Framework()
           .Blueprint(pathFrmWrk)
           .Assemble(tilePreFab, CENTER_ANCHOR)
           .Assemble(tilePreFab, NORTH_TILE_ANCHOR)
           .Assemble(tilePreFab, SOUTH_TILE_ANCHOR)
           .Assemble(tilePreFab, EAST_TILE_ANCHOR)
           .Assemble(tilePreFab, WEST_TILE_ANCHOR)
           .Assemble(tilePreFab, NORTH_EAST_TILE_ANCHOR)
           .Assemble(tilePreFab, NORTH_WEST_TILE_ANCHOR)
           .Assemble(tilePreFab, SOUTH_EAST_TILE_ANCHOR)
           .Assemble(tilePreFab, SOUTH_WEST_TILE_ANCHOR)
           .Position(position + new Vector3(0.0f, 5.0f, 0.0f))
           .Assemble(HasWall(mazeCell.HasNorthWall()), NORTH_WALL_ANCHOR)
           .Assemble(HasWall(mazeCell.HasSouthWall()), SOUTH_WALL_ANCHOR)
           .Assemble(HasWall(mazeCell.HasEastWall()), EAST_WALL_ANCHOR, 90.0f)
           .Assemble(HasWall(mazeCell.HasWestWall()), WEST_WALL_ANCHOR, 90.0f)
           .Assemble(HasPath(mazeCell.HasNorthPath()), NORTH_WALL_ANCHOR, 90.0f)
           .Assemble(HasPath(mazeCell.HasSouthPath()), SOUTH_WALL_ANCHOR, -90.0f)
           .Assemble(HasPath(mazeCell.HasEastPath()), EAST_WALL_ANCHOR, 0.0f)
           .Assemble(HasPath(mazeCell.HasWestPath()), WEST_WALL_ANCHOR, 0.0f)
           .Build();

        return (baseTile);
    }

    private GameObject HasWall(bool hasWall)
    {
        return (hasWall ? balustradePrefab : balustradePathPrefab);
    }

    private GameObject HasPath(bool hasPath)
    {
        return (hasPath ? balustradeWallPrefab : null);
    }

    //protected override GameObject CreateNorthPath(MazeCell mazeCell) => CreatePath(0.0f);
    //protected override GameObject CreateSouthPath(MazeCell mazeCell) => CreatePath(0.0f);
    //protected override GameObject CreateEastPath(MazeCell mazeCell) => CreatePath(90.0f);
    //protected override GameObject CreateWestPath(MazeCell mazeCell) => CreatePath(90.0f);

    //protected override GameObject CreateNorthWall(MazeCell mazeCell) => null;
    //protected override GameObject CreateSouthWall(MazeCell mazeCell) => null;
    //protected override GameObject CreateEastWall(MazeCell mazeCell) => null;
    //protected override GameObject CreateWestWall(MazeCell mazeCell) => null;

    /*protected override Vector3 GetOffSet()
    {
        return (new Vector3(0.0f, 5.0f, 0.0f));
    }*/

    protected GameObject CreatePath(float turn)
    {
        GameObject wall = new Framework()
            .Blueprint(wallFrmWrk)
            .Assemble(balustradePrefab, COLUMN_ANCHOR, turn)
            .Build();

        return (wall);
    }

    protected GameObject CreateNorthSouthWall(GameObject wallGameObject)
    {
        GameObject wall = new Framework()
            .Blueprint(wallFrmWrk)
            .Assemble(wallGameObject, COLUMN_ANCHOR)
            .Build();

        return (wall);
    }

    protected GameObject CreateEastWestWall(GameObject wallGameObject)
    {
        GameObject wall = new Framework()
            .Blueprint(wallFrmWrk)
            .Assemble(wallGameObject, COLUMN_ANCHOR, 90.0f)
            .Build();

        return (wall);
    }

}