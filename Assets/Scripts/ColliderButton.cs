using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;
public class ColliderButton : MonoBehaviour
{
    public UnityEvent onClick;

    private void OnMouseDown ()
    {
        onClick?.Invoke(); // onClick이 null이 아니면 Invoke 즉, 실행시켜라.
    }
}
