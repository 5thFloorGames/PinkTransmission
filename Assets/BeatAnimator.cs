using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatAnimator : MonoBehaviour {

    [SerializeField]
    private GameObject beatLinePrefab;
    private List<GameObject> beatLines;

    // Timer
    public float animationTime = 0.43f;
    private float previousTime;

    // Line movement
    public float lineSpeed = 600f;
    private int counter;

    // Use this for initialization
    void Start ()
    {
        beatLines = new List<GameObject>();
        MusicManager.OnBeat += CheckTime;
        previousTime = Time.time;
    }

    void OnDestroy()
    {
        MusicManager.OnBeat -= CheckTime;
    }

    private void CheckTime()
    {
        var delta = Time.time - previousTime;
        var ratio = delta / animationTime;
        if (ratio >= 0.5f && ratio <= 2.0f)
        {
            if (counter++ % 2 == 0)
            {
                // Left line
                var lineL = Instantiate(beatLinePrefab, transform);
                lineL.transform.localPosition = Vector3.left * lineSpeed * animationTime * 2;
                beatLines.Add(lineL);

                // Right line
                var lineR = Instantiate(beatLinePrefab, transform);
                lineR.transform.localPosition = Vector3.right * lineSpeed * animationTime * 2;
                beatLines.Add(lineR);
            }
        }
        previousTime = Time.time;
    }

    private void Update()
    {
        float delta = lineSpeed * Time.deltaTime;

        for(int i = beatLines.Count - 1; i >= 0; i -= 2)
        {
            // Right line
            var lineR = beatLines[i];
            lineR.transform.localPosition += Vector3.left * delta;

            // Left line
            var lineL = beatLines[i - 1];
            lineL.transform.localPosition += Vector3.right * delta;

            if(lineL.transform.localPosition.x >= 0)
            {
                Destroy(lineR.gameObject);
                beatLines.RemoveAt(i);
                Destroy(lineL.gameObject);
                beatLines.RemoveAt(i - 1);
            }
        }
    }
}
