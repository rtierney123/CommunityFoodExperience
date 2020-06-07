using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Utility;

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
        public TokenHolder tokenHolder;
        public WICVoucherView wicView;

        public GameObject walletTab;
        public GameObject walletTabBody;
        public GameObject infoTab;
        public GameObject infoTabBody;
        public GameObject transportationTab;
        public GameObject transportationTabBody;
        public GameObject nutritionTab;
        public GameObject nutritionTabBody;


        public Color selectColor;
        public Color inactiveColor;

        public NutritionDisplayer nutritionDisplayer;
        void Start()
        {

            selectNutritionStatus();
            
        }

        public override void reset()
        {
            base.reset();
            playerInfo = player.playerInfo;
            updateWallet();
            updateTransportationDisplay();
            updateInfo();
            WICVoucher voucher = player.voucher;
            wicView.setVoucher(player.voucher);
            wicView.updateView();
            selectNutritionStatus();
        }

        public void selectWalletTab()
        {
            setInactive();
            walletTab.GetComponent<Image>().color = selectColor;
            updateWallet();
            walletTabBody.SetActive(true);
        }

        public void selectNutritionStatus()
        {
            setInactive();
            nutritionTab.GetComponent<Image>().color = selectColor;
            updateNutrition();
            nutritionTabBody.SetActive(true);

        }

        public void selectInfoTab()
        {
            setInactive();
            infoTab.GetComponent<Image>().color = selectColor;
            updateInfo();
            infoTabBody.SetActive(true);
        }

        public void selectTransportationTab()
        {
            setInactive();
            transportationTab.GetComponent<Image>().color = selectColor;
            updateTransportationDisplay();
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


        public void updateNutrition()
        {
            nutritionDisplayer.updateDisplay(player);
        }


        public void updateTransportationDisplay()
        {
            if (playerInfo.busPass)
            {
                busPass.SetActive(true);
            }
            else
            {
                busPass.SetActive(false);
            }

            if (playerInfo.hasCar && !player.carBrokenDown)
            {
                carCard.SetActive(true);
            }
            else
            {
                carCard.SetActive(false);
            }

            tokenHolder.clearTokens();
            if(player.busTickets > 0)
            {
                for (int i = 0; i < player.busTickets; i++)
                {
                    tokenHolder.addToken();
                }
            }

            
            Text transText = transportationText.GetComponent<Text>();
             if (!playerInfo.busPass && (!playerInfo.hasCar||player.carBrokenDown) && !player.hasTemporaryRide )
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
            cashText.text = FormatText.formatCost(player.money);
            snapText.text = FormatText.formatCost(player.snapFunds);
            ctcAcquiredText.text = FormatText.formatCost(player.ctcAcquired);
            eitcAcquiredText.text = FormatText.formatCost(player.eitcAcquired);
            snapAcquiredText.text = FormatText.formatCost(player.snapAcquired);

        }

        private void setInactive()
        {
            walletTab.GetComponent<Image>().color = inactiveColor;
            infoTab.GetComponent<Image>().color = inactiveColor;
            transportationTab.GetComponent<Image>().color = inactiveColor;
            nutritionTab.GetComponent<Image>().color = inactiveColor;
            walletTabBody.SetActive(false);
            infoTabBody.SetActive(false);
            transportationTabBody.SetActive(false);
            nutritionTabBody.SetActive(false);
        }
  }

}
