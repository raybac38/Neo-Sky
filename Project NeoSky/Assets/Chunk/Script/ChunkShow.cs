using System.Collections.Generic;
using UnityEngine;
public class ChunkShow : MonoBehaviour
{
    public Vector2Int myChunk;
    public Vector2Int myAffectingZone;
    public int AffectiongScaleIsland = 8; //scale par rapport a 16
    public int height = 60; //hauteur du bruit cellulaire

    

    public GameObject cubePreview;
    public GameObject islandPreview;
    private List<Transform> previewThinks = new List<Transform>();

    private List<Vector3> verticies = new List<Vector3>();
    private List<int> triangles = new List<int>();

    public int seed = 4267984;

    private Mesh mesh;

    private int[] table = { 1, 72, 95, 4, 92, 89, 74, 18, 18, 9, 10, 11, 43, 78, 89, 63, 15, 15, 7, 30, 74, 75, 31, 95, 61, 57, 48, 76, 48, 18, 89, 76, 10, 30, 18, 75, 31, 32, 63, 74, 15, 92, 81, 70, 39, 48, 95, 42, 84, 89, 15, 39, 33, 53, 46, 76, 18, 33, 57, 76, 46, 43,
 10, 30, 59, 7, 89, 63, 64, 89, 75, 84, 89, 53, 76, 30, 31, 33, 89, 5, 57, 81, 81, 89, 30, 30, 84, 86, 18, 7, 76, 95, 7, 63, 39, 97, 48, 89};
    public Vector2[] affectingZonePoint = new Vector2[25];
    private float[,] baseMap = new float[17, 17];
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    private void Start()
    {

        Refresh();
    }
    public void Refresh()
    {
        myAffectingZone = myChunk = new Vector2Int(Mathf.FloorToInt(transform.position.x / (16 * AffectiongScaleIsland)), Mathf.FloorToInt(transform.position.z / (16 * AffectiongScaleIsland)));
        myChunk = new Vector2Int(Mathf.FloorToInt(transform.position.x / 16), Mathf.FloorToInt(transform.position.z / 16));
        CalculateAffectingZonePoint();

        CalculateBaseMap();
        GenerateMeshPreview();
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
    ///private Vector2 PseudoRandomeNum(Vector2 position)
    ///{
    ///    float nombre = (position.x + position.y) + 2f * position.x - 0.5f * position.y;
    ///    nombre = Mathf.Pow(nombre, 6);
    ///    nombre = nombre % 100000;
    ///    nombre = nombre / 100;
    ///    nombre = Mathf.Floor(nombre);
    ///    
    ///    float witdh = nombre % 98;
    ///    float height = Mathf.FloorToInt(nombre / 98) % 98;
    ///
    ///    return new Vector2(witdh / 1000f + 1f, height / 1000f + 1f) * AffectiongScaleIsland * 16f;
    ///}
    public void CalculateBaseMap()
    {
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
    private Vector2 ClosestPointVector(Vector2 position)
    {
        float distance = 50000f;
        Vector2 vector2 = Vector2.one;
        for (int i = 0; i < affectingZonePoint.Length; i++)
        {
            if (Vector2.Distance(position, affectingZonePoint[i]) < distance)
            {
                distance = Vector2.Distance(position, affectingZonePoint[i]);
                vector2 = affectingZonePoint[i];
            }
        }
        return vector2;
    }
    public void GenerateMeshPreview()
    {

        mesh.Clear();
        verticies.Clear();
        triangles.Clear();
        for (int i = 0; i < previewThinks.Count; i++)
        {
            Destroy(previewThinks[i].gameObject);
        }
        previewThinks.Clear();
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
                if (baseMap[i , j + 1] > height)
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
                        if (!verticies.Contains(positionTemporaire[w]))
                        {
                            verticies.Add(new Vector3(positionTemporaire[w].x, 1, positionTemporaire[w].y));
                        }
                    }
                    for (int w = 0; w < 3; w++)
                    {
                        triangles.Add(verticies.IndexOf(new Vector3(positionTemporaire[w].x, 1, positionTemporaire[w].y)));
                    }
                    
                }else if(value == 4)
                {
                    for (int w = 0; w < 4; w++)
                    {
                        if (!verticies.Contains(new Vector3(positionTemporaire[w].x, 1, positionTemporaire[w].y)))
                        {
                            verticies.Add(new Vector3(positionTemporaire[w].x, 1, positionTemporaire[w].y));
                        }
                    }
                    triangles.Add(verticies.IndexOf(new Vector3(positionTemporaire[0].x, 1, positionTemporaire[0].y)));
                    triangles.Add(verticies.IndexOf(new Vector3(positionTemporaire[1].x, 1, positionTemporaire[1].y)));
                    triangles.Add(verticies.IndexOf(new Vector3(positionTemporaire[2].x, 1, positionTemporaire[2].y)));
                    triangles.Add(verticies.IndexOf(new Vector3(positionTemporaire[2].x, 1, positionTemporaire[2].y)));
                    triangles.Add(verticies.IndexOf(new Vector3(positionTemporaire[3].x, 1, positionTemporaire[3].y)));
                    triangles.Add(verticies.IndexOf(new Vector3(positionTemporaire[0].x, 1, positionTemporaire[0].y)));
                }


            }
        }
        ///calcule des hauteurs des points

        for (int i = 0; i < verticies.Count; i++)
        {
            Vector2 position = ClosestPointVector(verticies[i]);
            int hauteur = GenerationHauteurIls(position);
            verticies[i] = new Vector3(verticies[i].x, hauteur, verticies[i].z);
        }


        ///mise des listes dans les array du mesh
        
        for (int i = 0; i < triangles.Count; i++)
        {
            if(triangles[i] == -1)
            {
                triangles[i] = 0;
            }
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
        Debug.Log(position);
        float x = VonNeumanNumber(position.x);
        float y = VonNeumanNumber(position.y);

        float value = x + y + (x * x) + (y*y);
        value = seed * value;
        value = value % 256;

        return Mathf.RoundToInt(value);

        
    }
}
