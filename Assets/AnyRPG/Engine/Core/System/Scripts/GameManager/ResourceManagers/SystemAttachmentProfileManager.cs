using AnyRPG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnyRPG {
    public class SystemAttachmentProfileManager : SystemResourceManager {

        #region Singleton
        private static SystemAttachmentProfileManager instance;

        public static SystemAttachmentProfileManager Instance {
            get {
                if (instance == null) {
                    instance = FindObjectOfType<SystemAttachmentProfileManager>();
                }

                return instance;
            }
        }
        #endregion

        const string resourceClassName = "AttachmentProfile";

        protected override void Awake() {
            //Debug.Log(this.GetType().Name + ".Awake()");
            base.Awake();
        }

        public override void LoadResourceList() {
            //Debug.Log(this.GetType().Name + ".LoadResourceList()");
            masterList.Add(Resources.LoadAll<AttachmentProfile>(resourceClassName));
            if (SystemGameManager.Instance.SystemConfigurationManager != null) {
                foreach (string loadResourcesFolder in SystemGameManager.Instance.SystemConfigurationManager.LoadResourcesFolders) {
                    masterList.Add(Resources.LoadAll<AttachmentProfile>(loadResourcesFolder + "/" + resourceClassName));
                }
            }
            base.LoadResourceList();
        }

        public AttachmentProfile GetResource(string resourceName) {
            //Debug.Log(this.GetType().Name + ".GetResource(" + resourceName + ")");
            if (!RequestIsEmpty(resourceName)) {
                string keyName = prepareStringForMatch(resourceName);
                if (resourceList.ContainsKey(keyName)) {
                    return (resourceList[keyName] as AttachmentProfile);
                }
            }
            return null;
        }

        public List<AttachmentProfile> GetResourceList() {
            List<AttachmentProfile> returnList = new List<AttachmentProfile>();

            foreach (UnityEngine.Object listItem in resourceList.Values) {
                returnList.Add(listItem as AttachmentProfile);
            }
            return returnList;
        }
    }

}