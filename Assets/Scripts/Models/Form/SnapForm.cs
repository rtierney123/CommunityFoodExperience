using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class SnapForm : Form
    {
        public override bool checkAlreadyEntered()
        {
            return false;
        }

        protected override bool checkValid()
        {
            return true;
        }

        protected override void successAction()
        {

        }

        protected override void failureAction()
        {

        }

    }

}
