using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Tests;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{
    public class SendRequestButtonBehaviour : MonoBehaviour, IInputClickHandler, IFocusable
    {
        private string itemName;

        void Start()
        {
            //derive item name from the gameobject's grandparent name
            string objectName = gameObject.transform.parent.parent.name;

            int pos = objectName.IndexOf('_');
            itemName = objectName.Substring(pos + 1);
            //if (itemName.Contains("dimmer"))
            //    itemName = itemName + "_Actuator";
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
            Actuator a = (Actuator)ItemManager.getInstance().getItem(itemName);
            if (a == null)
                Debug.Log("actuator " + itemName + " could not be found");
            //fetch instruction type and send appropriate request
            switch (this.gameObject.name)
            {
                case "ButtonON":
                    StartCoroutine(a.sendON());
                    break;
                case "ButtonOFF":
                    StartCoroutine(a.sendOFF());
                    break;
                case "ButtonINCREASE":
                    {
                        if (a is DimmerActuator)
                        {
                            StartCoroutine(((DimmerActuator)a).sendINCREASE());
                            break;
                        } else
                        {
                            if (a is HueDimmer)
                            {
                                StartCoroutine(((HueDimmer)a).sendINCREASE());
                                break;
                            }
                        }
                        break;    
                    }
                    
                case "ButtonDECREASE":
                    {
                        if (a is DimmerActuator)
                        {
                            StartCoroutine(((DimmerActuator)a).sendDECREASE());
                            break;
                        }
                        else
                        {
                            if (a is HueDimmer)
                            {
                                StartCoroutine(((HueDimmer)a).sendDECREASE());
                                break;
                            }
                        }
                        break;
                    }
                default:
                    return;
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
