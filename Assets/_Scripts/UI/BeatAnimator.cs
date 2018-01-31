using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatAnimator : MonoBehaviour {

    [SerializeField]
    private GameObject beatLinePrefab;
    private List<GameObject> beatLines;

    // Timer
    public float animationTime = 0.43f;
    private float previousTime;

    // Line movement
    public float lineSpeed = 600f;
    public int coeff = 1;
    private int counter;

    // Anim
    private Animator animator;

    public delegate void BeatAction();
    public event BeatAction OnBeat;

    // Use this for initialization
    void Start ()
    {
        beatLines = new List<GameObject>();
        MusicManager.OnBeat += CheckTime;
        previousTime = Time.time;
        animator = GetComponent<Animator>();
    }
    
    void OnDestroy()
    {
        MusicManager.OnBeat -= CheckTime;
    }

    public float Ratio
    {
        get { return (Time.time - previousTime) / animationTime; }
    }

    public bool CloseToBeat()
    {
        var ratio = Ratio;
        return (counter % coeff == 0 && ratio >= 0.65f) || (counter % coeff == coeff - 1 && ratio <= 0.35f);
    }

    private void CheckTime()
    {
        var ratio = Ratio;
        if (ratio >= 0.5f && ratio <= 2.0f)
        {
            if (counter++ % coeff == 0)
            {
                // Left line
                var lineL = Instantiate(beatLinePrefab, transform.parent);
                lineL.transform.localPosition = transform.localPosition
                    + Vector3.left * lineSpeed * animationTime * coeff;
                beatLines.Add(lineL);

                // Right line
                var lineR = Instantiate(beatLinePrefab, transform.parent);
                lineR.transform.localPosition = transform.localPosition
                    + Vector3.right * lineSpeed * animationTime * coeff;
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

            if(lineL.transform.localPosition.x >= transform.localPosition.x)
            {
                Destroy(lineR.gameObject);
                beatLines.RemoveAt(i);
                Destroy(lineL.gameObject);
                beatLines.RemoveAt(i - 1);
                animator.SetTrigger("Bump");

                if(OnBeat != null)
                    OnBeat();
            }
        }
    }

}
