using Api.Request.Video.Slicer.UseCase.UseCase.Interfaces;
using Api.Request.Video.Slicer.UseCase.UseCase;
using Api.Request.Video.Slicer.Domain.Enum;

namespace WebApi.Extensions;

public static class EnumExtension
{
    public static VideoType GetCurrentVideoType(this VideoType videoType, string extension)
    {
        switch (extension)
        {
            case "mp4":
                return VideoType.MP4;                
            case "mov":
                return VideoType.MOV;                
            case "avi":
                return VideoType.AVI;                
            case "webm":
                return VideoType.WEBM;                
            case "mkv":
                return VideoType.MKV;                
            case "flv":
                return VideoType.FLV;                
        }

        return VideoType.Outro;

    }
}