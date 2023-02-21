using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoseScreen : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        Button mainMenuButton = rootVisualElement.Q<Button>(name: "MainMenuButton");
        mainMenuButton.RegisterCallback<ClickEvent>(ev =>
        {
            MainMenuButton();
        }
        );
    }

    private void MainMenuButton()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
