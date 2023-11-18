namespace UrbanVogue.Models.Response
{
    public class ImageResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] Data { get; set; }
        public bool ForCatalogue { get; set; }
    }
}
