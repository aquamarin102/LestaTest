using System.Collections;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField] private float _windForce = 10f;
    [SerializeField] private float _directionChangeInterval = 2f;  
    private Vector3 _currentWindDirection;  
    private Coroutine _windCoroutine;

    private Renderer _trapRenderer;

    private void Start()
    {
        _trapRenderer = GetComponent<Renderer>();
        _trapRenderer.material.color = Color.blue;
        StartCoroutine(ChangeWindDirection());  
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(_currentWindDirection * _windForce);
            }
        }
    }

    IEnumerator ChangeWindDirection()
    {
        while (true)
        {
            float randomX = Random.Range(-1f, 1f);
            _currentWindDirection = new Vector3(randomX, 0f, 0f).normalized;  

            Debug.Log("Wind direction: " + _currentWindDirection);

            yield return new WaitForSeconds(_directionChangeInterval); 
        }
    }
}
