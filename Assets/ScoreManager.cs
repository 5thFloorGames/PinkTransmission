using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance { get; private set; }

    public Transform fawnTargetTransform, shroomTargetTransform;
    public Vector3 delta;

    // Text
    public Text missTextPrefab, scoreTextPrefab;
    public BeatAnimator fawnBeatAnimator, shroomBeatAnimator;


    private void Start()
    {
        Instance = this;
        fawnBeatAnimator = fawnTargetTransform.GetComponentInParent<BeatAnimator>();
        shroomBeatAnimator = shroomTargetTransform.GetComponentInParent<BeatAnimator>();
    }

    public void GenerateFloatText(int score, bool isFawn)
    {
        var text = Instantiate(score == 0 ? missTextPrefab : scoreTextPrefab, transform);
        if (score > 0)
            text.text = "+" + score.ToString("00");
        text.transform.position = isFawn ? fawnTargetTransform.position : shroomTargetTransform.position;
        text.transform.localPosition += delta;
    }
}
