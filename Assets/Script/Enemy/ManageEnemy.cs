using System.Collections;
using UnityEngine;

public class ManageEnemy : MonoBehaviour
{
    [SerializeField] private int force;
    [SerializeField] private float timeToForce;
    
    private int _pv = 100;
    private int _attack = 10;
    private float _reloadRight;
    private float _reloadLeft;
    private int _moveSpeed = 10;
    private int _rotationSpeed = 10;
    private int _score = 20;
    private bool _isReloadRight;
    private bool _isReloadLeftt;
    private bool _activeForce = true;
    private Rigidbody _rb;
    private GameObject _player;


    public void Player(GameObject player)
    {
        _player = player;
    }
    public int GetScore()
    {
        return _score;
    }
    public int GetMoveSpeed()
    {
        return _moveSpeed;
    }
    public int GetRotationSpeed()
    {
        return _rotationSpeed;
    }
    public int GetAttack()
    {
        return _attack;
    }
    public int GetPv()
    {
        return _pv;
    }
    public float GetReloadRight()
    {
        return _reloadRight;
    }
    public float GetReloadLeft()
    {
        return _reloadLeft;
    }

    public void Upgrade(int newPv, int newAttack, int newSpeed, int newScore)
    {
        _pv = newPv;
        _attack = newAttack;
        _rotationSpeed = newSpeed;
        _moveSpeed = newSpeed;
        _score = newScore;
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(BuoyancySimulator());
    }

    IEnumerator BuoyancySimulator()
    {
        while (_activeForce)
        {
            yield return new WaitForSeconds(timeToForce);
            _rb.AddForce(Vector3.up * -force);
        }
    }
      
        public void Damage(int damage)
        {
            _pv -= damage;
        }
        
        public void ReloadRight()
        {
            _isReloadRight = true;
        }
    
        public void ReloadLeft()
        {
            _isReloadLeftt = true;
        }

        private void IsDead()
        {
            if (_pv <= 0)
            {
                _player.GetComponent<ManagePlayer>().UpPoint(GetScore());
                Destroy(gameObject);
            }
        }

    // Update is called once per frame
    void Update()
    {
        IsDead();
        
        if (_isReloadRight)
        {
            _reloadRight += Time.deltaTime;
            
            if (_reloadRight >= 5)
            {
                _isReloadRight = false;
                _reloadRight = 0;
            }
        }

        if (_isReloadLeftt)
        {
            _reloadLeft += Time.deltaTime;
            
            if (_reloadLeft >= 5)
            {
                _isReloadLeftt = false;
                _reloadLeft = 0;
            }
        }
    }
}
