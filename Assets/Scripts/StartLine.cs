using UnityEngine;

public class StartLine : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    void Start()
    {
        this.GetComponent<Renderer>().material.color = Color.white;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameController.PlayerCrossedStartLine();
        }
    }

}