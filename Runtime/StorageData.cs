using System;
using System.ComponentModel;

namespace LittleBit.Modules.CoreModule
{
    public class StorageData<T> where T : Data, new()
    {
        private readonly IDataStorageService dataStorageService;
        private readonly string key;
        
        private readonly object handler;
        
        private T value;

        public T Value
        {
            get { return value; }
        }

        public StorageData(object handler, IDataStorageService dataStorageService, string key)
        {
            this.handler = handler;
            this.key = key;
            this.dataStorageService = dataStorageService;

            Update();
        }

        public void Update()
        {
            value = dataStorageService.GetData<T>(key);
        }

        public void Subscribe(IDataStorageService.GenericCallback<T> onUpdateData)
        {
            dataStorageService.AddUpdateDataListener(handler, key, onUpdateData);
        }

        public void Unsubscribe(IDataStorageService.GenericCallback<T> onUpdateData)
        {
            dataStorageService.RemoveUpdateDataListener(handler, key, onUpdateData);
        }
        public void RemoveAllListeners()
        {
            dataStorageService.RemoveAllUpdateDataListenersOnObject(handler);
        }

        public void Save()
        {
            dataStorageService.SetData(key, value);
        }
    }
}