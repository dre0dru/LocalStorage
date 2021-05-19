[![openupm](https://img.shields.io/npm/v/com.dre0dru.localstorage?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.dre0dru.localstorage/)
# Description
Configurable generic class for managing local data saved on device.
Unity 2020.1+

## Features
- Two generic classes with configuration options for data read/write and serialization/deserialization via file system or player prefs.
- Simple file provider that just reads/writes data.
- Data transformations that allow to preprocess data on read/write:
  - `IDataTransform` that uses `AES` encryption to encrypt/decrypt data.
  - `IDataTransform` that uses `GZip` or `Deflate` to compress/decompress data.
- Json serialization provider that uses `JsonUtility` for serialization.
- File storage saves data to `Application.persistentDataPath`.
- Player prefs storage saves data to `PlayerPrefs`. Data location depends on device, refer to [Unity](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html) documentation.
- Easy to use and understand abstractions that allow to create custom serialization/deserialization and data transformation processes.  
- Async/sync API.
# Installation
This package can be installed as unity module directly from git url in two ways:
- By adding following line in `Packages/manifest.json`:
```
"com.dre0dru.localstorage": "https://github.com/dre0dru/LocalStorage.git#upm",
```
- By using `Window/Package Manager/Add package from git URL...` in Unity:
```
https://github.com/dre0dru/LocalStorage.git#upm
```
- The package is also available on the [openupm registry](https://openupm.com/packages/com.dre0dru.localstorage/). You can install it via [openupm-cli](https://github.com/openupm/openupm-cli):
```
openupm add com.dre0dru.localstorage
```
  
# Usage
## FileStorage usage
```c#
//Serialization/deserialization implementation
ISerializationProvider serializationProvider = new UnityJsonSerializationProvider();

//Path to save/load from (optional)
string path = "dataFolder";  //Resulting path will be Application.persistentDataPath/dataFolder
//File save/load implementation
IFileProvider fileProvider = new FileProvider(path);

IFileStorage storage = new FileStorage(serializationProvider, fileProvider);

string fileName = "fileName.extension";

//Resulting path will be Application.persistentDataPath/dataFolder/fileName.extension
string filePath = storage.GetFilePath(fileName);

//Serializes data then saves file
storage.Save(new Vector2(1.0f, 1.0f), fileName);

//Async saving
await storage.SaveAsync(new Vector2(1.0f, 1.0f), fileName);

//Check if file exists
bool exists = storage.FileExists(fileName);

//Loads file then deserializes data
Vector2 deserialized = storage.Load<Vector2>(fileName);

//Async loading
Vector2 deserialized = await storage.LoadAsync<Vector2>(fileName);

//Deletes file if present
storage.Delete(fileName);
```
## PlayerPrefsStorage usage
```c#
//Serialization/deserialization implementation
ISerializationProvider serializationProvider = new UnityJsonSerializationProvider();

IPlayerPrefsStorage storage = new PlayerPrefsStorage(serializationProvider);

string dataKey = "key";

//Serializes data using ISerializationProvider then puts it into PlayerPrefs under provided key
storage.SetData(dataKey, new Vector2(1, 1));

//Async saving
await storage.SetDataAsync(dataKey, new Vector2(1, 1));

//Loads data from PlayerPrefs by key then deserializes using ISerializationProvider
Vector2 deserialized = storage.GetData<Vector2>(dataKey);

//Async loading
Vector2 deserialized = await storage.GetDataAsync<Vector2>(dataKey);
```
The rest of `IPlayerPrefsStorage` API mimics [Unity](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html) `PlayerPrefs` API.

**Warning!** `PlayerPrefsStorage` is not thread safe.
## Data transformation usage
`IDataTransform` implementations can be used to preprocess data during serialization/deserialization process.
Available data transformations are:
- `AesEncryptionDataTransform`
- `DeflateDataTransform`
- `GZipDataTransform`
```c#
//This is just an example, don't generate new key/IV every time
public class ExampleEncryptionSettings : IEncryptionSettings
{
    public byte[] Key { get; private set; }
    public byte[] InitializationVector { get; private set; }

    public ExampleEncryptionSettings()
    {
        //Create AES instance
        using var aes = Aes.Create();
        //Save AES key somewhere
        Key = aes.Key;
        //Save AES IV somewhere
        InitializationVector = aes.IV;
    }
}

//Encryption settings for AES
IEncryptionSettings encryptionSettings = new ExampleEncryptionSettings();

//Setup desired IDataTransform implementation
IDataTransform encryptionDataTransform = new AesEncryptionDataTransform(encryptionSettings);

//Base ISerializationProvider that will be used before/after data transform is applied
ISerializationProvider baseSP = new UnityJsonSerializationProvider();

//Setup ISerializationProvider that will use target data transformation during serialization/deserialization process
ISerializationProvider transformSerializationProvider =
    new DataTransformSerializationProvider(baseSP, encryptionDataTransform);

//Use DataTransformSerializationProvider with FileStorage
IFileProvider fp = new FileProvider();
IFileStorage fileStorage = new FileStorage(transformSerializationProvider, fp);

//Or with PlayerPrefsStorage
IPlayerPrefsStorage playerPrefsStorage = new PlayerPrefsStorage(transformSerializationProvider);
```
You can combine multiple `IDataTransform` to create chained data transformations:
```c#
IEncryptionSettings encryptionSettings = new ExampleEncryptionSettings();

//Encryption data transform
IDataTransform encryptionDataTransform = new AesEncryptionDataTransform(encryptionSettings);

//Compression data transform
IDataTransform compressionDataTransform = new GZipDataTransform();

//Create combined data transform
//First it will encrypt data, then it will compress the result
IDataTransform combinedDataTransform = new CombinedDataTransform(encryptionDataTransform, compressionDataTransform);

//Pass it to DataTransformSerializationProvider implementation
ISerializationProvider serializationProvider =
    new DataTransformSerializationProvider(new UnityJsonSerializationProvider(),
        combinedDataTransform);

//Data transformation chains can be created indefinitely
IDataTransform customDataTransform = new CustomDataTransform();

//This will result in applying CustomDataTransform data transformations first,
//then applying data transformations specified by combinedDataTransform instance
IDataTransform multipleCombinedDataTransform =
    new CombinedDataTransform(customDataTransform, combinedDataTransform);
```
## Generic storage usage
There are generic implementations and interfaces of storage classes:
- `IFileStorage<TSerialization>`
- `IPlayerPrefsStorage<TSerialization>`

Can be used with `DI containers` to bind multiple implementations.

# License
The software released under the terms of the [MIT license](./LICENSE.md).