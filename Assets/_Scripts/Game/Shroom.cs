using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShroomType
{
    Normal, Trap
}

public class Shroom : MonoBehaviour {

    public Sprite icon;
    public int x, y;
    private int explosionTurns, explosionRadius = 4;
    private int turnsPassed;
    private TextMesh turnText;

    // Animation
    private float beatTime = 0.43f;
    private float previousTime;

    private void Start()
    {
        ScoreManager.Instance.shroomBeatAnimator.OnBeat += CheckTurn;
        turnText = GetComponentInChildren<TextMesh>();
        explosionTurns = FindObjectOfType<MusicManager>().TimeToBarAfterNext();
        turnText.text = explosionTurns.ToString();
        
    }

    private void CheckTurn()
    {
        explosionTurns--;
        turnText.text = explosionTurns.ToString();

        if(explosionTurns == 1){
            AkSoundEngine.PostEvent("PercussionStinger",gameObject);
        }

        if(explosionTurns <= 0)
        {
            OnTurnsPassed();
            ScoreManager.Instance.shroomBeatAnimator.OnBeat -= CheckTurn;
        }
    }

    protected virtual void OnTurnsPassed()
    {
        TriggerExplosion();
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.shroomBeatAnimator.OnBeat -= CheckTurn;
        ScoreManager.Instance.fawnBeatAnimator.OnBeat -= Tick;
    }

    public virtual ShroomType GetShroomType()
    {
        return ShroomType.Normal;
    }

    public virtual void DestroyShroom()
    {
        Destroy(gameObject);
    }

    public void SetPos(int x, int y)
    {
        this.x = x;
        this.y = y;
        transform.localPosition = GenerateBoard.Instance.GetPos(x, y);
    }

    public virtual void TriggerExplosion()
    {
        /*
        var r = GetComponentInChildren<Renderer>();
        if (r)
            r.enabled = false;

        turnText.gameObject.SetActive(false);
        */
        previousTime = Time.time;
        Tick();
        ScoreManager.Instance.fawnBeatAnimator.OnBeat += Tick;
    }
    
    protected virtual void Tick()
    {
        var delta = Time.time - previousTime;
        var ratio = delta / beatTime;
        if (ratio >= 0.5f && ratio <= 2.0f)
        {
            if (turnsPassed == 0)
                turnText.gameObject.SetActive(false);

            turnsPassed++;

            int scoreDelta = 0;
            scoreDelta += GenerateBoard.Instance.ChangeTileOwner(x + turnsPassed, y, TileState.Shroom);
            scoreDelta += GenerateBoard.Instance.ChangeTileOwner(x - turnsPassed, y, TileState.Shroom);
            scoreDelta += GenerateBoard.Instance.ChangeTileOwner(x, y + turnsPassed, TileState.Shroom);
            scoreDelta += GenerateBoard.Instance.ChangeTileOwner(x, y - turnsPassed, TileState.Shroom);
            ScoreManager.Instance.GenerateFloatText(scoreDelta, false);

            if (turnsPassed >= explosionRadius)
            {
                ScoreManager.Instance.fawnBeatAnimator.OnBeat -= Tick;
                Destroy(gameObject);
            }
        }
        previousTime = Time.time;
    }
}
