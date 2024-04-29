using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathLevelUp : MazePath3x3
{
    //private GameObject[] tile3x3ListPrefab = null;
    //private GameObject pathFrmWrk = null;
    private GameObject balustradePathPrefab = null;
    private GameObject balustradeWallPrefab = null;

    public MazePathLevelUp(MazeData mazeData, MazeCell mazeCell, Vector3 position)
       : base(mazeData, mazeCell, position)
    {
        //this.tile3x3ListPrefab = mazeData.tile3x3ListPrefab;
        //this.pathFrmWrk = mazeData.mazePathFloorFw;
        this.balustradePathPrefab = mazeData.balustradePathPrefab;
        this.balustradeWallPrefab = mazeData.balustradeWallPrefab;
    }

    // Wall Rendering
    //---------------
    protected override GameObject RenderSouthPassage(MazeCell mazeCell)
    {
        return (mazeCell.HasSouthPath() ? CreateSouthPath(mazeCell) : CreateSouthWall(mazeCell));
    }

    protected override GameObject RenderWestPassage(MazeCell mazeCell)
    {
        return (mazeCell.HasWestPath() ? CreateWestPath(mazeCell) : CreateWestWall(mazeCell));
    }

    // Define the Level Up paths
    //--------------------------
    protected override GameObject CreateNorthPath(MazeCell mazeCell)
        => CreateNorthSouthWall(balustradePathPrefab, 180.0f);
    
    protected override GameObject CreateSouthPath(MazeCell mazeCell) 
        => CreateNorthSouthWall(balustradePathPrefab, 0.0f);

    protected override GameObject CreateEastPath(MazeCell mazeCell) 
        => CreateEastWestWall(balustradePathPrefab, -90.0f);

    protected override GameObject CreateWestPath(MazeCell mazeCell) 
        => CreateEastWestWall(balustradePathPrefab, 90.0f);

    //  Define the Level Up Walls
    //---------------------------
    protected override GameObject CreateNorthWall(MazeCell mazeCell) 
        => CreateNorthSouthWall(balustradeWallPrefab, 0.0f);

    protected override GameObject CreateSouthWall(MazeCell mazeCell) 
        => CreateNorthSouthWall(balustradeWallPrefab, 0.0f);

    protected override GameObject CreateEastWall(MazeCell mazeCell)
        => CreateEastWestWall(balustradeWallPrefab, 90.0f);

    protected override GameObject CreateWestWall(MazeCell mazeCell)
        => CreateEastWestWall(balustradeWallPrefab, 90.0f);

    // Level offset ... Distance to raise the tile
    //--------------------------------------------
    protected override Vector3 GetOffSet()
    {
        return (new Vector3(0.0f, 5.0f, 0.0f));
    }
}
