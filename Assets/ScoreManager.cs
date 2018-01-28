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

    private bool fawnTriggered, shroomTriggered;
    private int fawnScore, shroomScore;
    public void GenerateFloatText(int score, bool isFawn)
    {
        if(isFawn)
        {
            if (!fawnTriggered)
                StartCoroutine(GenerateFawnScore());
            fawnTriggered = true;
            fawnScore += score;
        } else
        {
            if (!shroomTriggered)
                StartCoroutine(GenerateShroomScore());
            shroomTriggered = true;
            shroomScore += score;
        }
    }   

    private IEnumerator GenerateFawnScore()
    {
        yield return new WaitForEndOfFrame();

        var text = Instantiate(fawnScore == 0 ? missTextPrefab : scoreTextPrefab, transform);
        if (fawnScore > 0)
            text.text = "+" + fawnScore.ToString("00");
        text.transform.position = fawnTargetTransform.position;
        text.transform.localPosition += delta;
        fawnScore = 0;
        fawnTriggered = false;
    }

    private IEnumerator GenerateShroomScore()
    {
        yield return new WaitForEndOfFrame();

        var text = Instantiate(shroomScore == 0 ? missTextPrefab : scoreTextPrefab, transform);
        if (shroomScore > 0)
            text.text = "+" + shroomScore.ToString("00");
        text.transform.position = shroomTargetTransform.position;
        text.transform.localPosition += delta;
        shroomScore = 0;
        shroomTriggered = false;
    }
}
