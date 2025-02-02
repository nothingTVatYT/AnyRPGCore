using AnyRPG;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AnyRPG {
    public class DescribableIcon : ConfiguredMonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        protected IDescribable describable = null;

        [SerializeField]
        protected TextMeshProUGUI stackSize;

        [SerializeField]
        protected Image icon;

        protected int count;

        // game manager references
        protected UIManager uIManager = null;

        public Image Icon { get => icon; set => icon = value; }
        public TextMeshProUGUI StackSizeText { get => stackSize; }
        public IDescribable Describable { get => describable; set => describable = value; }
        public virtual int Count { get => count; }

        public override void Configure(SystemGameManager systemGameManager) {
            base.Configure(systemGameManager);

            uIManager = systemGameManager.UIManager;
        }

        /// <summary>
        /// Sets the describable on the describablebutton
        /// </summary>
        /// <param name="describable"></param>
        public virtual void SetDescribable(IDescribable describable) {
            SetDescribableCommon(describable);
        }

        public virtual void SetDescribable(IDescribable describable, int count) {
            this.count = count;
            SetDescribableCommon(describable);
        }

        protected virtual void SetDescribableCommon(IDescribable describable) {
            this.Describable = describable;
            UpdateVisual();

            if (UIManager.MouseInRect(Icon.rectTransform)) {
                ProcessMouseEnter();
            }

        }

        public static Rect RectTransformToScreenSpace(RectTransform transform) {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            float x = transform.position.x + transform.anchoredPosition.x;
            float y = Screen.height - transform.position.y - transform.anchoredPosition.y;

            return new Rect(x, y, size.x, size.y);
        }

        public virtual void UpdateVisual(Item item) {
            UpdateVisual();
        }

        /// <summary>
        /// UPdates the visual representation of the describablebutton
        /// </summary>
        public virtual void UpdateVisual() {
            //Debug.Log("DescribableIcon.UpdateVisual()");
            if (Describable != null && Icon != null) {
                if (Icon.sprite != Describable.Icon) {
                    //Debug.Log("DescribableIcon.UpdateVisual(): Updating Icon for : " + MyDescribable.MyName);
                    Icon.sprite = null;
                    Icon.sprite = Describable.Icon;
                }
                Icon.color = Color.white;
            } else if (Describable == null && Icon != null) {
                Icon.sprite = null;
                Icon.color = new Color32(0, 0, 0, 0);
            }

            /*
            if (count > 1) {
                uIManager.UpdateStackSize(this, count);
            } else if (MyDescribable is BaseAbility) {
                uIManager.ClearStackCount(this);
            }
            */
        }

        public virtual void OnPointerEnter(PointerEventData eventData) {
            //Debug.Log("DescribableIcon.OnPointerEnter()");
            ProcessMouseEnter();
        }

        public virtual void ProcessMouseEnter() {
            IDescribable tmp = null;

            if (Describable != null && Describable is IDescribable) {
                tmp = (IDescribable)Describable;
                //Debug.Log("DescribableIcon.OnPointerEnter(): describable is not null");
                //uIManager.ShowToolTip(transform.position);
            }
            if (tmp != null) {
                //Debug.Log("DescribableIcon.OnPointerEnter(): showing tooltip");
                ShowToolTip(tmp);
            } else {
                uIManager.HideToolTip();
            }

        }

        public virtual void ShowToolTip(IDescribable describable) {
            uIManager.ShowToolTip(transform.position, describable);
        }

        public virtual void OnPointerExit(PointerEventData eventData) {
            uIManager.HideToolTip();
        }

        public virtual void CheckMouse() {
            if (UIManager.MouseInRect(Icon.rectTransform)) {
                uIManager.HideToolTip();
            }
        }

        public virtual void OnDisable() {
            if (SystemGameManager.IsShuttingDown) {
                return;
            }
            CheckMouse();
        }

    }

}