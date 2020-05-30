using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class WICVoucherView : View
    {
        public WICVoucher voucher;

        public GameObject fruitCheck;
        public GameObject vegCheck;
        public GameObject proteinCheck;
        public GameObject grainCheck;
        public GameObject dairyCheck;

        public Color activeColor;
        public Color inactiveColor;

        [HideInInspector]
        public bool activeVoucher = false;

        public void setVoucher(WICVoucher v)
        {
            voucher = v;
        }

        public void activateVoucher()
        {
            activeVoucher = true;
            Image image = this.gameObject.GetComponent<Image>();
            image.color = activeColor;
        }

        public void deactivateVoucher()
        {
            activeVoucher = false;
            Image image = this.gameObject.GetComponent<Image>();
            image.color = inactiveColor;
        }


        public override void updateView()
        {
            base.updateView();
            if(voucher == null)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                displayVoucher();
            }

            
        }



        public void displayVoucher()
        {
            this.gameObject.SetActive(true);
   
            if (voucher.fruitUsed)
            {
                displayPermCheck(fruitCheck);
            }
            else
            {
                fruitCheck.SetActive(false);
            }

            if (voucher.vegUsed)
            {
                displayPermCheck(vegCheck);
            }
            else
            {
                vegCheck.SetActive(false);
            }

            if (voucher.grainUsed)
            {
                displayPermCheck(grainCheck);
            }
            else
            {
                grainCheck.SetActive(false);
            }

            if (voucher.proteinUsed)
            {
                displayPermCheck(proteinCheck);
            }
            else
            {
                proteinCheck.SetActive(false);
            }

            if (voucher.dairyUsed)
            {
                displayPermCheck(dairyCheck);
            }
            else
            {
                dairyCheck.SetActive(false);
            }

            if (voucher.active)
            {
                setColor(this.gameObject, activeColor);
            }
            else
            {
                setColor(this.gameObject, inactiveColor);
            }
        }

        public void displayPotentialCheck(FoodType foodType)
        {
            switch (foodType)
            {
                case FoodType.Fruit:
                    displayTempCheck(fruitCheck);
                    return;
                case FoodType.Veg:
                    displayTempCheck(vegCheck);
                    return;
                case FoodType.Grain:
                    displayTempCheck(grainCheck);
                    return;
                case FoodType.Protein:
                    displayTempCheck(proteinCheck);
                    return;
                case FoodType.Dairy:
                    displayTempCheck(dairyCheck);
                    return;
            }
        }

        public void clearTempChecks()
        {
            if (!voucher.fruitUsed)
            {
                fruitCheck.SetActive(false);
            }
            if (!voucher.vegUsed)
            {
                vegCheck.SetActive(false);
            }
            if (!voucher.grainUsed)
            {
                grainCheck.SetActive(false);
            }
            if (!voucher.proteinUsed)
            {
                proteinCheck.SetActive(false);
            }
            if (!voucher.dairyUsed)
            {
                dairyCheck.SetActive(false);
            }
        }

        
        private void displayPermCheck(GameObject gameObject)
        {
            setColor(gameObject, Color.black);
            gameObject.SetActive(true);
        }

        private void displayTempCheck(GameObject gameObject)
        {
            setColor(gameObject, Color.white);
            gameObject.SetActive(true);
        }

        private void setColor(GameObject gameObject, Color color)
        {
            Image image = gameObject.GetComponent<Image>();
            image.color = color;
        }

    }
}

