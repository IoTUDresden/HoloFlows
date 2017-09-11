using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    public class GeneralButtonBehaviour : MonoBehaviour, IFocusable
    {
        private TextMesh BtnTextMesh;
        private SpriteRenderer BtnSpriteRenderer;

        private Style style;
        private bool isFocused, isSelected;

        // Use this for initialization
        void Start()
        {
            BtnTextMesh = this.transform.GetChild(0).GetComponent<TextMesh>();
            BtnSpriteRenderer = this.transform.GetChild(1).GetComponent<SpriteRenderer>();

            style = GameObject.Find("GlobalStyle").GetComponent<Style>();
        }

        public void OnFocusEnter()
        {
            BtnTextMesh.color = style.highlightColor;
            BtnSpriteRenderer.color = style.highlightColor;

            isFocused = true;
        }

        public void OnFocusExit()
        {
            if (!isSelected)
            {
                BtnTextMesh.color = style.defaultColor;
                BtnSpriteRenderer.color = style.defaultColor;
            }

            isFocused = false;
        }

        public void Select()
        {
            BtnTextMesh.color = style.ClickedOnColor;
            BtnSpriteRenderer.color = style.ClickedOnColor;

            isSelected = true;
        }

        public void Deselect()
        {
            BtnTextMesh.color = style.defaultColor;
            BtnSpriteRenderer.color = style.defaultColor;

            isSelected = false;
        }
    }
}
