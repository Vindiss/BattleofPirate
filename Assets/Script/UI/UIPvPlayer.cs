using UnityEngine;
using UnityEngine.Serialization;

public class UIPlayerManager : MonoBehaviour
{
    [SerializeField] private ManagePlayer player;
    [FormerlySerializedAs("PvPanel")] [SerializeField] private RectTransform pvPanel;


    private int _pvMax;
    private float _pvCurrent;
    private float _pvPercentage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pvMax = player.GetPv();
    }

    // Update is called once per frame
    void Update()
    {
        _pvMax = player.GetMaxPv();
        _pvCurrent = player.GetPv();
        _pvPercentage = _pvCurrent / _pvMax;
        if (_pvCurrent < _pvMax)
        {
            pvPanel.offsetMax = new Vector2(-(((1 - _pvPercentage) * 768.1641f)+700f), pvPanel.offsetMax.y);
        }
    }
}
