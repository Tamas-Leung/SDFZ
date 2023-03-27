using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleScreen : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        Button startButton = rootVisualElement.Q<Button>(name: "Start");

        startButton.RegisterCallback<ClickEvent>(ev => StartButton());
    }

    private void StartButton()
    {
        SceneManager.LoadScene("StoryScene", LoadSceneMode.Single);
    }
}
