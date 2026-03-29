using System;
using UnityEngine;
using UnityEngine.UI;

public class CodeUI : MonoBehaviour
{
    private float fadeTime = float.MaxValue;
    private Image image;
    private Color oldColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        oldColor = image.color;
    }

    private void Update()
    {
        if (Time.time >= fadeTime)
        {
            fadeTime = float.MaxValue;
            image.color = oldColor;
        }
    }

    public void OnValidate()
    {
        var data = EvolutionData.instance;
        if (data is null) return;
        var programLines = data.programLines;
        try
        {
            Brain.Brain.ParseProgram(programLines);
            fadeTime = Time.time + 1.0F;
            if (image is not null) image.color = Color.green;
        }
        catch (Exception _)
        {
            fadeTime = Time.time + 1.0F;
            if (image is not null) image.color = Color.red;
        }
    }
}