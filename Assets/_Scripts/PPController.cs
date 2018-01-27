using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PPController : MonoBehaviour {

    private PostProcessingBehaviour pp;
    private ColorGradingModel.BasicSettings basicSettings;

    public float animationTime = 1.0f;

    // Temperature
    public int temperatureRange = 20;
    private float temperatureTarget, temperatureSpeed;

    // 
    public int hueRange = 15;
    private float hueTarget, hueSpeed;

    // Timer
    private float previousTime;

	// Use this for initialization
	void Start () {
        pp = GetComponentInParent<PostProcessingBehaviour>();
        previousTime = Time.time;
        MusicManager.OnBeat += CheckTime;
        SetValues();
	}

    private void CheckTime()
    {
        var delta = Time.time - previousTime;
        var ratio = delta / animationTime;
        if (ratio >= 0.5f && ratio <= 2.0f)
        {
            animationTime = delta;
        }
        previousTime = Time.time;
    }

    void OnDestroy()
    {
        var temp = pp.profile.colorGrading.settings;
        temp.basic = basicSettings;
        pp.profile.colorGrading.settings = temp;
    }

    private void SetValues()
    {
        basicSettings = pp.profile.colorGrading.settings.basic;
        temperatureTarget = basicSettings.temperature;
        temperatureSpeed = 1;
        hueTarget = basicSettings.hueShift;
        hueSpeed = 1;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        UpdateColorGrading();
        UpdateHue();
	}

    private void UpdateColorGrading()
    {
        var temp = pp.profile.colorGrading.settings;
        if (temp.basic.temperature >= temperatureTarget && temperatureSpeed > 0)
        {
            temperatureSpeed = -1;
            temperatureTarget = basicSettings.temperature - temperatureRange;
            return;
        } else if (temp.basic.temperature <= temperatureTarget && temperatureSpeed < 0)
        {
            temperatureSpeed = 1;
            temperatureTarget = basicSettings.temperature + temperatureRange;
            return;
        }
        temp.basic.temperature += temperatureSpeed * temperatureRange * Time.fixedDeltaTime / animationTime;
        pp.profile.colorGrading.settings = temp;
    }

    private void UpdateHue()
    {
        var temp = pp.profile.colorGrading.settings;
        if (temp.basic.hueShift >= hueTarget && hueSpeed > 0)
        {
            hueSpeed = -1;
            hueTarget = basicSettings.hueShift - hueRange;
            return;
        }
        else if (temp.basic.hueShift <= hueTarget && hueSpeed < 0)
        {
            hueSpeed = 1;
            hueTarget = basicSettings.hueShift + hueRange;
            return;
        }
        temp.basic.hueShift += hueSpeed * hueRange * Time.fixedDeltaTime / animationTime * 2;
        pp.profile.colorGrading.settings = temp;
    }
}
