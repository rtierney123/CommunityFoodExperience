using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manage
{
    //updates player nutrition and shows ui of update
    public class TutorialManager : MonoBehaviour
    {
        public string[] instructions;
        public Sprite[] images;
        public Image displayImage;
        public Text displayInstruction;
        public GameObject tutorialScene;
        public GameObject leftButton;
        public GameObject rightButton;
        public GameObject finishButton;
        public int curIndex = 0;

        public void moveRight() {
            if (this.curIndex < this.images.Length - 1) {
                this.curIndex += 1;
                this.displayInstruction.text = this.instructions[this.curIndex];
                this.displayImage.sprite = images[this.curIndex];
                displayButton();
            }
        }
        public void moveLeft() {
            if (this.curIndex > 0) {
                this.curIndex -= 1;
                this.displayInstruction.text = this.instructions[this.curIndex];
                this.displayImage.sprite = images[this.curIndex];
                displayButton();
            }
        }
        public void finish() {
            this.tutorialScene.SetActive(false);
        }

        void OnEnable()
        {
            this.displayInstruction.text = this.instructions[this.curIndex];
            this.displayImage.sprite = images[this.curIndex];
            displayButton();
        }

        public void displayButton() {
            this.finishButton.SetActive(false);
            this.leftButton.SetActive(true);
            this.rightButton.SetActive(true);
            if (this.curIndex == 0) {
                this.leftButton.SetActive(false);
            }
            if (this.curIndex == this.images.Length - 1) {
                this.rightButton.SetActive(false);
                this.finishButton.SetActive(true);
            }
        }
    }
}

