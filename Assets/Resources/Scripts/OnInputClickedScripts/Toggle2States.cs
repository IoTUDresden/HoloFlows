using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    public class Toggle2States : MonoBehaviour
    {

        public GameObject ButtonOn, ButtonOff; // 

        // Use this for initialization
        void Start()
        {

        }


        public void SetOn()
        {
            ButtonOn.GetComponent<GeneralButtonBehaviour>().Select();
            ButtonOff.GetComponent<GeneralButtonBehaviour>().Deselect();
        }

        public void SetOff()
        {
            ButtonOn.GetComponent<GeneralButtonBehaviour>().Deselect();
            ButtonOff.GetComponent<GeneralButtonBehaviour>().Select();
        }
    }
}
