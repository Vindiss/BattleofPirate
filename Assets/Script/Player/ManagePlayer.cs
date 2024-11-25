using System.Collections;
using UnityEngine;

public class ManagePlayer : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int force;
    [SerializeField] private float timeToForce;
    
    
    private int _pv = 100;
    private int _attack = 20;
    private float _reloadRight;
    private float _reloadLeft;
    private int _moveSpeed = 10;
    private int _rotationSpeed = 10;
    private int _point;
    private bool _isReloadRight;
    private bool _isReloadLeft;
    private bool _activeForce = true;
    private bool _isDead;
    private int _maxPv;
    private Rigidbody _rb;
    
    
    public int GetPoint()
    {
        return _point;
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

    public int GetMaxPv()
    {
        return _maxPv;
    }
    public float GetRelodRight()
    {
        return _reloadRight;
    }
    public float GetRelodLeft()
    {
        return _reloadLeft;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(BuoyancySimulator());
        _maxPv = _pv;
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

    public void UpPoint(int point)
    {
        _point += point;
    }
    
    public void DropPoint(int point)
    {
        _point -= point;
    }

    public void ReloadRight()
    {
        _isReloadRight = true;
    }
    
    public void ReloadLeft()
    {
        _isReloadLeft = true;
    }
    
    public bool IsDead()
    {
        if (_pv <= 0)
        {
            return _isDead = true;
        }
        else
        {
            return _isDead = false;
        }
    }

    public void UpgradePv(int newPv)
    {
        _pv += newPv;
        _maxPv = _pv;
    }

    public void UpgradeAttack(int newAttack)
    {
        _attack = newAttack;
    }

    public void UpgradeSpeed(int newSpeed)
    {
        _moveSpeed = newSpeed;
        _rotationSpeed = newSpeed;
    }

    public void Repair(int newRepair)
    {
        _pv += newRepair;

        if (_pv > _maxPv)
        {
            _pv = _maxPv;
        }
    }
    
    private void Update()
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

        if (_isReloadLeft)
        {
            _reloadLeft += Time.deltaTime;
            
            if (_reloadLeft >= 5)
            {
                _isReloadLeft = false;
                _reloadLeft = 0;
            }
        }
    }
}
