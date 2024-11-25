using UnityEngine;

// Buoyancy.cs
// Version 3.0.0?
// https://forum.unity3d.com/threads/buoyancy-script.72974/
// Terms of use: do whatever you like
public class Buoyancy : MonoBehaviour
{
    [SerializeField] private float waterLevel;
    [SerializeField] private float buoyancyForce;
    [SerializeField] private float damping;

    
    private Rigidbody _rb;
    private float _depth;
    private Vector3 _force;

    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (transform.position.y < waterLevel)
        {
            _depth = waterLevel - transform.position.y;
            _force = Vector3.up * (buoyancyForce * _depth) - _rb.linearVelocity * damping;
            _rb.AddForce(_force, ForceMode.Acceleration);
        }
    }
}