using System;
using System.Collections.Generic;
using UnityEngine;

namespace ISN
{
    public class ObjectsPool<TComponent> where TComponent : Component
    {
        private readonly Func<TComponent> _factoryMethod;
        private readonly Stack<TComponent> _objects;

        public ObjectsPool(Func<TComponent> factoryMethod, int size)
        {
            _objects = new Stack<TComponent>(size);
            _factoryMethod = factoryMethod;

            for (var i = 0; i < size; i++)
            {
                var component = CreateNewObject();
                component.gameObject.SetActive(false);
            }
        }

        public void PutObjectBack(TComponent component)
        {
            component.gameObject.SetActive(false);
            _objects.Push(component);
        }

        public TComponent TakeObject() => _objects.Count > 0 ? TakeLastObject() : CreateNewObject();

        private TComponent TakeLastObject()
        {
            var component = _objects.Pop();
            component.gameObject.SetActive(true);
            return component;
        }

        private TComponent CreateNewObject()
        {
            var component = _factoryMethod.Invoke();
            _objects.Push(component);
            return component;
        }
    }
}
