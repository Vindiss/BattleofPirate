using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int attackRange;
    [FormerlySerializedAs("bordeRight")] [SerializeField] private GameObject bordeRightPrefab;
    [FormerlySerializedAs("bordeLeft")] [SerializeField] private GameObject bordeLeftPrefab;
    [SerializeField] private Transform bordeShootRight;
    [SerializeField] private Transform bordeShootLeft;
    [SerializeField] private float bordeForce = 100f;

    private Transform _player;
    private float _distanceToPlayer;
    private Vector3 _directionToPlayer;
    private float _crossProduit;
    
    public void Player(GameObject player)
    {
        _player = player.transform;
    }
    private void AttackPlayer()
    {
        _directionToPlayer = _player.position - transform.position;
        _crossProduit = Vector3.Cross(transform.forward, _directionToPlayer).y;
        
        if (_crossProduit > 0 ) //droite
        {
            Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * GetComponent<ManageEnemy>().GetRotationSpeed());
            if (GetComponent<ManageEnemy>().GetReloadRight() == 0)
            {
                AttackRight();
            }
        }
        else if (_crossProduit < 0) //gauche
        {
            Quaternion targetRotation = Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * GetComponent<ManageEnemy>().GetRotationSpeed());
            if (GetComponent<ManageEnemy>().GetReloadLeft() == 0)
            {
                AttackLeft();
            }
        }
    }
    private void AttackRight()
    {
        GameObject borde = Instantiate(bordeRightPrefab, bordeShootRight.position, bordeShootRight.rotation);
        borde.GetComponent<Rigidbody>().AddForce(gameObject.transform.right * bordeForce, ForceMode.Impulse);
        Destroy(borde, 5f);
        GetComponent<ManageEnemy>().ReloadRight();
    }
    private void AttackLeft()
    {
        GameObject borde = Instantiate(bordeLeftPrefab, bordeShootLeft.position, bordeShootLeft.rotation);
        borde.GetComponent<Rigidbody>().AddForce(gameObject.transform.right * -bordeForce, ForceMode.Impulse);
        Destroy(borde, 5f);
        GetComponent<ManageEnemy>().ReloadLeft();
    }

    private void Update()
    {
        _distanceToPlayer = Vector3.Distance(_player.position, transform.position);
        if (_player != null)
        {
            if (_distanceToPlayer < (attackRange))
            {
                if (GetComponent<ManageEnemy>().GetReloadRight() == 0 || GetComponent<ManageEnemy>().GetReloadLeft() == 0)
                {
                    AttackPlayer();
                }
            }
            else
            {
                Vector3 direction = (_player.position - transform.position).normalized;
        
                transform.position += direction * (GetComponent<ManageEnemy>().GetMoveSpeed() * Time.deltaTime);
        
                transform.LookAt(_player);
            }
        }

    }
}
