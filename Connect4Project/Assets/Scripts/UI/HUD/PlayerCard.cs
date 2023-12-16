using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class PlayerCard : MonoBehaviour
    {
        [Header("Movement")]
        public Vector2 highlightOffset;
        public float moveSpeed;

        [Header("Refs")]
        public RectTransform rt;
        public TMP_Text label;
        public Image bgImage;

        //vars
        private Vector2 startPos;
        private Vector2 highlightPos;

        private Vector2 targetPos;
        private Coroutine moveRoutine; //this throws a warning, it seems confused about compound assignments

        private void Awake()
        {
            //set up vars
            startPos = rt.anchoredPosition;
            highlightPos = startPos + highlightOffset;
        }

        //========== Movement ============
        public void Highlight()
        {
            targetPos = highlightPos;
            moveRoutine ??= StartCoroutine(MoveCo());
        }

        public void ResetPosition()
        {
            targetPos = startPos;
            moveRoutine ??= StartCoroutine(MoveCo());
        }

        private IEnumerator MoveCo()
        {
            while (Vector2.Distance(rt.anchoredPosition, targetPos) > 1f) {
                //move towards target
                rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            //reset routine
            moveRoutine = null;
        }
    }
}
