using Chi.Utilities.Networking;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_UrlPicService
    {
        IUrlPicService urlPicService;
        string url = "https://p10588-testing.s3.ap-northeast-1.amazonaws.com/UrlPic_Testing/Arch.png";
        [SetUp] // Do initalize before Test Run 
        public void SetUp() {
            this.urlPicService = new UrlPicService(url);
        }

        [Test]
        public void Test_TryGetUrlPic() {
            Texture2D texture = null;
            try {
                this.urlPicService.TryGetUrlPic().ContinueWith(task => {
                    if (task.IsCompleted) {
                        texture = task.Result;
                        Debug.Log("TESSSSSSSST");
                        Assert.AreEqual(true, texture != null);
                    } else {
                        texture = null;
                    }
                });

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        //[Test]
        public void Test() {
            var result = false;
            Assert.AreEqual(false, result);
        }

        //[TestCase(-1, 4)]
        public void TestCase_Assert(int i, int j) {
            var result = false;
            Assert.AreEqual(false, result);
        }




        //[Test]
        public void Test_Exception() {

            //Assert.Throws<ArgumentNullException>(
            //    () => { /* Do Some Test */ }
            //);
        }

        //[TearDown] // Do uninitalize after Test run
        public void TearDown() {
        }

    }
}
