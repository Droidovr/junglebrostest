
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ReactionEscape
{


    public class EscapeReaction : MonoBehaviour
    {
        public static EscapeReaction Instance;


        private EscapeReactionStates escapeReactionStates;
        public EscapeReactionStates EscapeReactionStates { get { return escapeReactionStates; } set { escapeReactionStates = value; } }

        [SerializeField] private GameObject exitGame;
        [SerializeField] private GameObject restartGame;
        [SerializeField] private GameObject exitMenu;
        private MainMenuHomeScene mainMenuHomeScene;



        void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

            //escapeReactionStates = EscapeReactionStates.FromMenu;
            SceneManager.sceneLoaded += OnLevelLoaded;
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnClickEscape();
                Debug.Log(escapeReactionStates);
            }
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                var menuScript = FindObjectOfType<MenuManager>();
                if (MenuManager.Instance != null)
                    if(!MenuManager.Instance.Gameover)
                        menuScript.Pause();
            }
        }

        private void OnClickEscape()
        {
            switch (escapeReactionStates)
            {
                case EscapeReactionStates.FromMenu:
                    FromMenuExecute();
                    break;
                case EscapeReactionStates.FromShop:
                    FromShopExecute();
                    break;
                case EscapeReactionStates.FromHelp:
                    FromHelpExecute();
                    break;
                case EscapeReactionStates.FromPureGame:
                    FromPureGameExecute();
                    break;
                case EscapeReactionStates.FromGamePause:
                    FromGamePauseExecute();
                    break;
                case EscapeReactionStates.FromWindowMenu:
                    FromWindowMenuExecute();
                    break;
                case EscapeReactionStates.FromWindowRestart:
                    FromWindowRestartExecute();
                    break;
                case EscapeReactionStates.FromWorldChoose:
                    FromWorldChoose();
                    break;
                case EscapeReactionStates.FromLevelChoose:
                    FromLevelChoose();
                    break;
                case EscapeReactionStates.FromExitPanelMenu:
                    FromExitPanelMenu();
                    break;
            }
        }

        private void FromMenuExecute()
        {
            OpenExitMenuConfirm();
        }

        private void FromShopExecute()
        {
            mainMenuHomeScene.OpenStartMenu();
        }

        private void FromHelpExecute()
        {
            mainMenuHomeScene.CloseHelpPanel();
        }

        private void FromPureGameExecute()
        {
            var menuScript = FindObjectOfType<MenuManager>();
            menuScript.Pause();
        }

        private void FromGamePauseExecute()
        {
            var menuScript = FindObjectOfType<MenuManager>();
            menuScript.Pause();
        }

        private void FromWindowMenuExecute()
        {
            CloseQuitMenuConfirm();
        }

        private void FromWindowRestartExecute()
        {
            CloseRestartConfirm();
        }

        private void FromWorldChoose()
        {
            mainMenuHomeScene.OpenStartMenu();
        }

        private void FromLevelChoose()
        {
            mainMenuHomeScene.OpenWorldChoose();
        }

        private void FromExitPanelMenu()
        {
            ClosedExitMenuConfirm();
        }


        private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "MainMenu")
            {
                escapeReactionStates = EscapeReactionStates.FromMenu;
                mainMenuHomeScene = FindObjectOfType<MainMenuHomeScene>();
            }
        }

        public void OpenExitMenuConfirm()
        {
            exitGame.SetActive(true);
            EscapeReactionStates = EscapeReactionStates.FromExitPanelMenu;
        }

        public void ClosedExitMenuConfirm()
        {
            exitGame.SetActive(false);
            EscapeReactionStates = EscapeReactionStates.FromMenu;
        }

        public void ShowRestartConfirm()
        {
            restartGame.SetActive(true);
            EscapeReactionStates = EscapeReactionStates.FromWindowRestart;
        }

        public void CloseRestartConfirm()
        {
            restartGame.SetActive(false);
            EscapeReactionStates = EscapeReactionStates.FromGamePause;
        }

        public void RestartGame()
        {
            var menuScript = FindObjectOfType<MenuManager>();
            menuScript.RestartGame();
            restartGame.SetActive(false);
        }

        public void ShowQuitConfirm()
        {
            exitMenu.SetActive(true);
            EscapeReactionStates = EscapeReactionStates.FromWindowMenu;
        }

        public void CloseQuitMenuConfirm()
        {
            exitMenu.SetActive(false);
            EscapeReactionStates = EscapeReactionStates.FromGamePause;

        }

        public void QuitMenu()
        {
            var menuScript = FindObjectOfType<MenuManager>();
            menuScript.HomeScene();
            escapeReactionStates = EscapeReactionStates.FromMenu;
            exitMenu.SetActive(false);
        }


    }
}
