using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    public class Toggle3States : MonoBehaviour
    {
        public GameObject Button1, Button2, Button3; 

        public void SetButton1()
        {
            Button1.GetComponent<GeneralButtonBehaviour>().Select();
            Button2.GetComponent<GeneralButtonBehaviour>().Deselect();
            Button3.GetComponent<GeneralButtonBehaviour>().Deselect();
        }

        public void SetButton2()
        {
            Button1.GetComponent<GeneralButtonBehaviour>().Deselect();
            Button2.GetComponent<GeneralButtonBehaviour>().Select();
            Button3.GetComponent<GeneralButtonBehaviour>().Deselect();
        }

        public void SetButton3()
        {
            Button1.GetComponent<GeneralButtonBehaviour>().Deselect();
            Button2.GetComponent<GeneralButtonBehaviour>().Deselect();
            Button3.GetComponent<GeneralButtonBehaviour>().Select();
        }
    }
}
