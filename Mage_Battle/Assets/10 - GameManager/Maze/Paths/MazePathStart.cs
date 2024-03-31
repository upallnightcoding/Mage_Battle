using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathStart : MazePath
{
    private MazeData mazeData = null;

    private GameObject pathFrmWrk = null;

    private Framework framework = null;
    private GameObject wallFrmWrk = null;
    private GameObject wallPreFab = null;

    public MazePathStart(MazeData mazeData) : base(mazeData)
    {
        this.framework = new Framework();

        this.mazeData = mazeData;
        this.pathFrmWrk = mazeData.mazePathFloorFw;
        this.wallFrmWrk = mazeData.mazeWallFw;
        this.wallPreFab = mazeData.buildingColumnPreFab;
    }

    public override GameObject RenderPath(MazeCell mazeCell, Vector3 position)
    {
        //int walls = CalculateWalls(mazeCell);

        GameObject northWall = (mazeCell.HasNorthWall() ) ? CreateNorthSouthWall(framework) : null;
        GameObject southWall = (mazeCell.HasSouthWall() ) ? CreateNorthSouthWall(framework) : null;
        GameObject eastWall = (mazeCell.HasEastWall() ) ? CreateEastWestWall(framework) : null;
        GameObject westWall = (mazeCell.HasWestWall() ) ? CreateEastWestWall(framework) : null;

        GameObject floor = Object.Instantiate(mazeData.mazeStartPathPreFab);

        GameObject path = framework
           .Blueprint(pathFrmWrk)
           .Assemble(northWall, NORTH_WALL_ANCHOR)
           .Assemble(southWall, SOUTH_WALL_ANCHOR)
           .Assemble(eastWall, EAST_WALL_ANCHOR)
           .Assemble(westWall, WEST_WALL_ANCHOR)
           .Position(position)
           .Build();

        return (path);
    }

    //private GameObject CreateNorthSouthWall(Framework framework)
    //{
    //    GameObject wall = framework
    //        .Blueprint(wallFrmWrk)
    //        .Assemble(wallPreFab, "ColumnAnchor")
    //        .Build(new Vector3(0.0f, 0.0f, 0.0f));

    //    return (wall);
    //}

    //private GameObject CreateEastWestWall(Framework framework)
    //{
    //    GameObject wall = framework
    //        .Blueprint(wallFrmWrk)
    //        .Assemble(wallPreFab, "ColumnAnchor")
    //        .Build(new Vector3(0.0f, 90.0f, 0.0f));

    //    return (wall);
    //}


}
