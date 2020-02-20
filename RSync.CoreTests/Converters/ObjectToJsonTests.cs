using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace RSync.Core.Converters.Tests
{
    [TestClass()]
    public class ObjectToJsonTests
    {    
        [TestMethod()]
        public void SerializeNullObjectTest()
        {
            object obj = null;
            string serializedObj = obj.Serialize();
            Assert.AreEqual("null",serializedObj);
        }

        [TestMethod()]
        public void SerializeNotNullObjectTest()
        {
            bool obj = default;
            string serializedObj = obj.Serialize();
            Assert.AreEqual(default(bool).ToString().ToLower(), serializedObj);
        }

        [TestMethod()]
        public void DeserializeNullableObjectTest()
        {
            object obj = null;
            string serializedObj = obj.Serialize();
            object deserializedObj = serializedObj.Deserialize<object>();
            Assert.AreEqual(obj, deserializedObj);
        }

        [TestMethod()]
        [ExpectedException(typeof(JsonSerializationException))]
        public void DeserializeJsonSerializationExceptionTest()
        {
            bool? obj = default;
            string serializedObj = obj.Serialize();
            serializedObj.Deserialize<bool>();
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeserializeArgumentNullExceptionTest()
        {
            string serializedObj = null;
            serializedObj.Deserialize<bool>();
        }

        [TestMethod()]
        public void DeserializeNotNullableObjectTest()
        {
            bool obj = default;
            string serializedObj = obj.Serialize();
            bool deserializedObj = serializedObj.Deserialize<bool>();
            Assert.AreEqual(obj, deserializedObj);
        }
    }
}