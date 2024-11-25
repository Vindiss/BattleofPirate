using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class UIUpgradePlayers : MonoBehaviour
{
    [FormerlySerializedAs("UiUpgradePlayer")] [SerializeField] private GameObject uiUpgradePlayer;
    [FormerlySerializedAs("MenuUpgradeAction")] [SerializeField] private InputActionReference menuUpgradeAction;
    [FormerlySerializedAs("Player")] [SerializeField] private ManagePlayer player;
    [FormerlySerializedAs("textMeshPV")] [SerializeField] private TextMeshProUGUI textMeshPv;
    [SerializeField] private TextMeshProUGUI textMeshAttack;
    [SerializeField] private TextMeshProUGUI textMeshSpeed;
    [SerializeField] private TextMeshProUGUI textMeshMediumKit;
    [SerializeField] private TextMeshProUGUI textMeshFullKit;

    private bool _isUpgraded;
    private int _countUpgradePv = 1;
    private float _upgradePv = 1.1f;
    private int _dropPointPv = 10;
    private int _countUpgradeAttack = 1;
    private float _upgradeAttack = 1.1f;
    private int _dropPointAttack = 10;
    private int _countUpgradeSpeed = 1;
    private readonly float _upgradeSpeed = 1.2f;
    private int _dropPointSpeed = 10;
    private static int _dropPointMediumKit = 5;
    private static int _dropPointFullKit =  10;
    private string _startTextPv;
    private string _startTextAttack;
    private string _startTextSpeed;
    private string _startTextMediumKit;
    private string _startTextFullKit;
    private readonly int _dropPointFullKitMax = _dropPointFullKit * 6 * 10;
    private readonly int _dropPointMediumKitMax = _dropPointMediumKit * 6 * 5;

    private void Start()
    {
        _startTextPv = textMeshPv.text;
        _startTextAttack = textMeshAttack.text;
        _startTextSpeed = textMeshSpeed.text;
        _startTextMediumKit = textMeshMediumKit.text;
        _startTextFullKit = textMeshFullKit.text;
        uiUpgradePlayer.SetActive(false);
    }

    private void OnEnable()
    {
        menuUpgradeAction.action.performed += MenuUpgrade;
        menuUpgradeAction.action.canceled += MenuUpgradeCanceled;
        menuUpgradeAction.action.Enable();
    }
    private void OnDisable()
    {
        menuUpgradeAction.action.performed -= MenuUpgrade;
        menuUpgradeAction.action.canceled -= MenuUpgradeCanceled;
        menuUpgradeAction.action.Disable();
    }
    private void MenuUpgradeCanceled(InputAction.CallbackContext context)
    {
    }
    private void MenuUpgrade(InputAction.CallbackContext context)
    {
        _isUpgraded = !_isUpgraded;
    }

    public bool GetUpgradded()
    {
        return _isUpgraded;
    }
    public void UpgradePvButton()
    {
        Debug.Log(player.GetPv());
        if (_countUpgradePv <= 6 && player.GetPoint() >= _dropPointPv)
        {
            float newPv = player.GetPv() * _upgradePv;
            int newPvPlayer = (int)Mathf.Ceil(newPv);
        
            player.UpgradePv(newPvPlayer);
            player.DropPoint(_dropPointPv);
            _countUpgradePv += 1;
            _upgradePv += 0.1f;
            _dropPointPv = _dropPointPv * 2;
        }
    }
    public void UpgradeAttackButton()
    {
        if (_countUpgradeAttack <= 6 && player.GetPoint() >= _dropPointPv)
        {
            float newAttack = player.GetAttack() * _upgradeAttack;
            int newAttackPlayer = (int)Mathf.Ceil(newAttack);
        
            player.UpgradeAttack(newAttackPlayer);
            player.DropPoint(_dropPointAttack);
            _countUpgradeAttack += 1;
            _upgradeAttack += 0.1f;
            _dropPointAttack = _dropPointAttack * 2;
        }
    }
    public void UpgradeSpeedButton()
    {
        if (_countUpgradeSpeed <= 6 && player.GetPoint() >= _dropPointPv)
        {
            float newSpeed = player.GetMoveSpeed() * _upgradeSpeed;
            int newSpeedPlayer = (int)Mathf.Ceil(newSpeed);
        
            player.UpgradeSpeed(newSpeedPlayer);
            player.DropPoint(_dropPointSpeed);
            _countUpgradeSpeed += 1;
            _dropPointSpeed = _dropPointSpeed * 2;
        }
        Debug.Log("speed apres"+player.GetMoveSpeed());
    }

    public void MediumKitUse()
    {
        if (_countUpgradePv <= 6 && _countUpgradePv != 1)
        {
            _dropPointMediumKit = _dropPointMediumKit * _countUpgradePv;
        }
        else if (_countUpgradePv > 6)
        {
            
            _dropPointMediumKit = _dropPointMediumKitMax;
        }
        
        int repairBoat = player.GetPv() / 2;
        if (player.GetMaxPv() != player.GetPv())
        {
            player.Repair(repairBoat);
            player.DropPoint(_dropPointMediumKit);   
        }
    }

    public void FullKitUse()
    {
        if (_countUpgradePv <= 6 && _countUpgradePv != 1)
        {
            _dropPointFullKit = _dropPointFullKit * _countUpgradePv;
        }
        else if (_countUpgradePv > 6)
        {
            
            _dropPointFullKit = _dropPointFullKitMax;
        }

        int repairBoat = player.GetPv();
        if (player.GetMaxPv() != player.GetPv())
        {
            player.Repair(repairBoat);
            player.DropPoint(_dropPointFullKit);
        }
    }
    
    private void UpdateText()
    {
        textMeshPv.text = _startTextPv + " " + _dropPointPv;
        textMeshAttack.text = _startTextAttack + " " + _dropPointAttack;
        textMeshSpeed.text = _startTextSpeed + " " + _dropPointSpeed;
        textMeshMediumKit.text = _startTextMediumKit + " " + _dropPointMediumKit;
        textMeshFullKit.text = _startTextFullKit + " " + _dropPointFullKit;
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateText();
        if (_isUpgraded)
        {
           uiUpgradePlayer.SetActive(true);
        }
        else
        {
            uiUpgradePlayer.SetActive(false);
        }
    }
}
