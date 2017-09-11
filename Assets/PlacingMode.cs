using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    public class PlacingMode : MonoBehaviour, IInputClickHandler
    {
        private bool placing;

        private Material defaultMat;
        private MeshRenderer meshRenderer;
        private Style style;

        private void Start()
        {
            placing = false;

            defaultMat = this.GetComponent<MeshRenderer>().material;
            meshRenderer = this.GetComponent<MeshRenderer>();
            style = GameObject.Find("GlobalStyle").GetComponent<Style>();

        }

        private void Update()
        {

        }
        void ActivatePlacingMode()
        {
            GameObject[] goList = GameObject.FindGameObjectsWithTag("PlacingCube");
            foreach (GameObject go in goList)
                go.GetComponent<TapToPlaceParent>().AllowPlacing();

            Debug.Log("Allow Placing");
            meshRenderer.material = style.highlightMat;
        }

        void DeactivatePlacingMode()
        {
            GameObject[] goList = GameObject.FindGameObjectsWithTag("PlacingCube");
            foreach (GameObject go in goList)
                go.GetComponent<TapToPlaceParent>().DisallowPlacing();

            Debug.Log("Disallow Placing");
            meshRenderer.material = defaultMat;
        }


        public void OnInputClicked(InputClickedEventData eventData)
        {
            if (placing)
            {
                this.DeactivatePlacingMode();
                placing = false;
            }else
            {
                this.ActivatePlacingMode();
                placing = true;
            }

    }
    }
}
