using AnyRPG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnyRPG {
    public class AbilityBookUI : MonoBehaviour, IPagedWindowContents {

        public event System.Action<bool> OnPageCountUpdate = delegate { };
        public event System.Action<ICloseableWindowContents> OnCloseWindow = delegate { };

        [SerializeField]
        private List<AbilityButton> abilityButtons = new List<AbilityButton>();

        [SerializeField]
        private List<GameObject> abilityButtonHolders = new List<GameObject>();

        private List<List<BaseAbility>> pages = new List<List<BaseAbility>>();

        private int pageSize = 10;

        private int pageIndex;

        [SerializeField]
        private Image backGroundImage;

        public Image MyBackGroundImage { get => backGroundImage; set => backGroundImage = value; }

        public virtual void Awake() {
            if (backGroundImage == null) {
                backGroundImage = GetComponent<Image>();
            }
        }

        public void Init() {
            // nothing for now, here to satisfy interface.  fix me at some point if possible
        }

        public void SetBackGroundColor(Color color) {
            if (backGroundImage != null) {
                backGroundImage.color = color;
            }
        }

        public int GetPageCount() {
            return pages.Count;
        }

        public void CreatePages() {
            //Debug.Log("AbilityBookUI.CreatePages()");
            ClearPages();
            List<BaseAbility> page = new List<BaseAbility>();
            foreach (BaseAbility newAbility in SystemGameManager.Instance.PlayerManager.MyCharacter.CharacterAbilityManager.AbilityList.Values) {
                if (newAbility.RequirementsAreMet()) {
                    page.Add(newAbility);
                    if (page.Count == pageSize) {
                        pages.Add(page);
                        page = new List<BaseAbility>();
                    }
                }
            }
            if (page.Count > 0) {
                pages.Add(page);
            }
            AddAbilities();
            OnPageCountUpdate(false);

        }

        public void AddAbilities() {
            //Debug.Log("AbilityBookUI.AddAbilities()");
            if (pages.Count > 0) {
                for (int i = 0; i < pageSize; i++) {
                    //for (int i = 0; i < pages[pageIndex].Count - 1; i++) {
                    //Debug.Log("AbilityBookUI.AddAbilities(): i: " + i);
                    if (i < pages[pageIndex].Count) {
                        //Debug.Log("adding ability");
                        abilityButtonHolders[i].SetActive(true);
                        abilityButtons[i].AddAbility(pages[pageIndex][i]);
                        abilityButtons[i].SetBackGroundTransparency();
                    } else {
                        //Debug.Log("clearing ability");
                        abilityButtons[i].ClearAbility();
                        abilityButtonHolders[i].SetActive(false);
                    }
                }
            }
        }

        public void ClearButtons() {
            foreach (GameObject go in abilityButtonHolders) {
                go.SetActive(false);
            }
        }

        public void LoadPage(int pageIndex) {
            ClearButtons();
            this.pageIndex = pageIndex;
            AddAbilities();
        }

        public void RecieveClosedWindowNotification() {
        }

        public void ReceiveOpenWindowNotification() {
            SetBackGroundColor(new Color32(0, 0, 0, (byte)(int)(PlayerPrefs.GetFloat("PopupWindowOpacity") * 255)));
            CreatePages();
        }

        private void ClearPages() {
            ClearButtons();
            pages.Clear();
            pageIndex = 0;
        }

    }
}