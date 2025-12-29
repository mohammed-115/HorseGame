using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private LayerMask horseChestMask;
    public event EventHandler onPlayerDie;

    private void OnEnable()
    {
        onPlayerDie += PlayerDieManager.instance.Obstacle_onPlayerDie;
    }


    private void OnDisable()
    {
        onPlayerDie -= PlayerDieManager.instance.Obstacle_onPlayerDie;
    }

    private void OnTriggerEnter(Collider other)
    {
        // الطريقة الصحيحة للتحقق مما إذا كانت طبقة الجسم داخل الـ LayerMask
        if (((1 << other.gameObject.layer) & horseChestMask) != 0)
        {
            Debug.Log("Game Over!");
            onPlayerDie?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Hit something, but not the right layer: " + LayerMask.LayerToName(other.gameObject.layer));
        }
        
    } 
}