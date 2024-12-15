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

    // Новые параметры для фракталов
    public int octaves = 4;
    public float persistence = 0.5f;
    public float lacunarity = 2f;

    public TerrainSettings(int xSize, int zSize, float smoothness, float heightMultiplier, Gradient gradient, int octaves, float persistence, float lacunarity)
    {
        this.xSize = xSize;
        this.zSize = zSize;
        this.smoothness = smoothness;
        this.heightMultiplier = heightMultiplier;
        this.gradient = gradient;
        this.octaves = octaves;
        this.persistence = persistence;
        this.lacunarity = lacunarity;
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
    Vector2[] uvs;

    // Интерактивные параметры ландшафта
    public TerrainSettings terrainSettings;

    float minTerrainHeight;
    float maxTerrainHeight;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Material terrainMaterial = Resources.Load<Material>("TerrainMaterial");
        if (terrainMaterial != null)
        {
            GetComponent<MeshRenderer>().material = terrainMaterial;

            // Установить минимальную и максимальную высоту в шейдер
            terrainMaterial.SetFloat("_MinHeight", minTerrainHeight);
            terrainMaterial.SetFloat("_MaxHeight", maxTerrainHeight);
        }
        else
        {
            Debug.LogError("TerrainMaterial not found in Resources folder.");
        }

        terrainSettings = new TerrainSettings(20, 20, 0.3f, 2f, new Gradient(), 4, 0.5f, 2f);
        CreateShape();
    }

    private void Update()
    {
        UpdateMesh();
    }

    void CreateShape()
    {
        int xSize = terrainSettings.xSize;
        int zSize = terrainSettings.zSize;

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = FractalNoise(x, z) * terrainSettings.heightMultiplier;
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
            }
            vert++;
        }

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

        uvs = new Vector2[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2((float)x / xSize, (float)z / zSize);
                i++;
            }
        }
    }

    float FractalNoise(float x, float z)
    {
        float amplitude = 1;
        float frequency = 1;
        float noiseHeight = 0;

        for (int i = 0; i < terrainSettings.octaves; i++)
        {
            float sampleX = x * terrainSettings.smoothness * frequency;
            float sampleZ = z * terrainSettings.smoothness * frequency;

            float perlinValue = Mathf.PerlinNoise(sampleX, sampleZ) * 2 - 1;
            noiseHeight += perlinValue * amplitude;

            amplitude *= terrainSettings.persistence;
            frequency *= terrainSettings.lacunarity;
        }

        return noiseHeight;
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
    }

    // Методы для управления параметрами
    public void SetSmoothness(float smoothness)
    {
        terrainSettings.smoothness = smoothness;
        CreateShape();
    }

    public void SetHeightMultiplier(float heightMultiplier)
    {
        terrainSettings.heightMultiplier = heightMultiplier;
        CreateShape();
    }

    public void SetDetailLevel(int xSize, int zSize)
    {
        terrainSettings.xSize = xSize;
        terrainSettings.zSize = zSize;
        CreateShape();
    }

    public void SetGradient(Gradient gradient)
    {
        terrainSettings.gradient = gradient;
        CreateShape();
    }

    public void SetOctaves(int octaves)
    {
        terrainSettings.octaves = octaves;
        CreateShape();
    }

    public void SetPersistence(float persistence)
    {
        terrainSettings.persistence = persistence;
        CreateShape();
    }

    public void SetLacunarity(float lacunarity)
    {
        terrainSettings.lacunarity = lacunarity;
        CreateShape();
    }
}
