namespace Api.Request.Video.Slicer.Domain.ValueObjects
{
    public struct VideoFile
    {
        public VideoFile(string fileName, string contetType, byte[] data)
        {
            var allowedExtensions = new string[]
            {
                ".MP4",
                ".MKV",
                ".AVI"
            };

            InvalidFileExtensionException.ThrowIfExtensionNotAllowed(fileName, allowedExtensions);

            FileName = fileName;
            ContentType = contetType;
            Data = data;

        }

        public string FileName { get; }
        public string ContentType { get; }
        public byte[] Data { get; }

        public string GetExtension()
        {
            return Path.GetExtension(FileName);
        }
    }
}
