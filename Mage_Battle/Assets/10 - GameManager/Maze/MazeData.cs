using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MazeData", menuName = "Mage Battle/MazeData")]
public class MazeData : ScriptableObject
{
    public static readonly uint NW = 8;
    public static readonly uint SW = 4;
    public static readonly uint SE = 2;
    public static readonly uint NE = 1;

    public static readonly uint N = 8;
    public static readonly uint S = 4;
    public static readonly uint E = 2;
    public static readonly uint W = 1;

    [Header("Maze Data")]
    public int height;
    public int width;
    public int cellSize;

    [Header("Framework")]
    public GameObject mazeWallFw;
    public GameObject mazePathFloorFw;

    [Header("PreFabs")]
    public GameObject buildingColumnPreFab;
    public GameObject buildingFloor01PreFab;

    public GameObject[] tileList;

    public GameObject parent;

    [Space]
    public GameObject[] mazeWallsSegmentsPreFab;

    [Header("Path PreFabs")]
    public GameObject mazeStartPathPreFab;
    public GameObject mazeBlankPathPreFab;
    public GameObject mazeEndPathPreFab;
    public GameObject mazeTilePathPreFab;
    public GameObject mazeTileFloor;

    [Header("Tile Attributes")]
    public GameObject FxPathDirection;

    [Header("Environment")]
    public GameObject grass01;

    private MazeGenerator maze; 
}
