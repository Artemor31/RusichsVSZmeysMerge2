﻿using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public class Pool<T> where T : MonoBehaviour, IPoolable
    {
        private readonly Stack<T> _data;
        private readonly T _prefab;

        public Pool(int capacity, int initAmount, T prefab)
        {
            _prefab = prefab;
            _data = new Stack<T>(capacity);

            for (int i = 0; i < initAmount; i++)
            {
                T instance = CreateInstance();
                _data.Push(instance);
            }
        }

        public T Get()
        {
            if (_data.TryPop(out var pop))
            {
                pop.Release();
                return pop;
            }

            return CreateInstance();
        }

        public void Collect(T instance)
        {
            instance.Collect();
            _data.Push(instance);
        }

        private T CreateInstance()
        {
            var position = new Vector3(0, 1000, 0);
            var instance = Object.Instantiate(_prefab, position, Quaternion.identity);
            instance.Collect();
            return instance;
        }
    }

    public interface IPoolable
    {
        void Collect();
        void Release();
    }
}