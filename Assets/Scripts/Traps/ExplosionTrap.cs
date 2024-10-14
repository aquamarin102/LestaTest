using System.Collections;
using UnityEngine;

public class ExplosionTrap : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _activationDelay = 1f;
    [SerializeField] private float _rechargeTime = 5f;

    [SerializeField] private Color _idleColor = Color.white;
    [SerializeField] private Color _activeColor = Color.yellow;
    [SerializeField] private Color _damageColor = Color.red; 
    
    private Renderer _trapRenderer;  

    private bool _isActivated = false;  

    private void Start()
    {
        _trapRenderer = GetComponent<Renderer>();
        _trapRenderer.material.color = _idleColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActivated && other.CompareTag("Player"))  
        {
            StartCoroutine(ActivateTrap());
        }
    }

    IEnumerator ActivateTrap()
    {
        _isActivated = true;
        _trapRenderer.material.color = _activeColor;  
        yield return new WaitForSeconds(_activationDelay);  

        _trapRenderer.material.color = _damageColor;  
        DealDamage();  

        yield return new WaitForSeconds(0.5f); 
        _trapRenderer.material.color = _idleColor;  

        yield return new WaitForSeconds(_rechargeTime);  
        _isActivated = false;  
    }

    private void DealDamage()
    {
        Vector3 boxSize = new Vector3(3f, 3f, 3f);
        Collider[] hitColliders = Physics.OverlapBox(transform.position, boxSize / 2);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                PlayerController player = hitCollider.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(_damage);
                }
                Debug.Log("Player takes damage: " + _damage);
                
            }
        }
    }
}
