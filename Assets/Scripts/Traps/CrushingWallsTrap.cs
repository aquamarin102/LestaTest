using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingWallsTrap : MonoBehaviour
{
    [SerializeField] private Transform _leftWall;
    [SerializeField] private Transform _rightWall;
    [SerializeField] private float _closeSpeed = 2f; 
    [SerializeField] private float _waitTimeBeforeClosing = 2f;  
    [SerializeField] private float _closedDuration = 1f;  
    [SerializeField] private float _openSpeed = 2f;  

    private Vector3 leftWallStartPosition;
    private Vector3 rightWallStartPosition;

    private void Start()
    {
        leftWallStartPosition = _leftWall.position;
        rightWallStartPosition = _rightWall.position;
        StartCoroutine(CrushingCycle());
    }

    private IEnumerator CrushingCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(_waitTimeBeforeClosing);
            StartCoroutine(CloseWalls());
            yield return new WaitForSeconds(_closedDuration);
            StartCoroutine(OpenWalls());
        }
    }

    private IEnumerator CloseWalls()
    {
        while (Vector3.Distance(_leftWall.position, _rightWall.position) > 0.5f)
        {
            _leftWall.position = Vector3.MoveTowards(_leftWall.position, _rightWall.position, _closeSpeed * Time.deltaTime);
            _rightWall.position = Vector3.MoveTowards(_rightWall.position, _leftWall.position, _closeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator OpenWalls()
    {
        while (_leftWall.position != leftWallStartPosition || _rightWall.position != rightWallStartPosition)
        {
            _leftWall.position = Vector3.MoveTowards(_leftWall.position, leftWallStartPosition, _openSpeed * Time.deltaTime);
            _rightWall.position = Vector3.MoveTowards(_rightWall.position, rightWallStartPosition, _openSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Vector3.Distance(_leftWall.position, _rightWall.position) < 1f)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(100f);  
            }
        }
    }
}
