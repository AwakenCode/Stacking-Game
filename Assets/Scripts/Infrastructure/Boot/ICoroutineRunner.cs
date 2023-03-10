using UnityEngine;
using System.Collections;
using Service;

namespace Infrastructure.Boot
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
    }
}