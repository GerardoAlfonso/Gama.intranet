namespace Gama.Intranet.BL.DTO.Response
{
    public class FileResponseDTO
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Folders { get; set; }
        public object Files { get; set; }
    }
}
