using UnityEngine;


namespace HoloToolkit.Unity.InputModule.Tests
{
    /// <summary>
    /// Test behaviour that simply prints out a message very time a supported event is received from the input module.
    /// This is used to make sure that the input module routes events appropriately to game objects.
    /// </summary>
    public class CancelButton : MonoBehaviour, IInputClickHandler, IFocusable



    {
        [HideInInspector]
        public string LineName;
        [HideInInspector]
        public Transform Window;
        private GameObject Line;
        private GameObject WindowGO;


        [Tooltip("Set to true if gestures update (ManipulationUpdated, NavigationUpdated) should be logged. Note that this can impact performance.")]
        [HideInInspector]
        public bool LogGesturesUpdateEvents = false;

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
            Window = this.transform.parent;
            Line = GameObject.Find(LineName);
            WindowGO = Window.gameObject;
            
            Destroy(WindowGO);
            Destroy(Line);

            GameObject.Find("Managers").GetComponent<Linedraw>().ClickonWurfel1 = false;
            GameObject.Find("Managers").GetComponent<Linedraw>().ClickonWurfel2 = false;

            if (this.name == "Delete_LogicCreation")
            {
                ConnectionManager.getInstance().deleteConnection(LineName);
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