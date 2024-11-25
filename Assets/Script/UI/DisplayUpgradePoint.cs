using TMPro;
using UnityEngine;

public class DisplayUpgradePoint : MonoBehaviour
{
    [SerializeField] private ManagePlayer player;
    [SerializeField] private TextMeshProUGUI textMesh;
    
    private int _textPoints;
    private string _startText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textPoints = player.GetPoint();
        _startText = textMesh.text;
        UpdateText();
    }

    private void UpdateText()
    {
        textMesh.text = _startText + " " + _textPoints;
    }
    // Update is called once per frame
    void Update()
    {
        if (_textPoints != player.GetPoint())
        {
            _textPoints = player.GetPoint();
            UpdateText();
        }
    }
}
