using UnityEngine;
using UnityEngine.SceneManagement;
// تأكد من استيراد هذه المكتبة التي تأتي مع الـ Package الذي قمت بتنصيبه
using FlutterUnityIntegration;

public class EndScreenButtons : MonoBehaviour
{

    public void BUTTON_PLAY_AGAIN()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // ملاحظة: تأكد من كتابة اسم الكلاس GameManagement بشكل صحيح (غالباً بالـ e وليس a)
        GameManagment.stopCompletly = false;
        GameManagment.AbleToMove = false;
        SpeedManager.GlobalSpeed = 10;
    }

    public void BUTTON_MAIN_MENU()
    {
        // إرسال رسالة إلى فلاتر تخبره أننا نريد العودة للقائمة
        // يمكنك تسمية الرسالة أي شيء، مثلاً "close" أو "back_to_menu"
        UnityMessageManager.Instance.SendMessageToFlutter("exit_game");
    }
}