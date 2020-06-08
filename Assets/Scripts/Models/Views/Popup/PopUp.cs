using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class PopUp : View
    {
        protected virtual void Start()
        {
            setChildrentoParent(this.transform);

        }

        void Update()
        {

        }

        private void setChildrentoParent(Transform transform)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.tag = this.gameObject.tag;
                setChildrentoParent(child);
            }
        }

        public override void close()
        {
            base.close();
            if(canvasController != null)
            {
                canvasController.closePopUp(gameObject);
            }
            
        }

    }


}

