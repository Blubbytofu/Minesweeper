using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI IDText;

    public int cellX;
    public int cellY;

    public bool isEmpty;
    public bool isMine;

    private List<GameObject> neighbors = new List<GameObject>();
    [SerializeField] private int touchingMines;

    public bool isFlagged;
    private bool isRevealed;
    private string cellStringID;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        IDText = GetComponentInChildren<TextMeshProUGUI>();
        IDText.text = " ";
        Invoke("CellSetup", 0.01f);
    }

    private void Update()
    {
        if (isFlagged)
        {
            IDText.text = "P";
        }
        else
        {
            if (isRevealed)
            {
                IDText.text = cellStringID;
            }
            else
            {
                IDText.text = " ";
            }
        }
    }

    public void RevealCell()
    {
        if (!isFlagged)
        {
            if (!isRevealed)
            {
                isRevealed = true;
                IDText.text = cellStringID;

                if (cellStringID == "*")
                {
                    gameManager.EndGame();
                }

                if (cellStringID == "-")
                {
                    foreach (GameObject cell in neighbors)
                    {
                        if (!cell.GetComponent<Cell>().isMine)
                        {
                            cell.GetComponent<Cell>().RevealCell();
                        }
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isFlagged)
        {
            RevealCell();
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!isRevealed)
            {
                isFlagged = !isFlagged;
            }
        }
    }

    private void CellSetup()
    {
        //IDText.text = "[" + cellX + "," + cellY + "]";
        if (isMine)
        {
            //IDText.text = "*";
            cellStringID = "*";
        }
        else if (GetMineNumber() > 0)
        {
            //IDText.text = "" + touchingMines;
            cellStringID = "" + touchingMines;
        }
        else
        {
            //IDText.text = " ";
            isEmpty = true;
            cellStringID = "-";
        }
    }

    private int GetMineNumber()
    {
        foreach (GameObject cell in neighbors)
        {
            if (cell.GetComponent<Cell>().isMine)
            {
                touchingMines++;
            }
        }
        return touchingMines;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Cell"))
        {
            neighbors.Add(collision.gameObject);
        }
    }

    public void PurgeCells()
    {
        Destroy(gameObject);
    }
}
