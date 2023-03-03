using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InstructionScreen : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        Button beginButton = rootVisualElement.Q<Button>(name: "Begin");

        beginButton.RegisterCallback<ClickEvent>(ev => BeginButton());
    }

    private void BeginButton()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
