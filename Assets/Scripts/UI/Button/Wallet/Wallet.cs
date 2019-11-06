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
        public TokenHolder tokenHolder;

        public Player player;

        protected override void Start()
        {

            setInactive();
            selectWalletTab();
            base.Start();
        }

        public void selectWalletTab()
        {
            setInactive();
            walletTab.GetComponent<Image>().color = new Color32(171, 117, 0, 255);
            walletTabBody.SetActive(true);
            updateWallet();
        }

        public void selectInfoTab()
        {
            setInactive();
            infoTab.GetComponent<Image>().color = new Color32(171, 117, 0, 255);
            infoTabBody.SetActive(true);
            foreach (Transform child in infoTabBody.transform)
            {
                if (child.name == "NameValue")
                {
                    child.gameObject.GetComponent<Text>().text = player.getFullName();
                }
                else if (child.name == "SSNValue")
                {
                    child.gameObject.GetComponent<Text>().text = player.socialSecurity;
                }
                else if (child.name == "DOBValue")
                {
                    child.gameObject.GetComponent<Text>().text = player.getDOB();
                }
                else if (child.name == "TelValue")
                {
                    child.gameObject.GetComponent<Text>().text = player.phoneNum;
                }
                else if (child.name == "AddressValue")
                {
                    child.gameObject.GetComponent<Text>().text = player.address;
                }
            }
        }

        public void selectTransportationTab()
        {
            setInactive();
            transportationTab.GetComponent<Image>().color = new Color32(171, 117, 0, 255);
            transportationTabBody.SetActive(true);
        }

        public void updateTransportationDisplay()
        {
            if (player.busPass)
            {
                busPass.SetActive(true);
            }
            else
            {
                busPass.SetActive(false);
            }

            if (player.hasCar)
            {
                carCard.SetActive(true);
            }
            else
            {
                carCard.SetActive(false);
            }

            tokenHolder.clearTokens();
            for (int i = 0; i < player.busTokens; i++)
            {
                tokenHolder.addToken();
            }

        }

        public void updateWallet()
        {
            foreach (Transform child in walletTabBody.transform)
            {
                if (child.name == "CashValue")
                {
                    child.gameObject.GetComponent<Text>().text = player.money.ToString();
                }
                else if (child.name == "SNAPValue")
                {
                    child.gameObject.GetComponent<Text>().text = player.snapFunds.ToString();
                }
                else if (child.name == "WICValue")
                {
                    // child.gameObject.GetComponent<Text>().text = player.wic ? "Yes" : "No";
                }
                else if (child.name == "EITCValue")
                {
                    child.gameObject.GetComponent<Text>().text = player.eitcFunds.ToString();
                }
                else if (child.name == "CTCValue")
                {
                    child.gameObject.GetComponent<Text>().text = player.ctcFunds.ToString();
                }
            }

            updateTransportationDisplay();
        }

        private void setInactive()
        {
            walletTab.GetComponent<Image>().color = new Color32(231, 201, 88, 255);
            infoTab.GetComponent<Image>().color = new Color32(231, 201, 88, 255);
            transportationTab.GetComponent<Image>().color = new Color32(231, 201, 88, 255);
            walletTabBody.SetActive(false);
            infoTabBody.SetActive(false);
            transportationTabBody.SetActive(false);
        }

    }

}
