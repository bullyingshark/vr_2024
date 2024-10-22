using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public MeshGenerator meshGenerator;
    public WaterGenerator waterGenerator;

    public Slider smoothnessSlider;
    public Slider heightMultiplierSlider;
    public Slider detailLevelSlider;

    public Slider waterLevelSlider;
    public Slider waveAmplitudeSlider;

    void Start()
    {
        // Прив'язка слайдерів для генерації ландшафту
        smoothnessSlider.onValueChanged.AddListener(OnSmoothnessChanged);
        heightMultiplierSlider.onValueChanged.AddListener(OnHeightMultiplierChanged);
        detailLevelSlider.onValueChanged.AddListener(OnDetailLevelChanged);

        // Прив'язка слайдерів для генерації води
        waterLevelSlider.onValueChanged.AddListener(OnWaterLevelChanged);
        waveAmplitudeSlider.onValueChanged.AddListener(OnWaveAmplitudeChanged);
    }

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

    void OnWaterLevelChanged(float value)
    {
        waterGenerator.SetWaterLevel(value);
    }

    void OnWaveAmplitudeChanged(float value)
    {
        waterGenerator.SetWaveAmplitude(value);
    }
}
