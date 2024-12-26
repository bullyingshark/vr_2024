using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEyeesConfig : MonoBehaviour
{
    public Camera m_left;    // Камера для левого глаза
    public Camera m_right;   // Камера для правого глаза

    public Color m_backgroundColor = Color.white;
    public float m_fieldOfView = 60f;
    public float m_nearDistance = 0.01f;
    public float m_farDistance = 1000f;

    private RenderTexture m_leftRenderTexture;
    private RenderTexture m_rightRenderTexture;

    private void Start()
    {
        // Создание RenderTexture для обеих камер
        int textureWidth = Mathf.Max(Screen.width / 2, 256);
        int textureHeight = Mathf.Max(Screen.height, 256);

        m_leftRenderTexture = new RenderTexture(textureWidth, textureHeight, 24);
        m_rightRenderTexture = new RenderTexture(textureWidth, textureHeight, 24);

        m_left.targetTexture = m_leftRenderTexture;
        m_right.targetTexture = m_rightRenderTexture;

        // Установка начальных параметров
        SetColor(m_backgroundColor);
        SetFieldOfView(m_fieldOfView);
        SetCameraViewDistance(m_nearDistance, m_farDistance);
    }

    public void SetCameras(Camera left, Camera right)
    {
        m_left = left;
        m_right = right;
    }

    public void SetFieldOfView(float angle)
    {
        m_fieldOfView = angle;
        m_left.fieldOfView = m_right.fieldOfView = angle;
    }

    public void SetColor(Color color)
    {
        m_backgroundColor = color;
        m_left.backgroundColor = m_right.backgroundColor = color;
    }

    public void SetCameraViewDistance(float nearDistance, float farDistance)
    {
        m_farDistance = farDistance;
        m_nearDistance = nearDistance;
        m_left.nearClipPlane = m_right.nearClipPlane = nearDistance;
        m_left.farClipPlane = m_right.farClipPlane = farDistance;
    }

    public void SetHDR(bool hdr)
    {
        m_left.allowHDR = m_right.allowHDR = hdr;
    }

    public void SetMSAA(bool msaa)
    {
        m_left.allowMSAA = m_right.allowMSAA = msaa;
    }

    private void OnValidate()
    {
        // Применение изменений в инспекторе
        SetColor(m_backgroundColor);
        SetFieldOfView(m_fieldOfView);
        SetCameraViewDistance(m_nearDistance, m_farDistance);
    }

    private void OnGUI()
    {
        // Вывод стереоизображения на экран
        if (m_leftRenderTexture != null && m_rightRenderTexture != null)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width / 2, Screen.height), m_leftRenderTexture, ScaleMode.StretchToFill, false);
            GUI.DrawTexture(new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height), m_rightRenderTexture, ScaleMode.StretchToFill, false);
        }
        else
        {
            Debug.LogError("RenderTextures не созданы!");
        }
    }
}
