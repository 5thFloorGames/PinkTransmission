using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance { get; private set; }

    public Transform fawnTargetTransform, shroomTargetTransform;
    public Vector3 delta;

    // Text
    public Text missTextPrefab, scoreTextPrefab;
    public BeatAnimator fawnBeatAnimator, shroomBeatAnimator;

    // Score
    public int maxScore = 200;
    public GameObject fawnWinScreen, shroomWinScreen;
    private float delay;
    public static bool end;
    private bool preend;

    private void Start()
    {
        Instance = this;

        if(!fawnBeatAnimator)
            fawnBeatAnimator = fawnTargetTransform.GetComponentInParent<BeatAnimator>();
        if(!shroomBeatAnimator)
            shroomBeatAnimator = shroomTargetTransform.GetComponentInParent<BeatAnimator>();
    }

    private void FixedUpdate()
    {
        if(end)
        {
            delay += Time.fixedDeltaTime;

            if(delay >= 1.0f && Input.anyKeyDown)
            {
                fawnWinScreen.SetActive(false);
                shroomWinScreen.SetActive(false);
                delay = 0;
                end = false;
                preend = false;
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                return;
            }
            return;
        }

        if(!preend && shroomTextScore < shroomScore)
        {
            shroomTextScore++;
            shroomTargetTransform.GetComponent<Text>().text = shroomTextScore.ToString("000");

            if(!end && shroomTextScore >= maxScore)
            {
                preend = true;
                MusicManager.Instance.EndGameMusic();
                shroomWinScreen.SetActive(true);
                StartCoroutine(SetEnd(1.0f));
            }
        }

        if(!preend && fawnTextScore < fawnScore)
        {
            fawnTextScore++;
            fawnTargetTransform.GetComponent<Text>().text = fawnTextScore.ToString("000");


            if (!end && fawnTextScore >= maxScore)
            {
                preend = true;
                MusicManager.Instance.EndGameMusic();
                fawnWinScreen.SetActive(true);
                StartCoroutine(SetEnd(1.0f));
            }
        }
    }

    private IEnumerator SetEnd(float delay)
    {
        yield return new WaitForSeconds(delay);

        end = true;
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
