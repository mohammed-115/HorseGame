using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator horseAnimator;
    public static bool gameHasStarted = false;

    void Update()
    {
        // التحقق من أول ضغطة لبدء اللعبة
        if (!gameHasStarted && (Input.anyKeyDown || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameHasStarted = true;
        // تفعيل باراميتر بدء اللعبة في الـ Animator
        horseAnimator.SetBool("IsGameStarting", true);
        Debug.Log("Game Started!");
    }
}