using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Chi.Utilities.Networking
{
    public interface IUrlPicService{
        Task<Texture2D> TryGetUrlPic(); 
    }

    public class UrlPicService : IUrlPicService
    {
        private string picUrl;

        public UrlPicService(string url) {
            this.picUrl = url;
        }

        async Task<Texture2D> IUrlPicService.TryGetUrlPic() {
            try {
                Texture2D texture = await this.GetUrlPic(this.picUrl);
                return texture;
            }catch(Exception e) {
                Debug.LogException(e);
            }
            return null;
        }

        private async Task<Texture2D> GetUrlPic(string url) {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(" Pic Url is null or empty");
            Texture2D texture = await DownloadImage(url);
            return texture;
        }

        private async Task<Texture2D> DownloadImage(string picUrl) {
            using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(picUrl)) {
                // Start the web request
                var asyncOperation = webRequest.SendWebRequest();

                // Wait until the web request is done
                while (!asyncOperation.isDone) {
                    await Task.Yield(); // Allow the Unity main thread to continue
                }

                if (webRequest.result == UnityWebRequest.Result.Success) {
                    // Get the downloaded texture
                    return DownloadHandlerTexture.GetContent(webRequest);
                } else {
                    throw new InvalidOperationException("Url Image Download failed: " + webRequest.error);
                }
            }
        }
    }
}