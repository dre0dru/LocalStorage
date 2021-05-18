using System;
using System.IO;
using System.Security.Cryptography;
using LocalStorage.Compression;
using LocalStorage.Encryption;
using LocalStorage.Providers;
using UnityEngine;

namespace LocalStorage.PlayModeTests
{
    public static class Constants
    {
        public static class Instances
        {
            public class EncryptionSettings : IEncryptionSettings
            {
                public byte[] Key { get; }
                public byte[] InitializationVector { get; }

                public EncryptionSettings()
                {
                    using var aes = Aes.Create();
                    Key = aes.Key;
                    InitializationVector = aes.IV;
                }
            }

            public static readonly IEncryptionSettings Es = new EncryptionSettings();
            public static readonly IDataTransform AesDT = new AesEncryptionDataTransform(Es);
            public static readonly IDataTransform DeflateDT = new DeflateDataTransform();
            public static readonly IDataTransform GZipDT = new GZipDataTransform();

            public static readonly ISerializationProvider UnityJsonSP
                = new UnityJsonSerializationProvider();

            public static readonly IFileProvider FP = new FileProvider();
        }

        public static class Data
        {
            public const string FileName = "file.test";

            public static string FilePath => Path.Combine(Application.persistentDataPath, FileName);

            public const string LoremIpsum =
                @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec condimentum tortor ut risus accumsan, quis egestas diam placerat. Aliquam fermentum sit amet nunc in fermentum. Vivamus cursus volutpat pulvinar. Donec sagittis fermentum lacus non facilisis. Donec efficitur nulla metus, nec euismod nisl pretium sed. Sed quis metus vel tortor sollicitudin volutpat vel eget libero. Pellentesque blandit laoreet tincidunt. Ut bibendum, ipsum a viverra semper, nulla orci sodales nisi, quis blandit mauris elit vel nisi. Etiam laoreet mattis vestibulum. Quisque id magna lectus.
Duis eleifend eros sit amet augue eleifend consectetur. Integer id eros et lorem consectetur convallis. Nunc nec porttitor ligula, et lobortis sapien. Nullam vel tortor vitae dui scelerisque condimentum. Pellentesque erat mauris, ultricies a sapien eu, blandit pellentesque dui. Donec accumsan nisi non venenatis tincidunt. Nam porttitor sodales porttitor. Fusce quis quam nulla. Pellentesque lobortis tristique neque, sed porttitor felis auctor vel. Fusce sodales turpis erat, vitae varius elit sollicitudin eu.
Phasellus non sem sapien. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Curabitur vitae ligula est. Vestibulum cursus ac felis quis accumsan. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aliquam congue ullamcorper neque, sit amet eleifend magna tincidunt eget. Donec magna lectus, suscipit ut efficitur nec, maximus sit amet eros. Maecenas varius imperdiet elit, vel iaculis leo ultrices quis. Fusce cursus lorem et ligula efficitur, et molestie turpis vulputate. Curabitur interdum imperdiet suscipit.
Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Vivamus pellentesque convallis commodo. Vestibulum eleifend mauris id efficitur pulvinar. Vestibulum hendrerit auctor nisi quis luctus. Aenean consequat justo ut quam tincidunt, non tempus nunc efficitur. Nulla pulvinar aliquam mi quis varius. Vestibulum nec purus et nunc tempus sagittis id vitae risus. In pulvinar ligula et scelerisque vehicula.
Pellentesque sem ex, pellentesque ac neque quis, vulputate sollicitudin mi. Morbi a urna sed nisl pretium lobortis a ac ex. Aenean vel lectus eget dolor auctor tempus. Sed quis aliquam ligula, et aliquet dui. Curabitur sodales convallis sem, eu aliquet quam sodales a. Praesent feugiat nec ligula ultricies vestibulum. Nam tristique vel metus ut pulvinar. Cras commodo ipsum dui, condimentum gravida felis sollicitudin id. Phasellus fringilla ligula lorem, vel viverra sapien posuere sit amet.";

            public static readonly object[] TestByteData =
            {
                new object[] {new byte[] {1, 2, 3}},
                new object[] {"string".ToBytes()},
                new object[] {LoremIpsum.ToBytes()}
            };

            [Serializable]
            public struct TestSerializableStruct
            {
                public string String;
                public float Float;
            }

            public static readonly object[] TestGenericData =
            {
                new object[] {new Vector2(10, 20)},
                new object[]
                {
                    new TestSerializableStruct()
                    {
                        String = "String",
                        Float = 33.33f
                    }
                },
            };
        }
    }
}