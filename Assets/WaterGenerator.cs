using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    // Інтерактивні параметри для води
    public int xSize = 20;
    public int zSize = 20;
    public float waterLevel = 0.7f;
    public float waveAmplitude = 0.3f; // Амплітуда хвиль

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateWaterSurface();
    }

    public void CreateWaterSurface()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x, waterLevel, z);
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

        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void Update()
    {
        AnimateWaterSurface();
    }

    // Анімація поверхні води з хвилями
    void AnimateWaterSurface()
    {
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.Sin(Time.time + (x + z) * 0.2f) * waveAmplitude;  // Хвилі на основі синусоїди
                vertices[i] = new Vector3(x, waterLevel + y, z);
                i++;
            }
        }
        UpdateMesh();
    }

    // Метод для встановлення рівня води
    public void SetWaterLevel(float newWaterLevel)
    {
        waterLevel = newWaterLevel;
    }

    // Метод для встановлення амплітуди хвиль
    public void SetWaveAmplitude(float newWaveAmplitude)
    {
        waveAmplitude = newWaveAmplitude;
    }

    // Метод для встановлення розміру сітки
    public void SetGridSize(int newXSize, int newZSize)
    {
        xSize = newXSize;
        zSize = newZSize;
        CreateWaterSurface(); // Оновлюємо поверхню після зміни розміру
    }
}
