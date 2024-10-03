using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public MeshGenerator meshGenerator;  
    public Slider smoothnessSlider;
    public Slider heightMultiplierSlider;
    public Slider detailLevelSlider;

    void Start()
    {
        // Привязываем слайдеры к методам MeshGenerator
        smoothnessSlider.onValueChanged.AddListener(OnSmoothnessChanged);
        heightMultiplierSlider.onValueChanged.AddListener(OnHeightMultiplierChanged);
        detailLevelSlider.onValueChanged.AddListener(OnDetailLevelChanged);

    }

    // Управление изменением пологости
    void OnSmoothnessChanged(float value)
    {
        meshGenerator.SetSmoothness(value);
    }

    // Управление изменением высоты
    void OnHeightMultiplierChanged(float value)
    {
        meshGenerator.SetHeightMultiplier(value);
    }

    // Управление изменением уровня детализации
    void OnDetailLevelChanged(float value)
    {
        int detailLevel = Mathf.FloorToInt(value);
        meshGenerator.SetDetailLevel(detailLevel, detailLevel);
    }

    // Пример управления градиентом через кнопки
    public void SetGradientPreset1()
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.yellow, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
        );
        meshGenerator.SetGradient(gradient);
    }

    public void SetGradientPreset2()
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
        );
        meshGenerator.SetGradient(gradient);
    }
}
