using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private LayerMask horseChestMask;

    private void OnTriggerEnter(Collider other)
    {
        // الطريقة الصحيحة للتحقق مما إذا كانت طبقة الجسم داخل الـ LayerMask
        if (((1 << other.gameObject.layer) & horseChestMask) != 0)
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("Hit something, but not the right layer: " + LayerMask.LayerToName(other.gameObject.layer));
        }
    }
}