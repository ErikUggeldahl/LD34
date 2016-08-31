using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputGatherer : MonoBehaviour
{
    [SerializeField]
    InputField inputField;

    [SerializeField]
    InputProcessor processor;

	void Start()
    {
        inputField.Select();
	}

    void Update()
    {
        if (!inputField.isFocused)
            inputField.Select();
    }

    public void OnInputSubmitted(string text)
    {
        inputField.text = string.Empty;
        inputField.ActivateInputField();
        inputField.Select();

        processor.Process(text);
    }
}
