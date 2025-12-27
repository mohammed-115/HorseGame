using UnityEngine;

public class SpanwNewMap : MonoBehaviour
{

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Transform mapOwner;

    private bool isSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        // الطريقة الصحيحة للتحقق مما إذا كانت طبقة الجسم داخل الـ LayerMask
        if (((1 << other.gameObject.layer) & playerMask) != 0)
        {
            if (!isSpawned)
            {
                isSpawned = true;
                Instantiate(mapOwner, new Vector3(0, 0, 470), Quaternion.identity);
            }
        }
        else
        {
            Debug.Log("Hit something, but not the right layer: " + LayerMask.LayerToName(other.gameObject.layer));
        }
    }

}
