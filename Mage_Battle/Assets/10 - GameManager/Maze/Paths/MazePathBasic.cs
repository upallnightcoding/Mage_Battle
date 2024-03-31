using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathBasic : MazePath
{
    private GameObject[] tilePreFab = null;
    private GameObject pathFrmWrk = null;
    private GameObject wallFrmWrk = null;
    private GameObject wallPreFab = null;

    private Framework framework = null;

    public MazePathBasic(MazeData mazeData) : base(mazeData)
    {
        this.framework = new Framework();

        this.tilePreFab = mazeData.tileList;
        this.pathFrmWrk = mazeData.mazePathFloorFw;
        this.wallFrmWrk = mazeData.mazeWallFw;
        this.wallPreFab = mazeData.buildingColumnPreFab;
    }

    public override GameObject RenderPath(MazeCell mazeCell, Vector3 position)
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
    }

   
}