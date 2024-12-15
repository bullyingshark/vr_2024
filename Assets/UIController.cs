using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public MeshGenerator meshGenerator;
    public WaterGenerator waterGenerator;

    public Material fractalMaterial; // Материал для фрактала

    // Слайдеры для ландшафта
    public Slider smoothnessSlider;
    public Slider heightMultiplierSlider;
    public Slider detailLevelSlider;

    // Слайдеры для воды
    public Slider waterLevelSlider;
    public Slider waveAmplitudeSlider;

    // Слайдеры и элементы для фракталов
    public Slider octavesSlider;
    public Slider persistenceSlider;
    public Slider lacunaritySlider;

    void Start()
    {
        // Привязка слайдеров для генерации ландшафта
        smoothnessSlider.onValueChanged.AddListener(OnSmoothnessChanged);
        heightMultiplierSlider.onValueChanged.AddListener(OnHeightMultiplierChanged);
        detailLevelSlider.onValueChanged.AddListener(OnDetailLevelChanged);

        // Привязка слайдеров для генерации воды
        waterLevelSlider.onValueChanged.AddListener(OnWaterLevelChanged);
        waveAmplitudeSlider.onValueChanged.AddListener(OnWaveAmplitudeChanged);

        // Привязка слайдеров для управления фракталами
        octavesSlider.onValueChanged.AddListener(OnOctavesChanged);
        persistenceSlider.onValueChanged.AddListener(OnPersistenceChanged);
        lacunaritySlider.onValueChanged.AddListener(OnLacunarityChanged);
    }

    // Методы для управления ландшафтом
    void OnSmoothnessChanged(float value)
    {
        meshGenerator.SetSmoothness(value);
    }

    void OnHeightMultiplierChanged(float value)
    {
        meshGenerator.SetHeightMultiplier(value);
    }

    void OnDetailLevelChanged(float value)
    {
        int detailLevel = Mathf.FloorToInt(value);
        meshGenerator.SetDetailLevel(detailLevel, detailLevel);
    }

    // Методы для управления водой
    void OnWaterLevelChanged(float value)
    {
        waterGenerator.SetWaterLevel(value);
    }

    void OnWaveAmplitudeChanged(float value)
    {
        waterGenerator.SetWaveAmplitude(value);
    }

    // Методы для управления фракталами
    void OnOctavesChanged(float value)
    {
        meshGenerator.SetOctaves(Mathf.FloorToInt(value));
    }

    void OnPersistenceChanged(float value)
    {
        meshGenerator.SetPersistence(value);
    }

    void OnLacunarityChanged(float value)
    {
        meshGenerator.SetLacunarity(value);
    }

    // Пример метода для открытия диалога выбора цвета
    Color OpenColorPicker()
    {
        // Здесь можно реализовать вызов стандартного или кастомного диалога
        // Для примера возвращаем белый цвет
        return Color.white;
    }
}
