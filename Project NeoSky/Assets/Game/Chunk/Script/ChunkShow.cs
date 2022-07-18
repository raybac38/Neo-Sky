using System.Collections.Generic;
using UnityEngine;
public class ChunkShow : MonoBehaviour
{
    public Vector2Int myChunk;
    public Vector2Int myAffectingZone;
    public int AffectiongScaleIsland = 8; //scale par rapport a 16
    public int height = 60; //hauteur du bruit cellulaire

    private List<Vector3> verticies = new List<Vector3>();
    private List<Vector3> upperVerticies = new List<Vector3>();

    private List<int> triangles = new List<int>();

    public int seed = 4267984;

    private Mesh mesh;
    public int octaves;
    public float persistance;
    public float lacunarity;
    public float scale;

    private Vector2 offset = new Vector2(0, 0);

    public Vector2[] affectingZonePoint = new Vector2[25];
    private float[,] baseMap = new float[17, 17];

    public float upperStrenght = 10;
    public float lowerStrenght = 20;
    MeshCollider collider;


    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;

    }
    public void Refresh()
    {
        CalculeOffsetVector();
        myAffectingZone = myChunk = new Vector2Int(Mathf.FloorToInt(transform.position.x / (16 * AffectiongScaleIsland)), Mathf.FloorToInt(transform.position.z / (16 * AffectiongScaleIsland)));
        myChunk = new Vector2Int(Mathf.FloorToInt(transform.position.x / 16), Mathf.FloorToInt(transform.position.z / 16));
        CalculateAffectingZonePoint();

        CalculateBaseMap();
        GenerateMeshPreview();

        collider = GetComponent<MeshCollider>();
        collider.sharedMesh = mesh;

    }
    public void CalculateAffectingZonePoint()
    {
        affectingZonePoint = new Vector2[25];
        int i = 0;
        for (float x = -2f; x < 3f; x++)
        {
            for (float y = -2f; y < 3f; y++)
            {
                affectingZonePoint[i] = (16 * AffectiongScaleIsland) * (myAffectingZone + new Vector2(x, y));
                affectingZonePoint[i] += GenerationPseudoAleatoire((new Vector2(x, y) + myAffectingZone) * AffectiongScaleIsland * 16f);
                i++;

            }
        }
    }
    public void CalculateBaseMap()
    {
        ///calcule de baseMap
        Vector2 position = myChunk * 16;
        for (int x = 0; x < baseMap.GetLength(0); x++)
        {
            for (int y = 0; y < baseMap.GetLength(1); y++)
            {
                float firstValue = ClosestPointDistance(new Vector2(position.x + x, position.y + y));
                baseMap[x, y] = SecondeClosestPointDistance(firstValue, new Vector2(position.x + x, position.y + y)) - firstValue;
            }
        }
    }
    private float SecondeClosestPointDistance(float closestDistance, Vector2 position)
    {
        float distance = 50000f;
        for (int i = 0; i < affectingZonePoint.Length; i++)
        {
            if (Vector2.Distance(position, affectingZonePoint[i]) == closestDistance)
            {

            }
            else if (Vector2.Distance(position, affectingZonePoint[i]) < distance)
            {
                distance = Vector2.Distance(position, affectingZonePoint[i]);
            }
        }
        return distance;
    }
    private float ClosestPointDistance(Vector2 position)
    {
        float distance = 50000f;
        for (int i = 0; i < affectingZonePoint.Length; i++)
        {
            if (Vector2.Distance(position, affectingZonePoint[i]) < distance)
            {
                distance = Vector2.Distance(position, affectingZonePoint[i]);
            }
        }
        return distance;
    }

    private Vector2Int ClosestPointDistanceVector(Vector2 position)
    {
        Vector2Int pointAffecting = Vector2Int.zero;
        float distance = 50000f;
        for (int i = 0; i < affectingZonePoint.Length; i++)
        {
            if (Vector2.Distance(position, affectingZonePoint[i]) < distance)
            {
                distance = Vector2.Distance(position, affectingZonePoint[i]);
                pointAffecting = new Vector2Int(Mathf.RoundToInt(affectingZonePoint[i].x), Mathf.RoundToInt(affectingZonePoint[i].y));
            }
        }
        return pointAffecting;
    }
    public void GenerateMeshPreview()
    {
        List<int> upperTriangles = new List<int>();
        mesh.Clear();
        verticies.Clear();
        triangles.Clear();
        ///trouver les points de bordures
        List<Vector3> borderVerticies = new List<Vector3>();
        for (int i = 0; i < baseMap.GetLength(0); i++)
        {
            if (baseMap[i, 0] > height)
            {
                borderVerticies.Add(new Vector3(i, 0, 0));
            }
            if (baseMap[i, 16] > height)
            {
                borderVerticies.Add(new Vector3(i, 0, 16));

            }
        }
        for (int i = 1; i < baseMap.GetLength(1) - 1; i++)
        {
            if (baseMap[0, i] > height)
            {
                borderVerticies.Add(new Vector3(0, 0, i));

            }
            if (baseMap[16, i] > height)
            {
                borderVerticies.Add(new Vector3(16, 0, i));
            }
        }
        for (int x = 1; x < baseMap.GetLength(0) - 1; x++)
        {
            for (int y = 1; y < baseMap.GetLength(1) - 1; y++)
            {
                if (baseMap[x, y] > height)
                {
                    // check si il y a un voisin qui est trop bas (donc un bord)
                    if (baseMap[x + 1, y] < height | baseMap[x - 1, y] < height | baseMap[x, y + 1] < height | baseMap[x, y - 1] < height)
                    {
                        borderVerticies.Add(new Vector3(x, 0, y));
                    }
                }
            }
        }
        /// fait les triangles de la face du desus
        for (int i = 0; i < baseMap.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < baseMap.GetLength(1) - 1; j++)
            {
                Vector2[] positionTemporaire = new Vector2[4];
                int value = 0;
                if (baseMap[i, j] > height)
                {
                    positionTemporaire[value] = new Vector2(i, j);
                    value++;
                }
                if (baseMap[i, j + 1] > height)
                {
                    positionTemporaire[value] = new Vector2(i, j + 1);
                    value++;
                }
                if (baseMap[i + 1, j + 1] > height)
                {
                    positionTemporaire[value] = new Vector2(i + 1, j + 1);
                    value++;
                }
                if (baseMap[i + 1, j] > height)
                {
                    positionTemporaire[value] = new Vector2(i + 1, j);
                    value++;
                }
                if (value == 3)
                {
                    /// make a triagle shape
                    /// 
                    for (int w = 0; w < 3; w++)
                    {
                        if (!upperVerticies.Contains(new Vector3(positionTemporaire[w].x, 1, positionTemporaire[w].y)))
                        {
                            upperVerticies.Add(new Vector3(positionTemporaire[w].x, 1, positionTemporaire[w].y));
                        }
                    }
                    for (int w = 0; w < 3; w++)
                    {
                        upperTriangles.Add(upperVerticies.IndexOf(new Vector3(positionTemporaire[w].x, 1, positionTemporaire[w].y)));
                    }
                }
                else if (value == 4)
                {
                    for (int w = 0; w < 4; w++)
                    {
                        if (!upperVerticies.Contains(new Vector3(positionTemporaire[w].x, 1, positionTemporaire[w].y)))
                        {
                            upperVerticies.Add(new Vector3(positionTemporaire[w].x, 1, positionTemporaire[w].y));
                        }
                    }
                    upperTriangles.Add(upperVerticies.IndexOf(new Vector3(positionTemporaire[0].x, 1, positionTemporaire[0].y)));
                    upperTriangles.Add(upperVerticies.IndexOf(new Vector3(positionTemporaire[1].x, 1, positionTemporaire[1].y)));
                    upperTriangles.Add(upperVerticies.IndexOf(new Vector3(positionTemporaire[2].x, 1, positionTemporaire[2].y)));
                    upperTriangles.Add(upperVerticies.IndexOf(new Vector3(positionTemporaire[2].x, 1, positionTemporaire[2].y)));
                    upperTriangles.Add(upperVerticies.IndexOf(new Vector3(positionTemporaire[3].x, 1, positionTemporaire[3].y)));
                    upperTriangles.Add(upperVerticies.IndexOf(new Vector3(positionTemporaire[0].x, 1, positionTemporaire[0].y)));
                }
            }
        }
        ///ajouter la partie basse 
        ///dupliquer les verticles de upperTriangle
        ///ajouter le liste upperTriangles en modifiant l'odre (sens anti horaire)
        for (int i = 0; i < upperVerticies.Count; i++)
        {
            verticies.Add(upperVerticies[i]);
            verticies.Add(new Vector3(upperVerticies[i].x, 0, upperVerticies[i].z));

        }
        for (int i = 0; i < Mathf.RoundToInt(upperTriangles.Count / 3); i++)
        {
            triangles.Add(verticies.IndexOf(upperVerticies[upperTriangles[i * 3]]));
            triangles.Add(verticies.IndexOf(upperVerticies[upperTriangles[i * 3 + 1]]));
            triangles.Add(verticies.IndexOf(upperVerticies[upperTriangles[i * 3 + 2]]));

            triangles.Add(verticies.IndexOf(upperVerticies[upperTriangles[i * 3 + 2]] + Vector3.down));
            triangles.Add(verticies.IndexOf(upperVerticies[upperTriangles[i * 3 + 1]] + Vector3.down));
            triangles.Add(verticies.IndexOf(upperVerticies[upperTriangles[i * 3]] + Vector3.down));

        }

        ///construire les faces des bords
        for (int i = -1; i < baseMap.GetLength(0) + 1; i++)
        {
            for (int j = -1; j < baseMap.GetLength(1) + 1; j++)
            {
                //effectue un recherche par carrer de 1 de taille
                int value = 0;
                List<Vector3> point = new List<Vector3>();
                point.Clear();
                if (borderVerticies.Contains(new Vector3(i, 0, j)))
                {
                    value++;
                    point.Add(new Vector3(i, 0, j));
                }
                if (borderVerticies.Contains(new Vector3(i + 1, 0, j)))
                {
                    value++;
                    point.Add(new Vector3(i + 1, 0, j));
                }
                if (borderVerticies.Contains(new Vector3(i, 0, j + 1)))
                {
                    value++;
                    point.Add(new Vector3(i, 0, j + 1));
                }
                if (borderVerticies.Contains(new Vector3(i + 1, 0, j + 1)))
                {
                    value++;
                    point.Add(new Vector3(i + 1, 0, j + 1));
                }
                if (value == 2)
                {
                    //ajout de la face (double face) ==> voir commment ammeliorer le processus
                    triangles.Add(verticies.IndexOf(point[0]));
                    triangles.Add(verticies.IndexOf(point[1]));
                    triangles.Add(verticies.IndexOf(point[0] - Vector3.down));
                    triangles.Add(verticies.IndexOf(point[0] - Vector3.down));
                    triangles.Add(verticies.IndexOf(point[1]));
                    triangles.Add(verticies.IndexOf(point[0]));

                    triangles.Add(verticies.IndexOf(point[0] - Vector3.down));
                    triangles.Add(verticies.IndexOf(point[1]));
                    triangles.Add(verticies.IndexOf(point[1] - Vector3.down));
                    triangles.Add(verticies.IndexOf(point[1] - Vector3.down));
                    triangles.Add(verticies.IndexOf(point[1]));
                    triangles.Add(verticies.IndexOf(point[0] - Vector3.down));
                }
                if (value == 3)
                {
                    //
                    for (int w = 0; w < 2; w++)
                    {
                        triangles.Add(verticies.IndexOf(point[0 + w]));
                        triangles.Add(verticies.IndexOf(point[1 + w]));
                        triangles.Add(verticies.IndexOf(point[0 + w] - Vector3.down));
                        triangles.Add(verticies.IndexOf(point[0 + w] - Vector3.down));
                        triangles.Add(verticies.IndexOf(point[1 + w]));
                        triangles.Add(verticies.IndexOf(point[0 + w]));

                        triangles.Add(verticies.IndexOf(point[0 + w] - Vector3.down));
                        triangles.Add(verticies.IndexOf(point[1 + w]));
                        triangles.Add(verticies.IndexOf(point[1 + w] - Vector3.down));
                        triangles.Add(verticies.IndexOf(point[1 + w] - Vector3.down));
                        triangles.Add(verticies.IndexOf(point[1 + w]));
                        triangles.Add(verticies.IndexOf(point[0 + w] - Vector3.down));
                    }
                    triangles.Add(verticies.IndexOf(point[2]));
                    triangles.Add(verticies.IndexOf(point[0]));
                    triangles.Add(verticies.IndexOf(point[2] - Vector3.down));
                    triangles.Add(verticies.IndexOf(point[2] - Vector3.down));
                    triangles.Add(verticies.IndexOf(point[0]));
                    triangles.Add(verticies.IndexOf(point[2]));

                    triangles.Add(verticies.IndexOf(point[2] - Vector3.down));
                    triangles.Add(verticies.IndexOf(point[0]));
                    triangles.Add(verticies.IndexOf(point[0] - Vector3.down));
                    triangles.Add(verticies.IndexOf(point[0] - Vector3.down));
                    triangles.Add(verticies.IndexOf(point[0]));
                    triangles.Add(verticies.IndexOf(point[2] - Vector3.down));

                }
            }
        }
        /// application du perlin noise sur le terrain 
        /// a faire: 
        /// noiseTextue du desus

        upperVerticies.Clear();
        for (int i = 0; i < verticies.Count; i++)
        {
            if (verticies[i].y == 1)
            {
                //chaque y qui doit etre modifier
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int w = 0; w < octaves; w++)
                {
                    float newX = ((verticies[i].x + offset.x + (16 * myChunk.x)) / scale * frequency) + (offset.x * octaves);
                    float newY = ((verticies[i].z + offset.y + (16 * myChunk.y)) / scale * frequency) + (offset.y * octaves);

                    float perlinValue = Mathf.PerlinNoise(newX, newY) * 2 - 0.5f;
                    noiseHeight = perlinValue * amplitude;
                    amplitude += amplitude * persistance;
                    frequency *= lacunarity;
                }
                float bord = ((baseMap[Mathf.RoundToInt(verticies[i].x), Mathf.RoundToInt(verticies[i].z)] / height) - 0.99f) / 1.5f;
                if (bord < 1)
                {
                    noiseHeight *= bord;
                }
                verticies[i] += Vector3.up * noiseHeight * 5 * upperStrenght;

            }else if (verticies[i].y == 0)
            {
                //chaque y qui doit etre modifier
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int w = 0; w < octaves - 2; w++)
                {
                    float newX = ((verticies[i].x + (16 * myChunk.x)) / scale * frequency) + (offset.x * octaves);
                    float newY = ((verticies[i].z + (16 * myChunk.y)) / scale * frequency) + (offset.y * octaves);

                    float perlinValue = Mathf.PerlinNoise(newX, newY) * 2 + 0.5f;
                    noiseHeight = perlinValue * amplitude;
                    amplitude += amplitude * persistance;
                    frequency *= lacunarity;
                }
                float bord = ((baseMap[Mathf.RoundToInt(verticies[i].x), Mathf.RoundToInt(verticies[i].z)] / height) - 0.99f) / 1.8f;
                if (bord < 3)
                {
                    noiseHeight *= bord;
                }
                verticies[i] += (Vector3.down * noiseHeight * 10 * lowerStrenght) + Vector3.up;
            }

        }

        


        ///calcule des hauteurs des points
        for (int i = 0; i < verticies.Count; i++)
        {
            Vector2 point = ClosestPointDistanceVector(new Vector2(transform.position.x + verticies[i].x, transform.position.z + verticies[i].z));
            int hauteur = GenerationHauteurIls(point);
            verticies[i] = new Vector3(verticies[i].x, verticies[i].y + hauteur * 5, verticies[i].z);

        }



        ///mise des listes dans les array du mesh

        for (int i = 0; i < triangles.Count; i++)
        {
            if (triangles[i] == -1)
            {
                triangles[i] = 0;
                //Debug.LogWarning("verticies faux");
            }
        }

        if (verticies.Count == 0)
        {
            return;
        }
        if (triangles.Count == 0)
        {
            return;
        }
        Vector3[] positionTempo = new Vector3[verticies.Count];
        int[] triangleTempo = new int[triangles.Count];
        for (int i = 0; i < verticies.Count; i++)
        {
            positionTempo[i] = verticies[i];
        }
        for (int i = 0; i < triangles.Count; i++)
        {
            triangleTempo[i] = triangles[i];
        }

        mesh.vertices = positionTempo;
        mesh.triangles = triangleTempo;

        Vector2[] uvs = new Vector2[positionTempo.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(positionTempo[i].x, positionTempo[i].z);
        }
        mesh.uv = uvs;

    }

    public int VonNeumanNumber(float value)
    {
        value = value * seed;
        value *= value;
        value = value % 1000000;
        value = value / 100;
        return Mathf.CeilToInt(value);

    }
    public Vector2 GenerationPseudoAleatoire(Vector2 position)
    {

        float x = VonNeumanNumber(position.x);
        float y = VonNeumanNumber(position.y);

        float value = x + y + (x * x) + (y * y);
        x = value % 99;
        y = (value / 99) % 99;
        x = x / 50;
        y = y / 50;

        return new Vector2(x, y) * AffectiongScaleIsland * 16f;
    }

    /// <summary>
    /// methode pour calculer la hauteur des ils en fonction de leur point d'influance
    /// </summary>
    /// <param name="position"> la position du point d'influance le plus proche</param>
    /// <returns>une valeur comprise entre 0 et 256</returns>
    public int GenerationHauteurIls(Vector2 position)
    {
        position = new Vector2(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
        float x = VonNeumanNumber(position.x);
        float y = VonNeumanNumber(position.y);

        float value = x + y + (x * x) + (y * y);

        value = value * seed;
        value = value % 256;


        return Mathf.FloorToInt(value);


    }
    public Vector2 CalculateOffSetVector(Vector2 position)
    {
        Vector2 offSet = Vector2.zero;
        float decalage = GenerationHauteurIls(position);
        decalage *= seed;

        offSet = new Vector2(decalage / 256, decalage / 256);

        return offSet;

    }

    public void CalculeOffsetVector()
    {
        int moduloX = Mathf.RoundToInt(seed % 100);
        int moduloY = Mathf.RoundToInt((seed % 10000) / 100);

        offset = new Vector2(seed % moduloX, seed % moduloY);

    }
}


