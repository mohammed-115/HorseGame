using System;
using UnityEngine;

public class PlayerDieManager : MonoBehaviour
{
    public static PlayerDieManager instance;

    [SerializeField] private GameObject arabicEndScreenUI;
    [SerializeField] private GameObject englishEndScreenUI;
    private GameObject defaultEndScreenUI;
    [SerializeField] private Animator horseAnimator;

    public event EventHandler onEndScreenShow;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        arabicEndScreenUI.SetActive(false);
        englishEndScreenUI.SetActive(false);

        // القيمة الافتراضية هي الإنجليزية
        defaultEndScreenUI = englishEndScreenUI;
    }

    // هذه الميثود سيتم استدعاؤها من طرف Flutter
    public void SetLanguage(string language)
    {
        if (language == "ar")
        {
            defaultEndScreenUI = arabicEndScreenUI;
            Debug.Log("Unity: Language set to Arabic");
        }
        else
        {
            defaultEndScreenUI = englishEndScreenUI;
            Debug.Log("Unity: Language set to English");
        }
    }

    public void Obstacle_onPlayerDie(object sender, EventArgs e)
    {
        // تفعيل الشاشة المختارة بناءً على اللغة
        if (defaultEndScreenUI != null)
        {
            defaultEndScreenUI.SetActive(true);
        }

        GameManagment.stopCompletly = true;
        GameManagment.AbleToMove = false;
        HorseAudioManager.instance.StopRunningSound();
        horseAnimator.SetBool("IsGameStarting", false);
        horseAnimator.SetBool("onHorseDie", true);

        onEndScreenShow?.Invoke(this, EventArgs.Empty);
    }
}