using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PPController : MonoBehaviour {

    private PostProcessingBehaviour pp;
    private ColorGradingModel.BasicSettings basicSettings;

    public int temperatureRange = 20;
    public float temperatureChangeTime = 1.0f;
    private float targetTemperature, temperatureSpeed;

	// Use this for initialization
	void Start () {
        pp = GetComponentInParent<PostProcessingBehaviour>();
        SetValues();
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
        targetTemperature = basicSettings.temperature;
        temperatureSpeed = 1;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        UpdateColorGrading();
	}

    private void UpdateColorGrading()
    {
        var temp = pp.profile.colorGrading.settings;
        if (temp.basic.temperature >= targetTemperature && temperatureSpeed > 0)
        {
            temperatureSpeed = -1;
            targetTemperature = basicSettings.temperature - temperatureRange;
            return;
        } else if (temp.basic.temperature <= targetTemperature && temperatureSpeed < 0)
        {
            temperatureSpeed = 1;
            targetTemperature = basicSettings.temperature + temperatureRange;
            return;
        }
        temp.basic.temperature += temperatureSpeed * temperatureRange * Time.fixedDeltaTime / temperatureChangeTime;
        pp.profile.colorGrading.settings = temp;
    }
}
