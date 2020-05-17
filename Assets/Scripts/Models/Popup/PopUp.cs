using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class PopUp : MonoBehaviour
    {
        // Start is called before the first frame update
        protected virtual void Start()
        {
            setChildrentoParent(this.transform);

        }

        // Update is called once per frame
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
    }


}

