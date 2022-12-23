using System.Collections.Generic;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;

public static class ReleaseTestConsts
{
    public static readonly HashSet<string> OfficialAppIds = new()
    {
        "wx570bc396a51b8ff8",
        "wx9252c5e0bb1836fc",
        "wx8e1097c5bc82cde9",
        "wx14550af28c71a144",
        "wxa35b9c23cfe664eb",
    };

    public static readonly HashSet<string> MiniProgramsAppIds = new()
    {
        "wxd101a85aa106f53e",
        "wxc39235c15087f6f3",
        "wx7720d01d4b2a4500",
        "wx05d483572dcd5d8b",
        "wx5910277cae6fd970",
    };
}