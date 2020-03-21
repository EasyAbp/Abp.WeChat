namespace EasyAbp.Abp.WeChat.Official.Services.CustomMenu
{
    /// <summary>
    /// 自定义菜单的按钮类型，相应的类型说明可以参考 https://developers.weixin.qq.com/doc/offiaccount/Custom_Menus/Creating_Custom-Defined_Menu.html 。
    /// </summary>
    public static class CustomButtonType
    {
        /// <summary>
        /// 当用户点击 <see cref="Click"/> 类型按钮后，微信服务器会通过消息接口推送消息类型为 event 的结构给开发者（参考消息接口指南），
        /// 并且带上按钮中开发者填写的 <see cref="CustomButton.Key"/> 值，开发者可以通过自定义的 Key 值与用户进行交互。
        /// </summary>
        public const string Click = "click";

        /// <summary>
        /// 用户点击 <see cref="View"/> 类型按钮后，微信客户端将会打开开发者在按钮中填写的网页 URL，可与网页授权获取用户基本信息
        /// 接口结合，获得用户基本信息。
        /// </summary>
        public const string View = "view";

        /// <summary>
        /// 用户点击按钮后，微信客户端将调起扫一扫工具，完成扫码操作后显示扫描结果（如果是 URL，将进入 URL），且会将扫码的结果传给
        /// 开发者，开发者可以下发消息。
        /// </summary>
        public const string ScanCodePush = "scancode_push";

        /// <summary>
        /// 弹出 “消息接收中” 提示框，当用户点击按钮后，微信客户端将调起扫一扫工具。完成扫码操作后，将扫码的结果传给开发者，同时收
        /// 起扫一扫工具，然后弹出 “消息接收中” 提示框，随后可能会收到开发者下发的消息。
        /// </summary>
        public const string ScanCodeWaitMessage = "scancode_waitmsg";

        /// <summary>
        /// 用户点击按钮后，微信客户端将调起系统相机，完成拍照操作后，会将拍摄的相片发送给开发者，并推送事件给开发者，同时收起系统
        /// 相机，随后可能会收到开发者下发的消息。
        /// </summary>
        public const string CallingSystemCameraPush = "pic_sysphoto";

        /// <summary>
        /// 用户点击按钮后，微信客户端将弹出选择器供用户选择 “拍照” 或者 “从手机相册选择”。用户选择后即走其他两种流程。
        /// </summary>
        public const string CallingSystemCameraOrAlbumPush = "pic_photo_or_album";

        /// <summary>
        /// 用户点击按钮后，微信客户端将调起微信相册，完成选择操作后，将选择的相片发送给开发者的服务器，并推送事件给开发者，同时收
        /// 起相册，随后可能会收到开发者下发的消息。
        /// </summary>
        public const string CallingWeChatAlbum = "pic_weixin";

        /// <summary>
        /// 用户点击按钮后，微信客户端将调起地理位置选择工具，完成选择操作后，将选择的地理位置发送给开发者的服务器，同时收起位置选
        /// 择工具，随后可能会收到开发者下发的消息。
        /// </summary>
        public const string ShowLocationSelectDialog = "location_select";

        /// <summary>
        /// 用户点击 <see cref="SendMediaMessage"/> 类型按钮后，微信服务器会将开发者填写的永久素材 Id 对应的素材下发给用户，永久
        /// 素材类型可以是图片、音频、视频、图文消息。请注意：永久素材 Id 必须是在 “素材管理/新增永久素材” 接口上传后获得的合法 Id。
        /// </summary>
        public const string SendMediaMessage = "media_id";

        /// <summary>
        /// 用户点击 <see cref="RedirectGraphicMessage"/> 类型按钮后，微信客户端将打开开发者在按钮中填写的永久素材 Id 对应的图
        /// 文消息 URL，永久素材类型只支持图文消息。请注意：永久素材 Id 必须是在 “素材管理/新增永久素材” 接口上传后获得的合法 Id。
        /// </summary>
        public const string RedirectGraphicMessage = "view_limited";
    }
}