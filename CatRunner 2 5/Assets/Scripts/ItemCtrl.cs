using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class ItemCtrl : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag.Equals("LAVA"))
            {
                Destroy(gameObject);
            }
        }
    }
}
