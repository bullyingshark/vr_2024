using UnityEngine;

public class InfiniteSpaceController : MonoBehaviour
{
    public Transform player; // Камера або об'єкт, який ми відстежуємо
    public Transform terrain; // Ландшафт, який треба переміщувати
    public Transform water; // Водна поверхня, якщо використовується
    public float boundary = 50f; // Відстань від центру до межі простору

    private Vector3 startPosition;

    void Start()
    {
        // Зберігаємо початкову позицію об'єкта
        startPosition = terrain.position;
    }

    void Update()
    {
        // Перевіряємо, чи вийшов гравець за межі простору
        Vector3 offset = player.position - startPosition;

        if (Mathf.Abs(offset.x) > boundary || Mathf.Abs(offset.z) > boundary)
        {
            RepositionTerrain();
        }
    }

    private void RepositionTerrain()
    {
        // Обчислюємо кількість "зсувів", які потрібно зробити
        int shiftX = Mathf.RoundToInt((player.position.x - startPosition.x) / (boundary * 2)) * (int)(boundary * 2);
        int shiftZ = Mathf.RoundToInt((player.position.z - startPosition.z) / (boundary * 2)) * (int)(boundary * 2);

        // Переміщуємо ландшафт і водну поверхню
        terrain.position = new Vector3(startPosition.x + shiftX, terrain.position.y, startPosition.z + shiftZ);

        if (water != null)
        {
            water.position = new Vector3(terrain.position.x, water.position.y, terrain.position.z);
        }
    }
}
