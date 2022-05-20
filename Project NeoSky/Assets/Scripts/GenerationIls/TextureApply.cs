using UnityEngine;

public class TextureApply : MonoBehaviour
{
    public TextureMap textureMap;
    public NoiseSupresseBorder supresseBorder;
    public NoiseRound noiseRound;
    public NoiseMapBorderDistance noiseMapBorderDistance;
    public TextureVerifiedAir textureVerified;
    public IslandConstructor islandConstructor;
    public int width;
    public int height;
    public float noiseScale;
    public float lacunarity;
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public Vector2 offSet;
    [Range(0, 1)]
    public float step;

    public bool isSupressBorder;
    public bool borderDistance;

    [Range(0, 500)]
    public int minAir;


    public void Show()
    {

        float[,] noiseMap = Noise.GenerationTexture(width, height, noiseScale, octaves, persistance, lacunarity, offSet);
        noiseMap = noiseRound.RoundePerlinMap(noiseMap, step);
        if (isSupressBorder)
        {

            noiseMap = supresseBorder.SuppresseBorder(noiseMap);
            if (borderDistance)
            {
                noiseMap = noiseMapBorderDistance.DistanceToBorderCount(noiseMap);
            }
        }
        textureMap.DrawNoiseMap(noiseMap);

    }
    public void VerifiedValue()
    {
        if (octaves < 1)
        {
            octaves = 1;
        }
        if (lacunarity < 0)
        {
            lacunarity = 0.001f;
        }


    }

    public float[,] CreateBaseIsland()
    {

        float[,] noiseMap = Noise.GenerationTexture(width, height, noiseScale, octaves, persistance, lacunarity, offSet);
        noiseMap = noiseRound.RoundePerlinMap(noiseMap, 0.75f);
        noiseMap = supresseBorder.SuppresseBorder(noiseMap);
        noiseMap = noiseMapBorderDistance.DistanceToBorderCount(noiseMap);
        return noiseMap;
    }
    public float[,] GenerateUpperIsland()
    {
        float[,] noiseMap = Noise.GenerationTexture(width, height, noiseScale, octaves, persistance, lacunarity, new Vector2(offSet.x + 6, offSet.y -4));
        return noiseMap;
    }
    public float[,] GenerateLowerIsland()
    {
        float[,] noiseMap = Noise.GenerationTexture(width, height, noiseScale, octaves, persistance, lacunarity, offSet);
        return noiseMap;
    }

}
