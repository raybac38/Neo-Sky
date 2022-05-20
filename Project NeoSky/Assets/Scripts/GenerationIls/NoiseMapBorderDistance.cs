using UnityEngine;

public class NoiseMapBorderDistance : MonoBehaviour
{
    public float[,] DistanceToBorderCount(float[,] noiseMap)
    {
        int mapWidth = noiseMap.GetLength(0);
        int mapHeight = noiseMap.GetLength(1);
        for (int i = 0; i < 4; i++)
        {
            for (int x = 1; x < mapWidth - 1; x++)
            {
                for (int y = 1; y < mapHeight - 1; y++)
                {
                    if (noiseMap[x, y] == 1)
                    {
                        if (noiseMap[x - 1, y] == 0 || noiseMap[x + 1, y] == 0 || noiseMap[x, y - 1] == 0 || noiseMap[x, y + 1] == 0)
                        {
                            noiseMap[x, y] = 0.1f;
                        }
                        else if (noiseMap[x - 1, y] == 0.1f || noiseMap[x + 1, y] == 0.1f || noiseMap[x, y - 1] == 0.1f || noiseMap[x, y + 1] == 0.1f)
                        {
                            noiseMap[x, y] = 0.2f;
                        }
                        else if (noiseMap[x - 1, y] == 0.2f || noiseMap[x + 1, y] == 0.2f || noiseMap[x, y - 1] == 0.2f || noiseMap[x, y + 1] == 0.2f)
                        {
                            noiseMap[x, y] = 0.3f;
                        }
                        else if (noiseMap[x - 1, y] == 0.3f || noiseMap[x + 1, y] == 0.3f || noiseMap[x, y - 1] == 0.3f || noiseMap[x, y + 1] == 0.3f)
                        {
                            noiseMap[x, y] = 0.4f;
                        }
                        else if (noiseMap[x - 1, y] == 0.4f || noiseMap[x + 1, y] == 0.4f || noiseMap[x, y - 1] == 0.4f || noiseMap[x, y + 1] == 0.4f)
                        {
                            noiseMap[x, y] = 0.5f;
                        }
                        else if (noiseMap[x - 1, y] == 0.5f || noiseMap[x + 1, y] == 0.5f || noiseMap[x, y - 1] == 0.5f || noiseMap[x, y + 1] == 0.5f)
                        {
                            noiseMap[x, y] = 0.6f;
                        }
                        else if (noiseMap[x - 1, y] == 0.6f || noiseMap[x + 1, y] == 0.6f || noiseMap[x, y - 1] == 0.6f || noiseMap[x, y + 1] == 0.6f)
                        {
                            noiseMap[x, y] = 0.7f;
                        }
                        else if (noiseMap[x - 1, y] == 0.7f || noiseMap[x + 1, y] == 0.7f || noiseMap[x, y - 1] == 0.7f || noiseMap[x, y + 1] == 0.7f)
                        {
                            noiseMap[x, y] = 0.8f;
                        }
                        else if (noiseMap[x - 1, y] == 0.8f || noiseMap[x + 1, y] == 0.8f || noiseMap[x, y - 1] == 0.8f || noiseMap[x, y + 1] == 0.8f)
                        {
                            noiseMap[x, y] = 0.9f;
                        }

                    }
                }
            }
        }
        

        return noiseMap;
    }
}
