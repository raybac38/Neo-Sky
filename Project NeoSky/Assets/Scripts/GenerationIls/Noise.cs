using UnityEngine;

public static class Noise
{
    //une seed est composer de 
    public static float[,] GenerationTexture(int mapWidth, int mapHeight, float mapScale, int octaves, float persistance, float lacunarity, Vector2 offSet)
    {
        float maxValue = 0;
        float minValue = 0;
        float[,] perlinMap = new float[mapWidth, mapHeight];
        if (mapScale < 0)
        {
            mapScale = 0.001f;
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 1;
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = x / mapScale * frequency + (octaves * 4 * offSet.x);
                    float sampleY = y / mapScale * frequency + (octaves * 4 * offSet.y);
                    float caseValue = 0;

                    caseValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += caseValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;

                }
                if (noiseHeight < minValue)
                {
                    minValue = noiseHeight;
                }
                else if (noiseHeight > maxValue)
                {
                    maxValue = noiseHeight;
                }
                perlinMap[x, y] = noiseHeight;

            }
        }
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                perlinMap[x, y] = Mathf.InverseLerp(minValue, maxValue, perlinMap[x, y]);
            }
        }
        return perlinMap;
    }
}
