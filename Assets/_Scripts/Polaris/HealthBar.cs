using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova;
using System.Text;

public class HealthBar : MonoBehaviour
{
    public FloatVariable playerhealth, playerMaxHealth;

    private float ratio;

    [Header("HealthRed")]
    [Tooltip("The health, not the max health")]
    public UIBlock2D Fill = null;

    [Header("Text Format")]
    [Tooltip("The Text Block use to display the health float")]
    public TextBlock healthText;

    /// <summary>
    /// A string builder used internal to generate the <see cref="textFormat"/> string.
    /// </summary>
    private StringBuilder textFormatBuilder = new StringBuilder();

    /// <summary>
    /// The format to use when displaying the current 
    /// progress state on the <see cref="Text"/> visual.
    /// </summary>
    private string textFormat = string.Empty;

    // Start is called before the first frame update

    public float HMratio
    {
        get => ratio;

        set
        {
            ratio = Mathf.Clamp01(value);
            UpdateProgressVisuals();
        }
    }
    void Awake()
    {
        ratio = playerhealth.value / playerMaxHealth.value;
        UpdateProgressVisuals();
    }

    private void Update()
    {
        ratio = playerhealth.value / playerMaxHealth.value;
        UpdateProgressVisuals();
    }

    private void UpdateProgressVisuals()
    {
        UpdateFillVisual();

        UpdateTextVisual();
    }

    private void UpdateFillVisual()
    {
        if (Fill == null)
        {
            return;
        }

        if (ratio == GetDisplayedPercent())
        {
            // Currently displaying the right percent.
            return;
        }


        Fill.AutoSize.X = AutoSize.None;
        Fill.Size.X = Length.Percentage(ratio * (1 - Fill.CalculatedMargin.X.Sum().Percent));
 
    }

    private void UpdateTextVisual()
    {
        if (healthText == null)
        {
            return;
        }

        string format = textFormat;

        // Special case 0 and 1 for "cleaner" visuals
        if (HMratio == 0)
        {
            //format
        }
        else if (HMratio == 1)
        {
            //format
        }

        // The percent string formats handle the 100x multiplication,
        // but the non-percent formats do not. 
        float plHealth = playerhealth.value;
        float plMaxHealh = playerMaxHealth.value;

        string plhealthtext = string.Format("{0}/{1}", plHealth, plMaxHealh);


        if (healthText.Text == plhealthtext)
        {
            return;
        }

        healthText.Text = plhealthtext;
    }
    private float GetDisplayedPercent()
    {
           return Fill.CalculatedSize.X.Percent / Mathf.Clamp01(1 - Fill.CalculatedMargin.X.Sum().Percent);
    }

    private void UpdateTextFormat()
    {
        textFormatBuilder.Clear();

        // End the format
        textFormatBuilder.Append("}");

        // Apply to the text format
        textFormat = textFormatBuilder.ToString();
    }

    private void OnValidate()
    {
        UpdateProgressVisuals();

        // This handles ensuring percent is clamped
        // and will update the visuals in edit mode.
        HMratio = ratio;
    }

}
