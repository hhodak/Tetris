using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    float previousTime;
    public float fallTime = 1f;
    public static int height = 21;
    public static int width = 10;
    static Transform[,] grid = new Transform[width, height];
    Button buttonRotate;
    Button buttonMoveLeft;
    Button buttonMoveRight;
    Button buttonDown;
    Button buttonPause;
    bool gamePaused = false;

    void Start()
    {
        buttonRotate = GameObject.Find("ButtonRotate").GetComponent<Button>();
        buttonMoveLeft = GameObject.Find("ButtonLeft").GetComponent<Button>();
        buttonMoveRight = GameObject.Find("ButtonRight").GetComponent<Button>();
        buttonDown = GameObject.Find("ButtonDown").GetComponent<Button>();
        buttonPause = GameObject.Find("ButtonPause").GetComponent<Button>();

        AddListeners();
        buttonPause.onClick.AddListener(() => PauseGame());
    }

    void Update()
    {
        if (transform.position.y < 21)
        {
            if (!gamePaused)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveTetrominoLeft();
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveTetrominoRight();
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    RotateTetromino();
                }
                if (Time.time - previousTime > (Input.GetKeyDown(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
                {
                    MoveDownTetromino();
                }
            }
        }
    }

    void PauseGame()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            RemoveListeners();
        }
        else
        {
            AddListeners();
        }
    }

    void CheckForLines()
    {
        int numOfLines = 0;
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                numOfLines++;
            }
        }
        UpdatePoints(numOfLines);
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
                return false;

            if (grid[roundedX, roundedY] != null)
                return false;
        }
        return true;
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }

    }

    void UpdatePoints(int rows)
    {
        GameObject.Find("PointsCalculator").GetComponent<PointsCalculator>().AddPoints(rows);
    }

    void CheckForGameEnd()
    {
        for (int i = 0; i < width; i++)
        {
            if (grid[i, 20] != null)
            {
                gamePaused = true;
                Debug.Log("Kraj igre!");
            }
        }
    }

    #region Moving tetromino
    public void MoveTetrominoLeft()
    {
        transform.position += new Vector3(-1, 0, 0);
        if (!ValidMove())
            transform.position -= new Vector3(-1, 0, 0);
    }

    public void MoveTetrominoRight()
    {
        transform.position += new Vector3(1, 0, 0);
        if (!ValidMove())
            transform.position -= new Vector3(1, 0, 0);
    }

    public void RotateTetromino()
    {
        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        if (!ValidMove())
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
    }

    public void MoveDownTetromino()
    {
        transform.position += new Vector3(0, -1, 0);
        if (!ValidMove())
        {
            transform.position -= new Vector3(0, -1, 0);
            AddToGrid();
            CheckForLines();
            this.enabled = false;
            CheckForGameEnd();
            if (!gamePaused)
                FindObjectOfType<SpawnTetromino>().NewTetromino();
        }
        previousTime = Time.time;

    }
    #endregion

    #region Add/Remove listeners
    void AddListeners()
    {
        buttonRotate.onClick.AddListener(() => RotateTetromino());
        buttonMoveLeft.onClick.AddListener(() => MoveTetrominoLeft());
        buttonMoveRight.onClick.AddListener(() => MoveTetrominoRight());
        buttonDown.onClick.AddListener(() => MoveDownTetromino());
    }

    void RemoveListeners()
    {
        buttonRotate.onClick.RemoveListener(() => RotateTetromino());
        buttonMoveLeft.onClick.RemoveListener(() => MoveTetrominoLeft());
        buttonMoveRight.onClick.RemoveListener(() => MoveTetrominoRight());
        buttonDown.onClick.RemoveListener(() => MoveDownTetromino());
    }
    #endregion
}
