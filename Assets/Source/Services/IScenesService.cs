using Source.Infrastructure;

namespace Source.Services
{
    public interface IScenesService : IService
    {
        public void LoadMenu();
        public void LoadGame();
        public bool IsMenu();
        public bool IsGame();
    }
}
