using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Wallet : PopUp
    {
        public Player player;
        [HideInInspector]
        public PlayerInfo playerInfo;

        public Text cashText;
        public Text snapText;
        public Text eitcAcquiredText;
        public Text ctcAcquiredText;
        public Text snapAcquiredText;

        public TextWrapper[] infoValues;

        public GameObject busPass;
        public GameObject carCard;
        public GameObject transportationText;
        public GameObject noTokenText;
        public TokenHolder tokenHolder;
        public WICVoucherView wicView;

        public GameObject walletTab;
        public GameObject walletTabBody;
        public GameObject infoTab;
        public GameObject infoTabBody;
        public GameObject transportationTab;
        public GameObject transportationTabBody;
       
        public Color selectColor;
        public Color inactiveColor;
        
        protected override void Start()
        {
            base.Start();
            selectWalletTab();
            
        }

        private void OnEnable()
        {

            playerInfo = player.playerInfo;
            updateWallet();
            updateTransportationDisplay();
            updateInfo();
            WICVoucher voucher = player.voucher;
            wicView.setVoucher(player.voucher);
            wicView.updateView();
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
            updateInfo();
        }

        public void selectTransportationTab()
        {
            setInactive();
            transportationTab.GetComponent<Image>().color = selectColor;
            transportationTabBody.SetActive(true);
        }

        public void updateInfo()
        {
            foreach (TextWrapper item in infoValues)
            {
                FormQuestionType type = item.questionType;
                string info  = playerInfo.getInfoText(type);
                item.setText(info);
            }
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
            if(player.busTickets > 0)
            {
                noTokenText.SetActive(false);
                for (int i = 0; i < player.busTickets; i++)
                {
                    hasTransporation = true;
                    tokenHolder.addToken();
                }
            }
            else
            {
                noTokenText.SetActive(true);
            }

            
            Text transText = transportationText.GetComponent<Text>();
            if (!playerInfo.busPass && !playerInfo.hasCar && !player.hasTemporaryRide)
            {
                transText.text = "You don't have any car or bus pass.";
                transportationText.SetActive(true);
            } else if (player.hasTemporaryRide)
            {
                transText.text = "Someone has offered to give you a ride from this location.";
                transportationText.SetActive(true);
            } else
            {
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
