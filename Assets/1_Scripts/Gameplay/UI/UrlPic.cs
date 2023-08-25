using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chi.Utilities.Networking;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class UrlPic : MonoBehaviour
{
    public string PicUrl;

    private RawImage _image;
    // Start is called before the first frame update

    private void Awake() {
        this._image = this.GetComponent<RawImage>();
    }
    private void Start()
    {
        SetImageTexture();
    }

    private async void SetImageTexture() {
        IUrlPicService urlPicService = new UrlPicService(PicUrl);
        this._image.texture = await urlPicService.TryGetUrlPic();
    }
}
