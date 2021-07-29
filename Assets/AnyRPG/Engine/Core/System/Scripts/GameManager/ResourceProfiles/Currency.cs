using AnyRPG;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.Collections.Generic;

namespace AnyRPG {
    [CreateAssetMenu(fileName = "NewCurrency", menuName = "AnyRPG/Currencies/Currency")]
    public class Currency : DescribableResource {

        public override string GetSummary() {
            return string.Format("Current Amount: {0}", SystemGameManager.Instance.PlayerManager.MyCharacter.CharacterCurrencyManager.GetCurrencyAmount(this));
            //return string.Format("{0}\nCurrent Amount: {1}", description, SystemGameManager.Instance.PlayerManager.MyCharacter.CharacterCurrencyManager.GetCurrencyAmount(this));
            //return string.Format("{0}", description);
        }

    }

    [System.Serializable]
    public struct CurrencyNode {

        public Currency currency;
        public int MyAmount;

    }


}