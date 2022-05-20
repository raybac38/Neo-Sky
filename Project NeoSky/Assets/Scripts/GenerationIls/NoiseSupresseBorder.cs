using System.Collections.Generic;
using UnityEngine;


public class NoiseSupresseBorder : MonoBehaviour
{

    public float[,] SuppresseBorder(float[,] noiseMap)
    {
        List<Vector2Int> infectedCase = new List<Vector2Int>();
        List<Vector2Int> newInfectedCase = new List<Vector2Int>();

        int mapWidth = noiseMap.GetLength(0);
        int mapHeight = noiseMap.GetLength(1);


        for (int x = 0; x < mapWidth; x++)
        {
            if (noiseMap[x, 0] == 1)
            {
                noiseMap[x, 0] = 0.5f;
                infectedCase.Add(new Vector2Int(x, 0));
            }
        }
        for (int x = 0; x < mapWidth; x++)
        {
            if (noiseMap[x, mapHeight - 1] == 1)
            {
                noiseMap[x, mapHeight - 1] = 0.5f;
                infectedCase.Add(new Vector2Int(x, mapHeight - 1));

            }
        }
        for (int y = 0; y < mapHeight; y++)
        {
            if (noiseMap[0, y] == 1)
            {
                noiseMap[0, y] = 0.5f;
                infectedCase.Add(new Vector2Int(0, y));

            }
        }
        for (int y = 0; y < mapHeight; y++)
        {
            if (noiseMap[mapWidth - 1, y] == 1)
            {
                noiseMap[mapWidth - 1, y] = 0.5f;
                infectedCase.Add(new Vector2Int(mapWidth - 1, y));

            }
        }
        ///int antiCrash = 0;
        ///
        ///Debug.Log(infectedCase.Count);
        //while (infectedCase.Count != 0)
        //{
        //    antiCrash++;
        //    if(antiCrash == 5)
        //    {
        //        Debug.LogError("erreur 404");
        //        break;
        //
        //    }
        //    Debug.Log(infectedCase.Count);
        //
        //    ///
        //    ///find all new pixel 
        //    for (int i = 0; i < infectedCase.Count; i++)
        //    {
        //        if (!infectedCase.Contains(infectedCase[i] + Vector2Int.up))
        //        {
        //
        //            infectedCase.Add(infectedCase[i] + Vector2Int.up);
        //        }
        //        if (!infectedCase.Contains(infectedCase[i] + Vector2Int.down))
        //        {
        //            infectedCase.Add(infectedCase[i] + Vector2Int.down);
        //        }
        //        if (!infectedCase.Contains(infectedCase[i] + Vector2Int.right))
        //        {
        //            infectedCase.Add(infectedCase[i] + Vector2Int.right);
        //        }
        //        if (!infectedCase.Contains(infectedCase[i] + Vector2Int.left))
        //        {
        //            infectedCase.Add(infectedCase[i] + Vector2Int.left);
        //        }
        //    }
        //
        //    ///
        //    //remove pixel out of range, already contaminated (0.5f) and black (0)
        //    for (int i = infectedCase.Count; i == -1; i--)
        //    {
        //        if (infectedCase[i].x == -1)
        //        {
        //            infectedCase.RemoveAt(i);
        //            Debug.Log("a");
        //        }
        //        else if (infectedCase[i].x > mapWidth - 1)
        //        {
        //            infectedCase.RemoveAt(i);
        //            Debug.Log("b");
        //        }
        //        else if (infectedCase[i].y == -1)
        //        {
        //            infectedCase.RemoveAt(i);
        //            Debug.Log("c");
        //        }
        //        else if (infectedCase[i].y > mapHeight - 1)
        //        {
        //            infectedCase.RemoveAt(i);
        //            Debug.Log("d");
        //        }
        //        else if (noiseMap[infectedCase[i].x, infectedCase[i].y] == 0.5f)
        //        {
        //            infectedCase.RemoveAt(i);
        //            Debug.Log("e");
        //        }
        //        else if (noiseMap[infectedCase[i].x, infectedCase[i].y] == 0)
        //        {
        //            infectedCase.RemoveAt(i);
        //            Debug.Log("f");
        //        }
        //    }
        //
        //    ///
        //    //contaminate all pixel 
        //
        //    for (int i = 0; i < infectedCase.Count; i++)
        //    {
        //        noiseMap[infectedCase[i].x, infectedCase[i].y] = 0.5f;
        //    }
        //}


        
        int middlePixel = 1;
        while (middlePixel != 0)
        {
            middlePixel = 0;
            for (int x = 1; x < mapWidth - 1; x++)
            {
                for (int y = 1; y < mapHeight - 1; y++)
                {
                    if (noiseMap[x, y] == 1)
                    {
                        if (noiseMap[x - 1, y] == 0.5)
                        {
                            noiseMap[x, y] = 0.5f;
                            middlePixel++;
                        }
                        else if (noiseMap[x + 1, y] == 0.5)
                        {
                            noiseMap[x, y] = 0.5f;
                            middlePixel++;
        
                        }
                        else if (noiseMap[x, y - 1] == 0.5)
                        {
                            noiseMap[x, y] = 0.5f;
                            middlePixel++;
        
                        }
                        else if (noiseMap[x, y + 1] == 0.5)
                        {
                            noiseMap[x, y] = 0.5f; 
                            middlePixel++;
        
                        }
                    }
                }
            }
        }
        


        ///
        // change contaminated pixel in to black pixel
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if(noiseMap[x, y] == 0.5f)
                {
                    noiseMap[x, y] = 0f;
                }
            }
        }
        
        return noiseMap;
    }
}
