using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Api.WX
{
    public enum UploadType
    {
        image,
        voice,
        video,
        thumb
    }

    public enum CustomMsgType
    {
        text,
        image,
        voice,
        video,
        music,
        news
    }

    public enum ButtonType
    {
        click,
        view
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MsgType
    {
        text,
        image,
        voice,
        video,
        location,
        link,
        Event,
        music,
        news
    }
}
