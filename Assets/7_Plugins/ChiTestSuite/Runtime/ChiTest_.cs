using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChiTest_ : MonoBehaviour
{
    [SerializeField] private InputField input;
    [SerializeField] private Button testBtn;
    [SerializeField] private InputField output;

    private void Awake() {
        testBtn.onClick.AddListener(()=> { OnBtnClick(input.text); });
    }

    private void OnDestroy() {
        testBtn.onClick.RemoveAllListeners();
    }

    private void OnBtnClick(string inputText) {
        try {

            //Do Some Test 

            OutputText(inputText);
        } catch(Exception e) {
            OutputText(e.Message);
            Debug.Log(e);
        }
    }

    private void OutputText(string outputText) {
        output.text += outputText + "\n";
    }
}


