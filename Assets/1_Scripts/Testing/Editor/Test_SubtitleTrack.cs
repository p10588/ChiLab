using Chi.Utilities.Timeline;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Chi.Utilities.Testing;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_SubtitleTrack {
        public TextAsset testAsset { get; private set; }

        ISubtitleAssetTrack subtitleTrack_Mock;

        SubtitleTrackController _controller;

        private const string TEST_ASSETS_PATH
            = "Assets/1_Scripts/Utilities/Timeline/Subtitle/TextData/TestText.txt";


        [SetUp] // Do initalize before Test Run 
        public void SetUp() {
            this.testAsset = TestingUtilities.LoadTestAsset<TextAsset>(TEST_ASSETS_PATH);
            this.subtitleTrack_Mock = ScriptableObject.CreateInstance<SubtitleAssetTrack>();
            this._controller = new SubtitleTrackController(this.subtitleTrack_Mock);

        }

        [Test]
        public void Test_AutoSetupSubtitle() {
            try {
                string[] subtitle  = new string[2] {
                    "0.1,David,YAAAAAAAAAA",
                    "1.5,David,ALLLLLLLLLL"
                };
                this._controller.AutoSetupSubtitle(this.testAsset, ref subtitle) ;
            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_ReadScript() {
            try {
                string[] subtitle = this._controller.ReadScript(testAsset);
                Assert.AreEqual(2, subtitle.Length);
            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_CreateSubtitleClips() {
            try {
                string[] subtitle = new string[2] {
                    "0.1,David,YAAAAAAAAAA",
                    "1.5,David,ALLLLLLLLLL"
                };
                this._controller.CreateSubtitleClips(subtitle);
            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }
        [Test]
        public void Test_ClearAllClips() {
            try {
                this._controller.ClearAllClips() ;
            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_ListIsNullOrEmpty() {
            List<string> subtitle = new List<string>(1) { null};

            bool checker = true;
            try {
                if (subtitle?.Any() != true) {
                    checker = false;
                } else {
                    checker = true;
                }
            } catch (Exception e) {
                throw e;
            }
            Assert.AreEqual(true, checker);
        }

        [TearDown] // Do uninitalize after Test run
        public void TearDown() {
            this.subtitleTrack_Mock = null;
            this._controller = null;
            this.testAsset = null;
        }

    }

}
