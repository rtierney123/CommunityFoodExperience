using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Wallet : PopUp
    {
        public GameObject walletTab;
        public GameObject walletTabBody;
        public GameObject infoTab;
        public GameObject infoTabBody;
        public GameObject transportationTab;
        public GameObject transportationTabBody;

        public GameObject plusSign;
        public GameObject minusSign;

        public GameObject busPass;
        public GameObject carCard;
        public GameObject transportationText;
        public TokenHolder tokenHolder;

        public Player player;
        public PlayerInfo playerInfo;

        private Color selectColor;
        private Color inactiveColor;
        
        protected override void Start()
        {
            base.Start();
            selectColor = new Color32(192, 118, 5, 255);
            inactiveColor = new Color32(255, 150, 0, 255);
            selectWalletTab();
            
        }

        private void OnEnable()
        {

            playerInfo = player.playerInfo;
            updateWallet();
            updateTransportationDisplay();
        }

        public void selectWalletTab()
        {
            setInactive();
            walletTab.GetComponent<Image>().color = selectColor;
            walletTabBody.SetActive(true);
            updateWallet();
        }

        public void selectInfoTab()
        {
            setInactive();
            infoTab.GetComponent<Image>().color = selectColor;
            infoTabBody.SetActive(true);
            foreach (Transform child in infoTabBody.transform)
            {
                if (child.name == "NameValue")
                {
                    child.gameObject.GetComponent<Text>().text = playerInfo.getFullName();
                }
                else if (child.name == "SSNValue")
                {
                    child.gameObject.GetComponent<Text>().text = playerInfo.ssn;
                }
                else if (child.name == "DOBValue")
                {
                    child.gameObject.GetComponent<Text>().text = playerInfo.getDOB();
                }
                else if (child.name == "MarriedValue") {
                    child.gameObject.GetComponent<Text>().text = playerInfo.married ? "Yes" : "No";
                }
                else if (child.name == "AgeValue") {
                    child.gameObject.GetComponent<Text>().text = "" + playerInfo.age;
                }
                else if (child.name == "TelValue")
                {
                    child.gameObject.GetComponent<Text>().text = playerInfo.phone;
                }
                else if (child.name == "AddressValue")
                {
                    child.gameObject.GetComponent<Text>().text = playerInfo.address;
                }
                else if (child.name == "IncomeValue") {
                    child.gameObject.GetComponent<Text>().text = "$" + playerInfo.monthlyIncome + "/month";
                }
                else if (child.name == "Description") {
                    child.gameObject.GetComponent<Text>().text = playerInfo.description;
                }
            }
        }

        public void selectTransportationTab()
        {
            setInactive();
            transportationTab.GetComponent<Image>().color = selectColor;
            transportationTabBody.SetActive(true);
        }

        public void updateTransportationDisplay()
        {
            bool hasTransporation = false;
            if (playerInfo.busPass)
            {
                hasTransporation = true;
                busPass.SetActive(true);
            }
            else
            {
                busPass.SetActive(false);
            }

            if (playerInfo.hasCar)
            {
                hasTransporation = true;
                carCard.SetActive(true);
            }
            else
            {
                carCard.SetActive(false);
            }

            tokenHolder.clearTokens();
            for (int i = 0; i < player.busTokens; i++)
            {
                hasTransporation = true;
                tokenHolder.addToken();
            }

            if (!hasTransporation) {
                transportationText.SetActive(true);
            } else {
                transportationText.SetActive(false);
            }
        }

        public void updateWallet()
        {
            foreach (Transform child in walletTabBody.transform)
            {
                if (child.name == "CashValue")
                {
                    child.gameObject.GetComponent<Text>().text = formatFunds(player.money);
                }
                else if (child.name == "SNAPValue")
                {
                    child.gameObject.GetComponent<Text>().text = formatFunds(player.snapFunds);
                }
                else if (child.name == "WICValue")
                {
                    // child.gameObject.GetComponent<Text>().text = player.wic ? "Yes" : "No";
                }
                else if (child.name == "EITCValue")
                {
                    child.gameObject.GetComponent<Text>().text = formatFunds(player.eitcFunds);
                }
                else if (child.name == "CTCValue")
                {
                    child.gameObject.GetComponent<Text>().text = formatFunds(player.ctcFunds);
                }
            }

            updateTransportationDisplay();
        }

        private void setInactive()
        {
            walletTab.GetComponent<Image>().color = inactiveColor;
            infoTab.GetComponent<Image>().color = inactiveColor;
            transportationTab.GetComponent<Image>().color = inactiveColor;
            walletTabBody.SetActive(false);
            infoTabBody.SetActive(false);
            transportationTabBody.SetActive(false);
        }
    public string formatFunds(double funds)
    {
      return System.String.Format("{0:C}", funds);
    }
  }

}
