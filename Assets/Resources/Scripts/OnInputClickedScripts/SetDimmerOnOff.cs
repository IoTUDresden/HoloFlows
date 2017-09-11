using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{
    /// <summary>
    /// Test behaviour that simply prints out a message very time a supported event is received from the input module.
    /// This is used to make sure that the input module routes events appropriately to game objects.
    /// </summary>
    public class SetDimmerOnOff : MonoBehaviour, IInputClickHandler, IFocusable
        
    {
        private bool WasIClicked;

        void Start()
        {
            WasIClicked = false;
        }
        public void OnInputUp(InputEventData eventData)
        {
            Debug.LogFormat("OnInputUp\r\nSource: {0}  SourceId: {1}", eventData.InputSource, eventData.SourceId);
        }

        public void OnInputDown(InputEventData eventData)
        {
            Debug.LogFormat("OnInputDown\r\nSource: {0}  SourceId: {1}", eventData.InputSource, eventData.SourceId);
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            //Debug.LogFormat("OnInputClicked\r\nSource: {0}  SourceId: {1} TapCount: {2}", eventData.InputSource, eventData.SourceId, eventData.TapCount);
            WasIClicked = !WasIClicked;
            if (WasIClicked == true)
            {
                DimmerActuator a = (DimmerActuator)ItemManager.getInstance().getItem(Settings.Dimmer1);
                StartCoroutine(a.sendON());

            }

            else
            {
                DimmerActuator a = (DimmerActuator)ItemManager.getInstance().getItem(Settings.Dimmer1);
                StartCoroutine(a.sendOFF());
            }

        }

        public void OnFocusEnter()
        {
        }

        public void OnFocusExit()
        {
        }
    }
}