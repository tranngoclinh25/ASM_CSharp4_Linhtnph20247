using ASM_CSharp4_Linhtnph20247.Models;

namespace ASM_CSharp4_Linhtnph20247.Services.IServices
{
    public interface ISizeService
    {
        public List<Size> GetAllSizes();
        public Size GetSizeById(Guid id);
            
    }
}
