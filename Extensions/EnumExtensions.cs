using Api.Request.Video.Slicer.UseCase.UseCase.Interfaces;
using Api.Request.Video.Slicer.UseCase.UseCase;
using Api.Request.Video.Slicer.Domain.Enum;

namespace WebApi.Extensions;

public static class EnumExtension
{
    public static VideoTypeEnum GetCurrentVideoType(this VideoTypeEnum videoType, string extension)
    {
        switch (extension)
        {
            case "mp4":
                return VideoTypeEnum.MP4;                
            case "mov":
                return VideoTypeEnum.MOV;                
            case "avi":
                return VideoTypeEnum.AVI;                
            case "webm":
                return VideoTypeEnum.WEBM;                
            case "mkv":
                return VideoTypeEnum.MKV;                
            case "flv":
                return VideoTypeEnum.FLV;                
        }

        return VideoTypeEnum.Outro;

    }
}