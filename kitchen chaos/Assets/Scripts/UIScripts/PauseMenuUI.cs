using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    public static PauseMenuUI instance { get; private set; }
    [SerializeField] private Button resumeButtom;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionButton;

    private void Awake()
    {
        instance = this;
        resumeButtom.onClick.AddListener(() =>
        {
            GameHandler.Instance.ChangePause();
        }
        );
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.OnSceneLoader(SceneLoader.Scenes.MainMenu);
        }
        );
        optionButton.onClick.AddListener(() =>
        {
            OptonMenuUI.instance.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
);
    }
    private void Start()
    {
        GameHandler.Instance.OnPauseChanged += GameHandler_OnPauseChanged;
        gameObject.SetActive( false );
    }

    private void GameHandler_OnPauseChanged(object sender, System.EventArgs e)
    {
        gameObject.SetActive(GameHandler.Instance.ReturnPause());
        OptonMenuUI.instance.gameObject.SetActive(false);
    }

}
