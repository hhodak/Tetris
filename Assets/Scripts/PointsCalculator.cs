using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsCalculator : MonoBehaviour
{
    public Text pointsText;

    public void ResetPoints()
    {
        pointsText.text = "0";
    }

    public void AddPoints(int rowNumber)
    {
        if (rowNumber > 0)
        {
            GetComponent<AudioSource>().Play();
        }
        int currentPoints = int.Parse(pointsText.text);
        switch (rowNumber)
        {
            case 1: currentPoints += 100; break;
            case 2: currentPoints += 400; break;
            case 3: currentPoints += 1000; break;
            case 4: currentPoints += 3000; break;
            default: break;
        }
        pointsText.text = currentPoints.ToString();
    }
}
