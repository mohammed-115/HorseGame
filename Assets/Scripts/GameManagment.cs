using System;
using UnityEngine;

public class GameManagment : MonoBehaviour
{
    public Animator horseAnimator;
    public static bool AbleToMove = false;
    public static bool stopCompletly = false;

    void Update()
    {
        // التحقق من أول ضغطة لبدء اللعبة
        if ((!AbleToMove && (Input.anyKeyDown || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))) && !stopCompletly)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        AbleToMove = true;
        // تفعيل باراميتر بدء اللعبة في الـ Animator
        horseAnimator.SetBool("IsGameStarting", true);
        Debug.Log("Game Started!");
    }
}