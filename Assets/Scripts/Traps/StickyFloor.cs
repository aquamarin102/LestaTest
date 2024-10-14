using UnityEngine;

public class StickyFloor : MonoBehaviour
{
    [SerializeField] private float slowMultiplier = 0.5f;
    private float originalSpeed;
    private Renderer _trapRenderer;

    private void Start()
    {
        _trapRenderer = GetComponent<Renderer>();
        _trapRenderer.material.color = Color.green;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                originalSpeed = player.GetMoveSpeed();
                player.SetMoveSpeed(originalSpeed * slowMultiplier);  
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.SetMoveSpeed(originalSpeed);
            }
        }
    }
}
