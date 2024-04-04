using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MazeCntrl : MonoBehaviour
{
    [SerializeField] private MazeData mazeData;
    //[SerializeField] private NavMeshSurface surface;

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

        maze = new MazeGenerator(mazeData.width, mazeData.height);
        Display(maze);

        world.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public Vector3 GetStartMazeCellPosition()
    {
        return (maze.GetStartMazeCell().Position);
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
                    case MazePathType.START:
                        path = new MazePathStart(mazeData).RenderPath(mazeCell, position);
                        break;
                    case MazePathType.PATH:
                        path = new MazePath3x3(mazeData).RenderPath(mazeCell, position);
                        path.GetComponentsInChildren<MazePathCntrl>()[0].Initialize(mazeData, mazeCell);
                        break;
                    case MazePathType.END:
                        path = new MazePathEnd(mazeData).RenderPath(mazeCell, position);
                        break;
                    default:
                        path = new MazePath3x3(mazeData).RenderPath(mazeCell, position);
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
