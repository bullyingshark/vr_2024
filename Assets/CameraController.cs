using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Цель, за которой следит камера
    public float speed = 2.0f;
    public float zoomSpeed = 2.0f; // Швидкість наближення/віддалення
    public float minDistance = 5.0f; // Мінімальна відстань
    public float maxDistance = 20.0f; // Максимальна відстань

    // Для правильного позиционирования камеры
    public int xSize = 20;  // Размер по оси X
    public int zSize = 20;  // Размер по оси Z
    public float heightMultiplier = 2f; // Множитель высоты (для ландшафта)
    public float waterLevel = 0.7f; // Уровень воды

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float distance;

    // Минимальные и максимальные координаты для ландшафта и воды
    private Vector3 terrainMin, terrainMax;

    void Start()
    {
        // Рассчитываем минимальные и максимальные координаты
        terrainMin = new Vector3(0, 0, 0);
        terrainMax = new Vector3(xSize, heightMultiplier, zSize);

        // Камера будет размещена так, чтобы видеть всю поверхность
        Vector3 terrainCenter = (terrainMin + terrainMax) / 2;
        float terrainSize = Mathf.Max(xSize, zSize);
        distance = terrainSize * 0.8f;

        transform.position = new Vector3(terrainCenter.x, terrainMax.y + distance, terrainCenter.z);
        transform.LookAt(terrainCenter); // Камера смотрит на центр

        // Начальные настройки для камеры
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void Update()
    {
        // Обработка вращения камеры с помощью мыши
        yaw += Input.GetAxis("Mouse X") * speed;
        pitch -= Input.GetAxis("Mouse Y") * speed;
        pitch = Mathf.Clamp(pitch, -30, 60); // Ограничиваем угол наклона

        // Обработка прокрутки колесика мыши для изменения расстояния
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        distance -= scrollInput * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance); // Ограничиваем расстояние

        // Рассчитываем новую позицию камеры
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 position = target.position - rotation * Vector3.forward * distance;

        // Устанавливаем новую позицию камеры
        transform.position = position;
        transform.LookAt(target); // Камера всегда смотрит на цель
    }
}
