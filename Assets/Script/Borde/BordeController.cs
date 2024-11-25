using System;
using UnityEngine;

public class BordeController : MonoBehaviour
{
    private GameObject _player;
    private GameObject _enemy;
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _enemy = GameObject.FindWithTag("Ennemie");
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            _player.GetComponent<ManagePlayer>().Damage(_enemy.GetComponent<ManageEnemy>().GetAttack());
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ennemie"))
        {
            GetComponent<AudioSource>().Play();
            _enemy.GetComponent<ManageEnemy>().Damage(_player.GetComponent<ManagePlayer>().GetAttack());
            Destroy(gameObject);
        }
    }
}
