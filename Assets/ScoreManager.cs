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

    // Score


    private void Start()
    {
        Instance = this;
        fawnBeatAnimator = fawnTargetTransform.GetComponentInParent<BeatAnimator>();
        shroomBeatAnimator = shroomTargetTransform.GetComponentInParent<BeatAnimator>();
    }

    private void FixedUpdate()
    {
        if(shroomTextScore < shroomScore)
        {
            shroomTextScore++;
            shroomTargetTransform.GetComponent<Text>().text = shroomTextScore.ToString("000");
        }

        if(fawnTextScore < fawnScore)
        {
            fawnTextScore++;
            fawnTargetTransform.GetComponent<Text>().text = fawnTextScore.ToString("000");
        }
    }

    private bool fawnTriggered, shroomTriggered;
    private int fawnDelta, shroomDelta;
    private int fawnScore, fawnTextScore, shroomScore, shroomTextScore;
    public void GenerateFloatText(int score, bool isFawn)
    {
        if(isFawn)
        {
            if (!fawnTriggered)
                StartCoroutine(GenerateFawnScore());
            fawnTriggered = true;
            fawnDelta += score;
        } else
        {
            if (!shroomTriggered)
                StartCoroutine(GenerateShroomScore());
            shroomTriggered = true;
            shroomDelta += score;
        }
    }   

    private IEnumerator GenerateFawnScore()
    {
        yield return new WaitForEndOfFrame();

        var text = Instantiate(fawnDelta == 0 ? missTextPrefab : scoreTextPrefab, transform);
        if (fawnDelta > 0)
            text.text = "+" + fawnDelta.ToString("00");
        text.transform.position = fawnTargetTransform.position;
        text.transform.localPosition += delta;
        fawnScore += fawnDelta;
        fawnDelta = 0;
        fawnTriggered = false;
    }

    private IEnumerator GenerateShroomScore()
    {
        yield return new WaitForEndOfFrame();

        var text = Instantiate(shroomDelta == 0 ? missTextPrefab : scoreTextPrefab, transform);
        if (shroomDelta > 0)
            text.text = "+" + shroomDelta.ToString("00");
        text.transform.position = shroomTargetTransform.position;
        text.transform.localPosition += delta;
        shroomScore += shroomDelta;
        shroomDelta = 0;
        shroomTriggered = false;
    }
}
