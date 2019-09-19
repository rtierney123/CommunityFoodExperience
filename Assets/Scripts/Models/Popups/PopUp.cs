using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class PopUp : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            setChildrentoParent();

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void setChildrentoParent()
        {
            foreach (Transform child in this.gameObject.transform)

                child.gameObject.tag = this.gameObject.tag;
    
        }
    }


}

