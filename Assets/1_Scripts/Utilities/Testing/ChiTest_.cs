using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Chi.Utilities.Testing
{
    public class ChiTest_ : MonoBehaviour
    {
        [SerializeField] private InputField input;
        [SerializeField] private Button testBtn;
        [SerializeField] private InputField output;

        private void Awake() {
            testBtn.onClick.AddListener(OnBtnClick);
        }

        private void OnDestroy() {
            testBtn.onClick.RemoveAllListeners();
        }

        private void OnBtnClick() {
            TestProcess(this.input.text);
        }

        private void TestProcess(string inputText) {
            try {

                //Do Some Test 

                OutputText(inputText);
            } catch (Exception e) {
                OutputText_Exception(e.Message);
                Debug.Log(e);
            }
        }

        private void OutputText(string outputText) {
            SetOutputText(outputText);
        }

        private void OutputText_Exception(string outputText) {
            SetOutputText("Exception: " + outputText);
        }

        private void SetOutputText(string output) {
            this.output.text = output;
        }

    }
}


