using System.Collections.Generic;
using UnityEngine;

public class IslandConstructor : MonoBehaviour
{
    public TextureApply textureApply;
    public Mesh mesh;
    public Material premierMateriaux;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.name = "ilsProcedural";
    }
    public void ConstrucIsland()
    {
        float[,] upperMap = textureApply.GenerateUpperIsland();
        float[,] lowerMap = textureApply.GenerateLowerIsland();
        float[,] baseMap = textureApply.CreateBaseIsland();
        NoiseMapToPoint(baseMap, lowerMap, upperMap, 1, 10);
    }

    public void NoiseMapToPoint(float[,] noiseMap, float[,] lowerNoiseTexture, float[,] upperNoiseTexture, int hauteur, int amplitude)
    {
        List<Vector3> pointTemporaire = new List<Vector3>();
        List<Vector3> pointTemporaireUpper = new List<Vector3>();
        List<Vector3> pointTemporaireLower = new List<Vector3>();
        List<Vector3> pointTemporaireMiddle = new List<Vector3>();


        List<int> triangles = new List<int>();

        int witdh = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < witdh; x++)
            {
                if (noiseMap[x, y] != 0 && noiseMap[x, y] != 0.25f)
                {
                    pointTemporaire.Add(new Vector3(x,  1, y));
                    pointTemporaireUpper.Add(new Vector3(x,  1, y));
                    pointTemporaire.Add(new Vector3(x, - 1, y));
                    pointTemporaireLower.Add(new Vector3(x,  - 1, y));

                }
                else if (noiseMap[x, y] == 0.25f)
                {
                    pointTemporaire.Add(new Vector3(x, 0, y));
                    pointTemporaireMiddle.Add(new Vector3(x, 0, y));


                }
            }
        }

        //construction : mesh
        for (int z =  - 1; z < 1; z++)
        {
            for (int x = 0; x < witdh - 1; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {

                    List<Vector3> points = new List<Vector3>();
                    for (int j = 0; j < 2; j++)
                    {
                        //les deux hauteur de j1
                        if (pointTemporaire.Contains(new Vector3(x + 1, z + j, y + 1)))
                        {
                            points.Add(new Vector3(x + 1, z + j, y + 1));

                        }
                        else if (pointTemporaire.Contains(new Vector3(x + 1, z + j, y + 1)))
                        {
                            points.Add(new Vector3(x + 1, z + j, y + 1));
                        }
                    }
                    for (int j = 0; j < 2; j++)
                    {
                        //les deux hauteur de j2
                        if (pointTemporaire.Contains(new Vector3(x + 1, z + j, y)))
                        {
                            points.Add(new Vector3(x + 1, z + j, y));

                        }
                        else if (pointTemporaire.Contains(new Vector3(x + 1, z + j, y)))
                        {
                            points.Add(new Vector3(x + 1, z + j, y));
                        }
                    }
                    for (int j = 0; j < 2; j++)
                    {
                        //les deux hauteur de j3
                        if (pointTemporaire.Contains(new Vector3(x, z + j, y)))
                        {
                            points.Add(new Vector3(x, z + j, y));

                        }
                        else if (pointTemporaire.Contains(new Vector3(x, z + j, y)))
                        {
                            points.Add(new Vector3(x, z + j, y));
                        }
                    }
                    for (int j = 0; j < 2; j++)
                    {
                        //les deux hauteur de j4
                        if (pointTemporaire.Contains(new Vector3(x, z + j, y + 1)))
                        {
                            points.Add(new Vector3(x, z + j, y + 1));

                        }
                        else if (pointTemporaire.Contains(new Vector3(x, z + j, y + 1)))
                        {
                            points.Add(new Vector3(x, z + j, y + 1));
                        }
                    }


                    ///////////////////////////////
                    //detection des points
                    ///

                    if (points.Count == 3)
                    {
                        Vector3 premier = points[0];
                        Vector3 deuxieme = points[1];
                        Vector3 troisieme = points[2];

                        if (z == 0)
                        {
                            triangles.Add(pointTemporaire.IndexOf(premier));
                            triangles.Add(pointTemporaire.IndexOf(deuxieme));
                            triangles.Add(pointTemporaire.IndexOf(troisieme));

                        }
                        else
                        {
                            triangles.Add(pointTemporaire.IndexOf(troisieme));
                            triangles.Add(pointTemporaire.IndexOf(deuxieme));
                            triangles.Add(pointTemporaire.IndexOf(premier));
                        }

                    }
                    else if (points.Count == 4)
                    {
                        Vector3 premier = points[0];
                        Vector3 deuxieme = points[1];
                        Vector3 troisieme = points[2];
                        Vector3 quatrieme = points[3];

                        if (z == 0)
                        {
                            triangles.Add(pointTemporaire.IndexOf(premier));
                            triangles.Add(pointTemporaire.IndexOf(deuxieme));
                            triangles.Add(pointTemporaire.IndexOf(troisieme));
                            triangles.Add(pointTemporaire.IndexOf(premier));
                            triangles.Add(pointTemporaire.IndexOf(troisieme));
                            triangles.Add(pointTemporaire.IndexOf(quatrieme));
                        }
                        else
                        {
                            triangles.Add(pointTemporaire.IndexOf(troisieme));
                            triangles.Add(pointTemporaire.IndexOf(deuxieme));
                            triangles.Add(pointTemporaire.IndexOf(premier));
                            triangles.Add(pointTemporaire.IndexOf(quatrieme));
                            triangles.Add(pointTemporaire.IndexOf(troisieme));
                            triangles.Add(pointTemporaire.IndexOf(premier));
                        }
                    }

                    points.Clear();
                }
            }
        }

        float highterValue = upperNoiseTexture[0,0];
        float lowerValue = upperNoiseTexture[0,0];
        int xNoise = upperNoiseTexture.GetLength(0);
        int yNoise = upperNoiseTexture.GetLength(1);
        for (int i = 0; i < xNoise; i++)
        {
            for (int j = 0; j < yNoise; j++)
            {
                if (upperNoiseTexture[i, j] < lowerValue) lowerValue = upperNoiseTexture[i, j];

                if (upperNoiseTexture[i, j] > highterValue) highterValue = upperNoiseTexture[i, j];
            }
        }
        for (int i = 0; i < xNoise; i++)
        {
            for (int j = 0; j < yNoise; j++)
            {
                upperNoiseTexture[i, j] = (upperNoiseTexture[i, j] - lowerValue) / (highterValue - lowerValue) * 2 - 0.5f;
            }
        }
        ///
        highterValue = lowerNoiseTexture[0, 0];
        lowerValue = lowerNoiseTexture[0, 0];
        xNoise = lowerNoiseTexture.GetLength(0);
        yNoise = lowerNoiseTexture.GetLength(1);
        for (int i = 0; i < xNoise; i++)
        {
            for (int j = 0; j < yNoise; j++)
            {
                if (lowerNoiseTexture[i, j] < lowerValue) lowerValue = lowerNoiseTexture[i, j];

                if (lowerNoiseTexture[i, j] > highterValue) highterValue = lowerNoiseTexture[i, j];
            }
        }
        for (int i = 0; i < xNoise; i++)
        {
            for (int j = 0; j < yNoise; j++)
            {
                lowerNoiseTexture[i, j] = (lowerNoiseTexture[i, j] - lowerValue) / (highterValue - lowerValue) * 2 - 0.5f;
            }
        }

        ///
        for (int i = 0; i < pointTemporaireUpper.Count; i++)
        {
            Vector3 tempo = pointTemporaireUpper[i];
            int indice = pointTemporaire.IndexOf(tempo);

            pointTemporaire.RemoveAt(indice);
            tempo = new Vector3(tempo.x, (upperNoiseTexture[Mathf.RoundToInt(tempo.x), Mathf.RoundToInt(tempo.z)] ) * (amplitude / 3) + hauteur, tempo.z);
            pointTemporaire.Insert(indice, tempo);

        }
        for (int i = 0; i < pointTemporaireLower.Count; i++)
        {
            Vector3 tempo = pointTemporaireLower[i];
            int indice = pointTemporaire.IndexOf(tempo);

            pointTemporaire.RemoveAt(indice);
            tempo = new Vector3(tempo.x, -2 -(lowerNoiseTexture[Mathf.RoundToInt(tempo.x), Mathf.RoundToInt(tempo.z)] ) * ((amplitude / 3) * 2) + hauteur, tempo.z);
            pointTemporaire.Insert(indice, tempo);

        }
        for (int i = 0; i < pointTemporaireMiddle.Count; i++)
        {
            Vector3 tempo = pointTemporaireMiddle[i];
            int indice = pointTemporaire.IndexOf(tempo);

            pointTemporaire.RemoveAt(indice);
            tempo = new Vector3(tempo.x,hauteur, tempo.z);
            pointTemporaire.Insert(indice, tempo);

        }




        Vector3[] verticies = new Vector3[pointTemporaire.Count];
        int[] triangle = new int[triangles.Count];
        for (int i = 0; i < pointTemporaire.Count; i++)
        {
            verticies[i] = pointTemporaire[i];
        }
        for (int i = 0; i < triangles.Count; i++)
        {
            triangle[i] = triangles[i];
        }





        Debug.Log(verticies);
        Debug.Log(triangle);
        mesh.vertices = verticies;
        mesh.triangles = triangle;
        GetComponent<MeshRenderer>().material = premierMateriaux;

    }



}
