using System;
using UnityEngine;

public class PlayerDieManager : MonoBehaviour
{

    public static PlayerDieManager instance;

    [SerializeField] private GameObject endScreenUI;
    [SerializeField] private Animator horseAnimator;

    public event EventHandler onEndScreenShow;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endScreenUI.SetActive(false);
    }

    public void Obstacle_onPlayerDie(object sender, EventArgs e)
    {

        endScreenUI.SetActive(true);
        GameManagment.stopCompletly = true;
        GameManagment.AbleToMove = false;
        HorseAudioManager.instance.StopRunningSound();
        horseAnimator.SetBool("IsGameStarting", false);
        horseAnimator.SetBool("onHorseDie", true);

        onEndScreenShow?.Invoke(this, EventArgs.Empty);

    }

}
