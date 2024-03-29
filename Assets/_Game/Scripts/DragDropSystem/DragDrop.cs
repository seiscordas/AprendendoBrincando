﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace LearningByPlaying
{
    public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Canvas canvas;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        public static bool IsOverChoiceSlot { get; set; }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!canvas)
                canvas = gameObject.transform.root.GetComponent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            if (IsOverChoiceSlot)
            {
                IsOverChoiceSlot = false;
                return;
            }
            ChoicePiece piece = eventData.pointerDrag.GetComponent<ChoicePiece>();
            ImageController.Instance.ResetImagePiecePosition(piece);
        }
    }
}
