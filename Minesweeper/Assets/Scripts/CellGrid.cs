using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid
{
    private int width;
    private int height;

    private float cellSize;

    public CellGrid(int w, int h, float size)
    {
        width = w;
        height = h;

        cellSize = size;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public void SetWidth(int newWidth)
    {
        width = newWidth;
    }

    public void SetHeight(int newHeight)
    {
        height = newHeight;
    }

    public void SetCellSize(int newSize)
    {
        cellSize = newSize;
    }
}
