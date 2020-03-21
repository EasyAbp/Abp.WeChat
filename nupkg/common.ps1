# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

# List of projects
$projects = (

    "src/Common/EasyAbp.Abp.WeChat.Common",
    "src/MiniProgram/EasyAbp.Abp.WeChat.MiniProgram",
    "src/Official/EasyAbp.Abp.WeChat.Official",
    "src/Official/EasyAbp.Abp.WeChat.Official.HttpApi",
    "src/Pay/EasyAbp.Abp.WeChat.Pay",
    "src/Pay/EasyAbp.Abp.WeChat.Pay.HttpApi"

)