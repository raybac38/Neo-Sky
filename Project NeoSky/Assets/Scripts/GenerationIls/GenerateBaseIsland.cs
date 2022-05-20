using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBaseIsland : MonoBehaviour
{
    public void OnEnable()
    {
        mesh = new Mesh();

    }
    public Mesh mesh;
    public NoiseSupresseBorder supresseBorder;
    public NoiseRound noiseRound;
    public int width;
    public int height;
    public float noiseScale;
    public float lacunarity;
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public Vector2 offSet;


    public bool isSupressBorder;

    [Range(0,100)]
    public float airMinimum;

    private float[,] noiseMap;
    public float[,] GenerateNoiseMap(float step)
    {
        
        float[,] noiseMap = Noise.GenerationTexture(width, height, noiseScale, octaves, persistance, lacunarity, offSet);
        noiseMap = noiseRound.RoundePerlinMap(noiseMap, step);
        if (isSupressBorder)
        {
            noiseMap = supresseBorder.SuppresseBorder(noiseMap);
        }
        // ==> texture deja faite, plus qu'a appliquer a la mesh
        return noiseMap;
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
    public int CalculateAir(float[,] noiseMap)
    {
        int mapWidth = noiseMap.GetLength(0);
        int mapHeight = noiseMap.GetLength(1);
        int air = 0;
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (noiseMap[x, y] == 1)
                {
                    air++;
                }
            }
        }
        return air;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            MakeGround();
            Debug.Log("lauchne");
        }
    }
    public void MakeGround()
    {
        int air = 0;
        int surfaceTotal = width * height;
        float step = 0;
        for (int i = 0; i < 10; i++)
        {
            step += 0.1f;
            VerifiedValue();
            noiseMap = GenerateNoiseMap(step);
            air = CalculateAir(noiseMap);
            Debug.Log(air);
            if(air > 500)
            {
                Debug.Log("find");
            }
        }
        

    }
    
    public void CreateMesh()
    {
        List<Vector3> maptemporaire = new List<Vector3>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(noiseMap[x,y] == 1)
                {
                    maptemporaire.Add(new Vector3(x, 1, y));
                }
            }
        }

        Vector3[] map = new Vector3[maptemporaire.Count];
        for (int i = 0; i < maptemporaire.Count; i++)
        {
            map[i] = maptemporaire[i];
        }
        mesh.vertices = map;
        //faire le scriptpour que les faces s'attaque une a une

    }


}
