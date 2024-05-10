using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MazeCntrl : MonoBehaviour
{
    [SerializeField] private MazeData mazeData;
    [SerializeField] private GameObject pickupGemPreFab;
    [SerializeField] private Transform map;
    [SerializeField] private GameObject tileMapPrefab;

    private GameObject world;

    private MazeGenerator maze;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void NewGame()
    {
        world = new GameObject("World");
        world.AddComponent<NavMeshSurface>();

        if (mazeData.randomSeed > 0)
        {
            UnityEngine.Random.InitState(mazeData.randomSeed);
        }

        maze = new MazeGenerator(mazeData.width, mazeData.height);

        Display(maze);

        //DisplayMap();

        world.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public Vector3 GetStartMazeCellPosition()
    {
        return (maze.GetStartMazeCell().Position);
    }

    private void DisplayMap()
    {
        for (int row = 0; row < maze.Height; row++)
        {
            for (int col = 0; col < maze.Width; col++)
            {
                GameObject tile = Instantiate(tileMapPrefab, map);
                tile.transform.localPosition = new Vector3(col, 0.0f, row);
            }
        }
    }

    private void Display(MazeGenerator maze)
    {
        Vector3 position = Vector3.zero;
        float cellSize = mazeData.cellSize;

        for (int row = 0; row < maze.Height; row++) 
        {
            for (int col = 0; col < maze.Width; col++)
            {
                MazeCell mazeCell = maze.GetMazeCell(col, row);
                mazeCell.Position = position;
                GameObject path = null;

                switch (mazeCell.PathType)
                {
                    case MazePathType.BLOCKED:
                        path = new MazePathArena(mazeData, mazeCell, position, 2, 2, 3, 3).RenderPath();
                        break;
                    case MazePathType.START:
                        path = new MazePathStart(mazeData, mazeCell, position).RenderPath();
                        break;
                    case MazePathType.OFFPATH:
                        if (mazeCell.IsRoom())
                        {
                            path = new MazePathRoom(mazeData, mazeCell, position).RenderPath();
                        } else
                        {
                            path = new MazePath3x3(mazeData, mazeCell, position).RenderPath();
                        }
                        break;
                    case MazePathType.PATH:

                        if (UnityEngine.Random.Range(0, 5) == 0)
                        {
                            if ((mazeCell.PathValue == 3) || (mazeCell.PathValue == 12))
                            {
                                path = new MazePathWater(mazeData, mazeCell, position).RenderPath();
                            } else
                            {
                                path = new MazePath3x3(mazeData, mazeCell, position).RenderPath();
                            }
                        } else if (UnityEngine.Random.Range(0, 5) == 0)
                        {
                            path = new MazePathLevelUp(mazeData, mazeCell, position).RenderPath();
                        } else
                        {
                            path = new MazePath3x3(mazeData, mazeCell, position).RenderPath();
                        }

                        path.GetComponentsInChildren<MazePathCntrl>()[0].CreateGem(pickupGemPreFab);
                        path.GetComponentsInChildren<MazePathCntrl>()[0].Initialize(mazeData, mazeCell);

                        break;
                    case MazePathType.END:
                        path = new MazePathEnd(mazeData, mazeCell, position).RenderPath();
                        break;
                }

                if (path != null)
                {
                    path.transform.SetParent(world.transform);
                }

                position.x += cellSize;
            }

            position.x = 0.0f;
            position.z += cellSize;
        }
    }
}
