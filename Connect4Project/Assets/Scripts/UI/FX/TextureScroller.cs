using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    [RequireComponent(typeof(Image))]
    public class TextureScroller : MonoBehaviour
    {
        [Header("Scroll Settings")]
        [SerializeField] private float scrollSpeed = 0.1f;
        [SerializeField] private Vector2 scrollDirection;

        //vars
        [HideInInspector] public Image imgShower;

        private void Awake()
        {
            imgShower = GetComponent<Image>();
            //normilize scroll direction
            scrollDirection.Normalize();
        }

        private void Update()
        {
            //scroll texture
            float offset = Time.time * scrollSpeed;
            imgShower.material.SetTextureOffset("_MainTex", scrollDirection * offset);
        }
    }
}