using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс для настройки параметров ландшафта
public class TerrainSettings
{
    public int xSize = 20;
    public int zSize = 20;
    public float smoothness = 0.3f;
    public float heightMultiplier = 2f;
    public Gradient gradient;

    public TerrainSettings(int xSize, int zSize, float smoothness, float heightMultiplier, Gradient gradient)
    {
        this.xSize = xSize;
        this.zSize = zSize;
        this.smoothness = smoothness;
        this.heightMultiplier = heightMultiplier;
        this.gradient = gradient;
    }
}

// Основной класс генератора сетки
[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    // Интерактивные параметры ландшафта
    public TerrainSettings terrainSettings;

    float minTerrainHeight;
    float maxTerrainHeight;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // Инициализация параметров ландшафта
        terrainSettings = new TerrainSettings(20, 20, 0.3f, 2f, new Gradient());

        StartCoroutine(CreateShape());
    }

    private void Update()
    {
        UpdateMesh();
    }

    IEnumerator CreateShape()
    {
        int xSize = terrainSettings.xSize;
        int zSize = terrainSettings.zSize;

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * terrainSettings.smoothness, z * terrainSettings.smoothness) * terrainSettings.heightMultiplier;
                vertices[i] = new Vector3(x, y, z);

                if (y > maxTerrainHeight)
                    maxTerrainHeight = y;
                if (y < minTerrainHeight)
                    minTerrainHeight = y;

                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

                yield return new WaitForSeconds(.01f);
            }
            vert++;
        }

        // Генерация цветов на основе высоты и градиента
        colors = new Color[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
                colors[i] = terrainSettings.gradient.Evaluate(height);
                i++;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();
    }

    // Добавляем возможность управления параметрами через UI или инпуты
    public void SetSmoothness(float smoothness)
    {
        terrainSettings.smoothness = smoothness;
        StartCoroutine(CreateShape());
    }

    public void SetHeightMultiplier(float heightMultiplier)
    {
        terrainSettings.heightMultiplier = heightMultiplier;
        StartCoroutine(CreateShape());
    }

    public void SetDetailLevel(int xSize, int zSize)
    {
        terrainSettings.xSize = xSize;
        terrainSettings.zSize = zSize;
        StartCoroutine(CreateShape());
    }

    public void SetGradient(Gradient gradient)
    {
        terrainSettings.gradient = gradient;
        StartCoroutine(CreateShape());
    }
}
