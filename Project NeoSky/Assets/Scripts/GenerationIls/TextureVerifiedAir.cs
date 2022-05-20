using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureVerifiedAir : MonoBehaviour
{
    public bool verifiedAir(float[,] noiseMap, int airMin)
    {
        int mapWitdh = noiseMap.GetLength(0);
        int mapHeight = noiseMap.GetLength(1);
        int airValue = 0;
        for (int x = 0; x < mapWitdh; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Debug.Log(noiseMap[x, y]);
                if(noiseMap[x, y] > 0.5f)
                {
                    airValue++;
                }
            }
        }
        return (airValue > airMin);
    }
}
