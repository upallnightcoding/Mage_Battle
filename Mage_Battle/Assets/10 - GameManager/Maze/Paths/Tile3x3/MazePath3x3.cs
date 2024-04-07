using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePath3x3 : MazePathBuild
{
    private GameObject[] tilePreFab = null;
    private GameObject wallPreFab = null;
    private GameObject pathFrmWrk = null;
    private GameObject wallFrmWrk = null;

    public MazePath3x3(MazeData mazeData) : base(mazeData)
    {
        this.wallPreFab = mazeData.buildingColumnPreFab;
        this.tilePreFab = mazeData.tileList;
        this.wallFrmWrk = mazeData.mazeWallFw;
        this.pathFrmWrk = mazeData.mazePathFloorFw;
    }

    protected override GameObject CreateBase()
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
           //.Position(position)
           .Build();

        return (baseTile);
    }

    protected override GameObject CreateNorthWall(MazeCell mazeCell)
    {
        return (mazeCell.HasNorthWall() ? CreateNorthSouthWall(wallPreFab) : null);
    }

    protected override GameObject CreateSouthWall(MazeCell mazeCell)
    {
        return (mazeCell.HasSouthWall() ? CreateNorthSouthWall(wallPreFab) : null);
    }

    protected override GameObject CreateEastWall(MazeCell mazeCell)
    {
        return (mazeCell.HasEastWall() ? CreateEastWestWall(wallPreFab) : null);
    }

    protected override GameObject CreateWestWall(MazeCell mazeCell)
    {
        return (mazeCell.HasWestWall() ? CreateEastWestWall(wallPreFab) : null);
    }

    private GameObject CreateNorthSouthWall(GameObject wallGameObject)
    {
        GameObject wall = new Framework()
            .Blueprint(wallFrmWrk)
            .Assemble(wallGameObject, COLUMN_ANCHOR)
            .Build(new Vector3(0.0f, 0.0f, 0.0f));

        return (wall);
    }

    private GameObject CreateEastWestWall(GameObject wallGameObject)
    {
        GameObject wall = new Framework()
            .Blueprint(wallFrmWrk)
            .Assemble(wallGameObject, COLUMN_ANCHOR)
            .Build(new Vector3(0.0f, 90.0f, 0.0f));

        return (wall);
    }


    /*public override GameObject RenderPath(Vector3 position)
    {
        //int walls = CalculateWalls(mazeCell);

        //GameObject northWall = (mazeCell.HasNorthWall() && ((walls & N) > 0)) ? CreateNorthSouthWall(framework) : null;
        //GameObject southWall = (mazeCell.HasSouthWall() && ((walls & S) > 0)) ? CreateNorthSouthWall(framework) : null;
        //GameObject eastWall = (mazeCell.HasEastWall() && ((walls & E) > 0)) ? CreateEastWestWall(framework) : null;
        //GameObject westWall = (mazeCell.HasWestWall() && ((walls & W) > 0)) ? CreateEastWestWall(framework) : null;

        GameObject northWall = (mazeCell.HasNorthWall() ) ? CreateNorthSouthWall(framework) : null;
        GameObject southWall = (mazeCell.HasSouthWall() ) ? CreateNorthSouthWall(framework) : null;
        GameObject eastWall = (mazeCell.HasEastWall() ) ? CreateEastWestWall(framework) : null;
        GameObject westWall = (mazeCell.HasWestWall() ) ? CreateEastWestWall(framework) : null;

        GameObject baseTile = framework
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
           .Position(position)
           .Build();

        GameObject tile = framework
            .Blueprint(pathFrmWrk)
            .Assemble(baseTile, CENTER_ANCHOR)
            .Assemble(northWall, NORTH_WALL_ANCHOR)
            .Assemble(southWall, SOUTH_WALL_ANCHOR)
            .Assemble(eastWall, EAST_WALL_ANCHOR)
            .Assemble(westWall, WEST_WALL_ANCHOR)
            .Position(position)
            .Build();

      

        return (tile);
    }*/

    /*protected virtual GameObject CreateNorthWall(GameObject wallGameObject)
    {
        GameObject wall = new Framework()
            .Blueprint(wallFrmWrk)
            .Assemble(wallGameObject, COLUMN_ANCHOR)
            .Build(new Vector3(0.0f, 0.0f, 0.0f));

        return (wall);
    }

    protected virtual GameObject CreateSouthWall(GameObject wallGameObject)
    {
        GameObject wall = new Framework()
            .Blueprint(wallFrmWrk)
            .Assemble(wallGameObject, COLUMN_ANCHOR)
            .Build(new Vector3(0.0f, 0.0f, 0.0f));

        return (wall);
    }

    protected virtual GameObject CreateEastWall(GameObject wallGameObject)
    {
        GameObject wall = new Framework()
            .Blueprint(wallFrmWrk)
            .Assemble(wallGameObject, COLUMN_ANCHOR)
            .Build(new Vector3(0.0f, 90.0f, 0.0f));

        return (wall);
    }

    protected virtual GameObject CreateWestWall(GameObject wallGameObject)
    {
        GameObject wall = new Framework()
            .Blueprint(wallFrmWrk)
            .Assemble(wallGameObject, COLUMN_ANCHOR)
            .Build(new Vector3(0.0f, 90.0f, 0.0f));

        return (wall);
    }*/


}