using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseRound : MonoBehaviour
{

    public float[,] RoundePerlinMap(float[,] noiseMap, float step)
    {
        int mapWidth = noiseMap.GetLength(0);
        int mapHeight = noiseMap.GetLength(1);
        float caseValue = 0;
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                caseValue = noiseMap[x, y];
                if(caseValue >= step)
                {
                    noiseMap[x, y] = 1;
                }else
                {
                    noiseMap[x, y] = 0;
                }
            }
        }
        return noiseMap;
    }
}
