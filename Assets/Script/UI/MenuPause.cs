using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuPause : MonoBehaviour
{
    [FormerlySerializedAs("MenuPauseUI")] [SerializeField] private GameObject menuPauseUI;
    [FormerlySerializedAs("MenuPauseAction")] [SerializeField] private InputActionReference menuPauseAction;
    [SerializeField] private ManagePlayer player;
    [SerializeField] private TextMeshProUGUI messageDead;
    
    private bool _paused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuPauseUI.SetActive(false);
    }

    private void OnEnable()
    {
        menuPauseAction.action.performed += Pause;
        menuPauseAction.action.canceled += PauseCanceled;
        menuPauseAction.action.Enable();
    }
    private void OnDisable()
    {
        menuPauseAction.action.performed -= Pause;
        menuPauseAction.action.canceled -= PauseCanceled;
        menuPauseAction.action.Disable();
    }
    private void PauseCanceled(InputAction.CallbackContext obj)
    {
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        _paused = !_paused;
    }

    public void Resume()
    {
        if (player.IsDead())
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            menuPauseUI.SetActive(false);
            Time.timeScale = 1f;
            _paused = false;
        }
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        if (_paused)
        {
            if (player.IsDead())
            {
                messageDead.gameObject.SetActive(true);
            }
            menuPauseUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Resume();
        }
    }
}
