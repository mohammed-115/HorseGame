using UnityEngine;

public class MapMover : MonoBehaviour
{

    [SerializeField] private float mapMoveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameHasStarted)
            return;

        transform.Translate(0, 0, mapMoveSpeed * Time.deltaTime * -1);
    }
}
