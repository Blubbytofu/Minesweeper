using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cell;

    private CellGrid grid;

    private void Start()
    {
        grid = new CellGrid(9, 9, 1f);
        CreateGrid(grid.GetWidth(), grid.GetHeight(), grid.GetCellSize(), GenerateMines(grid.GetWidth(), grid.GetHeight(), 10));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject[] allCells = GameObject.FindGameObjectsWithTag("Cell");
            foreach (GameObject cell in allCells)
            {
                cell.GetComponent<Cell>().PurgeCells();
            }

            grid = new CellGrid(9, 9, 1f);
            CreateGrid(grid.GetWidth(), grid.GetHeight(), grid.GetCellSize(), GenerateMines(grid.GetWidth(), grid.GetHeight(), 10));
        }
    }

    public void EndGame()
    {
        GameObject[] allCells = GameObject.FindGameObjectsWithTag("Cell");
        foreach (GameObject cell in allCells)
        {
            cell.GetComponent<Cell>().isFlagged = false;
            cell.GetComponent<Cell>().RevealCell();
        }
    }

    private void CreateGrid(int width, int height, float cellSize, int[,] minePositions)
    {
        float startingX = -width * 0.5f * cellSize + (cellSize / 2);
        float xPos = startingX;
        float yPos = height * 0.5f * cellSize - (cellSize / 2);

        for (int c = 0; c < height; c++)
        {
            for (int r = 0; r < width; r++)
            {
                GameObject cellObject = Instantiate(cell, new Vector3(xPos, 0, yPos), Quaternion.identity);
                cellObject.GetComponent<Cell>().cellX = r;
                cellObject.GetComponent<Cell>().cellY = c;

                if (minePositions[r, c] == 1)
                {
                    cellObject.GetComponent<Cell>().isMine = true;
                }

                xPos += cellSize;
            }
            xPos = startingX;
            yPos -= cellSize;
        }
    }

    private int[,] GenerateMines(int width, int height, int totalNumber)
    {
        int[,] calledCells = new int[width, height];

        for (int k = 0; k < totalNumber; k++)
        {
            bool unique = false;
            while (!unique)
            {
                int xPos = Random.Range(0, width);
                int yPos = Random.Range(0, height);
                if (calledCells[xPos, yPos] == 0)
                {
                    calledCells[xPos, yPos] = 1;
                    unique = true;
                }
            }
        }

        return calledCells;
    }

    /*
    int[,] result = GenerateMines(grid.GetWidth(), grid.GetHeight(), 9);
    for (int row = 0; row < result.GetLength(0); row++)
    {
        for (int col = 0; col < result.GetLength(1); col++)
        {
            if (result[row, col] == 1)
            {
                 Debug.Log("Mine: [" + row + "," + col + "]");
            }
        }
    }
    */
}
