using UnityEngine;
using System.Collections;

public class WorldAnimEventRelayer : MonoBehaviour
{
    [SerializeField]
    WorldDriver driver;

    public void OnTurnOnDone()
    {
        driver.OnTurnOnDone();
    }

    public void OnPauseToThink()
    {
        driver.OnPauseToThink();
    }

    public void OnSnake()
    {
        driver.OnSnake();
    }

    public void OnSnakeBite()
    {
        driver.OnSnakeBite();
    }

    public void OnFadeOut()
    {
        driver.OnFadeOut();
    }

    public void OnTitleDiary()
    {
        driver.OnTitleDiary();
    }

    public void OnSandWalkDone()
    {
        driver.OnSandWalkDone();
    }

    public void OnDanceDone()
    {
        driver.OnDanceDone();
    }

    public void OnBlankout()
    {
        driver.OnBlankout();
    }

    public void OnScorpion()
    {
        driver.OnScorpion();
    }

    public void OnDeath()
    {
        driver.OnDeath();
    }

    public void OnGameFinished()
    {
        driver.OnGameFinished();
    }
}
