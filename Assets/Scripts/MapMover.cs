using UnityEngine;

public class MapMover : MonoBehaviour
{
    void Update()
    {
        if (!GameManager.AbleToMove || GameManager.stopCompletly) return;

        // نستخدم SpeedManager.GlobalSpeed بدلاً من المتغير المحلي
        transform.Translate(0, 0, SpeedManager.GlobalSpeed * Time.deltaTime * -1);

        // إذا خرج الماب عن حدود الرؤية (خلف اللاعب) يحذف نفسه
        if (transform.position.z < -500f) { Destroy(gameObject); }
    }
}