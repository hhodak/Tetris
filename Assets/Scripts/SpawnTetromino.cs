using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    public GameObject[] tetrominos;
    public GameObject nextTetrominoPlaceholder;
    GameObject nextTetromino;
    int newTetrominoIndex;
    int nextTetrominoIndex;

    void Start()
    {
        newTetrominoIndex = Random.Range(0, tetrominos.Length);
        nextTetrominoIndex = Random.Range(0, tetrominos.Length);
        NewTetromino();
    }

    public void NewTetromino()
    {
        Instantiate(tetrominos[newTetrominoIndex], transform.position, Quaternion.identity);
        if (nextTetromino != null)
            Destroy(nextTetromino);
        nextTetromino = Instantiate(tetrominos[nextTetrominoIndex], nextTetrominoPlaceholder.transform.position, Quaternion.identity);
        newTetrominoIndex = nextTetrominoIndex;
        nextTetrominoIndex = Random.Range(0, tetrominos.Length);
    }
}
